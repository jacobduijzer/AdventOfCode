using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day09;

public class Solution : PuzzleBase<int[,]>
{
    private readonly List<(int X, int Y)> _offsets = new List<(int X, int Y)> {(-1, 0), (0, 1), (0, -1), (1, 0)};
    private int _maxRows;
    private int _maxColumns;

    public Solution(string inputFile) : base(inputFile)
    {
    }

    public override object SolvePart1()
    {
        var numbers = FindLowPoints();
        return numbers.Select(x => x.Value + 1).Sum();
    }

    public override object SolvePart2()
    {
        var lowPoints = FindLowPoints();
        return lowPoints
            .Select(lowPoint => GetBasinSize((lowPoint.Key.X, lowPoint.Key.Y)))
            .OrderByDescending(x => x).Take(3).Aggregate((x, y) => x * y);
    }

    private int GetBasinSize((int X, int Y) coord)
    {
        var queue = new List<(int X, int Y)> {coord};
        var visited = new List<(int X, int Y)>();
        while (queue.Count > 0)
        {
            var current = queue.First();
            visited.Add(current);
            queue.RemoveAt(0);
            var neighbours = _offsets
                .Select(x => (X: x.X + current.X, Y: x.Y + current.Y))
                .Where(x => GridHelper.IsValidPoint(x.X, x.Y, _maxColumns, _maxRows) && Input[x.Y, x.X] != 9 && !visited.Contains(x) && !queue.Contains(x)).ToList();
            queue = queue.Concat(neighbours).ToList();
        }

        return visited.Count;
    }

    private Dictionary<(int X, int Y), int> FindLowPoints()
    {
        Dictionary<(int X, int Y), int> numbers = new();
    
        for (var row = 0; row < _maxRows; row++)
        for (var column = 0; column < _maxColumns; column++)
        {
            var neighbours = _offsets
                .Select(point => (X: point.X + column, Y: point.Y + row))
                .Where(newPoint => GridHelper.IsValidPoint(newPoint.X, newPoint.Y, _maxColumns, _maxRows))
                .ToList();
            
            if (neighbours.All(point => Input[point.Y, point.X] > Input[row, column]))
                numbers.Add((column, row), Input[row, column]);
        }

        return numbers;
    }

    public override int[,] ParseInput(string inputFile)
    {
        var lines = DataReader.ReadLinesFromFile(inputFile);
        
        _maxRows  = lines.Length;
        _maxColumns = lines[0].Length; 

        var input = new int[_maxRows, _maxColumns];
        for (var row = 0; row < _maxRows; row++)
        for (var column = 0; column < _maxColumns; column++)
            input[row, column] = int.Parse(lines[row][column].ToString());

        return input;
    }
}