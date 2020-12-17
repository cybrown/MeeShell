using System;
using System.Collections;
using System.Collections.Generic;
using MeeShell.Mir;

namespace MeeShell.Runtime
{
    public static class MeeValueFactory
    {
        public static IMeeValue From(bool value)
        {
            return value ? BooleanMeeValue.True : BooleanMeeValue.False;
        }

        public static IMeeValue From(double value)
        {
            return new DoubleMeeValue(value);
        }

        public static IMeeValue From(string value)
        {
            return new StringMeeValue(value);
        }

        public static IMeeValue From(IDictionary dict)
        {
            return new DictMeeValue(dict);
        }

        public static IMeeValue From(List<IOperation> values)
        {
            return new BlockMeeValue(values);
        }

        public static IMeeValue From(IEnumerable values)
        {
            return new TupleMeeValue(values);
        }

        public static IMeeValue From(ICommand command)
        {
            return new CommandMeeValue(command);
        }

        public static IMeeValue FromObject(object value)
        {
            return value switch
            {
                IMeeValue mee => mee,
                null => NullMeeValue.Instance,
                bool b => From(b),
                double d => From(d),
                string s => From(s),
                ICommand c => From(c),
                IDictionary di => From(di),
                IEnumerable e => From(e),
                _ => throw new NotSupportedException("Unsupported type for MeeValue creation : " + value.GetType()),
            };
        }

        public static IMeeValue Null = NullMeeValue.Instance;
    }
}
