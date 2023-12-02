namespace AdventOfCode2023.Core.day02;

public class GameHistory(string[] gameHistory)
{
    public List<Game> Games { get; private set; } = gameHistory
        .Select<string, Game>(GameBuilder.Build)
        .ToList();

    public int SumOfIdsOfImpossibleGames(int red, int green, int blue)
    {
        return Games.Where(x => x.IsGamePossible(red, green, blue))
            .Select(y => y.Id)
            .Sum();
    }

    public int SumOfPower() =>
        Games.Sum(x => x.SumOfPower());
}