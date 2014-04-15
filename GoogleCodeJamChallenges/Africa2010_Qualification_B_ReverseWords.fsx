#load "Africa2010_Qualification.fs"
open GoogleCodeJam.Africa2010_Qualification.FileReaders

let lines = GoogleCodeJam.Africa2010_Qualification.FileReaders.readFileToEnd "C:\Dropfolder\GoogleBootcampFiles\B-large-practice.in"

let sList = lines |> List.tail
let outputResults = 
    sList 
    |> List.mapi (fun i s -> 
        let revSentence = s.Split(' ') |> Array.rev |> Array.toList
        (i+1, revSentence))
    |> List.map (fun (i, s) -> 
        System.String.Format("Case #{0}: {1}", i,  (System.String.Join (" ", s))))

printfn "Result: %A" outputResults
GoogleCodeJam.Africa2010_Qualification.FileReaders.writeListToFile "C:\Dropfolder\GoogleBootcampFiles\Africa201_qual_ReverseWords_large.out" outputResults