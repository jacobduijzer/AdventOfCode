using System.Text;
using AdventOfCode.Core.Common;
using AdventOfCode.Core.Puzzles.Day15;
using Spectre.Console;

namespace AdventOfCode.ConsoleApplication.Puzzles;

public class Day15
{
    private readonly Solution _solution;

    public Day15(string inputFile)
    {
        _solution = new Solution(inputFile);
    }

    public async Task Run()
    {
        var result = (PathFinderResult) _solution.SolvePart1();

        var width = result.StartGrid.Max(x => x.Key.X);
        var height = result.StartGrid.Max(x => x.Key.Y);
        
        var table = new Table()
            .Width(120)
            .Centered()
            .AddColumn("Advent Of Code 2021 - Day 15");

        // var start = 0;
        // var length = 5;
        // while (true)
        // {
        //     foreach (var row in Enumerable.Range(start, length))
        //     {
        //         Console.WriteLine(row);
        //
        //         if (row + length >= height)
        //             start = 0;
        //     }
        // }

        var start = 0;
        var length = 10;
        AnsiConsole.Clear();
        await AnsiConsole
            .Live(table)
            .StartAsync(async ctx =>
            {
                var path = result.Path;
                path.Insert(0, new Point(0, 0));
                while (true)
                {
                    // for (int i = 0; i < result.Path.Count; i++)
                    // {
                        table.Rows.Clear();

                        foreach (var row in Enumerable.Range(start, start + length))
                        {
                        // for(var row = 0; row < height; row++)
                        // {
                            StringBuilder sb = new();
                            foreach (var column in Enumerable.Range(0, width))
                            {
                                var newPoint = new Point(column, row);
                                if (path.Contains(newPoint) )//&& path.IndexOf(newPoint) <= i)
                                    sb.Append($"[aqua]{result.StartGrid[newPoint]}[/]");
                                else
                                    sb.Append(result.StartGrid[newPoint]);
                            }

                            table.AddRow(sb.ToString());
                            ctx.Refresh();

                            start++;
                            if (height-length <= start - length)
                                start = 0;
                        }

                        ctx.Refresh();
                        await Task.Delay(100);
                    // }
                }
            });

        // AnsiConsole.Clear();
        // await AnsiConsole
        //     .Live(table)
        //     .StartAsync(async ctx =>
        //     {
        //         while (true)
        //         {
        //             var grid = GetLines(result.StartGrid, count * screenHeight * width, screenHeight * width);
        //             
        //             table.Rows.Clear();
        //             StringBuilder sb = new();
        //             
        //             foreach (var column in Enumerable.Range(0, width))
        //             {
        //                 foreach (var item in grid)
        //                 {
        //                     sb.Append(result.StartGrid[item.Key]);
        //                 }
        //             }
        //
        //             table.AddRow(sb.ToString());
        //             
        //             ctx.Refresh();
        //             await Task.Delay(100);
        //             // Console.WriteLine($"{count}=>{grid.Count}");
        //             count++;
        //             if (count * screenHeight + screenHeight >= height)
        //                 count = 0;
        //         }
        //     });

        // var table = new Table()
        //     .Width(120)
        //     .Centered()
        //     .AddColumn("Advent Of Code 2021 - Day 15");
        //
        // AnsiConsole.Clear();
        // await AnsiConsole
        //     .Live(table)
        //     .StartAsync(async ctx =>
        //     {
        //         var path = result.Path;
        //         path.Insert(0, new Point(0, 0));
        //         while (true)
        //         {
        //             for (int i = 0; i < result.Path.Count; i++)
        //             {
        //                 table.Rows.Clear();
        //             
        //                 foreach (var row in Enumerable.Range(0, height))
        //                 {
        //                     StringBuilder sb = new();
        //                     foreach (var column in Enumerable.Range(0, width))
        //                     {
        //                         var newPoint = new Point(row, column);
        //                         if (path.Contains(newPoint) && path.IndexOf(newPoint) <= i)
        //                         {
        //                             sb.Append($"[aqua]{result.StartGrid[newPoint]}[/]");
        //                         }
        //                         else
        //                             sb.Append(result.StartGrid[newPoint]);
        //                     }
        //             
        //                     table.AddRow(sb.ToString());
        //                     table.Collapse();
        //                 } 
        //                 ctx.Refresh();
        //                 await Task.Delay(100);
        //             }
        //         }
        //     });


    }
    
    private Dictionary<Point, int> GetLines(Dictionary<Point, int> grid, int position, int numberOfLines)
    {
        var smallGrid = grid
            .Skip(position)
            .Take(numberOfLines)
            .ToDictionary(point => point.Key, point => point.Value);

        if (grid.Count > numberOfLines)
        {
            grid.Take(numberOfLines - grid.Count)
                .ToList().ForEach(point =>
                {
                    if (!smallGrid.ContainsKey(point.Key))
                        smallGrid.Add(point.Key, point.Value);
                });
        }

        return smallGrid;
    }
}