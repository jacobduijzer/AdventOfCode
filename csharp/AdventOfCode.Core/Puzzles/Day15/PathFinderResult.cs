using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day15;

public record PathFinderResult(
    Dictionary<Point, int> StartGrid,
    Dictionary<Point, int> CalculatedGrid,
    List<Point> Path,
    int LowestCost);