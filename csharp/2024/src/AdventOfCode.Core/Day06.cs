using AdventOfCode.Core.Utils;

namespace AdventOfCode.Core;

public class Day06 : IDay<int>
{
    public Grid<char> Grid { get; init; }
    
    public (int x, int y) GuardStart { get; init; }
    
    public Day06(string input)
    {
        Grid = Input.ParseCharGrid(input.Split(Environment.NewLine));
        GuardStart = Grid.FindAll('^').FirstOrDefault();
    }

    public int MoveUntil(Grid<char> grid, (int x, int y) start, (int x, int y) direction, string wall)
    {
        var steps = 1;
        var currDirection = direction;
        var row = start.x;
        var col = start.y;
        var visited = new HashSet<(int x, int y)>();

        while (true)
        {
            var nextPos = grid[row + currDirection.x, col + currDirection.y];
            if (grid.IsOnEdge(row + currDirection.x, col + currDirection.y) &&
                grid[row + currDirection.x, col + currDirection.y].ToString() != wall)
            {
                steps++;
                break;
            } else if (grid[row + currDirection.x, col + currDirection.y].ToString() == wall)
            {
                currDirection = new DirectionRotator().RotateClockwise(currDirection);
            } else {
                //grid[row, col] = 'X';
                var posKey = $"{row},{col}";
                if (!visited.Contains((row, col)))
                {
                    visited.Add((row, col));
                    steps++;
                }
                row += currDirection.x;
                col += currDirection.y;
            }
        }

        return steps;
    }

    public int SolvePart1()
    {
        return MoveUntil(Grid, GuardStart, Grid.GetNorth(GuardStart), "#");
    }

    public int SolvePart2()
    {
        throw new NotImplementedException();
    }
}