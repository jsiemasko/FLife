module FLife.GridTests

open FLife.Cell
open FLife.Grid
open FLife.File
open NUnit.Framework

module GridTests =
    let grid = createGrid 10 10
    let aliveGrid = //For tests that don't want to rely on default alive state
        let allPoints = {0 .. 9} |> Seq.allPairs {0.. 9} 
        allPoints |> Seq.map (fun point -> {Point = point; State = Alive})

    [<Test>]
    let ``createGrid starts at 0,0`` () =
        let result = grid |> Seq.exists (fun cell -> cell.Point = (0,0))
        Assert.True(result)

    [<Test>]
    let ``createGrid ends X at expected value`` () =
        let xOverExpected = 
            grid |> Seq.exists (fun cell -> cell.Point |> getX > 9)
        Assert.False(xOverExpected)

    [<Test>]
    let ``createGrid ends Y at expected value`` () =
        let yOverExpected = 
            grid |> Seq.exists (fun cell -> cell.Point |> getY > 9)
        Assert.False(yOverExpected)

    [<Test>]
    let ``createGrid contains correct number of cells`` () =
        Assert.AreEqual(100, grid |> Seq.length)

    [<Test>]
    let ``countLiving returns expected value`` () =
        let generatePairs = (seq {0..4}, seq {0..1}) ||> Seq.allPairs  |> Seq.toList       
        let convertPairToCell = 
            List.map (
                fun point -> {
                    Point = point 
                    State = //Set one row to alive
                        match point with
                        | n when n |> getY = 1 -> Alive  
                        | _ -> Dead})
        let numberOfLiving = generatePairs |> convertPairToCell |> countLiving
        Assert.AreEqual(5, numberOfLiving)

    [<TestCase(0, 0, 3)>]
    [<TestCase(1, 0, 5)>]
    [<TestCase(2, 2, 8)>]
    [<TestCase(9, 9, 3)>]
    let ``getNeighborCount returns expected value`` x y expectedValue =
        let cell = aliveGrid |> Seq.find (fun cell -> cell.Point = (x,y))
        let aliveNeighborCount = cell.Point |> getNeighborCount aliveGrid
        Assert.AreEqual(expectedValue, aliveNeighborCount)

    (*
        Next several tests confirm known forms work.  
        Examples of these forms: https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
    *)
    let createGridFromStates = List.map (fun (x, y, state) -> {Point = (x,y); State = state})
    
    [<Test>]
    let ``nextGridState block returns expected result`` () =
        let block = """
----
-++-
-++-
----"""
        let startState = block |> parseStringToGrid
        let expectedState = startState
        let nextState = startState |> nextGridState
        Assert.AreEqual(expectedState, nextState)

    [<Test>]
    let ``nextGridState blinker returns expected result`` () =
        let blinkerState1 = """
-----
--+--
--+--
--+--
-----"""
        let blinkerState2 = """
-----
-----
-+++-
-----
-----"""
        let startState = blinkerState1 |> parseStringToGrid
        let expectedState = blinkerState2 |> parseStringToGrid
        let mutable grid = startState |> nextGridState
        Assert.AreEqual(expectedState, grid)
        grid <- grid |> nextGridState
        Assert.AreEqual(startState, grid)
