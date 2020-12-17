using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class WhitespaceNode : IAstNode
    {
        public Position Position { get; }

        public WhitespaceNode(Position position)
        {
            Position = position;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
