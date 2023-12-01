namespace AdventOfCode.Core;

public static class DataReader
{
    public static string[] ReadLinesFromFile(string inputFile)
    {
        if (!File.Exists(inputFile))
            throw new FileNotFoundException($"The input file {inputFile} is not found");

        return File.ReadAllLines(inputFile);
    }
    
    public static string ReadTextFromFile(string inputFile)
    {
        if (!File.Exists(inputFile))
            throw new FileNotFoundException($"The input file {inputFile} is not found");

        return File.ReadAllText(inputFile);
    }
}