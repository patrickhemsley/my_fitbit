namespace Tests

open NUnit.Framework
open HeartRate

//[<TestClass>]
[<TestFixture>]
type ``When importing heart rate data`` () =
//type TestClass () =

    [<SetUp>]
    member this.Setup () =
        ()

    //[<Test>]
    //member this.Test1 () =
    //    Assert.Pass()

    //[<Test>]
    //member this.ThisFails () =
    //    Assert.Fail("Boo")

    [<Test>]
    member this.``source data should import OK`` () =
        //let path = __SOURCE_DIRECTORY__ + "\test_data\heart_rate-2016-12-26.json"
        let path = "heart_rate-2016-12-26.json"
        let values =  loadHeartRateFile path
        let min = minHeartRate values
        let max = maxHeartRate values
        Assert.AreEqual(42, min)

    [<Test>]
    member this.``heart rates of all files`` () = 
        heartSourceFiles
            |> List.map CalcHeartRateFileSummary
            |> List.iter (fun x -> 
               match x with  
                | Some(x) -> printfn "%s" x.date
                | _ -> ()
               ) 


