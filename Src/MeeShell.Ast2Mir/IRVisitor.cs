using System;
using System.Collections.Generic;
using MeeShell.Ast;
using MeeShell.Mir;

namespace MeeShell.Ast2Mir
{
    internal class IRVisitor : IVisitor
    {
        public List<IOperation> Operations { get; private set; } = new List<IOperation>();
        private readonly List<List<IOperation>> ListStack = new List<List<IOperation>>();

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
            Operations.Add(new OperationRunCommand());
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

        public void VisitLeave(MemberNode node)
        {
            Operations.Add(new OperationMember(node.Name.Value));
        }

        public void VisitLeave(IndexNode node)
        {
            Operations.Add(new OperationIndex());
        }

        public void VisitEnter(BlockNode node)
        {
            ListStack.Add(Operations);
            Operations = new List<IOperation>();
        }

        public void VisitLeave(BlockNode node)
        {
            var subOperations = Operations;
            Operations = ListStack[^1];
            ListStack.RemoveAt(ListStack.Count - 1);
            Operations.Add(new OperationPushBlock(subOperations));
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

        public void VisitEnter(MemberNode node)
        {
        }

        public void VisitEnter(ProgramNode node)
        {
        }

        public void VisitEnter(IndexNode node)
        {
        }
        #endregion
    }
}
