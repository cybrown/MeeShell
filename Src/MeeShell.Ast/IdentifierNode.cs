using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class IdentifierNode : IAstNode
    {
        public string Value;
        public Position Position { get; }

        public IdentifierNode(Position position, string value)
        {
            Position = position;
            Value = value;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
