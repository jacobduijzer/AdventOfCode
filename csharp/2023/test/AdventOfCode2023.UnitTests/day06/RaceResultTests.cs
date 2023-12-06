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
}