module FLife.TextDisplay
open System
open FLife.Cell
open FLife.Grid

[<AutoOpen>]
module StringHelpers =
    ///<summary>Curried version of String.join with a seq as input</summary>
    let join seperator = 
        let curriedJoin (seperator:String) (values:String[]) = String.Join(seperator, values)
        Seq.toArray >> curriedJoin seperator

[<AutoOpen>]
module CellHelpers =
    let statusDisplay (status : CellStatus) = 
        match status with 
        | Dead -> "-"
        | Alive -> "+"

[<AutoOpen>]
module GridHelpers =
    let private rowSeperator = System.String.Empty
    let private colSeperator = System.Environment.NewLine
    let cellDisplay cell = cell.Status |> statusDisplay
    
    ///<summary>Create a string display of the grid</summary>
    let gridDisplay : Grid -> string =
        let groupByRows = Seq.groupBy cellX >> Seq.map snd
        let rowsToString = 
            let rowDisplay = Seq.map cellDisplay >> join rowSeperator
            Seq.map rowDisplay
        groupByRows >> rowsToString >> join colSeperator