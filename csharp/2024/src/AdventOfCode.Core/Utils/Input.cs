using System.Collections.Immutable;

namespace AdventOfCode.Core.Utils;

public static partial class Input
{
    public const string DEFAULT_INPUT_FILENAME = "input.txt";
    public const string DEFAULT_SAMPLE_INPUT_FILENAME = "sample.txt";
		
    public static ImmutableList<char> ReadCharList(string fileName = DEFAULT_INPUT_FILENAME) => [.. File.ReadAllText(fileName)];

    public static ImmutableList<string> ReadStringList(string fileName = DEFAULT_INPUT_FILENAME) => [.. File.ReadAllLines(fileName)];

    public static ImmutableList<int> ReadIntList(string fileName = DEFAULT_INPUT_FILENAME) => [.. ReadStringList(fileName).Select(int.Parse)];

    public static ImmutableList<ImmutableList<int>> ReadIntListList(string fileName = DEFAULT_INPUT_FILENAME) => [.. ReadStringList(fileName).Select(l => ParseIntList(l))];

    public static ImmutableList<string> ParseStringList(string text, string delimiter = " ") => [.. text.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)];

    public static ImmutableList<int> ParseIntList(string text, string delimiter = " ") => [.. ParseStringList(text, delimiter).Select(int.Parse)];
		
    public static ImmutableList<long> ParseLongList(string text, string delimiter = " ") => [.. ParseStringList(text, delimiter).Select(long.Parse)];

    public static ImmutableList<(char c, int i)> ReadCharIntList(string fileName = DEFAULT_INPUT_FILENAME) =>
        ReadTokenized(fileName, tokens => (tokens[0][0], int.Parse(tokens[1])));
		
    public static IEnumerable<(string s, int i)> ReadStringIntList(string fileName = DEFAULT_INPUT_FILENAME) =>
        ReadTokenized(fileName, tokens => (tokens[0], int.Parse(tokens[1])));

    private static ImmutableList<T> ReadTokenized<T>(string fileName, Func<string[], T> tokenParser) => 
        ReadStringList(fileName).Select(l => tokenParser(l.Split(' '))).ToImmutableList();
}