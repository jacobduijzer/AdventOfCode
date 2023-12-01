using AdventOfCode2023.Core;

namespace AdventOfCode2023.UnitTests;

public class Part1Tests
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
        Part1 part1 = new();
        part1.SetLine(line);
        
        for(var i = 0; i < steps; i++)
            part1.RecoverNext();
        
        Assert.Equal(newLine, part1.GetNewLine());
    }

    [Theory]
    [InlineData("833", 83)]
    [InlineData("nkzjrdqrmpztpqninetwofour1znnkd", 11)]
    [InlineData("3four4", 34)]
    public void CalculateValue(string line, int expectedTotal)
    {
        Part1 part1 = new();
        part1.SetLine(line);
        
        for(var i = 0; i < line.Length; i++)
            part1.RecoverNext();
        
        Assert.Equal(expectedTotal, part1.LineResult());
    }
}