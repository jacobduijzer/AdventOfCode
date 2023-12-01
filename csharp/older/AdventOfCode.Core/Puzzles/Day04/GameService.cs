namespace AdventOfCode.Core.Puzzles.Day04;

public class GameService
{
    public List<BingoCard> Winners = new();
    private readonly List<BingoCard> _bingoCards = new();

    public bool HasWinner { get; private set; }
    public void AddBingoCard(BingoCard bingoCard)
    {
        _bingoCards.Add(bingoCard);
    }

    public void PlayNumber(int number)
    {
        foreach (var card in _bingoCards.Where(x => !x.HasBingo))
        {
            card.Stamp(number);
            if (card.HasBingo)
            {
                Winners.Add(card);
                HasWinner = true;
            }
        }
    }
}