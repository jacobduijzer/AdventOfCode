using System.Numerics;
using System.Text;
using AdventOfCode.Core.Puzzles.Day17;
using Spectre.Console;

namespace AdventOfCode.ConsoleApplication.Puzzles;

public class Day17
{
    private const int _fastSpeed = 40;
    private const int _slowSpeed = 250;
    
    private readonly Solution _solution;

    public Day17(string inputFile)
    {
        _solution = new Solution(inputFile);
    }

    public async Task Run()
    {
        var hitCount = 0;
        var probeCount = 0;

        var table = new Table()
            .Width(80)
            .Centered()
            .Title("[underline bold]Advent Of Code 2021[/]", new Style(new Color(0, 204, 0), null))
            .AddColumn("", config => config.RightAligned())
            .AddColumn("Day 17 - Trick Shot", config => config.Centered());
        AnsiConsole.Clear();
        AnsiConsole
            .Live(table)
            .Start(ctx =>
            {
                var sleep = _fastSpeed;
                var minXVelocity = (int) Math.Ceiling((Math.Sqrt(1 + _solution.Input.StartPoint.X * 8) - 1) / 2);

                for (var xVelocity = minXVelocity; xVelocity <= _solution.Input.EndPoint.X; xVelocity++)
                for (var yVelocity = (int) _solution.Input.EndPoint.Y;
                     yVelocity <= -_solution.Input.EndPoint.Y;
                     yVelocity++)
                {
                    var firingResult = _solution.FireProbe(new Vector2(xVelocity, yVelocity), out _);
                    probeCount++;
                    if (firingResult.IsHit)
                        hitCount++;
                    table.Caption($"Probes fired: {probeCount} Hits: [bold green]{hitCount}[/] Misses: [red]{probeCount-hitCount}[/]", new Style(Color.White));
                    table.Rows.Clear();
                    foreach (var row in Enumerable.Range((int)Math.Min(_solution.Input.StartPoint.Y, _solution.Input.EndPoint.Y), 50).Reverse())
                    {
                        StringBuilder sb = new();
                        foreach (var column in Enumerable.Range(0, (int)_solution.Input.EndPoint.X))
                        {
                            if ((row == 0 && column == 0) || firingResult.Path.Contains(new Vector2(column, row)))
                            {
                                if (firingResult.IsHit)
                                {
                                    sb.Append($"[green]{Emoji.Known.Rocket}[/]");
                                    sleep = _slowSpeed; 
                                }
                                else
                                {
                                    sb.Append($"[red]{Emoji.Known.Rocket}[/]");
                                    sleep = _fastSpeed;
                                }
                            }
                            else
                            {
                                if (_solution.Input.StartPoint.Y >= row && row >= _solution.Input.EndPoint.Y &&
                                    _solution.Input.StartPoint.X <= column && column <= _solution.Input.EndPoint.X)
                                    sb.Append($"[#00c8ff]{Emoji.Known.OrangeSquare}[/]");
                                else
                                    sb.Append(Emoji.Known.OrangeSquare);
                            }
                        }

                        table.AddRow(row.ToString(), sb.ToString());
                    }

                    ctx.Refresh();
                    Thread.Sleep(sleep);
                }
            });
    }
}