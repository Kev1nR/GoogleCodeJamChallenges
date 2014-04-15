// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.

#load "Africa2010_Qualification.fs"
open GoogleCodeJam.Africa2010_Qualification.FileReaders

let lines = GoogleCodeJam.Africa2010_Qualification.FileReaders.readFileToEnd "C:\Dropfolder\GoogleBootcampFiles\Africa201_qual_StoreCredit_large.in"

let numberOfRuns = System.Convert.ToInt32(lines.[0])

let tupledItems lines =
    let rec tupledItems nextlines items =
        match nextlines with
        | [] -> items
        | credit::itemCount::prices::t -> 
            tupledItems t (
                (System.Convert.ToInt32(credit : string), 
                 System.Convert.ToInt32(itemCount : string), 
                 prices) :: items) 
        | _ -> failwith "Invalid number of lines input string"
    tupledItems lines []
        

let permuteAndMatch credit inputArray =
    let arrLength = inputArray |> Array.length
    let copyArray = inputArray |> Array.copy
    let perm n = Array.permute (fun i -> (i + n) % arrLength)

    let rec find2Items n =
        let matchedItems = copyArray
                           |> perm n
                           |> Array.zip inputArray
                           |> Array.filter (fun (l : int*int, r) -> (snd l) + (snd r) = credit)
        match matchedItems with
        | [||] when n < arrLength -> 
            find2Items (n + 1)
        | [||] ->
            None
        | _ -> 
            let pickResult = 
                matchedItems.[0] 
                |> (fun (l, r) -> (fst l), (fst r))
            Some(pickResult)
    find2Items 1


let processRunItem run item =
    let credit, _, (pricesList : string) = item
    let matchedItems = 
        pricesList.Split(' ')
        |> Array.mapi (fun i stringPrice -> 
            i+1, System.Convert.ToInt32(stringPrice))
        |> permuteAndMatch credit
        |> (fun op -> 
            match op with
            | None -> failwith "Item not found"
            | Some (l, r) -> 
                if l < r then
                    l, r
                else
                    r, l)
    matchedItems
    
let outputResults = 
    lines 
    |> List.tail
    |> tupledItems
    |> List.rev
    |> List.mapi (fun k item -> 
        let result = processRunItem (k + 1) item
        "Case #" + (k + 1).ToString() + ": " + (fst result).ToString() + " " + (snd result).ToString())

printfn "Result: %A" outputResults
GoogleCodeJam.Africa2010_Qualification.FileReaders.writeListToFile "C:\Dropfolder\GoogleBootcampFiles\Africa201_qual_StoreCredit_large.out" outputResults
