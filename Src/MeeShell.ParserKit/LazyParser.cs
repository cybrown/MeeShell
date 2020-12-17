using System;

namespace MeeShell.ParserKit
{
    internal class LazyParser<CTX, T> : IParser<CTX, T>
        where CTX : IParserContext
    {
        private readonly Lazy<IParser<CTX, T>> internalParser;

        internal LazyParser(Func<IParser<CTX, T>> parserFactory)
        {
            internalParser = new Lazy<IParser<CTX, T>>(parserFactory);
        }

        public IParserResult<CTX, T> Parse(CTX ctx, string src)
        {
            return internalParser.Value.Parse(ctx, src);
        }
    }
}
