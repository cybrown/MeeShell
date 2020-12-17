using MeeShell.ParserKit;

namespace MeeShell.Ast
{
    public interface IAstNode
    {
        Position Position { get; }
        void Accept(IVisitor visitor);
    }
}
