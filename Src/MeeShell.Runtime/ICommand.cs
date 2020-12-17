using System.Collections.Generic;

namespace MeeShell.Runtime
{
    public interface ICommand
    {
        string Name { get; }
        object Run(VM vm, IMeeValue arg);
    }
}
