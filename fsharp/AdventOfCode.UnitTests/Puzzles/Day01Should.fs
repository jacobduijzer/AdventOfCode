module AdventOfCode.UnitTests.Puzzles.Day01Should

open AdventureOfCode.Puzzles
open Xunit

[<Fact>]
let ``Return the correct number of increasings`` () =
    let result = Day01.solvePart1 "Puzzles/Day01TestInput.txt"
    Assert.Equal(7, result)
    
[<Fact>]
let ``Return the correct number of increasings with sets of 3`` () =
    let result = Day01.solvePart2 "Puzzles/Day01TestInput.txt"
    Assert.Equal(5, result)