using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathScript.Visitors
{
    public class InterpreterVisitor : MathScriptBaseVisitor<object?>
    {
        public override object? Visit(IParseTree tree)
        {
            object? retVal = base.Visit(tree);

            if (MathScriptInfo.DebugLevel.HasFlag(DebugLevel.Interpreter))
            {
                AnsiConsole.MarkupLine($"[blue]{tree.GetText()} -> {retVal}[/]");
            }

            return retVal;
        }
    }
}
