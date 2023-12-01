using AdventOfCode.Core.Puzzles.Day14;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day14Should
{
    [Theory]
    [InlineData("TestInput/day14.txt", 1588)]
    [InlineData("Input/day14.txt", 2891)]
    public void GrowPolymer(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day14 = new (inputFile);

        // ACT
        var result = day14.SolvePart1();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData("TestInput/day14.txt", 2108829309)]
    [InlineData("Input/day14.txt", 4404244090)]
    public void GrowPolymerForPart2(string inputFile, long expectedResult)
    {
        // ARRANGE
        Solution day14 = new (inputFile);

        // ACT
        var result = day14.SolvePart2();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
}