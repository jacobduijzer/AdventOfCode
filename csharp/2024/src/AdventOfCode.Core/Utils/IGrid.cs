using System.Text;

namespace AdventOfCode.Core.Utils;

public interface IGrid<T> where T : struct
{
    public void Print();
    public void PrintTo(StringBuilder sb);
    public T this[(int x, int y) p] { get; set; }
    public T this[int x, int y] { get; set; }
    public IEnumerable<(int x, int y)> FindAll(T t);
    public IEnumerable<((int x, int y) p, T v)> FindAll(Func<((int x, int y) p, T v), bool> predicate);
    public void ForEach(Action<((int x, int y) p, T v)> callback);
}