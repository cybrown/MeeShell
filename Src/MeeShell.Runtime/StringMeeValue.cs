using System;

namespace MeeShell.Runtime
{
    internal class StringMeeValue : BaseMeeValue
    {
        private readonly string Value;

        public override object Internal => Value;

        public override string TypeName => "String";

        public override IMeeValue this[int index] => MeeValueFactory.From(Value[index]);

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
        }

        public StringMeeValue(string value)
        {
            Value = value;
        }

        public override IMeeValue Add(IMeeValue other)
        {
            if (other is StringMeeValue s)
            {
                return new StringMeeValue(Value + s.Value);
            }
            throw new NotSupportedException();
        }
    }
}
