using AdventOfCode2023.Core.day02;

namespace AdventOfCode2023.UnitTests.day02;

public class GameTests
{
    [Fact]
    public void ConstructGame()
    {
        Game game = GameBuilder.Build("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green");
        
        Assert.Equal(1, game.Id);
        Assert.Equal(3, game.Draws.Count);
        Assert.Equal(2, game.Draws.First().Cubes.Count);
        Assert.Equal(3, game.Draws.First().Cubes.FirstOrDefault( x => x.Color.Equals("blue")).Amount);
        Assert.Equal(4, game.Draws.First().Cubes.FirstOrDefault( x => x.Color.Equals("red")).Amount);
    }

    [Theory]
    [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 10, 10, 10, true)]
    [InlineData("Game 1: 3 blue, 12 red; 1 red, 2 green, 6 blue; 2 green", 10, 10, 10, false)]
    [InlineData("Game 1: 3 blue, 10 red; 1 red, 2 green, 6 blue; 2 green", 10, 10, 10, true)]
    public void DecideIfGameIsPossible(string gameInput, int red, int green, int blue, bool isGamePossible)
    {
        Game game = GameBuilder.Build(gameInput);
        
        Assert.Equal(isGamePossible, game.IsGamePossible(red, green, blue));
    }
}