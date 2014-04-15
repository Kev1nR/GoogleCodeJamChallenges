// Learn more about F# at http://fsharp.net

namespace GoogleCodeJam.Africa2010_Qualification

module FileReaders =
    open System.IO

    let readFileToEnd (filepath : string) =
        use stmr : StreamReader = new StreamReader(filepath, true)
        Seq.unfold (fun loopItem -> 
            match loopItem with
            | false, _ -> None
            | true, line when stmr.EndOfStream -> 
                Some (line, (false, stmr.ReadLine()))
            | true, line -> 
                Some(line, (true, stmr.ReadLine())) 
            | _, _ -> failwith "Invalid case") 
            
            (true, stmr.ReadLine())
        |> Seq.toList    

    let writeListToFile (filepath : string) lines =
        use stmr : StreamWriter = new StreamWriter(filepath)
        match lines with
        | [] -> failwith "Nothing to do"
        | _ ->
           lines 
           |> List.iter (fun (line : string) -> 
                stmr.WriteLine(line)
                stmr.Flush())


