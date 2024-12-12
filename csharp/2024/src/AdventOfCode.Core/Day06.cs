using AdventOfCode.Core.Utils;

namespace AdventOfCode.Core;

public class Day06 : IDay<int>
{
    public (int col, int row) GuardPosition { get; init; }

    private Grid<char> _grid;
    
    public Day06(string input)
    {
        _grid = Input.ParseCharGrid(input.Split(Environment.NewLine));
        GuardPosition = _grid.FindAll('^').FirstOrDefault();
    }

    private int MoveUntil(Grid<char> grid, (int col, int row) start, (int col, int row) direction, string wall)
    {
        var steps = 1;
        var currDirection = direction;
        var row = start.row;
        var col = start.col;
        var visited = new HashSet<(int col, int row)>();

        while (true)
        {
            
            if (grid.IsOnEdge( col + currDirection.col, row + currDirection.row) &&
                grid[col + currDirection.col, row + currDirection.row].ToString() != wall)
            {
                steps++;
                break;
            } 
            
            if (grid[col + currDirection.col, row + currDirection.row].ToString() == wall)
            {
                currDirection = new DirectionRotator().RotateClockwise(currDirection);
            } 
            else 
            {
                if (visited.Add((row, col)))
                    steps++;
                
                row += currDirection.row;
                col += currDirection.col;
            }
        }

        return steps;
    }

    public int SolvePart1()
    {
        return MoveUntil(_grid, GuardPosition, DirectionRotator.GetDirection(DirectionRotator.Directions.North), "#");
    }

    public int SolvePart2()
    {
        throw new NotImplementedException();
    }
}