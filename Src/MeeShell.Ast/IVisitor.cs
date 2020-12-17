namespace MeeShell.Ast
{
    public interface IVisitor
    {
        void Visit(NumberNode node);
        void Visit(IdentifierNode node);
        void Visit(WhitespaceNode node);
        void Visit(StringNode node);
        void VisitEnter(CommandNode node);
        void VisitLeave(CommandNode node);
        void VisitEnter(ExpressionCommandNode node);
        void VisitLeave(ExpressionCommandNode node);
        void VisitEnter(UnaryNode node);
        void VisitLeave(UnaryNode node);
        void VisitEnter(BinaryNode node);
        void VisitLeave(BinaryNode node);
        void VisitEnter(IndexNode node);
        void VisitLeave(IndexNode node);
        void VisitEnter(TupleNode node);
        void VisitLeave(TupleNode node);
        void VisitEnter(DotNode node);
        void VisitLeave(DotNode node);
        void VisitEnter(ProgramNode node);
        void VisitLeave(ProgramNode node);
    }
}
