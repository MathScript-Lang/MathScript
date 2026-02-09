using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using MathScript.Visitors;
using Spectre.Console;
using System.Text.Json;

namespace MathScript
{
    internal static class Interpreter
    {
        public static ITokenStream Lex(string code)
        {
            ICharStream stream = CharStreams.fromString(code);
            MathScriptLexer lexer = new(stream);
            CommonTokenStream tokens = new(lexer);

            if (MathScriptInfo.DebugLevel.HasFlag(DebugLevel.Lexer))
            {
                List<string> tokensStr = [];
                tokens.Fill();

                for (int i = 0; i < tokens.Size; i++)
                {
                    IToken token = tokens.Get(i);
                    tokensStr.Add(
                        $"<{(!string.IsNullOrEmpty(lexer.Vocabulary.GetSymbolicName(token.Type))
                            ? lexer.Vocabulary.GetSymbolicName(token.Type) + " " : "")
                        }{JsonSerializer.Serialize(token.Text)}>"
                    );
                }

                AnsiConsole.MarkupLine($"[green]Tokens: [[{String.Join(", ", tokensStr).EscapeMarkup()}]][/]");
            }

            return tokens;
        }

        public static IParseTree Parse(ITokenStream tokens)
        {
            MathScriptParser parser = new MathScriptParser(tokens);
            IParseTree tree = parser.prog();

            if (MathScriptInfo.DebugLevel.HasFlag(DebugLevel.Parser))
            {
                AnsiConsole.MarkupLine($"[yellow]AST: {tree.ToStringTree(parser).EscapeMarkup()}[/]");
            }

            return tree;
        }

        public static IParseTree Parse(string code)
        {
            return Parse(Lex(code));
        }

        public static object? Execute(IParseTree tree)
        {
            InterpreterVisitor visitor = new();
            return visitor.Visit(tree);
        }

        public static object? Execute(ITokenStream tokens)
        {
            return Execute(Parse(tokens));
        }

        public static object? Execute(string code)
        {
            return Execute(Parse(code));
        }

        public static object? Compile(IParseTree tree, string outPath)
        {
            return 2;
        }

        public static object? Compile(string code, string outPath)
        {
            return Compile(Parse(code), outPath);
        }

        public static object? Compile(ITokenStream tokens, string outPath)
        {
            return Compile(Parse(tokens), outPath);
        }
    }
}
