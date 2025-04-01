using System.CommandLine;
using System.CommandLine.Parsing;
using System.Reflection;

namespace MathScript
{
    internal class Program
    {
        internal static DebugLevel DebugLevel { get; set; } = DebugLevel.None;
        internal static string ProgramPath { get; set; } = string.Empty;
        internal static string? OutputPath { get; set; }

        private static async Task Main(string[] args)
        {
            Argument<string> pathArgument = new("program", "The MathScript program to compile");
            Option<string> outputOption = new(
                ["--output", "-o"],
                "Compile the MathScript program into the `output` executable"
            );
            Option<DebugLevelArgument> debugOption = new(
                ["--debug", "-d"],
                "Set the debug level (parser, lexer, parser_lexer, all)"
            ) { ArgumentHelpName = "debug_level" };
            Option<bool> versionOption = new(
                ["--version", "-v"],
                "Print the version number"
            );

            RootCommand rootCommand = new("MathScript Interpreter & Compiler")
            {
                pathArgument,
                outputOption,
                debugOption,
                versionOption
            };

            rootCommand.SetHandler((path) => ProgramPath = path, pathArgument);
            rootCommand.SetHandler((outputPath) => OutputPath = outputPath, outputOption);
            rootCommand.SetHandler((debugLevel) => DebugLevel = (DebugLevel)debugLevel, debugOption);
            rootCommand.SetHandler((showVersion) =>
            {
                if (showVersion)
                {
                    AssemblyName assemblyInfo = Assembly.GetExecutingAssembly().GetName();
                    Console.WriteLine($"{assemblyInfo.Name} v{assemblyInfo.Version}");
                }
            }, versionOption);

            _ = await rootCommand.InvokeAsync(args);
        }
    }
}
