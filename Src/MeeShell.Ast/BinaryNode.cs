using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class BinaryNode : IAstNode
    {
        public string Operator;
        public IAstNode Left;
        public IAstNode Right;
        public Position Position => new Position(Left.Position.StartLine, Right.Position.EndLine, Left.Position.StartCol, Right.Position.EndCol);

        public BinaryNode(string Operator, IAstNode Left, IAstNode Right)
        {
            this.Operator = Operator;
            this.Left = Left;
            this.Right = Right;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            Left.Accept(visitor);
            Right.Accept(visitor);
            visitor.VisitLeave(this);
        }
    }
}
