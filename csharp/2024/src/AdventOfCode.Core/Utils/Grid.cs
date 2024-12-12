using System.Text;

namespace AdventOfCode.Core.Utils;

public class Grid<T> : IGrid<T> where T : struct
{
    public Grid(int cols, int rows)
    {
        Cols = cols;
        Rows = rows;
        _data = new T[Rows, Cols];
    }

    public void Print() => Print(null);

    public void Print(Func<(int col, int row), (ConsoleColor color, ConsoleColor? background)>? func)
    {
        func ??= (p) => (Console.ForegroundColor, Console.BackgroundColor);
        for (var row = 0; row < Rows; row++)
        {
            for (var col = 0; col < Cols; col++)
            {
                var (color, background) = func((col, row));
                ConsoleEx.Write($"{_data[row, col]}", color, background);
            }

            Console.WriteLine();
        }
    }

    public void ForEach(Action<(int col, int row)> callback)
    {
        for (var row = 0; row < Rows; row++)
        {
            for (var col = 0; col < Cols; col++)
            {
                callback((col, row));
            }
        }
    }

    public Grid<T> Resize(int newCols, int newRows)
    {
        var newGrid = new Grid<T>(newCols, newRows);
        ForEach(p =>
        {
            if (newGrid.IsInBounds(p))
            {
                newGrid[p] = this[p];
            }
        });
        return newGrid;
    }

    public int Size => Cols * Rows;

    public override string ToString()
    {
        var sb = new StringBuilder();
        PrintTo(sb);
        return sb.ToString();
    }

