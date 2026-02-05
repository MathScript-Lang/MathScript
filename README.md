<div align="center">

<h1 style="display: flex; justify-content: center; align-items: center; gap: 0.2em;"><img src="https://avatars.githubusercontent.com/u/180905240?s=400&u=c4e0cc63c3e0f6496bf4d7ea02dcfc922f37813a&v=4" width="30" alt="MathScript logo">MathScript</h1>

<p>A Math programming language</p>

</div>

```
Description:
  MathScript Interpreter & Compiler

Usage:
  mathscript [<Program>] [options]

Arguments:
  <Program>  The MathScript program (.mscr file) to compile/execute

Options:
  -o, --Output, --output <Output>    Compile the MathScript program into the `<Output>` executable
  -d, --Debug, --debug <DebugLevel>  Set the debug level (Parser, Lexer, ParserLexer, All)
  -?, -h, --help                     Show help and usage information
  --version                          Show version information
  -v, --version, --Version           Print version information
```

## A Brief History of ~~Time~~ MathScript

### MathScript(.py)

MathScript started as just following a tutorial on how to make a programming language, by CodePulse.
This tutorial was implementing a BASIC-like language in Python with a manual parser-lexer. MathScript
was a mildly-customized implementation of it, mainly following the tutorial. Eventually, I just found
out that... this was shit. Not the type of syntax I wanted, very brittle, almost impossible to
implement anything easily. I had to make too many changes everywhere just to add a single grammar
rule. I had ideas of expanding it, by adding classes, adding packages/modules (I did even actually
make a package manager, MathGet(.py), btw coming back soon).

### MathScript(.cpp)

So, I went back from nothing, from the ground up. I, by the way, changed from Python to C++ as...
well... Python isn't really known for being efficient, nor that powerful. So, I went all in and...
I quickly came to the fact that, I was literally copying over the bad architecture, just adapting the
Python code into C++, but it'd still be brittle, still need too much efforts to change even one
grammar rule, ... The project was quickly abandoned, and from MathScript only deprecated versions
were.

### MathScript(.cs?)

Now, MathScript's in C#. Why C#? Well, in fact, Python sucks, C++ kinda suck (as it's, in fact, C
with extra steps), and C# has a pretty high-level syntax and API, and runs fast enough to not be
a turtle. However, MathScript v2 isn't what v1 was, in v1 (pre-C# MathScript), the syntax was
pretty BASIC (did you get the pun?), in v2 (C#-onward MathScript), the syntax is more conventional
and C-looking, even though it's `"not C!"`. MathScript also got more mathy (it better fits the name,
I guess), from just complex numbers and a pretty shitty implementation of `_`-indexing with sequences
as arrays/lists to Cayley-Dickson hypercomplex algebras, actually good `_`-indexing, still sequences
as arrays/lists, but also sets, set-builder notation, absolute value notation, norm/cardinality
notation. The grammar also just got... actually good... like, that one's almost Kevlar, it's
bulletproof, thanks to the use of ANTLR4.

#### A Brief History of ~~Still not Time~~ MathScript Implementations Naming

You maybe noticed that what's now MathScript.py was then called just MathScript, got the `.py` with the
start of the C++ rewrite project, which was itself just MathScript for the time I was in denial until
finding out that it wasn't gonna make it, when I archived it, renamed it MathScript.cpp and deprecated
it. Then, much later, I finally got the idea to use C#... got a long time forgetting about it with only
about the CLI done, then only recently (as of Thursday the 5th of February 2026) I thought of reading
about what actually the fuck was ANTLR, I already heard the name but went to search what it actually
was, then actually made the grammar (of course through a bit of docs reading and trial and error, and
sudden ideas) and integrated it into the CLI. Currently, as of Thursday the 5th of February 2026, the
interpreter/compiler part of it isn't yet done. Also, you may have noticed that earlier I wrote
"MathScript(.cs?)" for the C# version, and it's because... I already went through Python and C++, I'm
not sure another change won't happen, and it allows to differenciate between the language itself and
the implementation, between the grammar and the interpreter/compiler.

#### Signification of the Splash Texts

You maybe also noticed that MathScript has now splash texts, and the reason is quite simple, it's
because I had them in [`shell`](https://github.com/foxypiratecove37350/shell), liked it a lot, and on
a sudden idea implemented it but quickly forgot all the splash text ideas I had, so I had to find some.

Currently, there are 10 of them, all can be found as `MathScriptInfo.SplashTexts` in `MathScriptInfo.cs`:

- "Also try shell!": Obvious reference to [`shell`](https://github.com/foxypiratecove37350/shell).
- "has complex numbers!": Minecraft-style splash text about complex numbers
- "a + bi": Minecraft-style splash text about complex numbers
- "No, Jerry, 5+3/10 isn't 0.8, that's 5.3": That's actually a meme, or kinda, that's a test half of
  the people fail, just I didn't remember the actual numbers
- "6? 9? 7.5 ± 1.5 !": That's a meme, the one with a 6/9 and two people looking at it, one from up of it
  and the other from bottom of it
- "not C!": Minecraft-style splash text, equivalent of "Not linear!"
- "do not ask me about calculator.py's splash screens pls": Don't ask me pls...
- "Math + Script = MathScript.": Random
- "portals must bend gravity.": Reference to [optozorax](https://www.youtube.com/@optozorax_en)'s [Portals must bend gravity, actually](https://www.youtube.com/watch?v=DydIhwLrbMk)
- "Did I implement splash texts thinking I had a lot of splash text ideas but in fact forgot them and thus just put a bunch of random ones... maybe.": Pretty self-explanatory I'd say...
