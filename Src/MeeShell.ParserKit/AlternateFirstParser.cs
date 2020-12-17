using System.Collections.Generic;
using System.Linq;

namespace MeeShell.ParserKit
{
    internal class AlternateFirstParser<CTX, T> : IParser<CTX, T>
        where CTX : IParserContext
    {
        private readonly IParser<CTX, T>[] parsers;

        internal AlternateFirstParser(params IParser<CTX, T>[] parsers)
        {
            this.parsers = parsers;
        }

        public IParserResult<CTX, T> Parse(CTX ctx, string src)
        {
            var results = new List<IParserResult<CTX, T>>();
            foreach (var parser in parsers)
            {
                ctx.Save();
                var r = parser.Parse(ctx, src);
                if (r.Success)
                {
                    ctx.Dequeue();
                    return r;
                }
                ctx.Restore();
                results.Add(r);
            }

            var expected = new HashSet<string>();
            foreach (var r in results)
            {
                if (r.Expected != null)
                {
                    foreach (var exp in r.Expected)
                    {
                        expected.Add(exp);
                    }
                }
            }

            return ParserResult<CTX, T>.FailWithExpected(expected.ToArray());
        }
    }
}
