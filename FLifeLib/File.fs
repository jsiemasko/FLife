module FLife.File

open FLife.Cell
open System

let parseStringToGrid (stringToParse:string) = 
    let convertCharToState charToParse = charToParse = '+'
    let convertToCell rowNum colNum charToParse = {
        Point = rowNum, colNum
        State = charToParse |> convertCharToState
        Generations = 0 }
    let parseRow rowNum (rowString:string) = rowString |> Seq.mapi (convertToCell rowNum) |> Seq.toList
    stringToParse.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries) 
    |> Array.toList
    |> List.mapi (fun rowNum rowString -> rowString |> parseRow rowNum)
    |> List.concat