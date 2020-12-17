using System;
using System.Collections;
using System.Collections.Generic;

namespace MeeShell.Runtime
{
    internal class TupleMeeValue : IMeeValue
    {
        public List<object> Values { get; }

        public object Internal => Values;

        public IMeeValue this[int index] => MeeValueFactory.Create(Values[index]);

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

        public TupleMeeValue(IEnumerable values)
        {
            Values = new List<object>();
            foreach (var item in values)
            {
                Values.Add(item);
            }
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
