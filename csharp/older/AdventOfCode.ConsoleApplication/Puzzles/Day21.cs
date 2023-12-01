using System.Collections.Immutable;
using AdventOfCode.Core.Puzzles.Day21;
using Spectre.Console;

namespace AdventOfCode.ConsoleApplication.Puzzles;

public class Day21
{
    private readonly Solution _solution;
    private readonly Dice _dice;

    public Day21(string inputFile)
    {
        _solution = new Solution(inputFile);
        _dice = new Dice();
    }

    public async Task Run()
    {
        var player = 0;
        var game = _solution.Input;
        while (!game.GetWinner(1000).HasValue)
        {
            var numbers = _dice.Roll();
            game = game.PracticeRoll(player, numbers.Sum());

            AnsiConsole.Clear();
            AnsiConsole.Write(new BarChart()
                .Width(160)
                .Label("[green bold underline]Dirac Dice[/]")
                .CenterLabel()
                .WithMaxValue(1000)
                .AddItem(nameof(game.Player1), game.Player1.Score, Color.Aqua)
                .AddItem(nameof(game.Player2), game.Player2.Score, Color.Chartreuse1)
                .AddItem("Number of throws:", _dice.Rolled));

            await Task.Delay(50);

            player = (player + 1) % 2;
        }
    }

    public async Task RunPart2()
    {
        var wins = new long[2];
        var games = ImmutableHashSet.Create<(Game game, long count)>(
            (new Game(_solution.Input.Player1, _solution.Input.Player2), 1)
        );

        var player = 0;
        while (games.Count > 0)
        {
            var next = (
                from a in games
                from b in a.game.QuantumRoll(player)
                group (game: b.game, count: a.count * b.count) by b.game
                into g
                select (game: g.Key, count: g.Sum(x => x.count))).ToImmutableHashSet();

            var winningPlayers = (
                from a in next
                let winner = a.game.GetWinner(21)
                where winner.HasValue
                select (player: winner.Value, games: a)).ToList();

            if (winningPlayers.Count > 0)
            {
                foreach (var winningPlayer in winningPlayers)
                    wins[winningPlayer.player] += winningPlayer.games.count;

                next = next.Except(winningPlayers.Select(winningPlayer => winningPlayer.games));
            }

            games = next;
            player = (player + 1) % 2;

            AnsiConsole.Clear();
            AnsiConsole.Write(
                    new FigletText(wins.Max().ToString())
                    .LeftAligned()
                    .Color(Color.Red));
        }
    }
}