using System.Collections.Immutable;
using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day20;

public class Solution : PuzzleBase<(ImmutableArray<bool> Algorithm, Image Image)>
{
    public Solution(string inputFile) : base(inputFile) =>
        ParseInput(inputFile);
    
    public override object SolvePart1() => 
        Enhance(Input.Image, Input.Algorithm, 2).PixelCount;

    public override object SolvePart2() => 
        Enhance(Input.Image, Input.Algorithm, 50).PixelCount;
    
    private Image Enhance(Image image, ImmutableArray<bool> algo, int count)
    {
        var result = image;
        for (var i = 0; i < count; ++i)
            result = EnhanceStep(result, algo);

        return result;
    }

    private Image EnhanceStep(Image image, ImmutableArray<bool> algorithm)
    {
        var bounds = image.Bounds.Grow();

        var result = bounds.Points
            .Where(pt => algorithm[image.GetEnhanceInput(pt)])
            .ToImmutableHashSet();

        var infinite = image.InfiniteValue
            ? algorithm.Last()
            : algorithm.First();

        return new Image(bounds, result, infinite);
    }

    public sealed override (ImmutableArray<bool> Algorithm, Image Image) ParseInput(string inputFile)
    {
        var input = File.ReadAllLines(inputFile);
        var algorithm = input.First().Select(ch => ch == '#').ToImmutableArray();
        var image = ImmutableHashSet.CreateBuilder<Point>();
        foreach (var (line, y) in input.Skip(2).Select((a, i) => (a, i)))
        {
            image.UnionWith(line.Select((ch, i) => (x: i, value: ch == '#'))
                .Where(p => p.value)
                .Select(p => new Point(p.x, y)));
        }

        var bounds = new Rectangle(
            image.Min(p => p.X),
            image.Min(p => p.Y),
            image.Max(p => p.X),
            image.Max(p => p.Y));

        return (algorithm, new Image(bounds, image.ToImmutable()));
    }
}