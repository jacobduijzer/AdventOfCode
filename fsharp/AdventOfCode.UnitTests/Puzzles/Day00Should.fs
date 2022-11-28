module AdventOfCode.UnitTests.Puzzles.Day00Should

open Xunit

[<Theory>]
[<InlineData(0, "Welcome on day 0")>]
[<InlineData(10, "Welcome on day 10")>]
let ``Return a welcome message with the correct day`` (day, message) =
    Assert.True(Day00.welcomeMessage day = message)
