using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day17;

public class Solution : PuzzleBase<(Vector2 StartPoint, Vector2 EndPoint)>
{
    public Solution(string inputFile) : base(inputFile)
    {
    }

    public override object SolvePart1() =>
        GetMaxHeight(Input.StartPoint, Input.EndPoint);

    public override object SolvePart2() =>
        GetPossibleVelocityCount(Input.StartPoint, Input.EndPoint);

    private int GetMaxHeight(Vector2 startPoint, Vector2 endPoint)
    {
        var minXVelocity = (int)Math.Ceiling((Math.Sqrt(1 + startPoint.X * 8) - 1) / 2);
        var maxHeight = 0;

        for (var yVelocity = 0; yVelocity <= -endPoint.Y; yVelocity++)
        {
            if (FireProbe(new Vector2(minXVelocity, yVelocity), out var max).IsHit)
            {
                maxHeight = Math.Max(maxHeight, max);
            }
        }

        return maxHeight;
    } 
    
    private int GetPossibleVelocityCount(Vector2 startPoint, Vector2 endPoint)
    {
        var hitCount = 0;
        var minXVelocity = (int)Math.Ceiling((Math.Sqrt(1 + startPoint.X * 8) - 1) / 2);

        for (var xVelocity = minXVelocity; xVelocity <= endPoint.X; xVelocity++)
        for (var yVelocity = (int)endPoint.Y; yVelocity <= -endPoint.Y; yVelocity++)
        {
            if (FireProbe(new Vector2(xVelocity, yVelocity),out _).IsHit)
            {
                hitCount++;
            }
        }

        return hitCount;
    }

    public (bool IsHit, List<Vector2> Path) FireProbe(Vector2 speed, out int maxHeight)
    {
        var position = new Vector2(0, 0);
        maxHeight = 0;

        List<Vector2> path = new();

        while (position.X < Input.EndPoint.X && position.Y > Input.EndPoint.Y)
        {
            position += speed;
            speed = speed with
            {
                X = speed.X > 0 ? speed.X - 1 : 0,
                Y = speed.Y - 1,
            };

            path.Add(position);

            maxHeight = (int)Math.Max(maxHeight, position.Y);

            if (position.X >= Input.StartPoint.X && 
                position.Y <= Input.StartPoint.Y && 
                position.X <= Input.EndPoint.X && 
                position.Y >= Input.EndPoint.Y)
            {
                return (true, path);
            }
        }

        return (false, path);
    }


    public sealed override (Vector2 StartPoint, Vector2 EndPoint) ParseInput(string inputFile)
    {
        var coordinates = Regex
            .Split(File.ReadAllText(inputFile), @"target area: x=(\d+)..(\d+), y=(-\d+)..(-\d+)")
            .Where(x => !String.IsNullOrEmpty(x))
            .Select(int.Parse)
            .ToArray();
        
        Vector2 startPoint = new(Math.Min(coordinates[0], coordinates[1]), Math.Max(coordinates[2], coordinates[3]));
        Vector2 endPoint = new(Math.Max(coordinates[0], coordinates[1]), Math.Min(coordinates[2], coordinates[3]));

        return (startPoint, endPoint);
    }
}