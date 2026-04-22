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
	public class InterpreterVisitor(Parser parser) : MathScriptBaseVisitor<object?>
	{
		public Parser Parser = parser;

		public override object? Visit(IParseTree tree)
		{
			if (MathScriptInfo.DebugLevel.HasFlag(DebugLevel.Interpreter))
			{
				AnsiConsole.Markup($"[blue]{tree.ToStringTree(Parser).EscapeMarkup()} -> [/]");
			}

			try
			{
				object? retVal = base.Visit(tree);

				if (MathScriptInfo.DebugLevel.HasFlag(DebugLevel.Interpreter))
				{
					AnsiConsole.MarkupLine($"[blue]{retVal}[/]");
				}

				return retVal;
			}
			catch (Exception ex)
			{
				if (MathScriptInfo.DebugLevel.HasFlag(DebugLevel.Interpreter))
				{
					AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
				}

				return new RuntimeException(ex);
			}
		}
	}
}
