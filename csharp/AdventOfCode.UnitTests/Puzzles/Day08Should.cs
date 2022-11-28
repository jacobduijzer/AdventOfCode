using AdventOfCode.Core.Puzzles.Day08;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day08Should
{
    [Theory]
    [InlineData("TestInput/day08.txt", 26)]
    [InlineData("Input/day08.txt", 521)]
    public void SolvePart1(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day8 = new (inputFile);

        // ACT
        var result = day8.SolvePart1();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData("TestInput/day08.txt", 61229)]
    [InlineData("Input/day08.txt", 1016804)]
    public void SolvePart2(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day8 = new (inputFile);

        // ACT
        var result = day8.SolvePart2();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
}