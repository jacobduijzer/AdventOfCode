namespace AdventOfCode.Core.Utils;

public class DirectionRotator
{
    // Rotates a coordinate clockwise: North -> East, East -> South, South -> West, West -> North.
    public (int x, int y) RotateClockwise((int x, int y) direction)
    {
        return direction switch
        {
            (0, -1) => (1, 0),  // North -> East
            (1, 0) => (0, 1),   // East -> South
            (0, 1) => (-1, 0),  // South -> West
            (-1, 0) => (0, -1), // West -> North
            _ => throw new ArgumentException("Invalid direction input")
        };
    }

    // Optional: Map input to a direction string for debugging.
    public string GetDirectionName((int x, int y) direction)
    {
        return direction switch
        {
            (0, -1) => "North",
            (1, 0) => "East",
            (0, 1) => "South",
            (-1, 0) => "West",
            _ => "Unknown"
        };
    }
}