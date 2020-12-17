using System;

namespace MeeShell.Runtime
{
    internal class DoubleMeeValue : BaseMeeValue
    {
        private readonly double Value;

        public override object Internal => Value;

        public override string TypeName => "Double";

        public DoubleMeeValue(double value)
        {
            Value = value;
        }

        public override IMeeValue Add(IMeeValue other)
        {
            if (other is DoubleMeeValue d)
            {
                return new DoubleMeeValue(Value + d.Value);
            }
            throw new NotSupportedException();
        }

        public override IMeeValue Invert()
        {
            return new DoubleMeeValue(-Value);
        }

        public override IMeeValue UnaryPlus()
        {
            return this;
        }

        public override IMeeValue Substract(IMeeValue other)
        {
            if (other is DoubleMeeValue d)
            {
                return new DoubleMeeValue(Value - d.Value);
            }
            throw new NotSupportedException();
        }

        public override IMeeValue Multiply(IMeeValue other)
        {
            if (other is DoubleMeeValue d)
            {
                return new DoubleMeeValue(Value * d.Value);
            }
            throw new NotSupportedException();
        }

        public override IMeeValue Divide(IMeeValue other)
        {
            if (other is DoubleMeeValue d)
            {
                return new DoubleMeeValue(Value / d.Value);
            }
            throw new NotSupportedException();
        }
    }
}
