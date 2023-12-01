using System.Collections.Immutable;
using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day12;

public class Solution : PuzzleBase
{
    private readonly Dictionary<Cave, IEnumerable<Cave>> _map = new();

    public Solution(string inputFile)
    {
        var input = DataReader.ReadLinesFromFile(inputFile).ToList();
        _map = input
            .SelectMany(line => line.Split("-"))
            .Distinct()
            .Select(letter =>
            {
                var startCave = new Cave(letter);
                var endCaves = input
                    .Where(line => line.Split('-')[0] == letter || line.Split('-')[1] == letter)
                    .SelectMany(line => line.Split('-'))
                    .Where(split => split != letter)
                    .Select(n => new Cave(n));
                return (startCave, endCaves);
            })
            .ToDictionary(item => item.startCave, item => item.endCaves);
    }

    public override object SolvePart1() =>
        FindPaths(_map.Single(x => x.Key.Name.Equals("start")).Key, ImmutableHashSet<Cave>.Empty, false);

    public override object SolvePart2() =>
        FindPaths(_map.Single(x => x.Key.Name.Equals("start")).Key, ImmutableHashSet<Cave>.Empty, true);

    private int FindPaths(Cave currentCave, ImmutableHashSet<Cave> visitedCaves, bool canVisitMoreThanOnce)
    {
        switch (currentCave.Name)
        {
            case "end":
                return 1;
            case "start" when visitedCaves.Contains(currentCave):
                return 0;
        }

        if (visitedCaves.Contains(currentCave) && !canVisitMoreThanOnce)
            return 0;

        var newVisitedCaves = visitedCaves;
        if (currentCave.IsSmallCave)
            newVisitedCaves = visitedCaves.Add(currentCave);
        
        return _map[currentCave].Sum(neighbour => FindPaths(neighbour, newVisitedCaves, canVisitMoreThanOnce && !newVisitedCaves.Contains(currentCave)));
    }
}