namespace MathScript
{
    [Flags]
    internal enum DebugLevel
    {
        None = 0,
        Parser = 1,
        Lexer = 2,
        Interpreter = 4
    }

    internal enum DebugLevelArgument
    {
        Parser = DebugLevel.Parser,
        Lexer = DebugLevel.Lexer,
        ParserLexer = DebugLevel.Parser | DebugLevel.Lexer,
        All = DebugLevel.Parser | DebugLevel.Lexer | DebugLevel.Interpreter
    }
}
