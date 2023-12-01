namespace AdventOfCode.Core.Puzzles.Day04;

public record BingoCardNumber(int Number)
{
   public bool IsCalled { get; private set; }
   public void Stamp() => IsCalled = true;
}