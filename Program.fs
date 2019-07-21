module FLife.Program
open FLife.Grid
open FLife.TextDisplay
open System

module Main =
    [<EntryPoint>]
    let main argv = 
        let display = gridDisplay >> printfn "%s"
        let mutable grid = createGrid 20 20
        
        {1..10} |> Seq.iter (fun _ ->
            grid |> display
            grid <- grid |> nextGridState
            1000 |> Threading.Thread.Sleep
            Console.Clear ())
        0