namespace AdventOfCode2023.Core.day06;

public record struct RaceResult(long Time, long Distance)
{
    public List<RaceResult> GetAllOptions()
    {
        List<RaceResult> allOptions = new();
        for(var i = 0; i < Time; i++)
           allOptions.Add(new(i, i * (Time - i)));

        return allOptions;
    }

    public List<RaceResult> GetWinningOptions()
    {
        return new List<RaceResult>();
    }
}