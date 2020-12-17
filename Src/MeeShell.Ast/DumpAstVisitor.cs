using System;
using MeeShell.Ast;

namespace MeeShell.Util
{
    public class DumpAstVisitor : IVisitor
    {
        private int tabs;

        private void WriteLine(IAstNode node, string str)
        {
            Console.WriteLine($"[{node.Position.StartLine,3}:{node.Position.StartCol,3}]-[{node.Position.EndLine,3}:{node.Position.EndCol,3}] " + new string(' ', tabs * 2) + str);
        }

        public void Visit(NumberNode node)
        {
            WriteLine(node, "Number: " + node.Value);
        }

        public void Visit(IdentifierNode node)
        {
            WriteLine(node, "Identifier: " + node.Value);
        }

        public void Visit(WhitespaceNode node)
        {
            WriteLine(node, "Whitespace");
        }

        public void VisitEnter(CommandNode node)
        {
            WriteLine(node, "Command: " + node.Name.Value);
            tabs++;
        }

        public void VisitLeave(CommandNode node)
        {
            tabs--;
        }

        public void VisitEnter(ExpressionCommandNode node)
        {
            WriteLine(node, "ExpressionCommand: " + node.Name.Value);
            tabs++;
        }

        public void VisitLeave(ExpressionCommandNode node)
        {
            tabs--;
        }

        public void VisitEnter(UnaryNode node)
        {
            WriteLine(node, "Unary " + node.Operator.Value);
            tabs++;
        }

        public void VisitLeave(UnaryNode node)
        {
            tabs--;
        }

        public void VisitEnter(BinaryNode node)
        {
            WriteLine(node, "Binary: " + node.Operator);
            tabs++;
        }

        public void VisitLeave(BinaryNode node)
        {
            tabs--;
        }

        public void VisitEnter(TupleNode node)
        {
            WriteLine(node, "Tuple");
            tabs++;
        }

        public void VisitLeave(TupleNode node)
        {
            tabs--;
        }

        public void Visit(StringNode node)
        {
            WriteLine(node, "String: \"" + node.Value + "\"");
        }

        public void VisitEnter(DotNode node)
        {
            WriteLine(node, "Dot: " + node.Name.Value);
            tabs++;
        }

        public void VisitLeave(DotNode node)
        {
            tabs--;
        }

        public void VisitEnter(ProgramNode node)
        {
            WriteLine(node, "Program");
            tabs++;
        }

        public void VisitLeave(ProgramNode node)
        {
            tabs--;
        }

        public void VisitEnter(IndexNode node)
        {
            WriteLine(node, "Index");
            tabs++;
        }

        public void VisitLeave(IndexNode node)
        {
            tabs--;
        }
    }
}
