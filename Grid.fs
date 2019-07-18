namespace FLife.Grid
open FLife.Common
open System

[<AutoOpen>]
module Domain =
    ///<summary>Cell status - Alive of dead?</summary>
    type Status = 
    | Alive 
    | Dead
        ///<summary>Covert for display</summary>
        override this.ToString() = 
            match this with 
            | Dead -> "-"
            | Alive -> "+"

    ///<summary>Row and column coordinates</summary>
    type Coord = 
        {Row: int; Col: int}
        ///<summary>Override to allow for coordinate math</summary>
        static member (+) (a : Coord, b : Coord) = {
            Row = a.Row + b.Row
            Col = a.Col + b.Col}
        ///<summary>Override to allow for coordinate math</summary>
        static member (-) (a : Coord, b : Coord) = {
            Row = a.Row - b.Row
            Col = a.Col - b.Col}

    ///<summary>A cell in the grid</summary>
    type Cell = {Coord : Coord ; Status : Status}

    ///<summary>Grid of cells</summary>
    type Grid = Cell[][]

    ///<summary>Get a cell from the grid</summary>
    let getCell (grid:Grid) (coord:Coord) = grid.[coord.Row].[coord.Col]

    ///<summary>Create a square grid of dead cells</summary>
    let createGrid rows cols : Grid =
        let createRow row = 
            [| for col in 1 .. cols -> 
                {Coord = {Col = col; Row = row}; Status = Alive } |]
        [| for row in 1 .. rows -> createRow row |]
    
    ///<summary>Get the max row number of a grid</summary>
    let maxRow (grid:Grid) = grid.[0].Length - 1

    ///<summary>Get the max col number of a grid</summary>
    let maxCol (grid:Grid) = grid.Length - 1

    ///<summary>Get a sequence representing the grid row numbers</summary>
    let rowsNumbers grid = {0 .. grid |> maxRow}
    
    ///<summary>Get a sequence representing the grid column numbers</summary>
    let colNumbers grid = {0 .. grid |> maxCol}

    ///<summary>Convert a grid to a single string for display purposes</summary>
    let convertGridToString (grid:Grid) =
        let convertRowToString rowNum = 
            [| for cell in grid.[rowNum] -> cell.Status.ToString() |] |> join ""
        [| for row in (grid |> rowsNumbers) -> row |> convertRowToString|] |> join Environment.NewLine
    
    ///<summary>Neighbor cells</summary>
    type Neighbors = seq<Cell>

    ///<summary>Get all neighboring cells</summary>
    let getNeighbors (grid:Grid) (target:Cell) : Neighbors =
        let offsets = 
            [| {Row = -1 ; Col = -1} ; {Row = -1 ; Col = 0} ; {Row = -1 ; Col = 1}
               {Row =  0 ; Col = -1} ;                        {Row =  0 ; Col = 1}
               {Row =  1 ; Col = -1} ; {Row =  1 ; Col = 0} ; {Row =  1 ; Col = 1} |]
        let validRows row = row >= 0 && row <= (grid |> maxRow)
        let validCols col = col >= 0 && col <= (grid |> maxCol)
        let validCoord coord = 
            coord.Row |> validRows 
            && coord.Col |> validCols
        offsets |> Array.map (fun offset -> target.Coord + offset) |> Array.where validCoord |> Seq.map (getCell grid)