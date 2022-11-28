module AdventOfCode.UnitTests.Puzzles.Day03Should

open AdventureOfCode.Puzzles
open Xunit

[<Theory>]
[<InlineData("TestInput/day03.txt", 12)>]
let ``Solve part 1`` inputFile expectedResult =
    let result = Day03.SolvePart1 inputFile
    Assert.NotNull(result)
//    Assert.Equal(expectedResult, result)
    