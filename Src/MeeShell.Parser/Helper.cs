using System;
using System.Globalization;
using System.Text.RegularExpressions;
using MeeShell.Ast;
using MeeShell.ParserKit;
using MeeShell.Util;
using static MeeShell.ParserKit.Helper;

namespace MeeShell.Parser
{
    public static class Helper
    {
        public static IParser<ParserContext, string> StrRaw(string str)
        {
            return Match<ParserContext>((_, src) =>
              {
                  if (src.Length >= str.Length && src.Substring(0, str.Length) == str)
                  {
                      return str;
                  }
                  return null;
              }, str);
        }

        public static IParser<ParserContext, string> RegexRaw(string pattern, string expectedName)
        {
            var regexp = new Regex("^" + pattern);
            return Match<ParserContext>((_, src) =>
              {
                  var match = regexp.Match(src);
                  return match.Success ? match.Value : null;
              }, expectedName);
        }

        public static Lazy<IParser<ParserContext, WhitespaceNode>> Whitespace = new Lazy<IParser<ParserContext, WhitespaceNode>>(() =>
        {
            var regexp = new Regex(@"^[ \t]+");
            var regexpWithLineReturn = new Regex(@"^[ \t\n]+");
            return Match<ParserContext>((ctx, src) =>
              {
                  var match = (ctx.BracketDepth > 0 ? regexpWithLineReturn : regexp).Match(src);
                  return match.Success ? match.Value : null;
              }, "Whitespace")
                .Map((_, r) => new WhitespaceNode(r.Position));
        });

        public static IParser<ParserContext, object> OptWh = Opt(Whitespace.Value);

        public static IParser<ParserContext, string> Regex(string pattern, string expectedName) =>
            RegexRaw(pattern, expectedName).ThenIgnore(OptWh);

        public static IParser<ParserContext, string> Str(string str) =>
            StrRaw(str).ThenIgnore(OptWh);

        public static IParser<ParserContext, IdentifierNode> Variable =
            StrRaw("$").IgnoreThen(Regex("[a-zA-Z_][a-zA-Z0-9_]*", "Variable"))
                .Map((n, r) => new IdentifierNode(r.Position, n));

        public static IParser<ParserContext, IdentifierNode> Identifier =
            Regex("[a-zA-Z_][a-zA-Z0-9_]*", "Identifier")
                .Map((n, r) => new IdentifierNode(r.Position, n));

        public static IParser<ParserContext, StringNode> QuotedString =
            Regex("\"[^\"]*\"", "QuotedString")
                .Map((n, r) => new StringNode(r.Position, n[1..^1]));

        public static IParser<ParserContext, StringNode> UnquotedString =
            Regex("[^\" \\t\\n()[\\]]+", "UnquotedString")
                .Map((n, r) => new StringNode(r.Position, n));

        public static IParser<ParserContext, string> DigitsRaw =
            RegexRaw("[0-9]+", "Digits");

        public static IParser<ParserContext, NumberNode> Number =
            FirstOf(
                DigitsRaw.Then(StrRaw(".").IgnoreThen(DigitsRaw)),
                DigitsRaw.Then(Opt(StrRaw("."))).Map(n => Tuple.Create(n.Item1, "")),
                StrRaw(".").IgnoreThen(DigitsRaw).Map(n => Tuple.Create("", n))
            ).ThenIgnore(OptWh)
                .Map((n, r) => new NumberNode(r.Position, double.Parse(n.Item1 + "." + n.Item2, CultureInfo.InvariantCulture)));

        public static readonly IParser<ParserContext, IAstNode> Expression = Lazy(() => TupleExpression);

        public static IParser<ParserContext, ExpressionCommandNode> ExpressionCommand =
            Lazy(() => CommandStatement.Between(StrRaw("$(").UpdateContext(ctx => ctx.BracketDepth++).ThenIgnore(OptWh), StrRaw(")").UpdateContext(ctx => ctx.BracketDepth--).ThenIgnore(OptWh)))
                .Map(n => new ExpressionCommandNode(n.Name, n.Arg));

