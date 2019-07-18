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
    let createGrid size : Grid =
        let createRow row = 
            [| for col in 1 .. size -> 
                {Coord = {Col = col; Row = row}; Status = Dead } |]
        [| for row in 1 .. size -> createRow row |]

    ///<summary>Get a sequence representing the grid row numbers</summary>
    let rowsNumbers (grid:Grid) = 
        let maxRow = grid.[0].Length - 1
        {0 .. maxRow}
    
    ///<summary>Get a sequence representing the grid column numbers</summary>
    let colNumbers (grid:Grid) = 
        let maxCol = grid.Length - 1
        {0 .. maxCol}

    ///<summary>Convert a grid to a single string for display purposes</summary>
    let convertGridToString (grid:Grid) =
        let convertRowToString rowNum = 
            [| for cell in grid.[rowNum] -> cell.Status.ToString() |] |> join ""
        [| for row in (grid |> rowsNumbers) -> row |> convertRowToString|] |> join Environment.NewLine

    type Neighbors = seq<Cell>

    ///<summary>Get all neighboring cells</summary>
    let getNeighbors (grid:Grid) target : Neighbors =
        let validCoord coord = 
            coord.Row >= 0 && coord.Row <= grid.[0].Length - 1 
            && coord.Col >= 0 && coord.Col <= grid.Length - 1
        [| {Row = -1 ; Col = -1} ; {Row = -1 ; Col = 0} ; {Row = -1 ; Col = 1}
           {Row =  0 ; Col = -1} ;                        {Row =  0 ; Col = 1}
           {Row =  1 ; Col = -1} ; {Row =  1 ; Col = 0} ; {Row =  1 ; Col = 1} |]
        |> Array.map (fun offset -> target + offset)
        |> Array.where validCoord
        |> Seq.map (getCell grid)

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
    let determineLife cell neighbors =
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

module Main =
    open GridHelpers
    open Game

    [<EntryPoint>]
    let main argv = 
        createGrid 20 
        |> convertGridToString 
        |> printfn "%s"

        
        {Row = 0; Col = 0} |> getNeighbors (createGrid 20) |> Seq.length |> printf "%A"

        (*
        let grid = createGrid 20
        grid 
        |> rowsNumbers 

        |> Seq.map (fun row -> 
                        colNumbers |> Seq.map (fun col -> 
                            let cell = grid |> getCell {Row = row; Col = col}
                            cell)) |> printfn "%A"
                            *)
        //getNeighbors g {Row = 18; Col = 0} |> printfn "%A"


        Console.ReadLine() |> ignore
        0