using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day03;

public class Solution : PuzzleBase<Dictionary<(int Column, int Number), int>>
{
    public Solution(string inputFile) : base(inputFile)
    {
    }

    public override object SolvePart1()
    {
        var gamma = Enumerable.Range(0, Input.Keys.Max(x => x.Column) + 1)
            .Aggregate(string.Empty, (current, column) => current + (Input[(column, 0)] < Input[(column, 1)] ? 1 : 0));
        var epsilon = Enumerable.Range(0, Input.Keys.Max(x => x.Column) + 1)
            .Aggregate(string.Empty, (current, column) => current + (Input[(column, 0)] > Input[(column, 1)] ? 1 : 0));
        return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
    }

    public override object SolvePart2()
    {
        throw new NotImplementedException();
    }

    public override Dictionary<(int Column, int Number), int> ParseInput(string inputFile)
    {
        var lines = DataReader.ReadLinesFromFile(inputFile);
        Dictionary<(int Column, int Number), int> grid = new();
        for (var column = 0; column < lines[0].Length; column++)
        {
            lines.ToList()
                .ForEach(line =>
                {
                    if (grid.ContainsKey((column, line[column] - '0')))
                    {
                        grid[(column, line[column] - '0')] += 1;
                    }
                    else
                    {
                        grid.Add((column, line[column] - '0'), 1);
                    }
                });
        }

        return grid;
    }
}