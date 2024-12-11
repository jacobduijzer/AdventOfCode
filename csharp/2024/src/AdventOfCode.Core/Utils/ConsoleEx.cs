namespace AdventOfCode.Core.Utils;

public static class ConsoleEx
{
    public static void Info(string msg) => WriteLine(msg, ConsoleColor.Cyan);
    public static void Success(string msg) => WriteLine(msg, ConsoleColor.Green);
    public static void Error(string msg) => WriteLine(msg, ConsoleColor.Red);
		
    public static void WriteLine(string msg, ConsoleColor color, ConsoleColor? background = null) =>
        WriteToConsole(msg, color, background, true);
		
    public static void Write(string msg, ConsoleColor color, ConsoleColor? background = null) =>
        WriteToConsole(msg, color, background, false);
		
    private static void WriteToConsole(string msg, ConsoleColor color, ConsoleColor? background, bool lineBreak)
    {
        var saveColor = Console.ForegroundColor;
        var saveBackground = Console.BackgroundColor;
        Console.ForegroundColor = color;
        if (background.HasValue)
        {
            Console.BackgroundColor = background.Value;
        }
        Console.Write(msg);
        if (lineBreak)
        {
            Console.WriteLine();
        }
        Console.ForegroundColor = saveColor;
        Console.BackgroundColor = saveBackground;
    }
}