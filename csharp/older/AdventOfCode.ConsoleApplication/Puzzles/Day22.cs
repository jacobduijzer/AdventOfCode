using System.Text;
using AdventOfCode.Core.Puzzles.Day22;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace AdventOfCode.ConsoleApplication.Puzzles;

public class Day22
{
    private readonly Solution _solution;

    public Day22(string inputFile)
    {
        _solution = new Solution(inputFile);
    }

    public async Task Run()
    {
        var cubeList =
            _solution.CreateCubeList(_solution.FilterCubeList(_solution.Input, -50, 50, -50, 50, -50, 50));

        var summedCubeList = CreateSummedCubeList(cubeList);

        var minX = summedCubeList.Min(x => x.Key.Item1);
        var maxX = summedCubeList.Max(x => x.Key.Item1);

        var minY = summedCubeList.Min(x => x.Key.Item2);
        var maxY = summedCubeList.Max(x => x.Key.Item2);

        var table = new Table()
            .Width(80)
            .Centered()
            .Title("[underline bold]Advent Of Code 2021 - Day 22: Reactor Reboot[/]",
                new Style(new Color(0, 204, 0), null));

        table.AddColumn(string.Empty, options => options.Centered());
        for (var x = minX; x <= maxX; x++)
            table.AddColumn($"{x}", options => options.Centered());

        await AnsiConsole
            .Live(table)
            .StartAsync(async ctx =>
            {
                AnsiConsole.Clear();

                for (var y = minY; y <= maxY; y++)
                {
                    StringBuilder sb = new();
                    List<IRenderable> panels = new();
                    panels.Add(new Markup(y.ToString()));
                    for (int x = minX; x <= maxX; x++)
                        panels.Add(new Markup($"0"));

                    table.AddRow(panels);
                }

                ctx.Refresh();
                await Task.Delay(1000);
                // while (true)
                // {
                long numberOfCubesTurnedOn = 0;
                for (var y = minY; y <= maxY; y++)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        var newY = (maxY - y);
                        var newX = (maxX - x) + 1;
                        if (summedCubeList.ContainsKey((x, y)))
                        {
                            table.UpdateCell(newY, newX, new Markup(summedCubeList[(x, y)].ToString()));
                            numberOfCubesTurnedOn += summedCubeList[(x, y)];
                        }
                        else
                            table.UpdateCell(newY, newX, new Markup("0"));

                        table.Caption($"Number of cubes turned on: [green]{numberOfCubesTurnedOn}[/]",
                            new Style(Color.White));

                        ctx.Refresh();
                        await Task.Delay(100);
                    }
                }
                // }
            });
    }

    private Dictionary<(int, int), long> CreateSummedCubeList(HashSet<(int, int, int)> cubeList)
    {
        Dictionary<(int, int), long> summedList = new();
        foreach (var (x, y, z) in cubeList)
            summedList[(x, y)] = summedList.GetValueOrDefault((x, y), 0) + z;
        return summedList;
    }
}