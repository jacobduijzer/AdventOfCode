using AdventOfCode2023.Core.day04;

namespace AdventOfCode2023.UnitTests.day04;

public class ScratchCardStackTests
{
    private const string Input = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";
    
    [Fact]
    public void LoadSingleCard()
    {
        var card = "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53";
        
        ScratchCardStack stack = new (new []{card});
        
        Assert.True(stack.Cards.Count == 1);
    }

    [Fact]
    public void LoadTestData()
    {
        ScratchCardStack stack = new (Input.Split(Environment.NewLine));
        
        Assert.True(stack.Cards.Count == 6); 
        Assert.Equal(13, stack.TotalScore);
    }
    
    [Fact]
    public void RunPart1()
    {
        var directory = AppContext.BaseDirectory;
        var fileName = "day04.txt";
        var input = File.ReadAllLines(Path.Combine(directory, "input", fileName));

        ScratchCardStack stack = new (input);

        Assert.Equal(220, stack.Cards.Count);
        Assert.Equal(26346, stack.TotalScore);
    }
}