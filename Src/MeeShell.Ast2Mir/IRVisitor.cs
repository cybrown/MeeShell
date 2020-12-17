using System;
using System.Collections.Generic;
using MeeShell.Ast;
using MeeShell.Mir;

namespace MeeShell.Ast2Mir
{
    internal class IRVisitor : IVisitor
    {
        public List<IOperation> Operations { get; } = new List<IOperation>();

        public void Visit(NumberNode node)
        {
            Operations.Add(new OperationPushNumber(node.Value));
        }

        public void Visit(IdentifierNode node)
        {
            Operations.Add(new OperationLoadVariable(node.Value));
        }

        public void VisitLeave(CommandNode node)
        {
            Operations.Add(new OperationRunCommand(node.Name.Value));
        }

        public void VisitLeave(UnaryNode node)
        {
            Operations.Add(node.Operator.Value switch
            {
                "-" => new OperationUnaryMinus(),
                "+" => new OperationUnaryPlus(),
                _ => throw new Exception("Unary operator not implemented: " + node.Operator),
            });
        }

        public void VisitLeave(BinaryNode node)
        {
            Operations.Add(node.Operator switch
            {
                "+" => new OperationBinaryPlus(),
                "-" => new OperationBinaryMinus(),
                "*" => new OperationBinaryMultiply(),
                "/" => new OperationBinaryDivide(),
                _ => throw new Exception("Binary operator not implemented: " + node.Operator),
            });
        }

        public void VisitLeave(TupleNode node)
        {
            Operations.Add(new OperationMakeTuple(node.Values.Length));
        }

        public void Visit(StringNode node)
        {
            Operations.Add(new OperationPushString(node.Value));
        }

        public void VisitLeave(DotNode node)
        {
            Operations.Add(new OperationDot(node.Name.Value));
        }

        public void VisitLeave(ExpressionCommandNode node)
        {
            Operations.Add(new OperationRunCommand(node.Name.Value));
        }

        public void VisitLeave(IndexNode node)
        {
            Operations.Add(new OperationIndex());
        }

        #region do not implement
        public void VisitLeave(ProgramNode node)
        {
        }

        public void Visit(WhitespaceNode node)
        {
            throw new Exception("Visit WhitespaceNode not implemented");
        }

        public void VisitEnter(CommandNode node)
        {
        }

        public void VisitEnter(UnaryNode node)
        {
        }

        public void VisitEnter(BinaryNode node)
        {
        }

        public void VisitEnter(TupleNode node)
        {
        }

        public void VisitEnter(DotNode node)
        {
        }

        public void VisitEnter(ProgramNode node)
        {
        }

        public void VisitEnter(ExpressionCommandNode node)
        {
        }

        public void VisitEnter(IndexNode node)
        {
        }
        #endregion
    }
}
