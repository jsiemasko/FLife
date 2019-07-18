module FLife.Program
open FLife.Grid
open FLife.Game
open System

module Main =
    [<EntryPoint>]
    let main argv = 
        let display = convertGridToString >> printfn "%s"
        let mutable grid = createGrid 80 80
        
        {1..1000} |> Seq.iter (fun _ ->
            grid <- grid |> createNextGeneration
            grid |> display
            10 |> Threading.Thread.Sleep
            Console.Clear ())
        0