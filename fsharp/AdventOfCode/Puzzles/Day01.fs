namespace AdventureOfCode.Puzzles

module Day01 =
    let solvePart1 filePath =
        let lines = DataAccess.readLinesFromTextFile filePath
        let numbers = DataAccess.createListOfNumbers lines

        numbers
        |> List.pairwise
        |> List.filter (fun (x, y) -> y > x)
        |> List.length

    let solvePart2 filePath =
        let lines = DataAccess.readLinesFromTextFile filePath
        let numbers = DataAccess.createListOfNumbers lines
        
        numbers
        |> List.windowed 3
        |> List.map(fun x -> (x.[0] + x.[1] + x.[2]))
        |> List.pairwise
        |> List.filter (fun (x, y) -> y > x)
        |> List.length
