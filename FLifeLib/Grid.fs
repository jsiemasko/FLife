module FLife.Grid

open FLife.Cell
open FSharp.Collections.ParallelSeq

type Grid = Cell list

let createGrid numOfRows numOfCols =
    let allPoints = 
        {0 .. numOfCols - 1} |> Seq.allPairs {0.. numOfRows - 1}
    allPoints |> Seq.toList |> List.map createCell

let countLiving = List.where isCellAlive >> List.length

let neighborOffsets =
    [ (-1,-1) ; (-1, 0) ; (-1, 1)
      ( 0,-1) ;           ( 0, 1)
      ( 1,-1) ; ( 1, 0) ; ( 1, 1) ]

let neighborPoints targetPoint = 
    neighborOffsets |> List.map (addPoints targetPoint)

let getValidNeighbors grid =
    neighborPoints >> getCells grid >> Seq.toList
     
let getNeighborCount grid target =
    target |> getValidNeighbors grid |> countLiving

let nextGridState grid = 
    let addNeighborCount cell = (cell, cell.Point |> getNeighborCount grid)
    let getNextState (cell,neighborCount) = cell |> nextCellState neighborCount
    grid 
    |> List.toSeq
    |> PSeq.map (addNeighborCount >> getNextState)
    |> PSeq.sortBy (fun cell -> cell.Point)
    |> Seq.toList

let createGenerations generations grid = 
    let generationNumber = List.init generations (fun i -> i)
    generationNumber |> List.scan (fun gridState _ -> gridState |> nextGridState) grid
    