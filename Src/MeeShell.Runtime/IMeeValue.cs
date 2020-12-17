namespace MeeShell.Runtime
{
    public interface IMeeValue
    {
        IMeeValue Invert();
        IMeeValue UnaryPlus();
        IMeeValue Add(IMeeValue other);
        IMeeValue Substract(IMeeValue other);
        IMeeValue Multiply(IMeeValue other);
        IMeeValue Divide(IMeeValue other);
        IMeeValue this[IMeeValue index] { get; }
        IMeeValue this[string index] { get; }
        IMeeValue this[int index] { get; }
        object Internal { get; }
    }
}
