using System.Collections;
using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day14;

public class Solution : PuzzleBase
{
    private record QueueItem(int Index, char InsertChar);

    private readonly string _startString;
    private readonly IDictionary<string, char> _insertionRules;
    private Dictionary<string, long> _pairs;
    private readonly Dictionary<char, long> _counts;
    private readonly Dictionary<string,string[]> _insertionRulesPart2;

    public Solution(string inputFile)
    {
        var input = DataReader.ReadLinesFromFile(inputFile);

        _startString = input[0];

        _insertionRules = input
            .Skip(2)
            .Select(line =>
            {
                var split = line.Split("->", StringSplitOptions.TrimEntries);
                return (split[0], char.Parse(split[1]));
            })
            .ToDictionary(rule => rule.Item1, rule => rule.Item2);
        
        _insertionRulesPart2 = input.Skip(2)
            .Select(s => s.Split(" -> "))
            .ToDictionary(a => a[0], a => new[] { $"{a[0][0]}{a[1]}", $"{a[1]}{a[0][1]}" });
        
        _pairs = _startString
            .Zip(_startString.Skip(1), (l, r) => $"{l}{r}")
            .ToLookup(s => s, s => 1)
            .ToDictionary(g => g.Key, g => (long)g.Sum());
        
        _counts = _startString
            .ToLookup(c => c)
            .ToDictionary(g => g.Key, g => (long)g.Count());
    }

    public override object SolvePart1()
    {
        var newString = _startString;
        Enumerable.Range(0, 10).ToList().ForEach(i => newString = HandleString(newString));

        var maxCount = newString.Distinct().Select(x => (Character: x, Count: newString.Count(y => y.Equals(x))));
        return maxCount.Max(x => x.Count) - maxCount.Min(x => x.Count);
    }

    public override object SolvePart2()
    {
        Enumerable.Range(0, 30).ToList().ForEach(i => GrowPolymers());
        return _counts.Max(count => count.Value) - _counts.Min(count => count.Value);
    }

    private string HandleString(string startString)
    {
        Queue<QueueItem> myQueue = new ();
        var al = new ArrayList();
        for (var i = 0; i < startString.Length; i++)
            al.Insert(i, startString[i]);

        for (var i = 0; i < al.Count; i++)
        {
            if (i + 1 >= al.Count) 
                continue;
            
            if (_insertionRules.TryGetValue($"{al[i]}{al[i + 1]}", out var replaceChar))
                myQueue.Enqueue(new QueueItem(i + 1 + myQueue.Count, replaceChar));
        }

        var newString = HandleQueue(myQueue, al);

        return newString;
    }

    private string HandleQueue(Queue<QueueItem> myQueue, ArrayList al)
    {
        while (myQueue.Count > 0)
        {
            var (index, insertChar) = myQueue.Dequeue();
            al.Insert(index, insertChar);
        }

        return al.ToArray().Select(x => x).Aggregate(string.Concat)?.ToString() ?? string.Empty;
    }
    
    private void GrowPolymers() 
    {
        var tempCount = new Dictionary<string, long>();
        foreach(var pair in _pairs)
        {
            if (!_insertionRulesPart2.TryGetValue(pair.Key, out var rule)) 
                continue;
            
            var insertChar = rule[0][1];
            if (_counts.ContainsKey(insertChar)) 
                _counts[insertChar] += pair.Value;
            else 
                _counts[insertChar] = pair.Value;
                
            foreach (var newPair in _insertionRulesPart2[pair.Key]) 
            {
                if (tempCount.ContainsKey(newPair)) 
                    tempCount[newPair] += pair.Value;
                else 
                    tempCount[newPair] = pair.Value;
            }
        }
        _pairs = tempCount;
    }
}