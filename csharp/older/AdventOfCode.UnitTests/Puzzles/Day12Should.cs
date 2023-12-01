using AdventOfCode.Core.Puzzles.Day12;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day12Should
{
    [Theory]
    [InlineData("TestInput/day12.txt", 10)]
    [InlineData("Input/day12.txt", 3450)]
    public void FindCorrectNumberOfPath(string inputFile, int expectedPaths)
    {
        // ARRANGE
        Solution day12 = new (inputFile);

        // ACT
        var result = day12.SolvePart1();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedPaths, result);
    } 
    
    
    [Theory]
    [InlineData("TestInput/day12.txt", 10)]
    [InlineData("Input/day12.txt", 3450)]
    public void FindCorrectNumberOfPathWhenVisitedOnce(string inputFile, int expectedPaths)
    {
        // ARRANGE
        Solution day12 = new (inputFile);

        // ACT
        var result = day12.SolvePart2();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedPaths, result);
    }
}