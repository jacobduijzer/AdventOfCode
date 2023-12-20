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

    private const string TEST_DATA_2 = @"shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags.";

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

    [Fact]
    public void SolvePart1()
    {
        var input = Helpers.ReadFileContents("day7.txt");
        var result = new Day07().SolvePart1(input);
        
        Assert.Equal(316, result); 
    }
    
    [Fact]
    public void SolvePart2WithTestData1()
    {
        var result = new Day07().SolvePart2(TEST_DATA);
        
        Assert.Equal(32, result);
    }
    
    [Fact]
    public void SolvePart2WithTestData2()
    {
        var result = new Day07().SolvePart2(TEST_DATA_2);
        
        Assert.Equal(126, result);
    }
    
    [Fact]
    public void SolvePart2()
    {
        var input = Helpers.ReadFileContents("day7.txt");
        var result = new Day07().SolvePart2(input);
        
        Assert.Equal(11310, result); 
    }
}