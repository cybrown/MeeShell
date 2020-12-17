namespace MeeShell.Runtime
{
    internal class CommandMeeValue : BaseMeeValue
    {
        private readonly ICommand Value;

        public override object Internal => Value;

        public override string TypeName => "Command";

        public CommandMeeValue(ICommand command)
        {
            Value = command;
        }

        public override IMeeValue Invoke(VM vm, IMeeValue arg)
        {
            return MeeValueFactory.FromObject(Value.Run(vm, arg));
        }
    }
}
