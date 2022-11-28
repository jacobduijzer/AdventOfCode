using System.Collections.Immutable;

namespace AdventOfCode.Core.Puzzles.Day21;

public record Game(Player Player1, Player Player2)
{
    public int? GetWinner(int maxScore)
    {
        if (Player1.HasWon(maxScore))
            return 0;
        if (Player2.HasWon(maxScore))
            return 1;
        return default;
    }

    public Game PracticeRoll(int player, int rollTotal) =>
        player switch
        {
            0 => this with {Player1 = Player1.Move(rollTotal)},
            1 => this with {Player2 = Player2.Move(rollTotal)},
            _ => throw new InvalidOperationException()
        };

    public IEnumerable<(Game game, long count)> QuantumRoll(int player) =>
        player switch
        {
            0 => from m in quantumRoles
                select (this with {Player1 = Player1.Move(m.move)}, m.count),
            1 => from m in quantumRoles
                select (this with {Player2 = Player2.Move(m.move)}, m.count),
            _ => Enumerable.Empty<(Game, long)>()
        };

    private static ImmutableArray<(int move, long count)> quantumRoles =
    (
        from i in Enumerable.Range(1, 3)
        from j in Enumerable.Range(1, 3)
        from k in Enumerable.Range(1, 3)
        let sum = i + j + k
        group sum by sum
        into g
        select (g.Key, g.LongCount())).ToImmutableArray();
}