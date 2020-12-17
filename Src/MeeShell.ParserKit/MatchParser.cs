using System;

namespace MeeShell.ParserKit
{
    internal class MatchParser<CTX> : IParser<CTX, string>
        where CTX : IParserContext
    {
        private readonly Func<CTX, string, string> matcher;
        private readonly string expectedName;

        internal MatchParser(Func<CTX, string, string> matcher, string expectedName)
        {
            this.matcher = matcher;
            this.expectedName = expectedName;
        }

        public IParserResult<CTX, string> Parse(CTX ctx, string src)
        {
            var match = matcher(ctx, src);
            if (match == null)
            {
                return ParserResult<CTX, string>.FailWithExpected(expectedName);
            }

            var position = new Position(ctx);
            foreach (var c in match)
            {
                ctx.Col++;
                if (c == '\n')
                {
                    ctx.Line++;
                    ctx.Col = 0;
                }
            }
            position.EndFromContext(ctx);
            return ParserResult<CTX, string>.Ok(match, match.Length, position);
        }
    }
}
