using System;

namespace MeeShell.Runtime
{
    internal sealed class NullMeeValue : BaseMeeValue
    {
        public override string TypeName => "Null";

        private NullMeeValue() { }

        public static NullMeeValue Instance = new NullMeeValue();
    }
}
