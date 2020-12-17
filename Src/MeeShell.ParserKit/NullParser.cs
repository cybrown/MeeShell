using MeeShell.Util;

namespace MeeShell.ParserKit
{
    internal class NullParser<CTX> : IParser<CTX, None>
        where CTX : IParserContext
    {
        public IParserResult<CTX, None> Parse(CTX ctx, string src)
        {
            return ParserResult<CTX, None>.Ok(None.Instance, 0, new Position(ctx));
        }
    }
}
