namespace AdventOfCode2023.Core.day06;

public class RaceResults(string[] racesInput)
{
    public List<RaceResult> Races => ParseInput(); 
    
    public int TotalScore() => Races
        .Select(x => x.GetWinningOptions().Count)
        .Aggregate(1, (acc, val) => acc * val);

    private List<RaceResult> ParseInput()
    {
        var lines = racesInput
            .Select(line => line
                .Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1))
            .ToArray();
        
        return lines
            .First()
            .ToArray()
            .Zip(lines.Last().ToArray(), (first, second) => new RaceResult(long.Parse(first), long.Parse(second)))
            .ToList();
    }
}