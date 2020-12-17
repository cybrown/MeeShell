using System;
using System.Collections;

namespace MeeShell.Runtime
{
    public static class MeeValueFactory
    {
        public static IMeeValue Create(double value)
        {
            return new DoubleMeeValue(value);
        }

        public static IMeeValue Create(string value)
        {
            return new StringMeeValue(value);
        }

        public static IMeeValue Create(IDictionary dict)
        {
            return new DictMeeValue(dict);
        }

        public static IMeeValue Create(IEnumerable values)
        {
            return new TupleMeeValue(values);
        }

        public static IMeeValue Create(object value)
        {
            if (value is IMeeValue mee)
            {
                return mee;
            }
            else if (value == null)
            {
                return NullMeeValue.Instance;
            }
            else if (value is double d)
            {
                return Create(d);
            }
            else if (value is string s)
            {
                return Create(s);
            }
            else if (value is IDictionary di)
            {
                return Create(di);
            }
            else if (value is IEnumerable e)
            {
                return Create(e);
            }
            throw new NotSupportedException("Unsupported type for MeeValue creation : " + value.GetType());
        }
    }
}
