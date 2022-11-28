using System.Numerics;
using System.Text;
using AdventOfCode.Core.Puzzles.Day11;
using Spectre.Console;

namespace AdventOfCode.ConsoleApplication.Puzzles;

public class Day11
{
    private readonly Solution _solution;

    public Day11(string inputFile)
    {
        _solution = new Solution(inputFile);
    }

    public async Task RunPart1()
    {
        var numberOfFlashes = 0;
        for (int run = 0; run < 100; run++)
        {
            var table = new Table()
                .Width(40)
                .Centered()
                .Title("Advent Of Code 2021", new Style(new Color(0, 204, 0)))
                .AddColumn("Day 11 - Dumbo Octopus - Part I", config => config.Centered());
            AnsiConsole.Clear();
            AnsiConsole
                .Live(table)
                .Start(ctx =>
                {
                    Queue<(int X, int Y)> queue = new();
                    ISet<(int X, int Y)> flashedOctopi = new HashSet<(int X, int Y)>();
                    _solution.IncrementEnergyLevels(queue, flashedOctopi);
                    _solution.HandleFlashes(queue, flashedOctopi);
                    numberOfFlashes += flashedOctopi.Count;
                    table.Caption($"Number of flashes: {numberOfFlashes}", new Style(Color.White));

                    foreach (var row in Enumerable.Range(0, _solution.MaxRows))
                    {
                        StringBuilder sb = new();
                        foreach (var column in Enumerable.Range(0, _solution.MaxColumns))
                        {
                            if (_solution.Input[column, row].Level == 0)
                                sb.Append($"[#ffff66]{_solution.Input[column, row].Level}[/]");
                            else
                                sb.Append(_solution.Input[column, row].Level);
                        }

                        table.AddRow(sb.ToString());
                    }

                    ctx.Refresh();
                    Thread.Sleep(250);
                });
        }
    }
    
    public async Task RunPart2()
    {
        var step = 0;
        var stopRunning = false;
        while(!stopRunning)
        {
            var table = new Table()
                .Width(40)
                .Centered()
                .Title("Advent Of Code 2021", new Style(new Color(0, 204, 0)))
                .AddColumn("Day 11 - Dumbo Octopus - Part II", config => config.Centered());
            AnsiConsole.Clear();
            AnsiConsole
                .Live(table)
                .Start(ctx =>
                {
                    Queue<(int X, int Y)> queue = new();
                    ISet<(int X, int Y)> flashedOctopi = new HashSet<(int X, int Y)>();
                    _solution.IncrementEnergyLevels(queue, flashedOctopi);
                    _solution.HandleFlashes(queue, flashedOctopi);
                    step++;
                    if (flashedOctopi.Count == _solution.MaxRows * _solution.MaxColumns)
                    {
                        stopRunning = true;
                    }
                    
                    table.Caption($"Number of steps: {step}", new Style(Color.White));

                    foreach (var row in Enumerable.Range(0, _solution.MaxRows))
                    {
                        StringBuilder sb = new();
                        foreach (var column in Enumerable.Range(0, _solution.MaxColumns))
                        {
                            if (_solution.Input[column, row].Level == 0)
                                sb.Append($"[#ffff66]{Emoji.Known.Octopus}[/]");
                                // sb.Append($"[#ffff66]{_solution.Input[column, row].Level}[/]");
                            else
                                sb.Append(_solution.Input[column, row].Level);
                        }

                        table.AddRow(sb.ToString());
                    }

                    ctx.Refresh();
                    Thread.Sleep(100);
                });
        }
    }
}