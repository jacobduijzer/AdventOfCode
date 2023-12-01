using AdventOfCode.Core.Puzzles.Day17;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day17Should
{
    [Theory]
    [InlineData("TestInput/day17.txt", 40)]
    [InlineData("TestInput/day17.txt", 45)]
    [InlineData("Input/day17.txt", 23005)]
    public void SolvePart1(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day17 = new (inputFile);

        // ACT
        var result = day17.SolvePart1();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData("TestInput/day17.txt", 112)]
    [InlineData("Input/day17.txt", 2040)]
    public void SolvePart2(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day17 = new (inputFile);

        // ACT
        var result = day17.SolvePart2();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);

    } 
}