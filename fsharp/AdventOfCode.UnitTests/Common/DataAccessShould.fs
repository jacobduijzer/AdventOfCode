module AdventureOfCode.UnitTests.Common.DataAccessShould

open Xunit

[<Fact>]
let ``Read the contenst of a file`` () =
    let lines = DataAccess.readLinesFromTextFile "Common/testfile.txt"
    Assert.True(Seq.length(lines) = 7)
    