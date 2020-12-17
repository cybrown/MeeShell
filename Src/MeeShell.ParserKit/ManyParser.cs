using System.Collections.Generic;

namespace MeeShell.ParserKit
{
    public enum ManyEnum
    {
        NONE_OR_MANY,
        AT_LEAST_ONE,
    }

    internal class ManyParser<CTX, T> : IParser<CTX, T[]>
        where CTX : IParserContext
    {
        private readonly IParser<CTX, T> parser;
        private readonly ManyEnum atLeastOne;

        internal ManyParser(IParser<CTX, T> parser, ManyEnum atLeastOne)
        {
            this.parser = parser;
            this.atLeastOne = atLeastOne;
        }

        public IParserResult<CTX, T[]> Parse(CTX ctx, string src)
        {
            var finalSize = 0;
            var currentSource = src;
            var nodes = new List<T>();
            var first = true;

            var position = new Position(ctx);

            while (true)
            {
                var r = parser.Parse(ctx, currentSource);
                if (!r.Success)
                {
                    if (first && atLeastOne == ManyEnum.AT_LEAST_ONE)
                    {
                        return ParserResult<CTX, T[]>.FailWithExpected(r.Expected);
                    }
                    break;
                }
                nodes.Add(r.Value);
                finalSize += r.Size;
                currentSource = currentSource[r.Size..];
                first = false;
            }

            position.EndFromContext(ctx);

            return ParserResult<CTX, T[]>.Ok(nodes.ToArray(), finalSize, position);
        }
    }
}
