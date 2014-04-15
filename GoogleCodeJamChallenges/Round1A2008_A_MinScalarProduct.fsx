#load "Africa2010_Qualification.fs"
open GoogleCodeJam.Africa2010_Qualification.FileReaders

let lines = GoogleCodeJam.Africa2010_Qualification.FileReaders.readFileToEnd "C:\Dropfolder\GoogleBootcampFiles\Round1A2008\A-large-practice.in"

//let lines = ["2"; "3"; "1 3 -5"; "-2 4 1";  "5"; "1 2 3 4 5"; "1 0 1 0 1"]

// The minimum scalar product of two permuted vectors occurs when they are both ordered in opposition to each other
// i.e. A = [1, 3, -5], B = [-2, 4, 1] 
// Aord = [-5, 1, 3], Bord = [4, 1, -2]
// Aord . Bord = -20 + 1 - 6 = -25

let sList = lines |> List.tail // drop the first item

let numStringToNumList c (numString : string) = 
    numString.Split(c)
    |> Array.toList
    |> List.map (fun n -> System.Convert.ToInt64(n))

let makeNumList_Space = numStringToNumList [|' '|]

// tuple-up the items into (vector A, vector B) we don't need to worry about the vector length
let tupledItems sList = 
    let rec tupledItems rest items =
        match rest with
        | [] -> items
        | _::vectorA::vectorB::t -> 
            tupledItems t (
                (makeNumList_Space vectorA, 
                 makeNumList_Space vectorB) :: items) 
        | _ -> failwith "Invalid number of lines input string"
    tupledItems sList []
        
// sort and take scalar product
let minScalarProduct vectors =
    let vA, vB = vectors
    let vA_ord = vA |> List.sort 
    let vB_ord = vB |> List.sort |> List.rev
    vB_ord
    |> List.zip vA_ord
    |> List.map (fun (a : System.Int64 ,b) -> bigint(a*b))
    |> List.sum

let outputResults = 
    tupledItems sList
    |> List.rev
    |> List.mapi (fun i vs ->
        "Case #" + (i + 1).ToString() + ": " + (minScalarProduct vs).ToString())


printfn "Result: %A" outputResults
GoogleCodeJam.Africa2010_Qualification.FileReaders.writeListToFile "C:\Dropfolder\GoogleBootcampFiles\Round1A2008\A-large-practice.out" outputResults