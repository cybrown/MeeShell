using System;

namespace MeeShell.ParserKit
{
    internal class UpdateContextParser<CTX, T> : IParser<CTX, T>
        where CTX : IParserContext
    {
        private readonly IParser<CTX, T> parser;
        private readonly Action<CTX> contextUpdater;

        internal UpdateContextParser(IParser<CTX, T> parser, Action<CTX> contextUpdater)
        {
            this.parser = parser;
            this.contextUpdater = contextUpdater;
        }

        public IParserResult<CTX, T> Parse(CTX ctx, string src)
        {
            var result = parser.Parse(ctx, src);
            contextUpdater(ctx);
            return result;
        }
    }
}
