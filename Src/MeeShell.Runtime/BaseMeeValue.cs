using System;

namespace MeeShell.Runtime
{
    internal abstract class BaseMeeValue : IMeeValue
    {
        public virtual object Internal => null;

        public virtual string TypeName { get; }

        public virtual int Length => throw new NotSupportedException();
        public virtual IMeeValue this[int index] { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public virtual IMeeValue this[string index] { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public virtual IMeeValue this[IMeeValue index] { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        protected BaseMeeValue() { }

        public virtual IMeeValue Add(IMeeValue other)
        {
            throw new NotSupportedException();
        }

        public virtual IMeeValue Invert()
        {
            throw new NotSupportedException();
        }

        public virtual IMeeValue UnaryPlus()
        {
            throw new NotSupportedException();
        }

        public virtual IMeeValue Substract(IMeeValue other)
        {
            throw new NotSupportedException();
        }

        public virtual IMeeValue Multiply(IMeeValue other)
        {
            throw new NotSupportedException();
        }

        public virtual IMeeValue Divide(IMeeValue other)
        {
            throw new NotSupportedException();
        }

        public virtual IMeeValue Invoke(VM vm, IMeeValue arg)
        {
            throw new NotSupportedException("Can not invoke null");
        }

        public virtual IMeeValue Has(IMeeValue index)
        {
            throw new NotSupportedException();
        }

        public virtual IMeeValue Unset(IMeeValue index)
        {
            throw new NotSupportedException();
        }
    }
}
