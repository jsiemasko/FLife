module FLife.GridTests

open FLife.Cell
open FLife.Grid
open NUnit.Framework

module GridTests =
    [<Test>]
    let ``createGrid starts at 0,0`` () =
        let grid = createGrid 10 10
        let result = grid |> Seq.exists (fun cell -> cell.Point = (0,0))
        Assert.True(result)

    [<Test>]
    let ``createGrid ends X at expected value`` () =
        let grid = createGrid 10 10
        let xOverExpected = 
            grid |> Seq.exists (fun cell -> cell.Point |> getX > 9)
        Assert.False(xOverExpected)

    [<Test>]
    let ``createGrid ends Y at expected value`` () =
        let grid = createGrid 10 10
        let yOverExpected = 
            grid |> Seq.exists (fun cell -> cell.Point |> getY > 9)
        Assert.False(yOverExpected)

    [<Test>]
    let ``createGrid contains correct number of cells`` () =
        let grid = createGrid 10 10
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