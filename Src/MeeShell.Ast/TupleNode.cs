using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class TupleNode : IAstNode
    {
        public IAstNode[] Values;
        public Position Position
        {
            get
            {
                if (Values.Length > 0)
                    return new Position(Values[0].Position.StartLine, Values[^1].Position.EndLine, Values[0].Position.StartCol, Values[^1].Position.EndCol);
                else
                    return new Position();
            }
        }

        public TupleNode(IAstNode[] Values)
        {
            this.Values = Values;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            foreach (var value in Values)
            {
                value.Accept(visitor);
            }
            visitor.VisitLeave(this);
        }
    }
}
