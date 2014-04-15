#load "Africa2010_Qualification.fs"

open GoogleCodeJam.Africa2010_Qualification.FileReaders

//let lines = GoogleCodeJam.Africa2010_Qualification.FileReaders.readFileToEnd "C:\Dropfolder\GoogleBootcampFiles\Round1A2008\A-large-practice.in"

let lines = ["4"; "5"; "2"; "10";  "2000000000"; ]

let sList = lines |> List.tail // drop the first item

(* 
**  The secret here is to spot that (3 + root(5)) is equal to 2(1 + phi) where phi = the golden ratio (1.61803398875)#
**  then (3 + root(5))^n = 2^n * (1 + phi)^n
**  So we cann work out the second term and then left shift n times to get the result, finally picking the last three
**  numbers of the integer part.
*)

let phi_plus_1 = 2.61803398875

let nToInt l = l |> List.map (fun (n:string) -> System.Convert.ToInt32(n))

let outputResults = 
    nToInt sList
    |> List.map (fun n -> System.Math.Pow(phi_plus_1, double n) * (double (2L <<< (n - 1))))
//    |> List.map (fun n -> 
//        let integerPart = (floor n) 
//        let isolateFractionalPart = (integerPart / 1000.0)
//        isolateFractionalPart - (floor (integerPart / 1000.0)))
//    |> List.mapi (fun i n ->
//        "Case #" + (i + 1).ToString() + ": " + n.ToString("#.000").Substring(1))


printfn "Result: %A" outputResults
GoogleCodeJam.Africa2010_Qualification.FileReaders.writeListToFile "C:\Dropfolder\GoogleBootcampFiles\Round1A2008\A-large-practice.out" outputResults