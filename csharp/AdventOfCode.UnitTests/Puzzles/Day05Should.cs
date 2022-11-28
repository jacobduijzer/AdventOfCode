using AdventOfCode.Core.Puzzles.Day05;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day05Should
{
    [Theory]
    [InlineData("TestInput/day05.txt", 5)]
    [InlineData("Input/day05.txt", 7473)]
    public void SolvePart1(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day5 = new (inputFile);

        // ACT
        var result = day5.SolvePart1();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData("TestInput/day05.txt", 12)]
    [InlineData("Input/day05.txt", 24164)]
    public void SolvePart2(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day5 = new (inputFile);

        // ACT
        var result = day5.SolvePart2();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
}