using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day04;

public class Solution : PuzzleBase
{
    private readonly int[] _bingoNumbers;
    private readonly string[] _cardInput;

    public Solution(string inputFile)
    {
        var input = File.ReadAllLines(inputFile);
        _bingoNumbers = input[0].Split(',').Select(x => int.Parse(x)).ToArray();
        _cardInput = input
            .Where(line => line != string.Empty)
            .Skip(1)
            .Take(input.Length - 1)
            .ToArray();
    }

    public override object SolvePart1()
    {
        var numberOfCards = _cardInput.Length / 5;

        var gameService = new GameService();
        for (int i = 0; i < numberOfCards; i++)
            gameService.AddBingoCard(new BingoCard(_cardInput.Skip(i * 5).Take(5).ToArray()));

        foreach (var number in _bingoNumbers.ToList())
        {
            gameService.PlayNumber(number);
            if (gameService.HasWinner)
                break;
        }

        return gameService;
    }

    public override object SolvePart2()
    {
        var numberOfCards = _cardInput.Length / 5;

        var gameService = new GameService();
        for (int i = 0; i < numberOfCards; i++)
            gameService.AddBingoCard(new BingoCard(_cardInput.Skip(i * 5).Take(5).ToArray()));

        foreach (var number in _bingoNumbers.ToList())
            gameService.PlayNumber(number);

        return gameService;
    }
}