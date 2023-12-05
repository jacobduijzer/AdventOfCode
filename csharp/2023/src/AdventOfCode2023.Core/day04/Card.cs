using System.Text.RegularExpressions;

namespace AdventOfCode2023.Core.day04;

public class Card
{
    private readonly Regex _cardNumber = new Regex("\\d+");

    public readonly int CardNumber;
    public readonly int[] Numbers;
    public readonly int[] WinningNumbers;
    public readonly int[] MatchingNumbers;

    public int Score => (int)Math.Pow(2, MatchingNumbers.Length - 1);

    public Card(string cardInput)
    {
        CardNumber = int.Parse(_cardNumber.Match(cardInput.Split(':').First()).Value); 
        
       var numbers = cardInput
            .Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Last()
            .Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(num => num.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());

       Numbers = numbers.First().ToArray();
       WinningNumbers = numbers.Last().ToArray();
       MatchingNumbers = Numbers.Intersect(WinningNumbers).ToArray();
    }
}