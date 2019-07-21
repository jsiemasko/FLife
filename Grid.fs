module FLife.Grid

open FLife.Cell

type Grid = Cell seq

let createGrid numOfRows numOfCols =
    let allPoints = {0 .. numOfCols - 1} |> Seq.allPairs {0.. numOfRows - 1} 
    allPoints |> Seq.map createCell

let countLiving = Seq.where isCellAlive >> Seq.length

let neighborOffsets =
    [ (-1,-1) ; (-1, 0) ; (-1, 1)
      ( 0,-1) ;           ( 0, 1)
      ( 1,-1) ; ( 1, 0) ; ( 1, 1) ]

let neighborPoints targetPoint = 
    neighborOffsets |> Seq.map (addPoints targetPoint)

let getValidNeighbors grid =
    neighborPoints >> getCells grid
     
let getNeighborCount grid target =
    target |> getValidNeighbors grid |> countLiving

let nextGridState grid = 
    let addNeighborCount cell = (cell, cell.Point |> getNeighborCount grid)
    let getNextState (cell,neighborCount) = cell |> nextCellState neighborCount
    grid 
    |> Seq.map addNeighborCount 
    |> Seq.map getNextState 

let createGenerations generations grid = 
    let generationNumber = Seq.init generations (fun i -> i)
    generationNumber |> Seq.scan (fun gridState _ -> gridState |> nextGridState) grid
    