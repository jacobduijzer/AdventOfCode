using AdventOfCode2023.Core.day01;

namespace AdventOfCode2023.UnitTests.day01;

public class Part2Tests
{
    [Theory]
    [InlineData("on1to", 11)]
    [InlineData("onetwo", 12)]
    [InlineData("twone", 21)]
    [InlineData("1twone", 11)]
    [InlineData("sfdrtpvspsixsn5zbqmggb8vgkjseight", 68)]
    public void RegExTest(string line, int expectedCalibrationValue)
    {
        Part2 day01RegEx = new();
        
        var calibrationValue = day01RegEx.GetCalibrationValue(line);
        
        Assert.Equal(expectedCalibrationValue, calibrationValue);
    }

    [Fact]
    public void RunPart2()
    {
        Part2 day01RegEx = new();

        var directory = AppContext.BaseDirectory;
        var fileName = "day01.txt";

        var input = File.ReadAllLines(Path.Combine(directory, "input", fileName));
        var result = 0;
        foreach (var line in input)
        {
            result += day01RegEx.GetCalibrationValue(line);
        }
        
        Assert.Equal(56017, result);
    }
}