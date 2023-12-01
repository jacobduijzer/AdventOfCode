namespace AdventOfCode.Core.Puzzles.Day16;

public record Package(int Version, OperationType OperationType);

public record LiteralValuePackage(int Version, OperationType OperationType, long Value) : Package(Version, OperationType);

public record OperatorPackage(int Version, OperationType OperationType, List<Package> SubPackages) : Package(Version, OperationType);

public enum OperationType
{
    Sum,
    Product,
    Minimum,
    Maximum,
    Literal,
    GreaterThan,
    LessThan,
    Equal
}