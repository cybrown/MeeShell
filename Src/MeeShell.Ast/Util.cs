using MeeShell.Ast;

namespace MeeShell.Util
{
    public static class Utils
    {
        public static void DumpAst(IAstNode ast)
        {
            ast.Accept(new DumpAstVisitor());
        }
    }
}
