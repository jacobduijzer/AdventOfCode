namespace AdventOfCode.Core.Puzzles.Day04;

public class BingoCard
{
    public int LastNumber { get; private set; }
    public bool HasBingo { get; private set; }
    public Dictionary<int, BingoCardNumber[]> Lines = new();

    public BingoCard(string[] lines)
    {
        for (int i = 0; i < 5; i++)
        {
            var cardNumbers = lines[i].Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(character => character.Trim())
                .Select(x => new BingoCardNumber(int.Parse(x)));
            Lines.Add(i, cardNumbers.ToArray());
        }
    }

    public void Stamp(int number)
    {
        for (var i = 0; i < 5; i++)
        {
            foreach (var line in Lines[i].Where(x => !x.IsCalled))
            {
                if (line.Number != number) continue;
                line.Stamp();
                LastNumber = number;
            }
        }

        if (Lines.Any(line => line.Value.Count(x => x.IsCalled) == 5))
            HasBingo = true;

        for (var i = 0; i < 5; i++)
        {
            if (AllNumbersCalled(i))
                HasBingo = true;
        }
    }

    private bool AllNumbersCalled(int row) =>
        Lines[0][row].IsCalled &&
        Lines[1][row].IsCalled &&
        Lines[2][row].IsCalled &&
        Lines[3][row].IsCalled &&
        Lines[4][row].IsCalled;

    public int GetScore()
    {
        if (!HasBingo)
            throw new ArgumentException("No winner yet, can not calculate score.");

        var sum = Lines.Sum(lines => lines.Value.Where(x => !x.IsCalled).Sum(y => y.Number));
        return sum * LastNumber;
    }
}