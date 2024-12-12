namespace AdventOfCode.Core.Utils;

public class DirectionRotator
{
    public enum Directions
    {
       North,
       East,
       South,
       West
    } 
    
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
    public Directions GetDirectionName((int x, int y) direction) =>
        direction switch
        {
            (0, -1) => Directions.North,
            (1, 0) => Directions.East,
            (0, 1) => Directions.South,
            (-1, 0) => Directions.West,
            _ => throw new ArgumentException("Invalid direction input")
        };
    
    public static (int x, int y) GetDirection(Directions direction) => direction switch
    {
        Directions.North => (0, -1),
        Directions.East => (1, 0),
        Directions.South => (0, 1),
        Directions.West => (-1, 0),
        _ => throw new ArgumentException("Invalid direction name")
    };
}