using System.Reflection;
using Spdx;

namespace MathScript
{
    internal record FileType(string MimeType, string Extension);
    internal static class MathScriptInfo
    {
        public static string LanguageName { get; } = "MathScript";
        public static string Name => $"{LanguageName} Interpreter & Compiler";
        public static string Version { get; } = Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? "Unknown";
        public static string Author { get; } = "foxy pirate cove / Fnaf";
        public static string LanguageVersion { get; } = "2.0.0-alpha0.1.0";
        public static FileType FileType { get; } = new FileType("text/x.mathscript", "mscr");
        public static SpdxLicense License { get; } = SpdxLicense.GetById(BuildInfo.License) ?? new SpdxLicense("Unknown", "Unkown", false, false, true);
        public static DebugLevel DebugLevel { get; internal set; } = DebugLevel.None;
        public static string VersionText { get; } =
            $"""
            {MathScriptInfo.Name} [green]v{MathScriptInfo.Version}[/] ([yellow]tags/{BuildInfo.GitTag}:{BuildInfo.GitHash} {BuildInfo.BuildDate}[/])
                    
            Made by [red]{MathScriptInfo.Author}[/].

            Implementing [white on red]{MathScriptInfo.LanguageName}[/] syntax [green]v{MathScriptInfo.LanguageVersion}[/].

            Licensed under the [blue]{MathScriptInfo.License.Name}[/] license, © 2024-present [white on red]{MathScriptInfo.LanguageName}[/].
            """;
        public static List<string> SplashTexts { get; } = [
            "Also try shell!",
            "has complex numbers!",
            "a + bi",
            "No, Jerry, 5+3/10 isn't 0.8, that's 5.3",
            "6? 9? 7.5 ± 1.5 !",
            "not C!",
            "do not ask me about calculator.py's splash screens pls",
            "Math + Script = MathScript.",
            "portals must bend gravity.",
            "Did I implement splash texts thinking I had a lot of splash text ideas but in fact forgot them and thus just put a bunch of random ones... maybe."
        ];
        public static string ModulesBaseDirectory { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "modules");
    }
}
