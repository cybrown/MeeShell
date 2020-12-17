using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class BlockNode : IAstNode
    {
        public CommandNode[] Body;
        public Position Position
        {
            get
            {
                if (Body.Length > 0)
                {
                    return new Position(Body[0].Position.StartLine, Body[^1].Position.EndLine, Body[0].Position.StartCol, Body[^1].Position.EndCol);
                }
                else
                {
                    return new Position();
                }
            }
        }

        public BlockNode(CommandNode[] body)
        {
            Body = body;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            foreach (var command in Body)
            {
                command.Accept(visitor);
            }
            visitor.VisitLeave(this);
        }
    }
}
