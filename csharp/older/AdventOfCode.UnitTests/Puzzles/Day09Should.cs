using AdventOfCode.Core.Puzzles.Day09;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day09Should
{
    [Theory]
    [InlineData("TestInput/day09.txt", 15)]
    [InlineData("Input/day09.txt", 566)]
    public void SolvePart1(string filePath, int expectedResult)
    {
        // ARRANGE
        Solution day9 = new (filePath);

        // ACT
        var result = day9.SolvePart1();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    } 
    
    [Theory]
    [InlineData("TestInput/day09.txt", 1134)]
    [InlineData("Input/day09.txt", 891684)]
    public void SolvePart2(string filePath, int expectedResult)
    {
        // ARRANGE
        Solution day9 = new (filePath);

        // ACT
        var result = day9.SolvePart2();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    } 
}