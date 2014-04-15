#load "Africa2010_Qualification.fs"
open GoogleCodeJam.Africa2010_Qualification.FileReaders

let lines = GoogleCodeJam.Africa2010_Qualification.FileReaders.readFileToEnd "C:\Dropfolder\GoogleBootcampFiles\Round1A2008\B-small-practice.in"

//let lines = ["2"; "3"; "1 3 -5"; "-2 4 1";  "5"; "1 2 3 4 5"; "1 0 1 0 1"]

let sList = lines |> List.tail // drop the first item

let toInt32 (s : string) = System.Convert.ToInt32 (s)
let toInt = toInt32

type Flavour =
    | Malted
    | Unmalted
    | Ugh

type CustomerPrefs =
    {
        flavourCount : int
        flavours : Flavour list
    }

let createCustomerPref availableCount flavourList =
    let flavourPrefs = seq {for a in 1..availableCount do yield Ugh} |> Seq.toArray
    flavourList
    |> List.iter (fun flavourPref -> 
                    match flavourPref with
                    | (i, 0) -> flavourPrefs.[i-1] <- Unmalted
                    | (i, 1) -> flavourPrefs.[i-1] <- Malted
                    | _ -> ())
                    
    { 
        flavourCount = flavourList |> List.length
        flavours = flavourPrefs |> Array.toList 
    }
     

type milkshakes = 
    {
        FlavourCount : int
        Customers : CustomerPrefs list
    }

let pairUpFromList f (ls : #seq<'a>) =
  let rec pairUp rest tupledList =
      match rest with
      | a::b::t ->
          pairUp t ((f a,f b)::tupledList)
      | _ ->
          tupledList
  pairUp ls [];;

let processInput testCases = 
    let rec processInputInner rest tupledList =
        match rest with
        | flavourCount::(customerCount:string)::t ->
            let custPrefs = 
                t 
                |> Seq.take (toInt customerCount) 
                |> Seq.map(fun s -> s.Split[|' '|] |> Array.toList) |>  Seq.toList
                |> List.map (fun custPref -> 
                    match custPref with
                    | fc::cprefs -> createCustomerPref (toInt flavourCount) (pairUpFromList toInt cprefs)
                    | _ -> failwith "Invalid customer state")
                |> List.sortBy (fun cp -> cp.flavourCount)

            processInputInner 
                (t |> Seq.skip (toInt customerCount) |> Seq.toList) 
                ({FlavourCount = (toInt flavourCount); Customers = custPrefs} :: tupledList)
        | _ -> tupledList |> List.rev
    processInputInner testCases []

let processTestCase milkshakeCheck = 
    let provisioned = Array.init milkshakeCheck.FlavourCount  ((*)0)
    milkshakeCheck.Customers
    |> List.filter (fun cust -> cust.flavourCount = 1)
    |> List.filter (fun cust -> cust.flavours |> List.exists (fun flv -> flv = Malted))
    |> List.iter (fun pref -> 
        let maltedIndex = pref.flavours |> List.findIndex (fun flavour -> flavour = Malted)
        provisioned.[maltedIndex] <- 1 )
    provisioned
    
let processTestCase2 milkshakeCheck = 
    let provisioned = Array.init milkshakeCheck.FlavourCount  ((*)0)
    
    let rec procInner flvCount msCusts =
        msCusts
        |> List.partition (fun cust -> cust.flavourCount = flvCount)
        |> (fun (custL, custR) -> 
                if flvCount = 1
                then
                    printfn "Executing single flavour processing"
                    custL
                    |> List.partition (fun cust -> cust.flavours |> List.exists (fun flv -> flv = Malted))
                    |> (fun (malted, unmalted) -> 
                            malted 
                            |> List.iter (fun pref -> 
                                    let maltedIndex = pref.flavours |> List.findIndex (fun flavour -> flavour = Malted)
                                    provisioned.[maltedIndex] <- 1 )
                            unmalted
                            |> List.iter (fun pref -> 
                                    let unmaltedIndex = pref.flavours |> List.findIndex (fun flavour -> flavour = Unmalted)
                                    if provisioned.[unmaltedIndex] = 1
                                    then
                                         )) // we need continuations here I think
                     provisioned                                
                else
                    printfn "executing multi flavour processing"
                    provisioned)
    procInner 1 milkshakeCheck.Customers

//    |> List.filter (fun cust -> cust.flavours |> List.exists (fun flv -> flv = Malted))
//    |> List.iter (fun pref -> 
//        let maltedIndex = pref.flavours |> List.findIndex (fun flavour -> flavour = Malted)
//        provisioned.[maltedIndex] <- 1 )
//    provisioned 


//let outputResults = 
//    tupledItems sList
//    |> List.rev
//    |> List.mapi (fun i vs ->
//        "Case #" + (i + 1).ToString() + ": " + (minScalarProduct vs).ToString())
//

let outputResults = sList
printfn "Result: %A" outputResults
GoogleCodeJam.Africa2010_Qualification.FileReaders.writeListToFile "C:\Dropfolder\GoogleBootcampFiles\Round1A2008\A-large-practice.out" outputResults