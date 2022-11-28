using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day16;

public class Solution : PuzzleBase<Decoder>
{
    public Solution(string inputFile) : base(inputFile)
    {
    }

    public override object SolvePart1() =>
        CountVersions(Input.DecodeTransMission());

    public override object SolvePart2() =>
        HandlePackage(Input.DecodeTransMission());

    public sealed override Decoder ParseInput(string inputFile) => 
        new(File.ReadAllText(inputFile).Trim());

    private int CountVersions(Package package) => package switch
    {
        LiteralValuePackage => package.Version,
        OperatorPackage operatorPackage => operatorPackage.Version + operatorPackage.SubPackages.Sum(CountVersions),
        _ => 0
    };

    private long HandlePackage(Package package) => package switch
    {
        LiteralValuePackage valuePackage => valuePackage.Value,
        OperatorPackage operatorPackage => ExecutePackage(operatorPackage),
        _ => throw new NotSupportedException($"Package of type {package.GetType()} can not be handled")
    };

    private long ExecutePackage(OperatorPackage package) => package.OperationType switch
    {
        OperationType.Sum => package.SubPackages.Sum(HandlePackage),
        OperationType.Product => package.SubPackages.Select(HandlePackage).Aggregate((i, j) => i * j),
        OperationType.Minimum => package.SubPackages.Min(HandlePackage),
        OperationType.Maximum => package.SubPackages.Max(HandlePackage),
        OperationType.GreaterThan => HandlePackage(package.SubPackages[0]) > HandlePackage(package.SubPackages[1]) ? 1 : 0,
        OperationType.LessThan => HandlePackage(package.SubPackages[0]) < HandlePackage(package.SubPackages[1]) ? 1 : 0,
        OperationType.Equal => HandlePackage(package.SubPackages[0]) == HandlePackage(package.SubPackages[1]) ? 1 : 0,
        _ => throw new NotSupportedException($"Package of type {package.GetType()} can not be executed")
    };
}