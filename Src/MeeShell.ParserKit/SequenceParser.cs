using System;

namespace MeeShell.ParserKit
{
    internal class SequenceParser<CTX, A, B> : IParser<CTX, Tuple<A, B>>
        where CTX : IParserContext
    {
        private readonly IParser<CTX, A> parserA;
        private readonly IParser<CTX, B> parserB;

        internal SequenceParser(IParser<CTX, A> parserA, IParser<CTX, B> parserB)
        {
            this.parserA = parserA;
            this.parserB = parserB;
        }

        public IParserResult<CTX, Tuple<A, B>> Parse(CTX ctx, string src)
        {
            var position = new Position(ctx);

            var parseResultA = parserA.Parse(ctx, src);
            if (!parseResultA.Success)
            {
                return ParserResult<CTX, Tuple<A, B>>.FailWithExpected(parseResultA.Expected);
            }

            var parseResultB = parserB.Parse(ctx, src[parseResultA.Size..]);
            if (!parseResultB.Success)
            {
                return ParserResult<CTX, Tuple<A, B>>.FailWithExpected(parseResultB.Expected);
            }

            position.EndFromContext(ctx);

            return ParserResult<CTX, Tuple<A, B>>.Ok(
                Tuple.Create(parseResultA.Value, parseResultB.Value),
                parseResultA.Size + parseResultB.Size,
                position
            );
        }
    }
}
