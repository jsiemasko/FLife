module FLife.CellTests

open FLife.Cell
open FLife.Grid
open NUnit.Framework

module PointTests =
    [<Test>]
    let ``addPoints returns expected value`` () =
        let point1 = 1,2
        let point2 = 3,4
        let result = addPoints point1 point2
        Assert.AreEqual((4,6), result)

module CellTests =
    [<Test>]
    let ``createCell creates with supplied point`` () =
        let point = (1, 2)
        let cell = point |> createCell
        Assert.AreEqual(point, cell.Point)
        
    [<Test>]
    let ``createCell state equals default state`` () =
        let point = (1, 2)
        let cell = point |> createCell
        Assert.AreEqual(defaultCellState, cell.State)

    [<Test>]
    let ``cellX returns X value`` () =
        let x = (1,2) |> createCell |> cellX
        Assert.AreEqual(1, x)

    [<Test>]
    let ``cellY returns Y value`` () =
        let y = (1,2) |> createCell |> cellY
        Assert.AreEqual(2, y)

    [<TestCase(0, false)>]
    [<TestCase(1, false)>]
    [<TestCase(2, true)>]
    [<TestCase(3, true)>]
    [<TestCase(4, false)>]
    [<TestCase(5, false)>]
    [<TestCase(6, false)>]
    [<TestCase(7, false)>]
    [<TestCase(8, false)>]
    let ``nextCellState alive produces expected state`` neighbors expectedState=
        let cell = {Point = (0,0); State = true; Generations = 0}
        let newState = cell |> nextCellState neighbors |> (fun cell -> cell.State)
        Assert.AreEqual(expectedState, newState)

    [<TestCase(0, false)>]
    [<TestCase(1, false)>]
    [<TestCase(2, false)>]
    [<TestCase(3, true)>]
    [<TestCase(4, false)>]
    [<TestCase(5, false)>]
    [<TestCase(6, false)>]
    [<TestCase(7, false)>]
    [<TestCase(8, false)>]
    let ``nextCellState dead produces expected state`` neighbors expectedState=
        let cell = {Point = (0,0); State = false; Generations = 0}
        let newState = cell |> nextCellState neighbors |> (fun cell -> cell.State)
        Assert.AreEqual(expectedState, newState)

    [<Test>]
    let ``pointsToFind returns expected results`` () =
        let pointsToFind = [(0,0); (1,2); (3,4)] |> List.toSeq
        let grid = createGrid 10 10
        let cellResults = pointsToFind |> getCells grid 
        let pointResults = cellResults |> Seq.map (fun x -> x.Point)
        Assert.AreEqual(pointsToFind, pointResults)