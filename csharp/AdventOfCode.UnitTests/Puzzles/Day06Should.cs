using AdventOfCode.Core.Puzzles.Day06;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day06Should
{
    [Theory]
    [InlineData(new int[] {3, 4, 3, 1, 2}, new int[] {2, 3, 2, 0, 1}, 1)]
    [InlineData(new int[] {3, 4, 3, 1, 2}, new int[] {1, 2, 1, 6, 0, 8}, 2)]
    [InlineData(new int[] {3, 4, 3, 1, 2}, new int[] {0,1,0,5,6,7,8}, 3)]
    [InlineData(new int[] {3, 4, 3, 1, 2}, new int[] {6,0,6,4,5,6,7,8,8}, 4)]
    [InlineData(new int[] {3, 4, 3, 1, 2}, new int[] {6,0,6,4,5,6,0,1,1,2,6,0,1,1,1,2,2,3,3,4,6,7,8,8,8,8}, 18)]
    public void CalculateStateAfterOneDay(int[] initialState, int[] expectedState, int numberOfDays)
    {
        // ARRANGE
        Solution day06 = new();
        
        // ACT
        var newState = day06.SolvePart1(initialState, numberOfDays);

        // ASSERT
        Assert.Equal(expectedState, newState);
    }

    [Fact]
    public void CountCorrectNumberOfFist()
    {
        // ARRANGE
        Solution day06 = new();
        
        // ACT
        var newState = day06.SolvePart1(new[] {3, 4, 3, 1, 2}, 80);

        // ASSERT
        Assert.Equal(5934, ((int[]) newState).Length);
    }

    [Fact]
    public void FinalAnswer()
    {
        // ARRANGE
        Solution day06 = new();
        
        // ACT
        var newState = day06.SolvePart1(
            new[]
            {
                3, 5, 3, 5, 1, 3, 1, 1, 5, 5, 1, 1, 1, 2, 2, 2, 3, 1, 1, 5, 1, 1, 5, 5, 3, 2, 2, 5, 4, 4, 1, 5, 1, 4, 4,
                5, 2, 4, 1, 1, 5, 3, 1, 1, 4, 1, 1, 1, 1, 4, 1, 1, 1, 1, 2, 1, 1, 4, 1, 1, 1, 2, 3, 5, 5, 1, 1, 3, 1, 4,
                1, 3, 4, 5, 1, 4, 5, 1, 1, 4, 1, 3, 1, 5, 1, 2, 1, 1, 2, 1, 4, 1, 1, 1, 4, 4, 3, 1, 1, 1, 1, 1, 4, 1, 4,
                5, 2, 1, 4, 5, 4, 1, 1, 1, 2, 2, 1, 4, 4, 1, 1, 4, 1, 1, 1, 2, 3, 4, 2, 4, 1, 1, 5, 4, 2, 1, 5, 1, 1, 5,
                1, 2, 1, 1, 1, 5, 5, 2, 1, 4, 3, 1, 2, 2, 4, 1, 2, 1, 1, 5, 1, 3, 2, 4, 3, 1, 4, 3, 1, 2, 1, 1, 1, 1, 1,
                4, 3, 3, 1, 3, 1, 1, 5, 1, 1, 1, 1, 3, 3, 1, 3, 5, 1, 5, 5, 2, 1, 2, 1, 4, 2, 3, 4, 1, 4, 2, 4, 2, 5, 3,
                4, 3, 5, 1, 2, 1, 1, 4, 1, 3, 5, 1, 4, 1, 2, 4, 3, 1, 5, 1, 1, 2, 2, 4, 2, 3, 1, 1, 1, 5, 2, 1, 4, 1, 1,
                1, 4, 1, 3, 3, 2, 4, 1, 4, 2, 5, 1, 5, 2, 1, 4, 1, 3, 1, 2, 5, 5, 4, 1, 2, 3, 3, 2, 2, 1, 3, 3, 1, 4, 4,
                1, 1, 4, 1, 1, 5, 1, 2, 4, 2, 1, 4, 1, 1, 4, 3, 5, 1, 2, 1
            }, 80);
        
        // ASSERT
        Assert.Equal(365862, ((int[]) newState).Length);
    }
    
    [Fact]
    public void FinalAnswerPart2()
    {
        // ARRANGE
        Solution day06 = new ();
        
        // ACT
        var fishcount = day06.SolvePart2(
            new[]
            {
                3, 5, 3, 5, 1, 3, 1, 1, 5, 5, 1, 1, 1, 2, 2, 2, 3, 1, 1, 5, 1, 1, 5, 5, 3, 2, 2, 5, 4, 4, 1, 5, 1, 4, 4,
                5, 2, 4, 1, 1, 5, 3, 1, 1, 4, 1, 1, 1, 1, 4, 1, 1, 1, 1, 2, 1, 1, 4, 1, 1, 1, 2, 3, 5, 5, 1, 1, 3, 1, 4,
                1, 3, 4, 5, 1, 4, 5, 1, 1, 4, 1, 3, 1, 5, 1, 2, 1, 1, 2, 1, 4, 1, 1, 1, 4, 4, 3, 1, 1, 1, 1, 1, 4, 1, 4,
                5, 2, 1, 4, 5, 4, 1, 1, 1, 2, 2, 1, 4, 4, 1, 1, 4, 1, 1, 1, 2, 3, 4, 2, 4, 1, 1, 5, 4, 2, 1, 5, 1, 1, 5,
                1, 2, 1, 1, 1, 5, 5, 2, 1, 4, 3, 1, 2, 2, 4, 1, 2, 1, 1, 5, 1, 3, 2, 4, 3, 1, 4, 3, 1, 2, 1, 1, 1, 1, 1,
                4, 3, 3, 1, 3, 1, 1, 5, 1, 1, 1, 1, 3, 3, 1, 3, 5, 1, 5, 5, 2, 1, 2, 1, 4, 2, 3, 4, 1, 4, 2, 4, 2, 5, 3,
                4, 3, 5, 1, 2, 1, 1, 4, 1, 3, 5, 1, 4, 1, 2, 4, 3, 1, 5, 1, 1, 2, 2, 4, 2, 3, 1, 1, 1, 5, 2, 1, 4, 1, 1,
                1, 4, 1, 3, 3, 2, 4, 1, 4, 2, 5, 1, 5, 2, 1, 4, 1, 3, 1, 2, 5, 5, 4, 1, 2, 3, 3, 2, 2, 1, 3, 3, 1, 4, 4,
                1, 1, 4, 1, 1, 5, 1, 2, 4, 2, 1, 4, 1, 1, 4, 3, 5, 1, 2, 1
            }, 256);
        
        // ASSERT
        Assert.Equal(1653250886439, fishcount);
    }
}