using Antlr4.Runtime.Tree;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Spectre.Console;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MathScript
{
    internal static class Cli
    {
        public static Argument<string> inPathArg = (new Argument<string>("Program")
        {
            Description = $"The {MathScriptInfo.LanguageName} program (.{MathScriptInfo.FileType.Extension} file) to compile/execute",
            Arity = ArgumentArity.ZeroOrOne
        }).AcceptLegalFilePathsOnly();
        public static Option<string> outPathOpt = (new Option<string>("--Output", "--output", "-o")
        {
            Description = $"Compile the {MathScriptInfo.LanguageName} program into the `<Output>` executable",
            HelpName = "Output"
        }).AcceptLegalFilePathsOnly();
        public static Option<DebugLevelArgument> debugLevelOpt = (new Option<DebugLevelArgument>("--Debug", "--debug", "-d")
        {
            Description = $"Set the debug level ({string.Join(", ", Enum.GetNames<DebugLevelArgument>())})",
            HelpName = "DebugLevel",
        }).AcceptOnlyFromAmong(Enum.GetNames<DebugLevelArgument>());
        public static VersionOption versionOpt = new("--Version", "-v")
        {
            Description = "Print version information",
            Action = new VersionOptionAction()
        };

        public static RootCommand rootCommand = new(MathScriptInfo.Name)
        {
            inPathArg,
            outPathOpt,
            debugLevelOpt,
            versionOpt
        };

        static Cli()
        {
            rootCommand.Validators.Add((result) =>
            {
                string? inPath = result.GetValue(inPathArg);
                string? outPath = result.GetValue(outPathOpt);

                if (inPath == null && outPath != null)
                {
                    result.AddError(new ArgumentNullException(inPathArg.Name).Message);
                }

                if (inPath != null && !File.Exists(inPath))
                {
                    result.AddError(new FileNotFoundException(null, inPath).Message);
                }


                if (inPath != null && outPath != null && File.Exists(outPath))
                {
                    if (!AnsiConsole.Confirm(
                        $"The file {outPath} already exists. Do you want to overwrite it?",
                        false
                    ))
                    {
                        result.AddError("Aborting compilation.");
                        return;
                    }
                }
            });
            rootCommand.TreatUnmatchedTokensAsErrors = true;
            rootCommand.SetAction(result =>
            {
                MathScriptInfo.DebugLevel = (DebugLevel)result.GetValue(debugLevelOpt);
                CliMain(result.GetValue(inPathArg), result.GetValue(outPathOpt));
            });
        }
        static void CliMain(string? inPath, string? outPath)
        {
            if (inPath == null && outPath == null)
            {
                AnsiConsole.MarkupLine(
                    MathScriptInfo.SplashTexts[new Random().Next(MathScriptInfo.SplashTexts.Count)]
                    + "\n\n" + MathScriptInfo.VersionText
                    + $"\n{
                        (MathScriptInfo.DebugLevel != DebugLevel.None
                        ? $"Debug level: {MathScriptInfo.DebugLevel}\n\n" : "")
                    }Type 'exit' or press Ctrl + C to quit."
                );

                while (true)
                {
                    AnsiConsole.Markup($"[red]{MathScriptInfo.LanguageName}[/]> ");
                    string? line = Console.ReadLine();

                    if (line == null)
                    {
                        continue;
                    }
                    else if (line.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[grey]{Interpreter.Execute(line)}[/]");
                    }
                }
            }
            else
            {
                // Technically redundant because of the validators, but better safer than never...
                // and also makes the compiler shut up about inPath being nullable while when we make
                // it not nullable it starts yapping about type issues in the validators
                if (!File.Exists(inPath))
                {
                    throw new FileNotFoundException(null, inPath);
                }

                IParseTree AST = Interpreter.Parse(File.ReadAllText(inPath));

                if (outPath == null)
                {
                    Interpreter.Execute(AST);
                }
                else
                {
                    Interpreter.Compile(AST, outPath);
                }
            }
        }
    }

    class VersionOptionAction : SynchronousCommandLineAction
    {
        public override int Invoke(ParseResult parseResult)
        {
            AnsiConsole.MarkupLine(MathScriptInfo.VersionText);
            return 0;
        }
    }
}
