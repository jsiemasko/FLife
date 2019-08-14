module FLife.Cell

open System

[<AutoOpen>]
module Point =
    type Point = int * int

    let addPoints a b : Point = 
        ((fst a) + (fst b),(snd a) + (snd b))

type Cell = {
    Point : Point
    State : bool
    Generations : int}
let defaultCellState = false
let createCell point = {Point = point; State = defaultCellState; Generations = 0}
let createCellWithRandomState point (random:Random)= {
    Point = point
    State = random.NextDouble() > 0.5 
    Generations = 0}
let cellX cell = cell.Point |> fst
let cellY cell = cell.Point |> snd

///<summary>Based on the current state and number of neighbors return the new state</summary>  
let nextCellState neighborCount cell =
    let newState =
        match cell.State with
        | true -> neighborCount = 2 || neighborCount = 3
        | false -> neighborCount = 3
    {cell with 
        State = newState 
        Generations = if cell.State = newState then cell.Generations + 1 else 0 }

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