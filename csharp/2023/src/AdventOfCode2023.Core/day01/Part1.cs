namespace AdventOfCode2023.Core;

public class Part1
{
    
    
    private List<int> _totals = new();

    private string _line = string.Empty;
    private int _current = 0;
    private Dictionary<int, int> _digits = new ();

    public void SetLine(string line)
    {
        _current = 0;
        _digits.Clear();
        _line = line;
    }

    public void RecoverNext()
    {
        if (char.IsDigit(_line[_current]))
            _digits.Add(_current, _line[_current] - '0');

        _current++;
    }

    public string GetNewLine()
    {
        var newLine = "";
        for (var i = 0; i < _line.Length; i++)
        {
            if(_digits.TryGetValue(i, out var digit))
                newLine += $"[underline green]{digit}[/]";
            else
                if(i == _current - 1)
                    newLine += $"[darkgoldenrod]{_line[i]}[/]";
                else
                    newLine += _line[i];
        }

        return newLine;
    }

    public int LineResult()
    {
        if (!_digits.Any())
            return 0;

        var result = _digits.Count == 1
            ? _digits.First().Value * 10 + _digits.First().Value
            : _digits.First().Value * 10 + _digits.Last().Value;

        _totals.Add(result);
        
        return result;
    }

    public int TotalResult() => _totals.Sum();
}