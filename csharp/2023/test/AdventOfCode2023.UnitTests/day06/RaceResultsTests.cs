using AdventOfCode2023.Core.day06;

namespace AdventOfCode2023.UnitTests.day06;

public class RaceResultsTests
{
    private const string TEST_DATA = @"Time:      7  15   30
Distance:  9  40  200";

    private const string REAL_DATA = @"Time:        44     70     70     80
Distance:   283   1134   1134   1491";

    [Fact]
    public void ParseRaceResults()
    {
        RaceResults raceResults = new(TEST_DATA.Split(Environment.NewLine));

        Assert.Equal(3, raceResults.Races.Count); 
    }

    [Fact]
    public void CalculateScoreForPart1WithTestData()
    {
        RaceResults raceResults = new(TEST_DATA.Split(Environment.NewLine)); 
        Assert.Equal(288, raceResults.TotalScore());
    }
    
    [Fact]
    public void CalculateScoreForPart1WithRealData()
    {
        RaceResults raceResults = new(REAL_DATA.Split(Environment.NewLine)); 
        Assert.Equal(219849, raceResults.TotalScore());
    }
}