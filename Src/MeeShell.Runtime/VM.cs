using System;
using System.Collections.Generic;
using System.Linq;
using MeeShell.Mir;

namespace MeeShell.Runtime
{
    public class VM : IMirVisitor
    {
        private struct Context
        {
            public int PC;
            public List<IOperation> Operations;
            public List<IMeeValue> Stack;
        }

        private int PC;
        private List<IOperation> Operations;
        private List<IMeeValue> Stack = new List<IMeeValue>();
        private readonly Dictionary<string, IMeeValue> variables = new Dictionary<string, IMeeValue>();
        private readonly List<Context> ContextStack = new List<Context>();

        public void Push(IMeeValue value)
        {
            Stack.Add(value);
        }

        public IMeeValue Pop()
        {
            var value = Stack[^1];
            Stack.RemoveAt(Stack.Count - 1);
            return value;
        }

        public void LoadVariable(string name)
        {
            Push(variables[name]);
        }

        public void SetVariable(string name, object value)
        {
            variables[name] = MeeValueFactory.FromObject(value);
        }

        public void RegisterCommand(ICommand command)
        {
            SetVariable(command.Name, MeeValueFactory.From(command));
        }

        public IReadOnlyCollection<ICommand> RegisteredCommands => variables
            .Where(v => v.Value is CommandMeeValue)
            .Select(v => v.Value.Internal as ICommand)
            .ToList();

        public IReadOnlyDictionary<string, IMeeValue> Variables => variables;

        public void Run(List<IOperation> operations)
        {
            Stack.Clear();
            PC = 0;
            ContextStack.Clear();
            Operations = operations;
            while (true)
            {
                while (PC < Operations.Count)
                {
                    var op = Operations[PC];
                    op.Accept(this);
                    PC++;
                }
                if (ContextStack.Count == 0)
                {
                    break;
                }
                else
                {
                    var ctx = ContextStack[^1];
                    ContextStack.RemoveAt(ContextStack.Count - 1);
                    PC = ctx.PC + 1;
                    Operations = ctx.Operations;
                    var returnValue = Pop();
                    Stack = ctx.Stack;
                    Push(returnValue);
                }
            }
        }

        public void Visit(OperationPushNumber operation)
        {
            Push(MeeValueFactory.From(operation.Value));
        }

        public void Visit(OperationPushString operation)
        {
            Push(MeeValueFactory.From(operation.Value));
        }

        public void Visit(OperationMember operation)
        {
            var value = Pop();
            Push(value[MeeValueFactory.From(operation.Name)]);
        }

        public void Visit(OperationLoadVariable operation)
        {
            LoadVariable(operation.Name);
        }

        public void Visit(OperationUnaryMinus operation)
        {
            var value = Pop();
            Push(value.Invert());
        }

        public void Visit(OperationUnaryPlus operation)
        {
            var value = Pop();
            Push(value.UnaryPlus());
        }

        public void Visit(OperationBinaryPlus operation)
        {
            var value2 = Pop();
            var value1 = Pop();
            Push(value1.Add(value2));
        }

        public void Visit(OperationBinaryMinus operation)
        {
            var value2 = Pop();
            var value1 = Pop();
            Push(value1.Substract(value2));
        }

        public void Visit(OperationBinaryMultiply operation)
        {
            var value2 = Pop();
            var value1 = Pop();
            Push(value1.Multiply(value2));
        }

        public void Visit(OperationBinaryDivide operation)
        {
            var value2 = Pop();
            var value1 = Pop();
            Push(value1.Divide(value2));
        }

        public void Visit(OperationMakeTuple operation)
        {
            var values = new List<IMeeValue>();
            for (var i = 0; i < operation.Count; i++)
            {
                values.Insert(0, Pop());
            }
            Push(MeeValueFactory.From(values));
        }

        public void Visit(OperationRunCommand operation)
        {
            var args = Pop();
            var command = Pop();
            Push(command.Invoke(this, args));
        }

        public void Visit(OperationIndex operation)
        {
            var value2 = Pop();
            var value1 = Pop();
            Push(value1[value2]);
        }

        public void Visit(OperationPushBlock operationPushBlock)
        {
            Push(MeeValueFactory.From(operationPushBlock.Operations));
        }

        internal void PushContext(BlockMeeValue block)
        {
            ContextStack.Add(new Context()
            {
                PC = PC,
                Operations = Operations,
                Stack = Stack,
            });
            PC = -1;
            Operations = block.Operations;
            Stack = new List<IMeeValue>();
        }
    }
}
