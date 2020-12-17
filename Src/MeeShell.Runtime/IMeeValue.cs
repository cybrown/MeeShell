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
        IMeeValue Invoke(VM vm, IMeeValue arg);
        IMeeValue Has(IMeeValue index);
        IMeeValue Unset(IMeeValue index);
        IMeeValue this[IMeeValue index] { get; set; }
        IMeeValue this[string index] { get; set; }
        IMeeValue this[int index] { get; set; }
        object Internal { get; }
        string TypeName { get; }
        int Length { get; }
    }
}
