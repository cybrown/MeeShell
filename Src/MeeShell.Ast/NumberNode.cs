using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class NumberNode : IAstNode
    {
        public double Value;
        public Position Position { get; }

        public NumberNode(Position position, double value)
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
