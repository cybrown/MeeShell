namespace MeeShell.Runtime
{
    internal sealed class BooleanMeeValue : BaseMeeValue
    {
        private readonly bool Value;

        public override object Internal => Value;

        public override string TypeName => "Boolean";

        private BooleanMeeValue(bool value)
        {
            Value = value;
        }

        public static BooleanMeeValue True = new BooleanMeeValue(true);

        public static BooleanMeeValue False = new BooleanMeeValue(false);
    }
}
