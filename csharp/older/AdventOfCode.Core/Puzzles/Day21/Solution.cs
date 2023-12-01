using System.Collections.Immutable;
using System.Text.RegularExpressions;
using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day21;

public class Solution : PuzzleBase<Game>
{
    private readonly Dice _dice;

    public Solution(string inputFile) : base(inputFile)
    {
        _dice = new Dice();
    }

    public override object SolvePart1()
    {
        var player = 0;
        var game = Input;
        while (!game.GetWinner(1000).HasValue)
        {
            var numbers = _dice.Roll();
            game = game.PracticeRoll(player, numbers.Sum());
            player = (player + 1) % 2;
        }

        return Math.Min(game.Player1.Score, game.Player2.Score) * _dice.Rolled;
    }

    public override object SolvePart2()
    {
        var wins = new long[2];
        var games = ImmutableHashSet.Create<(Game game, long count)>(
            (new Game(Input.Player1, Input.Player2), 1)
        );
        
        var player = 0;
        while (games.Count > 0)
        {
            var next = (
                from a in games
                from b in a.game.QuantumRoll(player)
                group (game: b.game, count: a.count * b.count) by b.game into g
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
        }

        return wins.Max();
    }

    public override Game ParseInput(string inputFile)
    {
        var input = File.ReadLines(inputFile)
            .Select(x => Regex.Split(x, @"Player (\d+) starting position: (\d+)"))
            .Select(x => x.Where(y => !string.IsNullOrEmpty(y)));

        return new (
            new Player(int.Parse(input.First().ToArray()[1])), 
            new Player(int.Parse(input.Last().ToArray()[1])));
    }
}