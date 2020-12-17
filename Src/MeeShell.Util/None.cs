namespace MeeShell.Util
{
    // There is no null type representation in csharp so I must create my own...
    public sealed class None
    {
        private None() { }

        public static readonly None Instance = new None();
    }
}