open System
open AdventureOfCode.Puzzles

[<EntryPoint>]
let main argv =
    let welcomeMessage = Day00.welcomeMessage 0
    printfn $"%s{welcomeMessage}"
    let result1 = Day02.solvePart1 "Puzzles/Day02Input.txt"
    printfn $"result part 1: {result1}"
    let result2 = Day02.solvePart2 "Puzzles/Day02Input.txt"
    printfn $"result part 2: {result2}"
    Console.ReadLine() |> ignore
    0