    public void PrintTo(StringBuilder sb)
    {
        for (var row = 0; row < Rows; row++)
        {
            for (var col = 0; col < Cols; col++)
            {
                sb.Append(_data[row, col]);
            }

            sb.AppendLine();
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Grid<T> other || Cols != other.Cols || Rows != other.Rows)
        {
            return false;
        }

        for (var row = 0; row < Rows; row++)
        {
            for (var col = 0; col < Cols; col++)
            {
                if (!this[col, row].Equals(other[col, row]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override int GetHashCode() => (Width: Cols, Height: Rows).GetHashCode();

    public Grid<T> Clone()
    {
        var clone = new Grid<T>(Cols, Rows);
        for (var row = 0; row < Rows; row++)
        {
            for (var col = 0; col < Cols; col++)
            {
                clone._data[row, col] = _data[row, col];
            }
        }

        return clone;
    }

    public Dictionary<T, int> CountDistinct()
    {
        var counts = new Dictionary<T, int>();
        for (var row = 0; row < Rows; row++)
        {
            for (var col = 0; col < Cols; col++)
            {
                var c = _data[row, col];
                if (!counts.TryAdd(c, 1))
                {
                    counts[c]++;
                }
            }
        }

        return counts;
    }

    public (int col, int row) Find(T t)
    {
        var all = FindAll(t);
        return all.Any() ? all.First() : (-1, -1);
    }

    public IEnumerable<(int col, int row)> FindAll(T t) => FindAll(c => c.v.Equals(t)).Select(e => e.p);

    public IEnumerable<(int col, int row)> FindAll(Func<(int col, int row), bool> predicate)
    {
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Cols; x++)
            {
                var p = (x, y);
                if (predicate(p))
                {
                    yield return p;
                }
            }
        }
    }

    public IEnumerable<((int col, int row) p, T v)> FindAll(Func<((int col, int row) p, T v), bool> predicate)
    {
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Cols; x++)
            {
                var p = (col: x, row: y);
                var v = this[p];
                if (predicate((p, v)))
                {
                    yield return ((p, v));
                }
            }
        }
    }

    public void Fill(T t, Func<int, int, bool>? predicate = null)
    {
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Cols; x++)
            {
                if (predicate == null || predicate(x, y))
                {
                    _data[y, x] = t;
                }
            }
        }
    }

    public int Count(Func<T, bool> func) => Count(p => func(this[p]));

    public int Count(Func<(int col, int row), bool> func)
    {
        var count = 0;
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Cols; x++)
            {
                if (func((x, y)))
                {
                    count++;
                }
            }
        }

        return count;
    }

    public void ForEach(Action<((int col, int row) p, T v)> callback)
    {
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Cols; x++)
            {
                callback(((col: x, row: y), this[x, y]));
            }
        }
    }

    public (int col, int row) GetNorth((int col, int row) p) => (p.col, p.row - 1);

    public (int col, int row) GetSouth((int col, int row) p) => (p.col, p.row + 1);

    public (int col, int row) GetWest((int col, int row) p) => (p.col - 1, p.row);

    public (int col, int row) GetEast((int col, int row) p) => (p.col + 1, p.row);

    public IEnumerable<(int col, int row)> GetAdjacent4((int col, int row) p)
    {
        return GetAdjacent4(p.col, p.row);
    }

    public IEnumerable<(int col, int row)> GetAdjacent4(int col, int row) =>
        new[] { GetWest((col, row)), GetEast((col, row)), GetNorth((col, row)), GetSouth((col, row)) }
            .Where(IsInBounds);

    public IEnumerable<(int col, int row)> GetAdjacent8((int col, int row) p)
    {
        return GetAdjacent8(p.col, p.row);
    }

    public IEnumerable<(int col, int row)> GetAdjacent8(int col, int row)
    {
        foreach (var a in GetAdjacent4(col, row))
        {
            yield return a;
        }

        if (IsInBounds(col - 1, row - 1))
        {
            yield return (col - 1, row - 1);
        }

        if (IsInBounds(col + 1, row - 1))
        {
            yield return (col + 1, row - 1);
        }

        if (IsInBounds(col - 1, row + 1))
        {
            yield return (col - 1, row + 1);
        }

        if (IsInBounds(col + 1, row + 1))
        {
            yield return (col + 1, row + 1);
        }
    }

    public Dictionary<T, int> CountAdjacent4Distinct(int col, int row)
    {
        return GetAdjacentDistinct(col, row, false);
    }

    public Dictionary<T, int> CountAdjacent8Distinct(int col, int row)
    {
        return GetAdjacentDistinct(col, row, true);
    }

    private Dictionary<T, int> GetAdjacentDistinct(int col, int row, bool all)
    {
        var adjacents = new Dictionary<T, int>();

        void CheckThenAdd(int col, int row)
        {
            if (IsInBounds(col, row))
            {
                var c = _data[row, col];
                if (!adjacents.TryAdd(c, 1))
                {
                    adjacents[c]++;
                }
            }
        }

        CheckThenAdd(col - 1, row);
        CheckThenAdd(col + 1, row);
        CheckThenAdd(col, row - 1);
        CheckThenAdd(col, row + 1);
        if (all)
        {
            CheckThenAdd(col - 1, row - 1);
            CheckThenAdd(col + 1, row - 1);
            CheckThenAdd(col - 1, row + 1);
            CheckThenAdd(col + 1, row + 1);
        }

        return adjacents;
    }

    public int CountAdjacent8(int col, int row, T t)
    {
        var counts = CountAdjacent8Distinct(col, row);
        counts.TryGetValue(t, out var count);
        return count;
    }

    public int CountAdjacent4(int col, int row, T t)
    {
        var counts = CountAdjacent4Distinct(col, row);
        counts.TryGetValue(t, out var count);
        return count;
    }

    public bool IsInBounds((int col, int row) p) => IsInBounds(p.col, p.row);

    public bool IsInBounds(int col, int row)
    {
        return col >= 0 && row >= 0 && col < Cols && row < Rows;
    }

    public bool IsOnEdge((int col, int row) p) => IsOnEdge(p.col, p.row);

    public bool IsOnEdge(int col, int row)
    {
        return col == 0 || row == 0 || col == Cols - 1 || row == Rows - 1;
    }

    public int Count(T t)
    {
        var counts = CountDistinct();
        counts.TryGetValue(t, out var count);
        return count;
    }

    public T this[(int col, int row) p]
    {
        get => this[p.col, p.row];
        set => this[p.col, p.row] = value;
    }

    public T this[int col, int row]
    {
        get => _data[row, col];
        set => _data[row, col] = value;
    }

    public int Cols { get; }
    public int Rows { get; }
    
    private readonly T[,] _data;

    public DictionaryGrid<T> ToDictionaryGrid() => new(this);
}

public static partial class Input
{
    public static Grid<int> ReadIntGrid(string fileName = "input.txt") => ParseIntGrid(File.ReadAllLines(fileName));
    public static Grid<int> ParseIntGrid(IEnumerable<string> lines) => ParseGrid(lines, c => int.Parse(c.ToString()));

    public static Grid<char> ReadCharGrid(string fileName = "input.txt") => ParseCharGrid(File.ReadAllLines(fileName));
    public static Grid<char> ParseCharGrid(IEnumerable<string> lines) => ParseGrid(lines, c => c);

    private static Grid<T> ParseGrid<T>(IEnumerable<string> lines, Func<char, T> converter) where T : struct
    {
        var rows = lines.Count();
        var cols = lines.First().Length;
        var grid = new Grid<T>(cols, rows);
        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                grid[col, row] = converter(lines.ElementAt(row)[col]);
            }
        }

        return grid;
    }
}