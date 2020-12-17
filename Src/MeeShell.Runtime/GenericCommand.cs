using System;

namespace MeeShell.Runtime
{
    public class GenericCommand : ICommand
    {
        public string Name { get; }
        private readonly Func<VM, IMeeValue, IMeeValue> code;

        public GenericCommand(string name, Func<VM, IMeeValue, IMeeValue> code)
        {
            Name = name;
            this.code = code;
        }

        public object Run(VM vm, IMeeValue arg)
        {
            return code(vm, arg);
        }
    }
}
