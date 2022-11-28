module DataAccess

open System.IO

let readLinesFromTextFile filePath =
    File.ReadAllLines filePath
    |> Seq.map (fun x -> x.Trim())
    
let createListOfNumbers (list : seq<string>) =
    list
    |> List.ofSeq
    |> List.map (Operators.int)
