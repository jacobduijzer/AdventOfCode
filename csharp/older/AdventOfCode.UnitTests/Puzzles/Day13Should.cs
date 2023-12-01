using System.Linq;
using AdventOfCode.Core.Puzzles.Day13;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles;

public class Day13Should
{
   [Theory]
   [InlineData("TestInput/day13.txt", 17)]
   [InlineData("Input/day13.txt", 693)]
   public void CountCorrectDotsAfterFolding(string inputFile, int visibleDots)
   {
      // ARRANGE
      Solution day13 = new (inputFile);

      // ACT
      var result = day13.SolvePart1();

      // ASSERT
      Assert.NotNull(result);
      Assert.Equal(visibleDots, result);
   }
   
   [Theory]
   [InlineData("TestInput/day13.txt", 16, 9)]
   [InlineData("Input/day13.txt", 95, 139)]
   public void CalculateTheCorrectCode(string inputFile, int visibleHashes, int visibleDots)
   {
      // ARRANGE
      Solution day13 = new (inputFile);

      // ACT
      var result = day13.SolvePart2();

      // ASSERT
      Assert.NotNull(result);
      Assert.Equal(visibleHashes, result.ToString().Count(x => x == Solution.Character));
      Assert.Equal(visibleDots, result.ToString().Count(x => x == Solution.EmptyCharacter));
   }
}
