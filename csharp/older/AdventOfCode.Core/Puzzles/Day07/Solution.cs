using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day07;

public class Solution : PuzzleBase<int[]>
{
    public Solution(string inputFile) : base(inputFile)
    {
    }
    
    public override object SolvePart1() =>
        Enumerable
            .Range(Input.Min(), Input.Max())
            .Select(number => Input.Sum(item => Math.Abs(number - item)))
            .Prepend(int.MaxValue)
            .Min();

    public override object SolvePart2() => 
        Enumerable
            .Range(Input.Min(), Input.Max())
            .Select(number => Input.Select(item => Math.Abs(number - item))
                .Select(distance => distance * (distance + 1) / 2)
                .Sum())
            .Prepend(int.MaxValue)
            .Min();

    public override int[] ParseInput(string inputFile) =>
        DataReader.ReadTextFromFile(inputFile)
            .Split(',')
            .Select(int.Parse).ToArray(); 
}