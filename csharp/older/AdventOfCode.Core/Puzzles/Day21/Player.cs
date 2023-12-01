namespace AdventOfCode.Core.Puzzles.Day21;

public record Player(int CurrentPosition, int Score = 0)
{
    public Player Move(int positions)
    {
        var newPosition = ((CurrentPosition + positions - 1) % 10) + 1;
        var newScore = Score + newPosition;
        return new Player(newPosition, newScore);
    }

    public bool HasWon(int max) => Score >= max;
}