module FLife.File

open FLife.Cell
open System

let parseStringToGrid (stringToParse:string) = 
    let convertCharToState charToParse = 
        if charToParse = '+' then Alive else Dead
    let convertToCell rowNum colNum charToParse = {
        Point = rowNum, colNum; 
        State = charToParse |> convertCharToState}
    let parseRow rowNum (rowString:string) = rowString |> Seq.mapi (convertToCell rowNum) |> Seq.toList
    stringToParse.Split(Environment.NewLine) 
    |> Array.toList
    |> List.mapi (fun rowNum rowString -> rowString |> parseRow rowNum)
    |> List.concat

let blinker = """
-----
--+--
--+--
--+--
-----
"""
            |> parseStringToGrid

let notQuiteRandom = """
------++-+-++-+-+++-+-++-+-+
--+--+-+-++-+-+++-+-+-++_+--
--+------+++-++-+-++-++-+-++
--+--++++-++-+-++-++-++-+-++
-----+++--+++-+-+++-++-+-+++
"""
                    |> parseStringToGrid