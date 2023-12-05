using AdventOfCode2023.Core.day04;

namespace AdventOfCode2023.UnitTests.day04;

public class CardTests
{
    [Fact]
    public void LoadSingleCard()
    {
        var expectedNumbers = new[] { 41, 48, 83, 86, 17 };
        var expectedWinningNumbers = new[] { 83, 86, 6, 31, 17, 9, 48, 53 };
        var matchingNumbers = new[] { 48, 83, 86, 17 };
        var cardInput = "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53";

        Card card = new(cardInput);
        
        Assert.True(card.CardNumber == 1);
        Assert.Equal(expectedNumbers, card.Numbers);
        Assert.Equal(expectedWinningNumbers, card.WinningNumbers);
        Assert.Equal(matchingNumbers, card.MatchingNumbers);
        Assert.Equal(8, card.Score);
    }}