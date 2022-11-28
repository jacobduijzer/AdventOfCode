namespace AdventureOfCode.Puzzles

open System

module Day03 =
    
//    let stringToInts charArrayToInts = List.map Seq.toList >> List.map charArrayToInts
//    
//    let mostCommonDigit lst =
//                lst
//                |> stringToInts
//                |> Seq.reduce (List.map2 (+))
//                |> Seq.map (fun x -> x > lst.Length / 2)
//                
//    let leastCommonDigit = mostCommonDigit >> Seq.map not
//    
//    let boolsToDecimal (boolNumber: bool seq) =
//            let binary =
//                boolNumber
//                |> Seq.map (Convert.ToInt32 >> string)
//                |> String.concat ""
//            Convert.ToInt32(binary, 2)
    
    let SolvePart1 inputFile =
        let input =
            DataAccess.readLinesFromTextFile inputFile
            |> Seq.toArray
        let len = Seq.length input[0]
        
        let counts = [|
            for i in 0..len-1 do
                input
                |> Array.groupBy (fun x -> Seq.item i x)
                |> Array.sortBy fst
                |> Array.map (fun (k,v) -> Seq.length v) |]
        
        let gamma =
            seq {0..len-1}
                |> Seq.fold
                    (fun a i ->
                        let count = Seq.item i counts
                        if count.[1] > count.[0] then
                            a + (1 <<< (len-1-i))
                        else
                            a
                    ) 0
                
        input
//        let mostCommonDigitsAsInt = mostCommonDigit >> boolsToDecimal
            
            
        
            
//        let charArrayToInts = List.map (fun x -> int x - 48)
//
//        
//
//        let mostCommonDigit lst =
//            lst
//            |> stringToInts
//            |> Seq.reduce (List.map2 (+))
//            |> Seq.map (fun x -> x > lst.Length / 2)
//
//        let leastCommonDigit = mostCommonDigit >> Seq.map not
//

//
//        let mostCommonDigitsAsInt = mostCommonDigit >> boolsToDecimal
//        let leastCommonDigitsAsInt = leastCommonDigit >> boolsToDecimal
//        
//        (mostCommonDigitsAsInt, leastCommonDigitsAsInt)
    //        let len = Seq.length input
    //        len