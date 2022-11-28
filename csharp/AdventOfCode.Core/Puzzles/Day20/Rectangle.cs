using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day20;

public record Rectangle(int MinX, int MinY, int MaxX, int MaxY)
{
    public bool Contains(Point pt) => pt.X >= MinX && pt.X <= MaxX && pt.Y >= MinY && pt.Y <= MaxY;

    public Rectangle Grow()
        => new Rectangle(MinX - 1, MinY - 1, MaxX + 1, MaxY + 1);

    public IEnumerable<Point> Points
        => from y in Enumerable.Range(MinY, MaxY - MinY + 1)
            from x in Enumerable.Range(MinX, MaxX - MinX + 1)
            select new Point(x, y);
}