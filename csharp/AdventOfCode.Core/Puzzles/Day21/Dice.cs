namespace AdventOfCode.Core.Puzzles.Day21;

public class Dice
{
    public int Rolled { get; private set; }
    private int _current = 1;
    private int _take = 3;
    private int _max = 1000;

    public int[] Roll()
    {
        Rolled += _take;
        var rolled = Enumerable.Range(_current, _take).ToArray();
        _current += _take;

        var toLarge = rolled.Count(x => x > _max);
        if (toLarge <= 0) 
            return rolled;
        
        _current = 1;
        foreach (var item in rolled.Select((value, i) => new {i, value}))
        {
            if (item.value <= _max) 
                continue;
            
            rolled[item.i] = _current;
            _current++;
        }

        return rolled;
    }
}