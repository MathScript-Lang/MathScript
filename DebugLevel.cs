using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathScript
{
    internal enum DebugLevel
    {
        None,
        Parser,
        Lexer,
        Interpreter
    }

    internal enum DebugLevelArgument
    {
        parser = DebugLevel.Parser,
        lexer = DebugLevel.Lexer,
        parser_lexer = DebugLevel.Parser | DebugLevel.Lexer,
        all = DebugLevel.Parser | DebugLevel.Lexer | DebugLevel.Interpreter
    }
}
