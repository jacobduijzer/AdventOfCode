using System.Text.RegularExpressions;

namespace AdventOfCode.Core.Puzzles.Day13;

public class Grid
{
    private HashSet<(int X, int Y)> _startGrid;
    
    public readonly List<Fold> Folds;

    private readonly Regex _foldsRegex = new Regex(@"^fold along (x|y)=(\d+)$");

    public Grid(string[] input)
    {
        var indexOfInstructions = Array.IndexOf(input, string.Empty);

        _startGrid = input
            .Take(indexOfInstructions)
            .Select(line =>
            {
                var coord = line.Split(',', StringSplitOptions.TrimEntries);
                return (int.Parse(coord[0]), int.Parse(coord[1]));
            }).ToHashSet();

        Folds = input.Skip(indexOfInstructions + 1).ToList().Select(x =>
        {
            var match = _foldsRegex.Match(x);
            return new Fold(match.Groups[1].Value, int.Parse(match.Groups[2].Value));
        }).ToList();
    }

    public HashSet<(int X, int Y)> HandleFolds(List<Fold> folds) =>
        folds.Aggregate(_startGrid, HandleFold);

    private HashSet<(int X, int Y)> HandleFold(HashSet<(int X, int Y)> startGrid, Fold fold)
    {
        HashSet<(int X, int Y)> newGrid = new();
        foreach (var newCoord in startGrid.Select(coord => FoldingHandlerFactory
                         .CreateFoldingHandler(fold.Axis)
                         .Fold(coord, fold)))
            newGrid.Add(newCoord);

        return newGrid;
    }
}