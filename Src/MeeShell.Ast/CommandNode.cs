using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class CommandNode : IAstNode
    {
        public IdentifierNode Name;
        public IAstNode Arg;
        public Position Position => new Position(Name.Position.StartLine, Arg.Position.EndLine, Name.Position.StartCol, Arg.Position.EndCol);

        public CommandNode(IdentifierNode name, IAstNode arg)
        {
            Name = name;
            Arg = arg;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            Arg.Accept(visitor);
            visitor.VisitLeave(this);
        }
    }
}
