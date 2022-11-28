namespace AdventOfCode.Core.Puzzles.Day13;

public interface IFoldingHandler
{
    (int X, int Y) Fold((int X, int Y) coordinate, Fold fold);
}

public static class FoldingHandlerFactory
{
    public static IFoldingHandler CreateFoldingHandler(string axis) => axis switch
    {
        "x" => new FoldX(),
        "y" => new FoldY(),
        _ => throw new Exception("Unknown axis, cannot fold")
    };
}

public class FoldX : IFoldingHandler
{
    public (int X, int Y) Fold((int X, int Y) coordinate, Fold fold)
    {
        if (coordinate.X < fold.Position)
            return coordinate;

        var diff = coordinate.X - fold.Position;
        return (coordinate.X - 2 * diff, coordinate.Y);
    }
}

public class FoldY : IFoldingHandler
{
    public (int X, int Y) Fold((int X, int Y) coordinate, Fold fold)
    {
        if (coordinate.Y < fold.Position)
            return coordinate;
        
        var diff = coordinate.Y - fold.Position;
        return (coordinate.X, coordinate.Y - 2 * diff);
    }
}