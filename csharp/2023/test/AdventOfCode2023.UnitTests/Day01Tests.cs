using AdventOfCode2023.Core;

namespace AdventOfCode2023.UnitTests;

public class Day01Tests
{
    [Theory]
    [InlineData("abcde", 1, "[darkgoldenrod]a[/]bcde")]
    [InlineData("1bcde", 1, "[underline green]1[/]bcde")]
    [InlineData("1b2de", 2, "[underline green]1[/][darkgoldenrod]b[/]2de")]
    [InlineData("1b2de", 3, "[underline green]1[/]b[underline green]2[/]de")]
    [InlineData("1b2d5", 4, "[underline green]1[/]b[underline green]2[/][darkgoldenrod]d[/]5")]
    [InlineData("1b2d5", 5, "[underline green]1[/]b[underline green]2[/]d[underline green]5[/]")]
    public void SetLine(string line, int steps, string newLine)
    {
        Day01 day01 = new();
        day01.SetLine(line);
        
        for(var i = 0; i < steps; i++)
            day01.RecoverNext();
        
        Assert.Equal(newLine, day01.GetNewLine());
    }

    [Fact]
    public void CalculateValue()
    {
        Day01 day01 = new();
        day01.SetLine("833");
        
        for(var i = 0; i < 3; i++)
            day01.RecoverNext();
        
        Assert.Equal(83, day01.LineResult());
    }
}