using System.Diagnostics;
using AdventOfCode2023.Core.day02;
using Spectre.Console;

namespace AdventOfCode2023.UI.Visualisations;

public static class Day02
{
    public static async Task Run()
    {
        var directory = AppContext.BaseDirectory;
        var fileName = "day02.txt";

        var input = File.ReadAllLines(Path.Combine(directory, "input", fileName));
        GameHistory gameHistory = new GameHistory(input);

        await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                // Define tasks
                var task1 = ctx.AddTask("[red]Red[/]");
                var task2 = ctx.AddTask("[green]Green[/]");
                var task3 = ctx.AddTask("[blue]Blue[/]");
                var task4 = ctx.AddTask("Total Games");
                var task5 = ctx.AddTask("Impossible Games");

                var impossibleGames = 0;

                for (var i = 0; i <= gameHistory.Games.Count; i++)
                {
                    if (!gameHistory.Games[i].IsGamePossible(12, 13, 14))
                        impossibleGames++;
                    
                    foreach (var draw in gameHistory.Games[i].Draws)
                    {
                        // Simulate some work
                        await Task.Delay(150);

                        var red = draw.Cubes.Any(x => x.Color.Equals("red"))
                            ? draw.Cubes.FirstOrDefault(x => x.Color.Equals("red")).Amount
                            : 0;
                        var green = draw.Cubes.Any(x => x.Color.Equals("green"))
                            ? draw.Cubes.FirstOrDefault(x => x.Color.Equals("green")).Amount
                            : 0;
                        var blue = draw.Cubes.Any(x => x.Color.Equals("blue"))
                            ? draw.Cubes.FirstOrDefault(x => x.Color.Equals("blue")).Amount
                            : 0;
                        
                        

                        // Increment
                        task1.Value((int)((red / 12.0) * 100));
                        task2.Value((int)((green / 13.0) * 100));
                        task3.Value((blue / 14.0) * 100);
                        task4.Value = (i / (double)gameHistory.Games.Count) * 100;
                        task4.Description = $"Total Games ({i}/{gameHistory.Games.Count})";
                        task5.Value = (impossibleGames / (double)gameHistory.Games.Count) * 100;
                        task5.Description = $"Impossible Games ({impossibleGames}/{gameHistory.Games.Count})";
                    }
                }
            });
    }
}