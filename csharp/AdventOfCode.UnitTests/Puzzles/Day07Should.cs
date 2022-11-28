using AdventOfCode.Core.Puzzles.Day07;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day07Should
{
    [Theory]
    [InlineData("TestInput/day07.txt", 37)]
    [InlineData("Input/day07.txt", 356990)]
    public void SolvePart1(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day7 = new (inputFile);

        // ACT
        var result = day7.SolvePart1();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData("TestInput/day07.txt", 168)]
    [InlineData("Input/day07.txt", 101267361)]
    public void SolvePart2(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day7 = new (inputFile);

        // ACT
        var result = day7.SolvePart2();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
}