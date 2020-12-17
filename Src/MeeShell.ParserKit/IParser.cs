using System;
using static MeeShell.ParserKit.Helper;

namespace MeeShell.ParserKit
{
    public interface IParser<CTX, out T>
        where CTX : IParserContext
    {
        IParserResult<CTX, T> Parse(CTX ctx, string src);
    }

    public interface IParserContext
    {
        int Col { get; set; }
        int Line { get; set; }
        void Save();
        void Restore();
        void Dequeue();
    }

    public static class ParserExtension
    {
        public static IParser<CTX, T> UpdateContext<CTX, T>(this IParser<CTX, T> parser, Action<CTX> contextUpdater)
            where CTX : IParserContext
        {
            return new UpdateContextParser<CTX, T>(parser, contextUpdater);
        }

        public static IParser<CTX, U> Map<CTX, T, U>(this IParser<CTX, T> parser, Func<T, IParserResult<CTX, T>, U> mapper)
            where CTX : IParserContext
        {
            return new ParserMapper<CTX, T, U>(parser, mapper);
        }

        public static IParser<CTX, U> Map<CTX, T, U>(this IParser<CTX, T> parser, Func<T, U> mapper)
            where CTX : IParserContext
        {
            return new ParserMapper<CTX, T, U>(parser, (t, _) => mapper(t));
        }

        public static IParser<CTX, Tuple<A, B>> Then<CTX, A, B>(this IParser<CTX, A> parserA, IParser<CTX, B> parserB)
            where CTX : IParserContext
        {
            return new SequenceParser<CTX, A, B>(parserA, parserB);
        }

        public static IParser<CTX, Tuple<A, B, C>> Then<CTX, A, B, C>(this IParser<CTX, A> parserA, IParser<CTX, B> parserB, IParser<CTX, C> parserC)
            where CTX : IParserContext
        {
            return parserA.Then(parserB).Then(parserC).Map(n => Tuple.Create(n.Item1.Item1, n.Item1.Item2, n.Item2));
        }

        public static IParser<CTX, Tuple<A, B, C, D>> Then<CTX, A, B, C, D>(this IParser<CTX, A> parserA, IParser<CTX, B> parserB, IParser<CTX, C> parserC, IParser<CTX, D> parserD)
            where CTX : IParserContext
        {
            return parserA.Then(parserB).Then(parserC).Then(parserD).Map(n => Tuple.Create(n.Item1.Item1.Item1, n.Item1.Item1.Item2, n.Item1.Item2, n.Item2));
        }

        public static IParser<CTX, A> ThenIgnore<CTX, A, B>(this IParser<CTX, A> parserA, IParser<CTX, B> parserB)
            where CTX : IParserContext
        {
            return parserA.Then(parserB).Map(n => n.Item1);
        }

        public static IParser<CTX, B> IgnoreThen<CTX, A, B>(this IParser<CTX, A> parserA, IParser<CTX, B> parserB)
            where CTX : IParserContext
        {
            return parserA.Then(parserB).Map(n => n.Item2);
        }

        public static IParser<CTX, A> Between<CTX, A, B, C>(this IParser<CTX, A> inside, IParser<CTX, B> left, IParser<CTX, C> right)
            where CTX : IParserContext
        {
            return left.IgnoreThen(inside).ThenIgnore(right);
        }
    }
}
