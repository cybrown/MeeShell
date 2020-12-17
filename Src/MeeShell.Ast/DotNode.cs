using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class DotNode : IAstNode
    {
        public IAstNode Expression;
        public IdentifierNode Name;
        public Position Position => new Position(Expression.Position.StartLine, Name.Position.EndLine, Expression.Position.StartCol, Name.Position.EndCol);

        public DotNode(IAstNode Expression, IdentifierNode Name)
        {
            this.Expression = Expression;
            this.Name = Name;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            Expression.Accept(visitor);
            visitor.VisitLeave(this);
        }
    }
}
