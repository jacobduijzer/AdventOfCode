namespace AdventOfCode.Core.Common;

public static class DictionaryExtensions
{
    public static void AddOrUpdate<T, TK>(this IDictionary<T, TK> source, T key, TK value)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (!source.TryAdd(key, value))
            source[key] = value;
    } 
}