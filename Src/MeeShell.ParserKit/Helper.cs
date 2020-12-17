using System;
using System.Collections.Generic;
using System.Linq;
using MeeShell.Util;

namespace MeeShell.ParserKit
{
    public static class Helper
    {
        public static IParser<CTX, T> Lazy<CTX, T>(Func<IParser<CTX, T>> factory)
            where CTX : IParserContext
        {
            return new LazyParser<CTX, T>(factory);
        }

        public static IParser<CTX, T[]> Many<CTX, T>(IParser<CTX, T> parser, ParserKit.ManyEnum atLeastOne = ManyEnum.NONE_OR_MANY)
            where CTX : IParserContext
        {
            return new ManyParser<CTX, T>(parser, atLeastOne);
        }

        public static IParser<CTX, T> FirstOf<CTX, T>(params IParser<CTX, T>[] parsers)
            where CTX : IParserContext
        {
            return new AlternateFirstParser<CTX, T>(parsers);
        }

        public static IParser<CTX, object> Opt<CTX>(IParser<CTX, object> parser)
            where CTX : IParserContext
        {
            return new AlternateFirstParser<CTX, object>(new IParser<CTX, object>[] { parser, new NullParser<CTX>() });
        }

        public static IParser<CTX, None> EoS<CTX>() where CTX : IParserContext => new MatchParser<CTX>((_, str) =>
        {
            if (str.Length == 0)
            {
                return "";
            }
            return null;
        }, "End of Source").Map(_ => None.Instance);

        public static IParser<CTX, string> Match<CTX>(Func<CTX, string, string> matcher, string expectedName)
            where CTX : IParserContext
        {
            return new MatchParser<CTX>(matcher, expectedName);
        }

        public enum ManySeperatedByEnum
        {
            END_FORBIDDEN,
            END_OPTIONNAL
        }

        public static IParser<CTX, T[]> ManySeperatedBy<CTX, T, U>(IParser<CTX, T> elements, IParser<CTX, U> separator, ManySeperatedByEnum optionnalAtEnd = ManySeperatedByEnum.END_FORBIDDEN)
            where CTX : IParserContext
        => elements.Then(
            Many(separator.IgnoreThen(elements)),
            optionnalAtEnd == ManySeperatedByEnum.END_OPTIONNAL ? Opt(separator.Map(_ => None.Instance)).Map(_ => None.Instance) : new NullParser<CTX>()
        ).Map(n => new List<T>() { n.Item1 }.Concat(n.Item2).ToArray());
    }
}
