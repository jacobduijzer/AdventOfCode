using AdventOfCode2023.Core.day06;
using Spectre.Console;

namespace AdventOfCode2023.UI.Visualisations;

public static class Day06
{
    public static async Task Run()
    {
        try
        {
            var directory = AppContext.BaseDirectory;
            var fileName = "day06.txt";

            var input = File.ReadAllLines(Path.Combine(directory, "input", fileName));

            RaceResults raceResults = new(input);
            
            // List<(int, int)> endResult = new List<(int, int)>();
            Dictionary<int, int> score = new Dictionary<int, int>();
            var endResult = new Dictionary<int, int>();

            for(var gameNr = 0; gameNr < raceResults.Races.Count; gameNr++)
            {
                var raceResult = raceResults.Races[gameNr];
                
                var maxDistance = raceResult.GetWinningOptions().Max(x => x.Distance);
                foreach (var race in raceResult.GetAllOptions())
                {
                    if (race.Distance > raceResult.Distance)
                    {
                        if (!score.TryAdd(gameNr, 1))
                            score[gameNr]++;
                    }

                    Console.Clear();

                    var canvas = new Canvas(100, 9);

                    AnsiConsole.WriteLine();
                    AnsiConsole
                        .Write(new Rule($"[yellow]Reference race: {raceResult.Distance} ({raceResult.Time}ms)[/]")
                        .LeftJustified()
                        .RuleStyle("grey"));
                    AnsiConsole.WriteLine();
                    for (var i = 0; i < raceResults.Races.Count; i++)
                    {
                        if(score.TryGetValue(i, out var currentScore))
                            AnsiConsole.WriteLine($"Game {i+1}/{raceResults.Races.Count}, winning: {currentScore}");
                        else 
                            AnsiConsole.WriteLine($"Game {i+1}/{raceResults.Races.Count}, winning: 0");
                    }
                    AnsiConsole.WriteLine($"Total score: {score.Values.ToList().Aggregate(1, (acc, val) => acc * val)}");


                    AnsiConsole.WriteLine();
                    AnsiConsole.Write(new Rule().RuleStyle("grey"));
                    AnsiConsole.WriteLine();
                    AnsiConsole.Write($"Holding button for {race.Time} ms, racing {race.Distance} mm");
                    AnsiConsole.WriteLine();
                    AnsiConsole.WriteLine();
                    await Task.Delay(TimeSpan.FromMilliseconds(race.Time));

                    DrawWaters(canvas);
                    DrawReferenceRace(canvas, raceResult, maxDistance);
                    await AnsiConsole.Live(canvas)
                        .AutoClear(false)
                        .Overflow(VerticalOverflow.Ellipsis)
                        .Cropping(VerticalOverflowCropping.Bottom)
                        .StartAsync(async ctx =>
                        {
                            var width = Map((int)race.Distance, 0, (int)maxDistance, 0, 98);
                            for (var i = 1; i < width; i++)
                            {
                                if (i < Map((int)raceResult.Distance, 0, (int)maxDistance, 0, 98))
                                    canvas.SetPixel(i, 5, Color.Red);
                                else
                                {
                                    RedrawBeginning(canvas, i);
                                    canvas.SetPixel(i, 5, Color.Green);
                                }

                                ctx.Refresh();
                                await Task.Delay(10);
                            }
                        });
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static void DrawWaters(Canvas canvas)
    {
        for (var i = 0; i < canvas.Width; i++)
        {
            canvas.SetPixel(i, 0, Color.White);
            canvas.SetPixel(i, canvas.Height - 1, Color.White);
        }

        for (var i = 0; i < canvas.Height - 1; i++)
        {
            canvas.SetPixel(0, i, Color.White);
            canvas.SetPixel(canvas.Width - 1, i, Color.White);
        }

        for (var i = 1; i < canvas.Width - 1; i++)
        {
            for (var j = 1; j < canvas.Height - 1; j++)
            {
                canvas.SetPixel(i, j, Color.Blue);
            }
        }
    }

    private static void DrawReferenceRace(Canvas canvas, RaceResult raceResult, long maxDistance)
    {
        var width = Map((int)raceResult.Distance, 0, (int)maxDistance, 0, 98);

        for (var i = 1; i < width; i++)
        {
            canvas.SetPixel(i, 3, Color.Orange1);
        }
    }

    private static void RedrawBeginning(Canvas canvas, long lastRedPosition)
    {
        for (var i = 1; i < lastRedPosition; i++)
        {
            canvas.SetPixel(i, 5, Color.Green);
        }
    }

    private static int Map(int value, int fromLow, int fromHigh, int toLow, int toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
}