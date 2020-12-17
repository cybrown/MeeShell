using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class IndexNode : IAstNode
    {
        public IAstNode Expression;
        public IAstNode Index;
        public Position Position => new Position(Expression.Position.StartLine, Index.Position.EndLine, Expression.Position.StartCol, Index.Position.EndCol);

        public IndexNode(IAstNode Expression, IAstNode Index)
        {
            this.Expression = Expression;
            this.Index = Index;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            Expression.Accept(visitor);
            Index.Accept(visitor);
            visitor.VisitLeave(this);
        }
    }
}
