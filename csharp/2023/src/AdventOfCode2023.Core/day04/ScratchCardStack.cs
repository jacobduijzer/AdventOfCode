namespace AdventOfCode2023.Core.day04;

public class ScratchCardStack(string[] cardInput)
{
    public List<Card> Cards { get; private set; } = cardInput 
        .Select(card => new Card(card))
        .ToList();

    public int TotalScore => Cards.Sum(card => card.Score);

}