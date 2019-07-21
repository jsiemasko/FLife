module FLife.Program
open FLife.Grid
open FLife.Cell
open FLife.TextDisplay
open System

module Main =
    [<EntryPoint>]
    let main argv = 
        let display = gridDisplay >> printfn "%s"
        let mutable grid = 
            let createGridFromStates = List.map (fun (x, y, state) -> {Point = (x,y); State = state})
            [ (0,0,Dead) ; (0,1,Dead) ; (0,2,Dead)  ; (0,3,Dead) ; (0,4,Dead)
              (1,0,Dead) ; (1,1,Dead) ; (1,2,Alive) ; (1,3,Dead) ; (1,4,Dead)
              (2,0,Dead) ; (2,1,Dead) ; (2,2,Alive) ; (2,3,Dead) ; (2,4,Dead)
              (3,0,Dead) ; (3,1,Dead) ; (3,2,Alive) ; (3,3,Dead) ; (3,4,Dead)
              (4,0,Dead) ; (4,1,Dead) ; (4,2,Dead)  ; (4,3,Dead) ; (4,4,Dead) ]
            |> createGridFromStates
        {1..10000} |> Seq.iter (fun _ ->
            grid |> display
            grid <- grid |> nextGridState
            100 |> Threading.Thread.Sleep
            Console.Clear ())

        0