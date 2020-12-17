namespace MeeShell.Mir
{
    public interface IOperation
    {
        void Accept(IMirVisitor visitor);
    }
}
