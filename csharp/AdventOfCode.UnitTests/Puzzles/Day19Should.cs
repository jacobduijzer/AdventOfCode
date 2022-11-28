using System.Linq;
using AdventOfCode.Core.Puzzles.Day19;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day19Should
{
    [Theory]
    [InlineData("TestInput/day19.txt", 5)]
    [InlineData("Input/day19.txt", 28)]
    public void ParseInputData(string inputFile, int scannerCount)
    {
        // ARRANGE
        Solution day19 = new(inputFile); 

        // ASSERT
        Assert.NotNull(day19.Input);
        Assert.Equal(scannerCount, day19.Input.Count());
    }

    [Theory]
    [InlineData("TestInput/day19.txt", 79, 3621)]
    [InlineData("Input/day19.txt", 323, 10685)]
    public void SolvePart1And2(string inputFile, int expectedCount, long expectedDistance)
    {
        // ARRANGE
        Solution day19 = new(inputFile);

        // ACT
        var result = (Result)day19.SolvePart1();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.VisibleBeacons);
        Assert.Equal(expectedDistance, result.MaxDiscance);
        
    }
}