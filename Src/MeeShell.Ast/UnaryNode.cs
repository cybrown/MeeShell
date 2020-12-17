using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class UnaryNode : IAstNode
    {
        public IdentifierNode Operator;
        public IAstNode Operand;
        public Position Position => new Position(Operator.Position.StartLine, Operand.Position.EndLine, Operator.Position.StartCol, Operand.Position.EndCol);

        public UnaryNode(IdentifierNode Operator, IAstNode Operand)
        {
            this.Operator = Operator;
            this.Operand = Operand;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            Operand.Accept(visitor);
            visitor.VisitLeave(this);
        }
    }
}
