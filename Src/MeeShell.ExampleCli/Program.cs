using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MeeShell.Ast2Mir;
using MeeShell.Runtime;
using MeeShell.Util;
using static MeeShell.Parser.Helper;

namespace MeeShell.ExampleCli
{
    public class EchoCommand : ICommand
    {
        public string Name => "echo";

        public object Run(VM vm, IMeeValue args)
        {
            return args[0];
        }
    }

    public class SetCommand : ICommand
    {
        public string Name => "set";

        public object Run(VM vm, IMeeValue args)
        {
            vm.SetVariable(args[0].Internal as string, args[1]);
            return args[1];
        }
    }

    public class VarsCommand : ICommand
    {
        public string Name => "vars";

        public object Run(VM vm, IMeeValue args)
        {
            return vm.Variables;
        }
    }

    public class HelpCommand : ICommand
    {
        public string Name => "help";

        public object Run(VM vm, IMeeValue args)
        {
            Console.WriteLine(".c     Clear current multiline");
            Console.WriteLine(".e     Throw on Runtime exceptions");
            Console.WriteLine(".q     Quit REPL");
            Console.WriteLine(".v     Toggle dump AST");
            Console.WriteLine("");
            Console.WriteLine("Available commands:");
            Console.WriteLine("");
            foreach (var command in vm.RegisteredCommands)
            {
                Console.WriteLine(command.Name);
            }
            return null;
        }
    }

    public class PwdCommand : ICommand
    {
        public string Name => "pwd";

        public object Run(VM vm, IMeeValue args)
        {
            return Directory.GetCurrentDirectory();
        }
    }

    public class CdCommand : ICommand
    {
        public string Name => "cd";

        public object Run(VM vm, IMeeValue args)
        {
            Directory.SetCurrentDirectory(args[0].Internal as string);
            vm.SetVariable("cwd", Directory.GetCurrentDirectory());
            return null;
        }
    }

    public class LsCommand : ICommand
    {
        public string Name => "ls";

        public object Run(VM vm, IMeeValue args)
        {
            return Directory
                .GetDirectories(Directory.GetCurrentDirectory())
                .Concat(Directory.GetFiles(Directory.GetCurrentDirectory()))
                .Select(path => Path.GetFileName(path));
        }
    }

    public class GetenvCommand : ICommand
    {
        public string Name => "getenv";

        public object Run(VM vm, IMeeValue args)
        {
            return Environment.GetEnvironmentVariable(args[0].Internal as string);
        }
    }

    public class EnvCommand : ICommand
    {
        public string Name => "env";

        public object Run(VM vm, IMeeValue args)
        {
            return Environment.GetEnvironmentVariables();
        }
    }

    public class Player
    {
        public string Name;
        public double X;
        public double Y;
        public double Z;

        public Player(string name, double x = 0, double y = 0, double z = 0)
        {
            Name = name;
            X = x;
            Y = y;
            Z = z;
        }
    }

    public static class Program
    {
        public static void Main()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

            var vm = new VM();

            vm.SetVariable("pi", 3.14);
            vm.SetVariable("cwd", Directory.GetCurrentDirectory());

            vm.RegisterCommand(new EchoCommand());
            vm.RegisterCommand(new SetCommand());
            vm.RegisterCommand(new VarsCommand());
            vm.RegisterCommand(new HelpCommand());
            vm.RegisterCommand(new PwdCommand());
            vm.RegisterCommand(new CdCommand());
            vm.RegisterCommand(new LsCommand());
            vm.RegisterCommand(new GetenvCommand());
            vm.RegisterCommand(new EnvCommand());

            bool showAst = false;
            bool throwOnRuntimeExceptions = false;
            string src = "";

            Console.WriteLine("Welcome to MeeShell, type help then enter for help, .q then enter to quit !");
            while (true)
            {
                if (src.Length == 0)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(". ");
                }
                var line = Console.ReadLine();

                if (line == ".q")
                {
                    break;
                }
                if (line == ".c")
                {
                    src = "";
                    continue;
                }
                if (line == ".v")
                {
                    showAst = !showAst;
                    continue;
                }
                if (line == ".e")
                {
                    throwOnRuntimeExceptions = !throwOnRuntimeExceptions;
                    continue;
                }

                var astCompiler = new AstCompiler();
                List<Mir.IOperation> opcodes;
                try
                {
                    if (src?.Length == 0)
                    {
                        src = line;
                    }
                    else
                    {
                        src += "\n";
                        src += line;
                    }

                    var result = ParseProgram.Parse(new Parser.ParserContext(), src);
                    if (!result.Success)
                    {
                        if (!(result.Expected.Length == 1 && result.Expected[0] == "End of Source"))
                        {
                            src = "";
                            Console.WriteLine("Parse failed, expected: " + string.Join(", ", result.Expected));
                        }
                        continue;
                    }
                    src = "";
                    if (showAst)
                    {
                        Utils.DumpAst(result.Value);
                    }
                    opcodes = astCompiler.Compile(result.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Compilation exception: " + ex.Message);
                    continue;
                }

                try
                {
                    vm.Run(opcodes);
                    var result = vm.Pop();
                    if (result == null)
                    {
                        Console.WriteLine("-> null");
                    }
                    else
                    {
                        Console.WriteLine("-> " + result.GetType());
                        if (result.Internal is string s)
                        {
                            Console.WriteLine(s);
                        }
                        else if (result.Internal is IDictionary dictionary)
                        {
                            foreach (var key in dictionary.Keys)
                            {
                                Console.WriteLine(key + ": " + dictionary[key]);
                            }
                        }
                        else if (result.Internal is IEnumerable enumerable)
                        {
                            foreach (var element in enumerable)
                            {
                                Console.WriteLine(element);
                            }
                        }
                        else
                        {
                            Console.WriteLine(result.Internal);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Runtime exception: " + ex.Message);
                    if (throwOnRuntimeExceptions)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
