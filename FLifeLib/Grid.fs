module FLife.Grid

open FLife.Cell
open FSharp.Collections.ParallelSeq
open System

type Grid = Cell list

let allPoints numOfCols numOfRows = 
    {0 .. numOfCols - 1} |> Seq.allPairs {0.. numOfRows - 1}

let createGrid numOfRows numOfCols =
    allPoints numOfCols numOfRows
    |> Seq.toList 
    |> List.map createCell

let createGridWithRandomState numOfRows numOfCols (random:Random) =
    allPoints numOfCols numOfRows
    |> Seq.toList 
    |> List.map (fun point -> random |> createCellWithRandomState point)

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
    