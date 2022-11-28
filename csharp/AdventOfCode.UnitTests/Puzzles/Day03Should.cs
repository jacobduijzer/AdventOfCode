using AdventOfCode.Core.Puzzles.Day03;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day03Should
{
    [Theory]
    [InlineData("TestInput/day03.txt", 198)]
    [InlineData("Input/day03.txt", 852500)]
    public void SolvePart1(string filePath, int expectedResult)
    {
        // ARRANGE
        Solution day03 = new (filePath);
        
        // ACT
        var result = day03.SolvePart1();
         

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
    
    // [Theory]
    // [InlineData("TestInput/day03.txt", 13, 1924)]
    // [InlineData("Input/day03.txt", 47, 13912)]
    // public void SolvePart2(string filePath, int lastNumber, int score)
    // {
    //     // ARRANGE
    //     Solution day04 = new (filePath);
    //         
    //     // ACT
    //     GameService result = (GameService)day04.SolvePart2();
    //     
    //     // ASSERT
    //     Assert.NotNull(result);
    //     Assert.Equal(lastNumber, result.Winners.Last().LastNumber);
    //     Assert.Equal(score, result.Winners.Last().GetScore());
    // } 
}