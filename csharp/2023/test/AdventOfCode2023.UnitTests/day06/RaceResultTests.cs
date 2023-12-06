using AdventOfCode2023.Core.day06;

namespace AdventOfCode2023.UnitTests.day06;

public class RaceResultTests
{
    [Fact]
    public void ReturnAllOptions()
    {
        RaceResult raceResult = new(7, 9);
        
        Assert.Equal(7, raceResult.GetAllOptions().Count);
        Assert.Equal(0, raceResult.GetAllOptions().First().Distance);
        Assert.Equal(6, raceResult.GetAllOptions().Last().Distance);
    }

    [Fact]
    public void ReturnWinningOptions()
    {
        RaceResult raceResult = new(7, 9);

        var winningOptions = raceResult.GetWinningOptions();
        
        Assert.Equal(4, winningOptions.Count);
        Assert.Equal(10, winningOptions[0].Distance);
        Assert.Equal(12, winningOptions[1].Distance);
        Assert.Equal(12, winningOptions[2].Distance);
        Assert.Equal(10, winningOptions[3].Distance);
    }
}