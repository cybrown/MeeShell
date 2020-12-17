using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public class ProgramNode : IAstNode
    {
        public CommandNode[] Commands;
        public Position Position => new Position(Commands[0].Position.StartLine, Commands[^1].Position.EndLine, Commands[0].Position.StartCol, Commands[^1].Position.EndCol);

        public ProgramNode(CommandNode[] commands)
        {
            Commands = commands;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitEnter(this);
            foreach (var node in Commands)
            {
                node.Accept(visitor);
            }
            visitor.VisitLeave(this);
        }
    }
}
