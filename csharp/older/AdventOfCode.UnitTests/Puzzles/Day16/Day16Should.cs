using AdventOfCode.Core.Puzzles.Day16;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles.Day16;

public class Day16Should
{
    private const string DayFile = "day16.txt";
    [Theory]
    [InlineData($"Input/{DayFile}", 906)]
    public void SolvePart1(string inputFile, int expectedResult)
    {
        // ARRANGE
        Solution day16 = new (inputFile);

        // ACT
        var result = day16.SolvePart1();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
    
    [Theory]
    [InlineData($"Input/{DayFile}", 819324480368)]
    public void SolvePart2(string inputFile, long expectedResult)
    {
        // ARRANGE
        Solution day16 = new (inputFile);

        // ACT
        var result = day16.SolvePart2();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }

    
}