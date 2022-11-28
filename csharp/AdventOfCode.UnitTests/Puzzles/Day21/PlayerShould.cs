using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AdventOfCode.Core.Puzzles.Day21;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class PlayerShould
{
    [Theory]
    [InlineData(1)]
    public void StartWithCorrectStartPosition(int startPosition)
    {
        Player player = new(startPosition);

        Assert.Equal(startPosition, player.CurrentPosition);
    }

    [Theory]
    [InlineData(4, new[] {1, 2, 3}, 10)]
    [InlineData(4, new[] {1, 2, 3, 7, 8, 9}, 4)]
    [InlineData(4, new[] {1, 2, 3, 7, 8, 9, 13, 14, 15}, 6)]
    public void MoveToCorrectPosition(int startPosition, int[] rolls, int expectedPosition)
    {
        // ARRANGE
        Player player = new(startPosition);

        // ACT
        player = rolls.Aggregate(player, (current, roll) => current.Move(roll));

        // ASSERT
        Assert.Equal(expectedPosition, player.CurrentPosition);
    }
    
    [Theory]
    [MemberData(nameof(Data))]
    public void Score(int startPosition, List<int[]> rolls, int expectedScore)
    {
        // ARRANGE
        Player player = new(startPosition);

        // ACT
        player = rolls.Aggregate(player, (current, roll) => current.Move(roll.Sum()));

        // ASSERT
        Assert.Equal(expectedScore, player.Score);
    }
    
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[]
            {
                4, new List<int[]>
                {
                    new[] {1, 2, 3} 
                },
                10
            },
            new object[]
            {
                4, 
                new List<int[]>
                {
                    new[] {1, 2, 3},
                    new[] {7, 8, 9},
                },
                14
            },
            new object[]
            {
                4, 
                new List<int[]>
                {
                    new[] {1, 2, 3},
                    new[] {7, 8, 9},
                    new[] {13, 14, 15},
                },
                20
            },
            
        };
}