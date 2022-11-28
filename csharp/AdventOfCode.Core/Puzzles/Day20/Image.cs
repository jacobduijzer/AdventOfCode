using System.Collections.Immutable;
using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day20;

public record Image(Rectangle Bounds, ImmutableHashSet<Point> Pixels, bool InfiniteValue = false)
{
    public bool this[Point pt]
        => Bounds.Contains(pt) ? Pixels.Contains(pt) : InfiniteValue;

    public int GetEnhanceInput(Point pt)
    {
        var values = from yi in Enumerable.Range(pt.Y - 1, 3)
            from xi in Enumerable.Range(pt.X - 1, 3)
            select this[new Point(xi, yi)] ? 1 : 0;

        return values.Aggregate(0, (p, n) => (p << 1) | n);
    }

    public int PixelCount
        => Pixels.Count(Bounds.Contains);
}