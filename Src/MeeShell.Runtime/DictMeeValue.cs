using System;
using System.Collections;

namespace MeeShell.Runtime
{
    internal class DictMeeValue : IMeeValue
    {
        public IDictionary Values { get; }

        public object Internal => Values;

        public IMeeValue this[int index] => throw new NotSupportedException();

        public IMeeValue this[string index] => MeeValueFactory.Create(Values[index]);

        public IMeeValue this[IMeeValue index]
        {
            get
            {
                if (index.Internal is string s)
                {
                    return this[s];
                }
                throw new NotSupportedException();
            }
        }

        public DictMeeValue(IDictionary values)
        {
            Values = values;
        }

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
