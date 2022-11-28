namespace AdventOfCode.Core.Puzzles.Day12;

public record Cave(string Name)
{
   public bool IsSmallCave => Name.All(char.IsLower);
}