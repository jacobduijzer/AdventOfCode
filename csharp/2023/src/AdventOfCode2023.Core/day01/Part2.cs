using System.Text.RegularExpressions;

namespace AdventOfCode2023.Core.day01;

public class Part2
{
    private readonly string[] _stringDigits = {
        "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
    };

    private readonly Regex _regex = new Regex("(?=(\\d|one|two|three|four|five|six|seven|eight|nine))");

    public int GetCalibrationValue(string line)
    {
        var matches = _regex.Matches(line);

        if (matches.Count == 1)
        {
            var first = matches.First().Groups.Values.Last().Value;
            var realValue = Match(first);
            return realValue * 10 + realValue;
        }
        
        var firstValue = Match(matches.First().Groups.Values.Last().Value);
        var lastValue = Match(matches.Last().Groups.Values.Last().Value);
        return firstValue * 10 + lastValue;
    }

    private int Match(string value)
    {
        if (value.Length == 1)
            return char.Parse(value) - '0';

        return Array.FindIndex(_stringDigits, digit => digit.Equals(value)) + 1;
    }
}