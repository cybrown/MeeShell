using System;
using System.Collections;

namespace MeeShell.Runtime
{
    internal class DictMeeValue : BaseMeeValue
    {
        private readonly IDictionary Values;

        public override object Internal => Values;

        public override string TypeName => "Dict";

        public override IMeeValue this[string index]
        {
            get
            {
                return MeeValueFactory.FromObject(Values[index]);
            }
            set
            {
                Values[index] = value;
            }
        }

        public override IMeeValue this[IMeeValue index]
        {
            get
            {
                if (index.Internal is string s)
                {
                    return this[s];
                }
                throw new NotSupportedException();
            }
            set
            {
                if (index.Internal is string s)
                {
                    this[s] = value;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public DictMeeValue(IDictionary values)
        {
            Values = values;
        }

        public override IMeeValue Has(IMeeValue index)
        {
            return MeeValueFactory.From(Values.Contains((string)index.Internal));
        }

        public override IMeeValue Unset(IMeeValue index)
        {
            var key = (string)index.Internal;
            var result = Values[key];
            Values.Remove(key);
            return MeeValueFactory.FromObject(result);
        }
    }
}
