namespace AdventureOfCode.Puzzles

open System
open System.IO

module Day07 =

    let parseInput filePath =
        let numbers =
            File.ReadAllText(filePath).Split(',')
            |> Array.map (fun x -> x.Trim())
            |> Array.map (Operators.int)

        numbers

    let solvePart1 filePath =
        let horizontalPositions = parseInput filePath
        let min = Seq.min horizontalPositions
        let max = Seq.max horizontalPositions - 1

        let minimumFuelConsumption =
            seq { min .. max }
            |> Seq.map
                (fun step ->
                    horizontalPositions
                    |> Seq.map (fun number -> Math.Abs(number - step))
                    |> Seq.sum)
            |> Seq.min

        minimumFuelConsumption

    let solvePart2 filePath =
        let horizontalPositions = parseInput filePath
        let min = Seq.min horizontalPositions
        let max = Seq.max horizontalPositions - 1

        let minimumFuelConsumtion =
            seq { min .. max }
            |> Seq.map
                (fun i ->
                    horizontalPositions
                    |> Seq.map
                        (fun horizontalPosition ->
                            (horizontalPosition - i)
                            |> (Math.Abs >> fun number -> number * (number + 1) / 2))
                    |> Seq.sum)
            |> Seq.min
        minimumFuelConsumtion

