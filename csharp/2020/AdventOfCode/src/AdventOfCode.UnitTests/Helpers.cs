using System.Reflection;

namespace AdventOfCode.UnitTests;

public static class Helpers
{
    public static string ReadFileContents(string inputFile)
    {
        string? executableLocation = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location);
        return File.ReadAllText(Path.Combine(executableLocation, "Input", inputFile));
    }
}