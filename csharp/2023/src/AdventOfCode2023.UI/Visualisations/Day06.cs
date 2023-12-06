using System.Runtime.CompilerServices;
using AdventOfCode2023.Core.day06;
using Spectre.Console;

namespace AdventOfCode2023.UI.Visualisations;

public static class Day06
{
    public static async Task Run()
    {
        var directory = AppContext.BaseDirectory;
        var fileName = "day06.txt";

        var input = File.ReadAllLines(Path.Combine(directory, "input", fileName));

        RaceResults raceResults = new(input);
        foreach (var raceResult in raceResults.Races)
        {
            foreach (var race in raceResult.GetAllOptions())
            {
                // Create a canvas
                var canvas = new Canvas(100, 9);
                DrawWaters(canvas);
                DrawReferenceRace(canvas, raceResult);
                await AnsiConsole.Live(canvas)
                    .AutoClear(false)
                    .Overflow(VerticalOverflow.Ellipsis)
                    .Cropping(VerticalOverflowCropping.Bottom)
                    .StartAsync(async ctx =>
                    {
                        for (var i = 1; i < race.Time; i++)
                        {
                            if (i <= (int)raceResult.Distance / 10)
                                canvas.SetPixel(i, 5, Color.Red);
                            else
                            {
                                RedrawBeginning(canvas, i);
                                canvas.SetPixel(i, 5, Color.Green);
                            }


                            ctx.Refresh();
                            await Task.Delay(25);
                        }
                    });
                Console.WriteLine($"Time: {race.Time}, Distance: {race.Distance}");
            }
            
        }
    }

    private static void DrawWaters(Canvas canvas)
    {
        // Draw some shapes
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

    private static void DrawReferenceRace(Canvas canvas, RaceResult raceResult)
    {
        var width = (int)(raceResult.Distance / 10);

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
}