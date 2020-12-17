namespace MeeShell.ParserKit
{
    public class Position
    {
        public int StartLine { get; }
        public int EndLine { get; private set; }
        public int StartCol { get; }
        public int EndCol { get; private set; }

        public Position() { }

        public Position(IParserContext ctx)
        {
            StartLine = ctx.Line;
            StartCol = ctx.Col;
        }

        public void EndFromContext(IParserContext context)
        {
            EndLine = context.Line;
            EndCol = context.Col;
        }

        public Position(int startLine, int endLine, int startCol, int endCol)
        {
            StartLine = startLine;
            EndLine = endLine;
            StartCol = startCol;
            EndCol = endCol;
        }
    }
}