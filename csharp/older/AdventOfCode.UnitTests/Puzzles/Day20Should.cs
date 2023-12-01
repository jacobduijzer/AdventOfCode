using AdventOfCode.Core.Puzzles.Day20;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day20Should
{
    [Theory]
    [InlineData("TestInput/day20.txt", 35)]
    [InlineData("Input/day20.txt", 5400)]
    public void SolvePart1(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day20 = new (inputFile);

        // ACT
        var result = day20.SolvePart1();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData("TestInput/day20.txt", 3351)]
    [InlineData("Input/day20.txt", 18989)]
    public void SolvePart2(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day20 = new (inputFile);

        // ACT
        var result = day20.SolvePart2();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
}