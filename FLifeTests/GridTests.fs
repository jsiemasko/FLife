module FLife.GridTests

open FLife.Cell
open FLife.Grid
open NUnit.Framework

module GridTests =
    let grid = createGrid 10 10
    let aliveGrid = //For tests that don't want to rely on default alive state
        let allPoints = {0 .. 9} |> Seq.allPairs {0.. 9} 
        allPoints |> Seq.map createCell

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
        let generatePairs = (seq {0..4}, seq {0..1}) ||> Seq.allPairs         
        let convertPairToCell = 
            Seq.map (
                fun point -> {
                    Point = point 
                    Status = //Set one row to alive
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
    let createGridFromStates = List.toSeq >> Seq.map (fun (x, y, status) -> {Point = (x,y); Status = status})
    
    [<Test>]
    let ``nextGridStatus block returns expected result`` () =
        let startState = 
            [ (0,0,Dead) ; (0,1,Dead)  ; (0,2,Dead)  ; (0,3,Dead)
              (1,0,Dead) ; (1,1,Alive) ; (1,2,Alive) ; (1,3,Dead)
              (2,0,Dead) ; (2,1,Alive) ; (2,2,Alive) ; (2,3,Dead)
              (3,0,Dead) ; (3,1,Dead)  ; (3,2,Dead)  ; (3,3,Dead) ]
            |> createGridFromStates
        let expectedState = 
            [ (0,0,Dead) ; (0,1,Dead)  ; (0,2,Dead)  ; (0,3,Dead)
              (1,0,Dead) ; (1,1,Alive) ; (1,2,Alive) ; (1,3,Dead)
              (2,0,Dead) ; (2,1,Alive) ; (2,2,Alive) ; (2,3,Dead)
              (3,0,Dead) ; (3,1,Dead)  ; (3,2,Dead)  ; (3,3,Dead) ]
            |> createGridFromStates
        let nextState = startState |> nextGridStatus
        Assert.AreEqual(expectedState, nextState)

    [<Test>]
    let ``nextGridStatus blinker returns expected result`` () =
        let startState = 
            [ (0,0,Dead) ; (0,1,Dead) ; (0,2,Dead)  ; (0,3,Dead) ; (0,4,Dead)
              (1,0,Dead) ; (1,1,Dead) ; (1,2,Alive) ; (1,3,Dead) ; (1,4,Dead)
              (2,0,Dead) ; (2,1,Dead) ; (2,2,Alive) ; (2,3,Dead) ; (2,4,Dead)
              (3,0,Dead) ; (3,1,Dead) ; (3,2,Alive) ; (3,3,Dead) ; (3,4,Dead)
              (4,0,Dead) ; (4,1,Dead) ; (4,2,Dead)  ; (4,3,Dead) ; (4,4,Dead) ]
            |> createGridFromStates
        let expectedState = 
            [ (0,0,Dead) ; (0,1,Dead)  ; (0,2,Dead)  ; (0,3,Dead)  ; (0,4,Dead)
              (1,0,Dead) ; (1,1,Dead)  ; (1,2,Dead)  ; (1,3,Dead)  ; (1,4,Dead)
              (2,0,Dead) ; (2,1,Alive) ; (2,2,Alive) ; (2,3,Alive) ; (2,4,Dead)
              (3,0,Dead) ; (3,1,Dead)  ; (3,2,Dead)  ; (3,3,Dead)  ; (3,4,Dead)
              (4,0,Dead) ; (4,1,Dead)  ; (4,2,Dead)  ; (4,3,Dead)  ; (4,4,Dead) ]
            |> createGridFromStates
        let nextState = startState |> nextGridStatus
        Assert.AreEqual(expectedState, nextState)