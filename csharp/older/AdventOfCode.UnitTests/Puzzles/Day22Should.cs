using AdventOfCode.Core.Puzzles.Day22;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day22Should
{
    [Theory]
    [InlineData("TestInput/day22_1.txt", 4)]
    [InlineData("TestInput/day22_2.txt", 22)]
    public void ParseInputData(string inputFile, int expectedCount)
    {
        Solution day22 = new(inputFile);
        
        Assert.NotNull(day22.Input);
        Assert.Equal(expectedCount, day22.Input.Count);
    }
    
    [Theory]
    [InlineData("TestInput/day22_1.txt", 39)]
    [InlineData("TestInput/day22_2.txt", 590784)]
    [InlineData("Input/day22.txt", 611378)]
    public void SolvePart1(string inputFile, int expectedCount)
    {
        // ARRANGE
        Solution day22 = new(inputFile);
        
        // ACT
        var result = day22.SolvePart1();
        
        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result);
    }
    
    [Theory]
    [InlineData("TestInput/day22_1.txt", 39)]
    [InlineData("TestInput/day22_2.txt", 39769202357779)]
    [InlineData("TestInput/day22_3.txt", 2758514936282235)]
    [InlineData("Input/day22.txt", 1214313344725528)]
    public void SolvePart2(string inputFile, long expectedCount)
    {
        // ARRANGE
        Solution day22 = new(inputFile);
        
        // ACT
        var result = day22.SolvePart2();
        
        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result);
    }
}