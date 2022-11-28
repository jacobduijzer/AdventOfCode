using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day11;

public class Solution : PuzzleBase<Octopus[,]>
{
    public int MaxRows;
    public int MaxColumns;

    private readonly List<(int X, int Y)> _offsets = new() 
    {
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1), (0, 1),
        (1, -1), (1, 0), (1, 1)
    };

    public Solution(string inputFile) : base(inputFile)
    {
    }

    public override object SolvePart1()
    {
        var numberOfFlashes = 0;

        for (int run = 0; run < 100; run++)
        {
            Queue<(int X, int Y)> queue = new();
            ISet<(int X, int Y)> flashedOctopi = new HashSet<(int X, int Y)>();

            IncrementEnergyLevels(queue, flashedOctopi);
            HandleFlashes(queue, flashedOctopi);
            numberOfFlashes += flashedOctopi.Count;
        }

        return numberOfFlashes;
    }

    public override object SolvePart2()
    {
        var step = 0;

        while(true)
        {
            Queue<(int X, int Y)> queue = new();
            ISet<(int X, int Y)> flashedOctopi = new HashSet<(int X, int Y)>();

            IncrementEnergyLevels(queue, flashedOctopi);
            HandleFlashes(queue, flashedOctopi);

            step++;
            if (flashedOctopi.Count == MaxRows * MaxColumns)
                break;
        }

        return step;
    }

    public void IncrementEnergyLevels(Queue<(int X, int Y)> queue, ISet<(int X, int Y)> flashedOctopi)
    {
        for (var row = 0; row < MaxRows; row++)
        for (var column = 0; column < MaxColumns; column++)
            if (Input[row, column].IncreaseLevel() > 9)
            {
                queue.Enqueue((column, row));
                flashedOctopi.Add((column, row));
            }
    }
    
    public void HandleFlashes(Queue<(int X, int Y)> queue, ISet<(int X, int Y)> flashedOctopi)
    {
        while (queue.Count > 0)
        {
            var flashingOctopus = queue.Dequeue();
            Input[flashingOctopus.Y, flashingOctopus.X].Reset();

            foreach (var neighbour in _offsets
                         .Select(x => (X: x.X + flashingOctopus.X, Y: x.Y + flashingOctopus.Y))
                         .Where(x => GridHelper.IsValidPoint(x.X, x.Y, MaxColumns, MaxRows)))
            {
                if (flashedOctopi.Contains(neighbour)) continue;
                if (Input[neighbour.Y, neighbour.X].IncreaseLevel() <= 9) continue;
                
                queue.Enqueue(neighbour);
                flashedOctopi.Add(neighbour);
            }
        }
    }

    public sealed override Octopus[,] ParseInput(string inputFile)
    {
        var lines = DataReader.ReadLinesFromFile(inputFile);
        MaxRows = lines.Length;
        MaxColumns = lines[0].Length;
        var grid = new Octopus[MaxRows, MaxColumns];
        for (var row = 0; row < MaxRows; row++)
        for (var column = 0; column < MaxColumns; column++)
            grid[row, column] = new Octopus(int.Parse(lines[row][column].ToString()));

        return grid;
    }
}