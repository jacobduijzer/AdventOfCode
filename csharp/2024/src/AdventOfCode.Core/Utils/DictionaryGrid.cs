using System.Text;

namespace AdventOfCode.Core.Utils;

public class DictionaryGrid<T> : IGrid<T> where T : struct
{
    private readonly Dictionary<(int col, int row), T> _dict = new();

    public DictionaryGrid(Grid<T> grid)
    {
        grid.ForEach(p => this[p] = grid[p]);
    }

    public void Print()
    {
        var sb = new StringBuilder();
        PrintTo(sb);
        Console.Write(sb);
    }

    public int GetMinX() => _dict.Min(e => e.Key.col);
    public int GetMinY() => _dict.Min(e => e.Key.row);
    public int GetMaxX() => _dict.Max(e => e.Key.col);
    public int GetMaxY() => _dict.Max(e => e.Key.row);

    public IEnumerable<(int col, int row)> FindAll(T t) => FindAll(c => c.v.Equals(t)).Select(e => e.p);

    public IEnumerable<((int col, int row) p, T v)> FindAll(Func<((int col, int row) p, T v), bool> predicate)
    {
        foreach (var e in _dict)
        {
            var c = (e.Key, e.Value);
            if (predicate(c))
            {
                yield return c;
            }
        }

        ;
    }

    public void ForEach(Action<((int col, int row) p, T v)> callback)
    {
        foreach (var e in _dict)
        {
            callback((e.Key, e.Value));
        }
    }

    public T this[(int col, int row) p]
    {
        get => _dict[p];
        set => _dict[p] = value;
    }

    public T this[int col, int row]
    {
        get => this[(col, row)];
        set => this[(col, row)] = value;
    }

    public IEnumerable<((int col, int row) p, T v)> GetAdjacent8((int col, int y) p, bool getOrCreate = false,
        T createWith = default)
        => GetAdjacent8(p.col, p.y, getOrCreate, createWith);

    public IEnumerable<((int col, int row) p, T v)> GetAdjacent8(int x, int y, bool getOrCreate = false,
        T createWith = default)
    {
        var cells = new[]
        {
            (x - 1, y - 1), // NW
            (x, y - 1), // N
            (x + 1, y - 1), // NE
            (x - 1, y), // W
            (x + 1, y), // E
            (x - 1, y + 1), // SW
            (x, y + 1), // S
            (x + 1, y + 1), // SE
        };
        foreach (var c in cells)
        {
            if (getOrCreate)
            {
                yield return GetOrCreate(c, createWith);
            }
            else
            {
                if (Exists(c))
                {
                    yield return Get(c);
                }
            }
        }
    }

    public IEnumerable<((int col, int row) p, T v)> GetAdjacent4((int x, int y) p, bool getOrCreate = false,
        T createWith = default)
        => GetAdjacent4(p.x, p.y, getOrCreate, createWith);

    public IEnumerable<((int col, int row) p, T v)> GetAdjacent4(int x, int y, bool getOrCreate = false,
        T createWith = default)
    {
        var cells = new[]
        {
            (x, y - 1), // N
            (x - 1, y), // W
            (x + 1, y), // E
            (x, y + 1), // S
        };
        foreach (var c in cells)
        {
            if (getOrCreate)
            {
                yield return GetOrCreate(c, createWith);
            }
            else
            {
                if (Exists(c))
                {
                    yield return Get(c);
                }
            }
        }
    }

    public ((int col, int row) p, T v) Get(int x, int y) => Get((x, y));
    public ((int col, int row) p, T v) Get((int col, int row) p) => new(p, _dict[p]);

    public ((int x, int y) p, T v)? TryGet(int x, int y) => TryGet((x, y));

    public ((int x, int y) p, T v)? TryGet((int col, int row) p) =>
        _dict.TryGetValue(p, out T value) ? new(p, value) : null;

    public ((int col, int row) p, T v) GetOrCreate(int x, int y, T createWith = default) => GetOrCreate((x, y), createWith);

    public ((int col, int row) p, T v) GetOrCreate((int col, int row) p, T createWith = default)
    {
        if (!_dict.ContainsKey(p))
        {
            _dict.Add(p, createWith);
        }

        return Get(p);
    }

    public bool Exists(int x, int y) => Exists((x, y));
    public bool Exists((int col, int row) p) => _dict.ContainsKey(p);

    public void PrintTo(StringBuilder sb)
    {
        var minX = GetMinX();
        var maxX = GetMaxX();
        var minY = GetMinY();
        var maxY = GetMaxY();
        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                var p = (col: x, row: y);
                var o = " ";
                if (_dict.TryGetValue(p, out T value))
                {
                    o = value.ToString();
                }

                sb.Append(o);
            }

            sb.AppendLine();
        }
    }
}

public static partial class Input
{
    public static DictionaryGrid<char> ReadCharDictionaryGrid(string fileName = Input.DEFAULT_INPUT_FILENAME)
        => new(ReadCharGrid(fileName));
}