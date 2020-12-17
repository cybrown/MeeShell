namespace MeeShell.ParserKit
{
    public interface IParserResult<CTX, out T>
        where CTX : IParserContext
    {
        string[] Expected { get; }
        T Value { get; }
        bool Success { get; }
        int Size { get; }
        Position Position { get; }
    }

    public sealed class ParserResult<CTX, T> : IParserResult<CTX, T>
        where CTX : IParserContext
    {
        public string[] Expected { get; set; }
        public T Value { get; set; }
        public bool Success { get; set; }
        public int Size { get; set; }
        public Position Position { get; set; }

        private ParserResult() { }

        public static ParserResult<CTX, T> Ok(T value, int size, Position position)
        {
            return new ParserResult<CTX, T>()
            {
                Success = true,
                Value = value,
                Size = size,
                Position = position,
            };
        }

        public static ParserResult<CTX, T> FailWithExpected(params string[] expected)
        {
            return new ParserResult<CTX, T>()
            {
                Success = false,
                Expected = expected,
            };
        }
    }
}
