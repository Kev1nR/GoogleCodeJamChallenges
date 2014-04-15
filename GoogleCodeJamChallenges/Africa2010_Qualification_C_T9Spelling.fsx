#load "Africa2010_Qualification.fs"
open GoogleCodeJam.Africa2010_Qualification.FileReaders

let lines = GoogleCodeJam.Africa2010_Qualification.FileReaders.readFileToEnd "C:\Dropfolder\GoogleBootcampFiles\C-large-practice.in"

//let lines = ["2"; "twas brillig and the slithy tove"; "it is an ancient mariner"]

let (|T9Key|_|) (c : char) =
    if "abc".IndexOf(c) >= 0 then
        let index = "abc".IndexOf(c)
        Some ("2", index+1)
    else if "def".IndexOf(c) >= 0 then
        let index = "def".IndexOf(c)
        Some ("3", index+1)
    else if "ghi".IndexOf(c) >= 0 then
        let index = "ghi".IndexOf(c)
        Some ("4", index+1)
    else if "jkl".IndexOf(c) >= 0 then
        let index = "jkl".IndexOf(c)
        Some ("5", index+1)
    else if "mno".IndexOf(c) >= 0 then
        let index = "mno".IndexOf(c)
        Some ("6", index+1)
    else if "pqrs".IndexOf(c) >= 0 then
        let index = "pqrs".IndexOf(c)
        Some ("7", index+1)
    else if "tuv".IndexOf(c) >= 0 then
        let index = "tuv".IndexOf(c)
        Some ("8", index+1)
    else if "wxyz".IndexOf(c) >= 0 then
        let index = "wxyz".IndexOf(c)
        Some ("9", index+1)
    else if c = ' ' then
        Some ("0", 1)
    else
        None

let processSMS sms =
    sms
    |> Array.ofSeq
    |> Array.map (fun c -> 
        match c with 
        | T9Key (key, count) -> String.replicate count key
        | _ -> failwith "Invalid T9 mapping")
    |> Array.fold (fun (acc : string) t9 -> 
        let prevKey = 
            if acc.Length > 0 then
                acc.Chars (acc.Length - 1)
            else
                System.Char.MinValue
        let curKey = t9.Chars 0
        if prevKey = curKey then
            acc + " " + t9
        else
            acc + t9)
        ""

let sList = lines |> List.tail
let outputResults = 
    sList 
    |> List.mapi (fun i sms -> 
        let t9Mapping = processSMS sms 
        (i+1, t9Mapping))
    |> List.map (fun (i, s) -> 
        System.String.Format("Case #{0}: {1}", i,  (System.String.Join (" ", s))))

printfn "Result: %A" outputResults
GoogleCodeJam.Africa2010_Qualification.FileReaders.writeListToFile "C:\Dropfolder\GoogleBootcampFiles\Africa201_qual_T9Spelling_large.out" outputResults
