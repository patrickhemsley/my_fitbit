// Learn more about F# at http://fsharp.org

open System
open FSharp.Data
open FSharp.Data.JsonExtensions
open HeartRate
open System.IO



[<EntryPoint>]


let main argv =
    printfn "starting.."

    let fileName = "heart_rate.csv"
    let outFile= File.OpenWrite(fileName)
    let stream = new StreamWriter(outFile)
    

    heartSourceFiles
        |> List.map CalcHeartRateFileSummary
        |> List.choose id
        |> List.map (fun r -> System.String.Concat([r.date;",";r.minHeartRate.ToString();",";r.maxHeartRate.ToString()]))
        |> List.iter (stream.WriteLine)
    stream.Flush()
    stream.Close()

    printfn "results written to %s" fileName
          
//(funcTimer (fun () -> createFile  10000 "file10000.xml"))

    0
