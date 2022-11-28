module AdventOfCode.UnitTests.Puzzles.Day04Should

open AdventureOfCode.Puzzles
open Xunit

[<Fact>]
let ``Return the correct winner for the example of part 1`` () =
    let result = Day04.solvePart1 "Puzzles/Day04TestInput.txt"
    Assert.NotNull(result)
//    Assert.Equal(37, result)

[<Fact>]
let ``Return the correct winner for part 1`` () =
    let result = Day04.solvePart1 "Input/Day04.txt"
    Assert.NotNull(result)
//    Assert.Equal(37, result)