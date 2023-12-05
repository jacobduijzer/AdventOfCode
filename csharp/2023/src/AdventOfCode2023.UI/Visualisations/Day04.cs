using AdventOfCode2023.Core.day04;
using Spectre.Console;

namespace AdventOfCode2023.UI.Visualisations;

public static class Day04
{
    public static async Task Run()
    {
        const int NUMBER_SCORE_DELAY = 25;
        const int LINE_DELAY = 100;

        var directory = AppContext.BaseDirectory;
        var fileName = "day04.txt";

        var input = File.ReadAllLines(Path.Combine(directory, "input", fileName));

        ScratchCardStack stack = new(input);
        var root = new Tree("[underline blue]Game Deck[/] | Total score: 0");
        await AnsiConsole.Live(root)
            .AutoClear(false)
            .Overflow(VerticalOverflow.Ellipsis)
            .Cropping(VerticalOverflowCropping.Bottom)
            .StartAsync(async ctx =>
                {
                    var currentLine = 0;
                    var totalScore = 0;
                    foreach (var card in stack.Cards)
                    {
                        if (root.Nodes.Count == 2)
                        {
                            root.Nodes.RemoveAt(0);
                            currentLine--;
                        }

                        var foo = root.AddNode($"[yellow]Card {card.CardNumber}[/]");

                        var table = new Table()
                            .RoundedBorder()
                            .AddColumn("Card numbers")
                            .AddColumn("Winning numbers")
                            .AddColumn("Matching numbers");
                       
                        for (var i = 0; i < card.WinningNumbers.Length; i++)
                            table.AddRow(i < card.Numbers.Length ? card.Numbers[i].ToString() : string.Empty, card.WinningNumbers[i].ToString()); 

                        foo.AddNode(table);

                        var found = 0;
                        for (var i = 0; i < card.WinningNumbers.Length; i++)
                        {
                            if(i > 0)
                                table.UpdateCell(i-1, 1, $"{card.WinningNumbers[i-1]}"); 
                            
                            table.UpdateCell(i, 1, $"[underline red]{card.WinningNumbers[i]}[/]");

                            if (card.Numbers.Contains(card.WinningNumbers[i]))
                            {
                                table.UpdateCell(found, 2, $"{card.WinningNumbers[i]}");
                                found++;
                            }
                            
                            ctx.Refresh();
                            await Task.Delay(NUMBER_SCORE_DELAY);
                        }
                        
                        // reset last 
                        table.UpdateCell(card.WinningNumbers.Length-1, 1, $"{card.WinningNumbers[^1]}");
                        
                        foo.AddNode($"[blue]Card score: {card.Score}[/]");
                        totalScore += card.Score;
                        foo.AddNode($"[blue]Total score: {totalScore}[/]");

                        ctx.Refresh();
                        await Task.Delay(LINE_DELAY);
                    }
                }
            );
    }
}