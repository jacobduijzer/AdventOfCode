using System.Text;

namespace AdventOfCode.Core.Utils;

public interface IGrid<T> where T : struct
{
    public void Print();
    public void PrintTo(StringBuilder sb);
    public T this[(int col, int row) p] { get; set; }
    public T this[int col, int row] { get; set; }
    public IEnumerable<(int col, int row)> FindAll(T t);
    public IEnumerable<((int col, int row) p, T v)> FindAll(Func<((int col, int row) p, T v), bool> predicate);
    public void ForEach(Action<((int col, int row) p, T v)> callback);
}