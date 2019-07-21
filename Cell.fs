module FLife.Cell

[<AutoOpen>]
module Point =
    type Point = int * int
    let getX = fst
    let getY = snd
    let addPoints a b : Point = 
        let newX = (a |> getX) + (b |> getX)
        let newY = (a |> getY) + (b |> getY)
        (newX,newY)

[<AutoOpen>]
module Status =
    type CellStatus = Alive | Dead
    let isAlive status =
        match status with
            | Alive -> true
            | Dead -> false

[<AutoOpen>]
module Cell =
    type Cell = {Point : Point ; Status : CellStatus}
    let defaultCellStatus = Alive
    let createCell point = {Point = point; Status = defaultCellStatus}
    let cellX cell = cell.Point |> getX
    let cellY cell = cell.Point |> getY
    let isCellAlive cell = cell.Status |> isAlive

    ///<summary>Based on the current status and number of neighbors return the new status</summary>  
    let nextCellStatus neighborCount cell =
        let newStatus =
            match cell.Status with
            | Alive -> 
                match neighborCount with
                | n when n < 2 -> Dead
                | n when n > 3 -> Dead
                | _ -> Alive
            | Dead -> 
                match neighborCount with 
                | 3 -> Alive
                | _ -> Dead
        {cell with Status = newStatus}

    [<AutoOpen>]
    module CellCollectionHelpers =
        ///<summary>Get a seq of cells by passing in a seq of coordinates</summary>
        let getCells searchCells pointsToFind =
            seq {
                for point in pointsToFind do
                    for cell in searchCells do
                        if (cell.Point = point) then
                            yield (cell)
            }