using AdventOfCode.Core.Utils;

namespace AdventOfCode.Core;

public class Day06 : IDay<int>
{
    public (int col, int row) GuardStartPosition { get; init; }

    public record Step(
        (int col, int row) CurrentGuardPosition,
        DirectionRotator.Directions CurrentDirection,
        bool IsDone);

    private readonly Grid<char> _grid;
    private readonly HashSet<(int col, int row)> _visited;

    public Day06(string input)
    {
        _grid = Input.ParseCharGrid(input.Split(Environment.NewLine));
        GuardStartPosition = _grid.FindAll('^').FirstOrDefault();
        _visited = new HashSet<(int col, int row)>();
    }
    
    public Step NextStep(Step step)
    {
        var currentGuardPosition = step.CurrentGuardPosition;
        var currentDirection = DirectionRotator.GetDirection(step.CurrentDirection);
        var newCol = currentGuardPosition.col + currentDirection.col;
        var newRow = currentGuardPosition.row + currentDirection.row;

        if (_grid.IsOnEdge(newCol, newRow) && _grid[newCol, newRow].ToString() != "#")
        {
            return step with { IsDone = true }; 
        }

        if (_grid[newCol, newRow].ToString() == "#")
        {
            currentDirection = new DirectionRotator().RotateClockwise(currentDirection);
        }
        else
        {
            if (_visited.Add((currentGuardPosition.col, currentGuardPosition.row)))
            {
                currentGuardPosition= (currentGuardPosition.col + currentDirection.col, currentGuardPosition.row + currentDirection.row);
            }
        }

        return new Step(currentGuardPosition, DirectionRotator.GetDirectionName(currentDirection), false);
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
        var steps = 0;
        Step step = new Step(GuardStartPosition, DirectionRotator.Directions.North, false);
        while (true)
        {
            step = NextStep(step);
            if (step.IsDone)
                break;
            steps++;
        }
        return steps;
    }

    public int SolvePart2()
    {
        throw new NotImplementedException();
    }
}