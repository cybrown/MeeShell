using System;

namespace MeeShell.ParserKit
{
    internal class ParserMapper<CTX, T, U> : IParser<CTX, U>
        where CTX : IParserContext
    {
        private readonly IParser<CTX, T> parser;
        private readonly Func<T, IParserResult<CTX, T>, U> mapper;

        internal ParserMapper(IParser<CTX, T> parser, Func<T, IParserResult<CTX, T>, U> mapper)
        {
            this.parser = parser;
            this.mapper = mapper;
        }

        public IParserResult<CTX, U> Parse(CTX ctx, string src)
        {
            var result = parser.Parse(ctx, src);
            if (result.Success)
            {
                return ParserResult<CTX, U>.Ok(mapper(result.Value, result), result.Size, result.Position);
            }
            return ParserResult<CTX, U>.FailWithExpected(result.Expected);
        }
    }
}
