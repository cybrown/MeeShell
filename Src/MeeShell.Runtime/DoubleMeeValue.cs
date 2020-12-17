using System;

namespace MeeShell.Runtime
{
    internal class DoubleMeeValue : IMeeValue
    {
        public double Value { get; }

        public object Internal => Value;

        public IMeeValue this[int index] => throw new NotSupportedException();

        public IMeeValue this[string index] => throw new NotSupportedException();

        public IMeeValue this[IMeeValue index] => throw new NotSupportedException();

        public DoubleMeeValue(double value)
        {
            Value = value;
        }

        public IMeeValue Add(IMeeValue other)
        {
            if (other is DoubleMeeValue d)
            {
                return new DoubleMeeValue(Value + d.Value);
            }
            throw new NotSupportedException();
        }

        public IMeeValue Invert()
        {
            return new DoubleMeeValue(-Value);
        }

        public IMeeValue UnaryPlus()
        {
            return new DoubleMeeValue(Value);
        }

        public IMeeValue Substract(IMeeValue other)
        {
            if (other is DoubleMeeValue d)
            {
                return new DoubleMeeValue(Value - d.Value);
            }
            throw new NotSupportedException();
        }

        public IMeeValue Multiply(IMeeValue other)
        {
            if (other is DoubleMeeValue d)
            {
                return new DoubleMeeValue(Value * d.Value);
            }
            throw new NotSupportedException();
        }

        public IMeeValue Divide(IMeeValue other)
        {
            if (other is DoubleMeeValue d)
            {
                return new DoubleMeeValue(Value / d.Value);
            }
            throw new NotSupportedException();
        }
    }
}
