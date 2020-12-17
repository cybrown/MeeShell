namespace MeeShell.Mir
{
    public interface IMirVisitor
    {
        void Visit(OperationPushNumber operation);
        void Visit(OperationPushString operation);
        void Visit(OperationDot operation);
        void Visit(OperationIndex operation);
        void Visit(OperationLoadVariable operation);
        void Visit(OperationUnaryMinus operation);
        void Visit(OperationUnaryPlus operation);
        void Visit(OperationBinaryPlus operation);
        void Visit(OperationBinaryMinus operation);
        void Visit(OperationBinaryMultiply operation);
        void Visit(OperationBinaryDivide operation);
        void Visit(OperationMakeTuple operation);
        void Visit(OperationRunCommand operation);
    }
}
