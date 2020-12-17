using System.Collections.Generic;
using MeeShell.Ast;
using MeeShell.Mir;

namespace MeeShell.Ast2Mir
{
    public class AstCompiler
    {
        public List<IOperation> Compile(IAstNode node)
        {
            var irVisitor = new IRVisitor();
            node.Accept(irVisitor);
            return irVisitor.Operations;
        }
    }
}
