module FLife.Program
open FLife.Cell
open FLife.Grid
open FLife.TextDisplay
//open FLife.Game
open System

module Main =
    [<EntryPoint>]
    let main argv = 
        let display = gridDisplay >> printfn "%s"

        let grid = createGrid 10 5
        grid |> display
        grid |> nextGridStatus |> display
        grid |> nextGridStatus |> nextGridStatus |> display
        (*
        let display = createGridDisplay >> printfn "%s"
        let mutable grid = createGrid 20 20
        
        {1..10} |> Seq.iter (fun _ ->
            grid <- grid |> createNextGeneration
            grid |> display
            1000 |> Threading.Thread.Sleep
            Console.Clear ())
        *)
        0