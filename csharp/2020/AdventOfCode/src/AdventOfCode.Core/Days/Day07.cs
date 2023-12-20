using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AdventOfCode.UnitTests")]

namespace AdventOfCode.Core.Days;

public class Day07
{
    public int SolvePart1(string input)
    {
        var map = ParseInput(input);
        return map.Keys.Count(key => CanHoldShinyBag(map, key));
    }

    private bool CanHoldShinyBag(IReadOnlyDictionary<string, Dictionary<string, int>> map, string currentColor,
        HashSet<string>? holdMap = null) =>
        (holdMap ??= new()).Add(currentColor)
        && (map[currentColor].ContainsKey("shiny gold")
            || map[currentColor]
                .Where(kvp => !holdMap.Contains(kvp.Key))
                .Any(kvp => CanHoldShinyBag(map, kvp.Key, holdMap)));

    // private bool CanHoldShinyBag(
    //     IReadOnlyDictionary<string, Dictionary<string, int>> map, 
    //     string currentColor,
    //     ISet<string> holdMap)
    // {
    //     if (holdMap.Contains(currentColor))
    //         return true;
    //
    //     if (map.TryGetValue(currentColor, out var next))
    //     {
    //         foreach (var item in next)
    //         {
    //             if (item.Key.Equals("shiny gold") || CanHoldShinyBag(map, item.Key, holdMap))
    //             {
    //                 holdMap.Add("shiny gold");
    //                 return true;
    //             }
    //         }
    //     }
    //
    //     return false;
    // }

    internal Dictionary<string, Dictionary<string, int>> ParseInput(string input)
    {
        return input
            .Split(Environment.NewLine)
            .Select(SplitLineOnContains)
            .Select(ColorAndContaining)
            .ToDictionary(
                c => c.Item1,
                c => c.Item2.ToDictionary(
                    r => r.bag,
                    r => r.count));

        // local functions
        string[] SplitLineOnContains(string line) =>
            line[..^1].Split("bags contain", StringSplitOptions.TrimEntries);

        (string color, (int count, string bag)[] contained) ColorAndContaining(string[] r) =>
            r[1] == "no other bags"
                ? (r[0], Array.Empty<(int count, string bag)>())
                : (r[0].Trim(), CountsOfBags(r[1]));

        (int count, string bag)[] CountsOfBags(string contains) => contains
            .Split(',')
            .Select(split => split
                .Replace(".", "")
                .Replace("bags", "")
                .Replace("bag", "")
                .Trim())
            .Select(CountOfBag)
            .ToArray();

        (int count, string bag) CountOfBag(string c) => (int.Parse(c[..1]), c[2..]);
    }
}