namespace AdventOfCode.Core;

public interface IDay<out T>
{
    T SolvePart1();
    T SolvePart2();
}