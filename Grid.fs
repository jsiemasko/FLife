module FLife.Grid

open FLife.Cell

type Grid = Cell seq

let createGrid numOfRows numOfCols =
    let createCell point = {Point = point; Status = defaultCellStatus}
    let allPoints = {1 .. numOfCols} |> Seq.allPairs {1.. numOfRows} 
    allPoints |> Seq.map createCell

let countLiving = Seq.where cellIsAlive >> Seq.length
    
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
