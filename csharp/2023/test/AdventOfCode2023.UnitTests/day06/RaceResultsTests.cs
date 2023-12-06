using AdventOfCode2023.Core.day06;

namespace AdventOfCode2023.UnitTests.day06;

public class RaceResultsTests
{
    private const string TEST_DATA = @"Time:      7  15   30
Distance:  9  40  200";

    [Fact]
    public void ParseRaceResults()
    {
        RaceResults raceResults = new(TEST_DATA.Split(Environment.NewLine));

        Assert.Equal(3, raceResults.Races.Count); 
    }
}