using AdventOfCode2023.Core.day02;

namespace AdventOfCode2023.UnitTests.day02;

public class GameHistoryTests
{
    [Fact]
    public void ConstructFullGameHistory()
    {
        var gameHistoryInput = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

        GameHistory gameHistory = new(gameHistoryInput.Split(Environment.NewLine));

        Assert.Equal(5, gameHistory.Games.Count);
    }

    [Fact]
    public void SumIdsOfImpossibleGames()
    {
        var gameHistoryInput = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

        GameHistory gameHistory = new(gameHistoryInput.Split(Environment.NewLine));

        Assert.Equal(8, gameHistory.SumOfIdsOfImpossibleGames(12, 13, 14));
    }

    [Fact]
    public void RunPart1()
    {
        var directory = AppContext.BaseDirectory;
        var fileName = "day02.txt";
        var input = File.ReadAllLines(Path.Combine(directory, "input", fileName));

        GameHistory gameHistory = new(input);

        Assert.Equal(2679, gameHistory.SumOfIdsOfImpossibleGames(12, 13, 14));
    }

    [Fact]
    public void RunPart2()
    {
        var directory = AppContext.BaseDirectory;
        var fileName = "day02.txt";
        var input = File.ReadAllLines(Path.Combine(directory, "input", fileName));

        GameHistory gameHistory = new(input);

        Assert.Equal(77607, gameHistory.SumOfPower());
    }
}