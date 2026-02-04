namespace MathScript
{
    internal partial class Program
    {
        private static async Task<int> Main(string[] args)
        {
            return await Cli.rootCommand.Parse(args).InvokeAsync();
        }
    }
}