        public static readonly IParser<ParserContext, IAstNode> Parenthesis =
            Expression.Between(
                StrRaw("(").UpdateContext(ctx => ctx.BracketDepth++).ThenIgnore(OptWh),
                StrRaw(")").UpdateContext(ctx => ctx.BracketDepth--).ThenIgnore(OptWh)
            );

        public static readonly IParser<ParserContext, IAstNode> SubExpression = FirstOf(
            Parenthesis,
            ExpressionCommand,
            Number,
            Variable,
            QuotedString,
            UnquotedString
        );

        public static readonly IParser<ParserContext, IAstNode> DotExpression =
            SubExpression.Then(Many(FirstOf(
                Str(".").IgnoreThen(Identifier).Map(n => Tuple.Create("dot", n as IAstNode)),
                Expression.Between(Str("["), Str("]")).Map(n => Tuple.Create("index", n))
            )))
                .Map(n =>
                {
                    IAstNode currentNode = n.Item1;
                    foreach (var node in n.Item2)
                    {
                        if (node.Item1 == "dot")
                        {
                            currentNode = new DotNode(currentNode, node.Item2 as IdentifierNode);
                        }
                        else if (node.Item1 == "index")
                        {
                            currentNode = new IndexNode(currentNode, node.Item2);
                        }
                    }
                    return currentNode;
                });

        public static readonly IParser<ParserContext, IAstNode> UnaryExpression = Lazy(() => FirstOf(
            Regex("[+-]", "UnaryOperator").Map((n, r) => new IdentifierNode(r.Position, n)).Then(UnaryExpression).Map(n => new UnaryNode(n.Item1, n.Item2)),
            DotExpression
        ));

        public static readonly IParser<ParserContext, IAstNode> BinaryExpressionMulDiv =
            CreateBinaryExpressionParser(UnaryExpression, Regex("[*/]", "* or /"));

        public static readonly IParser<ParserContext, IAstNode> BinaryExpressionAddSub =
            CreateBinaryExpressionParser(BinaryExpressionMulDiv, Regex("[+-]", "+ or -"));

        public static IParser<ParserContext, IAstNode> CreateBinaryExpressionParser(IParser<ParserContext, IAstNode> lowerParser, IParser<ParserContext, string> operatorParser) =>
            lowerParser.Then(Many(operatorParser.Then(lowerParser)))
                .Map(n =>
                {
                    var rest = n.Item2;
                    IAstNode nodeToReturn = n.Item1;
                    foreach (var node in rest)
                    {
                        nodeToReturn = new BinaryNode(node.Item1, nodeToReturn, node.Item2);
                    }
                    return nodeToReturn;
                });

        public static readonly IParser<ParserContext, IAstNode> TupleExpression =
            Many(BinaryExpressionAddSub.ThenIgnore(Opt(Str(","))))
                .Map(n => n.Length > 1 ? new TupleNode(n) : n.Length == 1 ? n[0] : new TupleNode(new IAstNode[] { }));

        public static readonly IParser<ParserContext, CommandNode> CommandStatement =
            Identifier.Then(Opt(Expression))
                .Map(n => new CommandNode(n.Item1, ForceTuple(n.Item2)));

        public static TupleNode ForceTuple(object node) => node switch
        {
            TupleNode t => t,
            None _ => new TupleNode(new IAstNode[] { }),
            IAstNode n => new TupleNode(new IAstNode[] { n }),
            _ => throw new InvalidOperationException(),
        };

        public static IParser<ParserContext, ProgramNode> ParseProgram =
            Many(FirstOf(Regex("[\n;]", "CommandSeparator").Map((_, r) => new WhitespaceNode(r.Position)), Whitespace.Value)).Then(
                ManySeperatedBy(
                    CommandStatement,
                    Many(Regex("[\n;]", "CommandSeparator"), ManyEnum.AT_LEAST_ONE),
                    ManySeperatedByEnum.END_OPTIONNAL
                ),
                EoS<ParserContext>()
            )
                .Map(n => new ProgramNode(n.Item2));
    }
}
