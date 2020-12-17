using System.Collections.Generic;
using MeeShell.Mir;

namespace MeeShell.Runtime
{
    internal class BlockMeeValue : BaseMeeValue
    {
        public List<IOperation> Operations { get; }

        public override object Internal => Operations;

        public override string TypeName => "Block";

        public BlockMeeValue(List<IOperation> operations)
        {
            Operations = operations;
        }

        public override IMeeValue Invoke(VM vm, IMeeValue arg)
        {
            vm.PushContext(this);
            return null;
        }
    }
}
