module AdventOfCode.UnitTests.Puzzles.Day02Should

open AdventureOfCode.Puzzles
open Xunit

[<Fact>]
let ``Return the correct position calculation`` () =
    let result = Day02.solvePart1 "Puzzles/Day02TestInput.txt"
    Assert.NotNull(result)
    Assert.Equal(150, result)
    
[<Fact>]
let ``Return the correct position calculation including aim`` () =
    let result = Day02.solvePart2 "Puzzles/Day02TestInput.txt"
    Assert.NotNull(result)
    Assert.Equal(900, result)