namespace AdventOfCode.Core;

public class Day01 : IDay<int>
{
    public int[] LeftLocations { get; set; }
    public int[] RightLocations { get; set; }
    
    public void AddData(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        
        LeftLocations = new int[lines.Length];
        RightLocations = new int[lines.Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            LeftLocations[i] = int.Parse(parts[0]);
            RightLocations[i] = int.Parse(parts[1]);
        }
        
        Array.Sort(LeftLocations);
        Array.Sort(RightLocations); 
    }

    public int SolvePart1() 
        => LeftLocations
            .Select((t, i) 
                => Math.Abs(t - RightLocations[i]))
            .Sum();

    public int SolvePart2(string input)
    {
        throw new NotImplementedException();
    }
}