module FLife
open System


module StringHelpers =
    ///<summary>Curried version of String.join</summary>
    let join (seperator:String) (values:String[]) = String.Join(seperator, values)

module GridHelpers =
    open StringHelpers

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

module Game =
    open GridHelpers

    ///<summary>Count the number of living neighbors</summary>
    let countLiving : Neighbors -> int =
        Seq.where (fun cell -> 
                            match cell.Status with 
                            | Alive -> true 
                            | Dead -> false) 
        >> Seq.length

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
        grid |> Array.map generateNextGenerationRow

module Main =
    open GridHelpers
    open Game

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