module FLife.Program
open FLife.Grid
open FLife.Cell
open FLife.TextDisplay
open FLife.File
open System

module Main =
    [<EntryPoint>]
    let main argv = 
        let display = gridDisplay >> printfn "%s"

        let mutable grid = notQuiteRandom
        {1..10000} |> Seq.iter (fun _ ->
            grid |> display
            grid <- grid |> nextGridState
            100 |> Threading.Thread.Sleep
            Console.Clear ())
        0