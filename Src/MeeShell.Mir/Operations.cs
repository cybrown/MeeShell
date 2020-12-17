namespace MeeShell.Mir
{
    public class OperationPushNumber : IOperation
    {
        public double Value { get; }

        public OperationPushNumber(double value)
        {
            Value = value;
        }

        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationPushString : IOperation
    {
        public string Value { get; }

        public OperationPushString(string value)
        {
            Value = value;
        }

        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationDot : IOperation
    {
        public string Name { get; }

        public OperationDot(string name)
        {
            Name = name;
        }

        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationIndex : IOperation
    {
        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationLoadVariable : IOperation
    {
        public string Name { get; }

        public OperationLoadVariable(string name)
        {
            Name = name;
        }

        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationUnaryMinus : IOperation
    {
        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationUnaryPlus : IOperation
    {
        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationBinaryPlus : IOperation
    {
        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationBinaryMinus : IOperation
    {
        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationBinaryMultiply : IOperation
    {
        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationBinaryDivide : IOperation
    {
        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationMakeTuple : IOperation
    {
        public int Count { get; }

        public OperationMakeTuple(int count)
        {
            Count = count;
        }

        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OperationRunCommand : IOperation
    {
        public string Name { get; }

        public OperationRunCommand(string name)
        {
            Name = name;
        }

        public void Accept(IMirVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
