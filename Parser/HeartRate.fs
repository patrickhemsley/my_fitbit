#if INTERACTIVE
#else
module HeartRate
#endif



open System
open FSharp.Data
open FSharp.Data.JsonExtensions
open System.IO

type THeartRateValue = {
bpm: int;
confidence: int
 }


type THeartRate = {
  name: string;
  age: int;
  dateTime: DateTime;
  value:  THeartRateValue;
}

//let doc = JsonProvider<__SOURCE_DIRECTORY__ + "/../source_data/heart_rate-2016-12-26.json">.GetSample()


let IsConfident reading =
   reading?value?confidence.AsInteger() > 2

let loadHeartRateFile file =
    JsonValue.Load(__SOURCE_DIRECTORY__ + "/../source_data/" + file).AsArray()
        |> Array.toList
        |> List.filter(fun x -> IsConfident x) 



    //let validReadings = List.filter(fun x -> IsConfident x) arr
    //validReadings

let minHeartRate (readings) : int =    
    let heartRates = List.map(fun x -> x?value?bpm.AsInteger()) readings
    List.min(heartRates)

let maxHeartRate (readings) : int =    
    let heartRates = List.map(fun x -> x?value?bpm.AsInteger()) readings
    List.max(heartRates)

let heartSourceFiles =
    Directory.GetFiles(__SOURCE_DIRECTORY__ + "/../source_data/", "heart*.json")
        |> Array.map System.IO.Path.GetFileName   
        |> Array.toList
    
type THeartRateFileSummary = {
  date: string;
  minHeartRate: int;
  maxHeartRate: int;
}


let CalcHeartRateFileSummary (path: string): THeartRateFileSummary option =  
  let date = Path.GetFileName(path).[11..20]
  let validReadings = loadHeartRateFile path
  if validReadings.Length = 0 then
    None
  else
    let min = minHeartRate validReadings
    let max = maxHeartRate validReadings
    Some { date = date; minHeartRate  = min; maxHeartRate= max }

