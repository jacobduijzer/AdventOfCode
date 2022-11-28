using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day08;

public class Solution : PuzzleBase<IEnumerable<Solution.Entry>>
{
    public Solution(string inputFile) : base(inputFile)
    {
    }

    public override object SolvePart1()
    {
        int[] lengths = {2, 3, 4, 7};
        var count = Input
            .SelectMany(x => x.Outputs)
            .Count(y => lengths.Contains(y.Length));

        return count;
    }

    public override object SolvePart2()
    {
        var input = Input 
            .Select(MapNumberToSignal)
            .Sum();
        return input;
    }

    private int MapNumberToSignal(Entry entry)
    {
        var map = MapSignalsToNumbers(entry.Signals);
        return entry.Outputs
            .Select((output, index) => 
                map.SingleOrDefault(x => 
                    x.Value.SetEquals(output)).Key * (int) Math.Pow(10, entry.Outputs.Count - index - 1))
            .Sum();
    }

    private Dictionary<int, HashSet<char>> MapSignalsToNumbers(List<String> signals)
    {
        Dictionary<int, HashSet<char>> map = new();

        var values = signals.OrderBy(x => x.Length).ToArray();
        var one = values[0].ToHashSet();
        var four = values[2].ToHashSet();
        var seven = values[1].ToHashSet();
        var eight = values[^1].ToHashSet();
       
        var d = seven.Except(one).ToHashSet();
        var ef = four.Except(one).ToHashSet();
        var gc = eight.Except(four.Union(d)).ToHashSet();

        var c = values
            .Where(x => x.Length == 6)
            .Select(x => x.Except(four.Union(d)))
            .Single(x => x.Count() == 1)
            .ToHashSet();

        var g = gc.Except(c)!;
        var e = values
            .Where(x => x.Length == 6)
            .Select(x => x.Except(seven.Union(gc)))
            .Single(x => x.Count() == 1)
            .ToHashSet();

        var f = ef.Except(e);
        var a = values
            .Where(x => x.Length == 5)
            .Select(x => x.Except(gc.Union(f).Union(d)))
            .Single(x => x.Count() == 1)
            .ToHashSet();

        var b = one.Except(a);

        var zero = eight.Except(f).ToHashSet();
        var two = eight.Except(e).Except(b).ToHashSet();
        var three = eight.Except(e).Except(g).ToHashSet();
        var five = eight.Except(a).Except(g).ToHashSet();
        var six = eight.Except(a).ToHashSet();
        var nine = eight.Except(g).ToHashSet(); 
        
        map.Add(0, zero);
        map.Add(1, one); 
        map.Add(2, two);
        map.Add(3, three);
        map.Add(4, four);
        map.Add(5, five);
        map.Add(6, six);
        map.Add(7, seven);
        map.Add(8, eight);
        map.Add(9, nine);

        return map;
    }
    
    public record Entry(List<string> Signals, List<string> Outputs);

    public override IEnumerable<Entry> ParseInput(string inputFile) =>
        DataReader.ReadLinesFromFile(inputFile)
            .ToList()
            .Select(x => x.Split('|', StringSplitOptions.TrimEntries))
            .Select(y => new Entry(
                y[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList(),
                y[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList()))
            .ToList();
}

