module FLife.Grid

open FLife.Cell

type Grid = Cell seq

let createGrid numOfRows numOfCols =
    let allPoints = {0 .. numOfCols - 1} |> Seq.allPairs {0.. numOfRows - 1} 
    allPoints |> Seq.map createCell

let countLiving = Seq.where isCellAlive >> Seq.length
    
let getNeighborCount grid target =
    let offsets = 
        [| (-1,-1) ; ( 0,-1) ; ( 1,-1)
           (-1, 0) ;           (-1, 1)
           (-1,-1) ; ( 0, 1) ; ( 1, 1) |]
    offsets |> Seq.map (addPoints target.Point) |> getCells grid |> countLiving

let nextGridStatus grid = 
    let addNeighborCount cell = cell, cell |> getNeighborCount grid
    let getNextStatus (cell,neighborCount) = cell |> nextCellStatus neighborCount
    grid 
    |> Seq.map addNeighborCount 
    |> Seq.map getNextStatus 
