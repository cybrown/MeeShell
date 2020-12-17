using System;
using System.Collections;
using System.Collections.Generic;

namespace MeeShell.Runtime
{
    internal class TupleMeeValue : BaseMeeValue
    {
        private readonly List<object> Values;

        public override object Internal => Values;

        public override string TypeName => "Tuple";

        public override IMeeValue this[int index]
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
                if (index.Internal is double d)
                {
                    return this[(int)d];
                }
                throw new NotSupportedException();
            }
            set
            {
                if (index.Internal is double d)
                {
                    this[(int)d] = value;
                }
                else
                {
                    throw new NotSupportedException();
                }
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

        public override int Length => Values.Count;
    }
}
