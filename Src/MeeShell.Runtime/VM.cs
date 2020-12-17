using System;
using System.Collections.Generic;
using System.Linq;
using MeeShell.Mir;

namespace MeeShell.Runtime
{
    public class VM : IMirVisitor
    {
        private readonly List<IMeeValue> Stack = new List<IMeeValue>();
        private readonly Dictionary<string, IMeeValue> variables = new Dictionary<string, IMeeValue>();
        private readonly Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>();

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

        public void RunCommand(string name, IMeeValue arg)
        {
            if (!Commands.ContainsKey(name))
            {
                throw new Exception($"Command '{name}' is not registered.");
            }
            var result = Commands[name].Run(this, arg);
            Push(MeeValueFactory.Create(result));
        }

        public void SetVariable(string name, object value)
        {
            variables[name] = MeeValueFactory.Create(value);
        }

        public void RegisterCommand(ICommand command)
        {
            Commands[command.Name] = command;
        }

        public IReadOnlyCollection<ICommand> RegisteredCommands => Commands.Values;
        public IReadOnlyCollection<string> Variables => variables.Select(v => v.Key).ToList();

        public void Run(List<IOperation> operations)
        {
            foreach (var op in operations)
            {
                op.Accept(this);
            }
        }

        public void Visit(OperationPushNumber operation)
        {
            Push(MeeValueFactory.Create(operation.Value));
        }

        public void Visit(OperationPushString operation)
        {
            Push(MeeValueFactory.Create(operation.Value));
        }

        public void Visit(OperationDot operation)
        {
            var value = Pop();
            Push(value[MeeValueFactory.Create(operation.Name)]);
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
            Push(MeeValueFactory.Create(values));
        }

        public void Visit(OperationRunCommand operation)
        {
            var args = Pop();
            RunCommand(operation.Name, args);
        }

        public void Visit(OperationIndex operation)
        {
            var value2 = Pop();
            var value1 = Pop();
            Push(value1[value2]);
        }
    }
}
