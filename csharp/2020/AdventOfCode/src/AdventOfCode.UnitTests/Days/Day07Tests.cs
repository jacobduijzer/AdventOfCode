using AdventOfCode.Core.Days;

namespace AdventOfCode.UnitTests.Days;

public class Day07Tests
{
    private const string TEST_DATA = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

    [Fact]
    public void ParseTestInputToMap()
    {
        var map = new Day07().ParseInput(TEST_DATA);
        
        Assert.Equal(9, map.Count);
        Assert.Equal("light red", map.First().Key);
        Assert.Equal("dotted black", map.Last().Key);
    }
    
    [Fact]
    public void SolvePart1WithTestData()
    {
        var result = new Day07().SolvePart1(TEST_DATA);
        
        Assert.Equal(4, result);
    }
}