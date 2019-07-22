module FLife.Program
open FLife.Grid
open FLife.TextDisplay
open FLife.File
open System

module Main =
    [<EntryPoint>]
    let main argv = 
        let display = gridDisplay >> printfn "%s"

        let mutable grid = notQuiteRandom
        
        grid |> display
        "Generating..." |> printfn "%s"
        
        let generations = grid |> createGenerations 100 |> List.toArray 
        
        {1..100} |> Seq.iter (fun i ->
            generations.[i] |> display
            //grid <- grid |> nextGridState
            100 |> Threading.Thread.Sleep
            Console.Clear ())
        0