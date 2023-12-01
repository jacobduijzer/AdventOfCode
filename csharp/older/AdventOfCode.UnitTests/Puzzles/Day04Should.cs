using System.Linq;
using AdventOfCode.Core.Puzzles.Day04;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day04Should
{
    [Theory]
    [InlineData("TestInput/day04.txt", 24, 4512)]
    [InlineData("Input/day04.txt", 95, 72770)]
    public void SolvePart1(string filePath, int lastNumber, int score)
    {
        // ARRANGE
        Solution day04 = new (filePath);
        
        // ACT
        GameService result = (GameService)day04.SolvePart1();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(lastNumber, result.Winners.First().LastNumber);
        Assert.Equal(score, result.Winners.First().GetScore());
    }
    
    [Theory]
    [InlineData("TestInput/day04.txt", 13, 1924)]
    [InlineData("Input/day04.txt", 47, 13912)]
    public void SolvePart2(string filePath, int lastNumber, int score)
    {
        // ARRANGE
        Solution day04 = new (filePath);
            
        // ACT
        GameService result = (GameService)day04.SolvePart2();
        
        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(lastNumber, result.Winners.Last().LastNumber);
        Assert.Equal(score, result.Winners.Last().GetScore());
    }
}