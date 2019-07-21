
namespace FLife.Game
open FLife.Grid
open FLife.Cell
(*
[<AutoOpen>]
module Game =

    ///<summary>Based on current status and neighbor count should this cell live or die</summary>
    let getNextCellState cell neighbors =
        let newStatus =
            match cell.Status with
            | Alive -> 
                match neighbors |> countLiving with
                | n when n < 2 -> Dead
                | n when n > 3 -> Dead
                | _ -> Alive
            | Dead -> 
                match neighbors |> countLiving with 
                | 3 -> Alive
                | _ -> Dead
        {cell with Status = newStatus}

    let createNextGeneration (grid:Grid) = 
        let generateNextGenerationCell cell = cell |> getNeighbors grid |> getNextCellState cell
        let generateNextGenerationRow row = row |> Array.map generateNextGenerationCell
        grid |> Seq.map generateNextGenerationRow
        *)