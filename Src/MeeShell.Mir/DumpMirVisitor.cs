using System;

namespace MeeShell.Mir
{
    public class DumpMirVisitor : IMirVisitor
    {
        public void Visit(OperationPushNumber operation)
        {
            Console.WriteLine("PushNumber: " + operation.Value);
        }

        public void Visit(OperationPushString operation)
        {
            Console.WriteLine("PushString: \"" + operation.Value + "\"");
        }

        public void Visit(OperationMember operation)
        {
            Console.WriteLine("Member: " + operation.Name);
        }

        public void Visit(OperationIndex operation)
        {
            Console.WriteLine("Index");
        }

        public void Visit(OperationLoadVariable operation)
        {
            Console.WriteLine("LoadVariable: " + operation.Name);
        }

        public void Visit(OperationUnaryMinus operation)
        {
            Console.WriteLine("UnaryMinus");
        }

        public void Visit(OperationUnaryPlus operation)
        {
            Console.WriteLine("UnaryPlus");
        }

        public void Visit(OperationBinaryPlus operation)
        {
            Console.WriteLine("BinaryPlus");
        }

        public void Visit(OperationBinaryMinus operation)
        {
            Console.WriteLine("BinaryMinus");
        }

        public void Visit(OperationBinaryMultiply operation)
        {
            Console.WriteLine("BinaryMultiply");
        }

        public void Visit(OperationBinaryDivide operation)
        {
            Console.WriteLine("BinaryDivide");
        }

        public void Visit(OperationMakeTuple operation)
        {
            Console.WriteLine("MakeTuple: " + operation.Count);
        }

        public void Visit(OperationRunCommand operation)
        {
            Console.WriteLine("RunCommand");
        }

        public void Visit(OperationPushBlock operationPushBlock)
        {
            Console.WriteLine("PushBlock");
        }
    }
}
