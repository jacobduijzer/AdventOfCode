using AdventOfCode.Core.Puzzles.Day21;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class DiceShould
{
    [Fact]
    public void StartWith123()
    {
        // ARRANGE
        var dice = new Dice();
        
        // ACT
        var result = dice.Roll();

        // ASSERT
        Assert.Equal(new int[] { 1, 2, 3}, result);
    }
    
    
    [Theory]
    [InlineData(1, new [] {1, 2, 3})]
    [InlineData(2, new [] {4, 5, 6})]
    [InlineData(334, new [] {1000, 1, 2})]
    [InlineData(335, new [] {3, 4, 5})]
    public void ReturnCorrectNumbers(int numberOfRolls, int[] expected)
    {
        // ARRANGE
        var dice = new Dice();
        
        // ACT
        for(int i = 0; i < numberOfRolls - 1; i++)
            dice.Roll();
        var result = dice.Roll();

        // ASSERT
        Assert.Equal(expected, result);
    }
}