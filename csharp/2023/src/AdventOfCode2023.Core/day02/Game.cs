using System.Globalization;

namespace AdventOfCode2023.Core.day02;

public class Game(int id, List<Draw> draws)
{
    public int Id => id;

    public List<Draw> Draws => draws;

    public bool IsGamePossible(int red, int green, int blue)
    {
        foreach (var cube in Draws.SelectMany(draw => draw.Cubes))
        {
            switch (cube.Color)
            {
                case "red" when cube.Amount > red:
                case "green" when cube.Amount > green:
                case "blue" when cube.Amount > blue:
                    return false;
            }
        }

        return true;
    }
}

public record Draw(List<Cube> Cubes);

public record Cube(string s)
{
    private string[] Arr => s.Split(" ");
    public string Color => Arr[1];
    public int Amount => int.Parse(Arr[0]);
}

public class GameBuilder
{
    public static Game Build(string game_data)
    {
        var split = game_data.Split(':');
        var id = int.Parse(split.First().Substring(5));

        var draws = split[1]
            .Split(";", StringSplitOptions.TrimEntries)
            .Select(x =>
                x.Split(',', StringSplitOptions.TrimEntries)
                    .Select(y => new Cube(y)))
            .Select(all => new Draw(all.ToList()));

        return new Game(id, draws.ToList());
    }
}