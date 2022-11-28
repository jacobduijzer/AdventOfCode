namespace AdventureOfCode.Puzzles

open System.IO
open System.Text.RegularExpressions

module Day02 =

    type Direction = { Command: string; Distance: int }

    type SubmarinePosition = { Position: int; Depth: int; Aim: int }

    type Command =
        | Forward of int
        | Down of int
        | Up of int

    let directionsRegex =
        Regex(@"(\w+) (\d+)", RegexOptions.Compiled)

    let solvePart1 filePath =
        let lines =
            File.ReadAllLines filePath
            |> Seq.map (fun x -> x.Trim())
            |> Seq.filter (fun line -> (directionsRegex.Match line).Success = true)
            |> Seq.map (fun line -> (directionsRegex.Match line))
            |> Seq.map
                (fun line ->
                    let command =
                        match line.Groups.[1].Value with
                        | "forward" -> Forward
                        | "down" -> Down
                        | "up" -> Up

                    command (int line.Groups.[2].Value))

        let apply state command =
            match command with
            | Forward forward -> { state with Position = state.Position + forward }
            | Down down -> { state with Depth = state.Depth + down }
            | Up up -> { state with Depth = state.Depth - up }


        let startPosition = { Position = 0; Depth = 0; Aim = 0 }
        let currentState = lines |> Seq.fold apply startPosition

        currentState.Position * currentState.Depth

    let solvePart2 filePath =
        let lines =
            File.ReadAllLines filePath
            |> Seq.map (fun x -> x.Trim())
            |> Seq.filter (fun line -> (directionsRegex.Match line).Success = true)
            |> Seq.map (fun line -> (directionsRegex.Match line))
            |> Seq.map
                (fun line ->
                    let command =
                        match line.Groups.[1].Value with
                        | "forward" -> Forward
                        | "down" -> Down
                        | "up" -> Up

                    command (int line.Groups.[2].Value))

        let apply state command =
            match command with
            | Forward forward ->
                { state with
                      Position = state.Position + forward
                      Depth = state.Depth + (state.Aim * forward) 
                }
            | Down down -> { state with Aim = state.Aim + down }
            | Up up -> { state with Aim = state.Aim - up }

        let startPosition = { Position = 0; Depth = 0; Aim = 0 }
        let currentState = lines |> Seq.fold apply startPosition

        currentState.Position * currentState.Depth
