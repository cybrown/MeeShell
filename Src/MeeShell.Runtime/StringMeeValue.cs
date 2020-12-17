using System;

namespace MeeShell.Runtime
{
    internal class StringMeeValue : IMeeValue
    {
        public string Value { get; }

        public object Internal => Value;

        public IMeeValue this[int index] => MeeValueFactory.Create(Value[index]);

        public IMeeValue this[string index] => throw new NotSupportedException();

        public IMeeValue this[IMeeValue index]
        {
            get
            {
                if (index.Internal is double d)
                {
                    return this[(int)d];
                }
                throw new NotSupportedException();
            }
        }

        public StringMeeValue(string value)
        {
            Value = value;
        }

        public IMeeValue Add(IMeeValue other)
        {
            if (other is StringMeeValue s)
            {
                return new StringMeeValue(Value + s.Value);
            }
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

        public IMeeValue Substract(IMeeValue value2)
        {
            throw new NotSupportedException();
        }

        public IMeeValue Multiply(IMeeValue value2)
        {
            throw new NotSupportedException();
        }

        public IMeeValue Divide(IMeeValue value2)
        {
            throw new NotSupportedException();
        }
    }
}
