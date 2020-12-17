using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class StringNode : IAstNode
    {
        public string Value;
        public Position Position { get; }

        public StringNode(Position position, string value)
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
