using System;

namespace MeeShell.Runtime
{
    internal sealed class NullMeeValue : IMeeValue
    {
        public object Internal => null;

        public IMeeValue this[int index] => throw new NotSupportedException();

        public IMeeValue this[string index] => throw new NotSupportedException();

        public IMeeValue this[IMeeValue index] => throw new NotSupportedException();

        private NullMeeValue() { }

        public IMeeValue Add(IMeeValue other)
        {
            throw new NotSupportedException();
        }

        public IMeeValue Invert()
        {
            throw new NotSupportedException();
        }

        public IMeeValue UnaryPlus()
        {
            throw new NotSupportedException();
        }

        public IMeeValue Substract(IMeeValue other)
        {
            throw new NotSupportedException();
        }

        public IMeeValue Multiply(IMeeValue other)
        {
            throw new NotSupportedException();
        }

        public IMeeValue Divide(IMeeValue other)
        {
            throw new NotSupportedException();
        }

        public static NullMeeValue Instance = new NullMeeValue();
    }
}
