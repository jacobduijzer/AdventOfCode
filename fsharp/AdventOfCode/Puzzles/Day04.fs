namespace AdventureOfCode.Puzzles

open System

module Day04 =

//    type Row = {}
//    type BingoCard = { HasBingo: bool; LastNumber: int; }
//    
    let bingoNumbers (lines: seq<string>) =
        lines
        |> Seq.head
        |> fun line -> line.Split(',', StringSplitOptions.TrimEntries)
        |> Seq.map int
        
    let createBingoCards (input:seq<string>) =
        Seq.skip 2 input
        |> Seq.filter (fun line -> line.Length > 0)
        |> Seq.chunkBySize 5
        |> Seq.map (fun board ->
                            board
                            |> Seq.collect (
                                fun s -> 
                                    s.Split(' ', StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)
                                    |> Array.map int
                                    |> Array.toList))
        
    let solvePart1 filePath =
        let lines =
            DataAccess.readLinesFromTextFile filePath

        let numbers = bingoNumbers lines
        let bingoCards = createBingoCards lines

        numbers 

    let solvePart2 filePath = filePath
