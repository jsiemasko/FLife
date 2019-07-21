module FLife.CellTests

open FLife.Cell
open FLife.Grid
open NUnit.Framework

module PointTests =
    [<Test>]
    let ``getX returns X value`` () =
        let x = (1,2) |> getX
        Assert.AreEqual(1, x)

    [<Test>]
    let ``getY returns Y value`` () =
        let y = (1,2) |> getY
        Assert.AreEqual(2, y)

    [<Test>]
    let ``addPoints returns expected value`` () =
        let point1 = 1,2
        let point2 = 3,4
        let result = addPoints point1 point2
        Assert.AreEqual((4,6), result)

module StatusTests =
    [<Test>]
    let ``isAlive returns true when alive`` () =
        let aliveStatus = Alive |> isAlive
        Assert.True(aliveStatus)

    [<Test>]
    let ``isAlive returns false when dead`` () =
        let aliveStatus = Dead |> isAlive
        Assert.False(aliveStatus)

module CellTests =
    [<Test>]
    let ``createCell creates with supplied point`` () =
        let point = (1, 2)
        let cell = point |> createCell
        Assert.AreEqual(point, cell.Point)
        
    [<Test>]
    let ``createCell status equals default status`` () =
        let point = (1, 2)
        let cell = point |> createCell
        Assert.AreEqual(defaultCellStatus, cell.Status)

    [<Test>]
    let ``cellX returns X value`` () =
        let x = (1,2) |> createCell |> cellX
        Assert.AreEqual(1, x)

    [<Test>]
    let ``cellY returns Y value`` () =
        let y = (1,2) |> createCell |> cellY
        Assert.AreEqual(2, y)

    [<Test>]
    let ``isCellAlive returns true when alive`` () =
        let aliveStatus = {Point = 0,0; Status = Alive} |> isCellAlive
        Assert.True(aliveStatus)

    [<Test>]
    let ``isCellAlive returns false when dead`` () =
        let aliveStatus = {Point = 0,0; Status = Dead} |> isCellAlive
        Assert.False(aliveStatus)

    [<TestCase(0, false)>]
    [<TestCase(1, false)>]
    [<TestCase(2, true)>]
    [<TestCase(3, true)>]
    [<TestCase(4, false)>]
    [<TestCase(5, false)>]
    [<TestCase(6, false)>]
    [<TestCase(7, false)>]
    [<TestCase(8, false)>]
    let ``nextCellStatus alive produces expected status`` neighbors expectedStatus=
        let cell = {Point = (0,0); Status = Alive}
        let newStatus = cell |> nextCellStatus neighbors |> isCellAlive
        Assert.AreEqual(expectedStatus, newStatus)

    [<TestCase(0, false)>]
    [<TestCase(1, false)>]
    [<TestCase(2, false)>]
    [<TestCase(3, true)>]
    [<TestCase(4, false)>]
    [<TestCase(5, false)>]
    [<TestCase(6, false)>]
    [<TestCase(7, false)>]
    [<TestCase(8, false)>]
    let ``nextCellStatus dead produces expected status`` neighbors expectedStatus=
        let cell = {Point = (0,0); Status = Dead}
        let newStatus = cell |> nextCellStatus neighbors |> isCellAlive
        Assert.AreEqual(expectedStatus, newStatus)

    [<Test>]
    let ``pointsToFind returns expected results`` () =
        let pointsToFind = [(0,0); (1,2); (3,4)] |> List.toSeq
        let grid = createGrid 10 10
        let cellResults = pointsToFind |> getCells grid 
        let pointResults = cellResults |> Seq.map (fun x -> x.Point)
        Assert.AreEqual(pointsToFind, pointResults)