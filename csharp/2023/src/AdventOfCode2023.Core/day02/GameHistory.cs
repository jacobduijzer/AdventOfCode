namespace AdventOfCode2023.Core.day02;

public class GameHistory
{
    public List<Game> Games { get; private set; }
    public GameHistory(string[] gameHistory)
    {
        Games = gameHistory
            .Select(GameBuilder.Build)
            .ToList();
    }

    public int SumOfIdsOfImpossibleGames(int red, int green, int blue)
    {
        return Games.Where(x => x.IsGamePossible(red, green, blue))
            .Select(y => y.Id)
            .Sum();
    }
    
    //public int SumOfIds(int red, int green, int blue)
    //{
    //    List<int> possible = new();

    //    Games.Select(x => x.Draws)
    //        .Select(y => y.Sum(z => z.Cubes.))
    //    
    //    foreach (Game game in Games)
    //    {
    //        bool isPossible = true;

    //        foreach (Draw round in game.Draws)
    //        {
    //            foreach (var test in round.Cubes)
    //            {
    //                
    //            }
    //            // if (round.Cubes.First(x => x.Color.Equals("red")) > red
    //            //     || round.Blue > blue
    //            //     || round.Green > green)
    //            // {
    //            //     isPossible = false;
    //            //     break;
    //            // }
    //        }

    //        if (!isPossible)
    //        {
    //            continue;
    //        }

    //        possible.Add(game.Id);
    //    }

    //    return possible.Sum();
    //}
}