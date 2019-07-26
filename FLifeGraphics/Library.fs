namespace FLife

open SkiaSharp
open FLife.Cell
open System.IO

module Graphics =
    let renderGrid width height strokeWidth (xScale : int) (yScale : int) (grid:Cell list) =
        use surface = new SKImageInfo(width, height) |> SKSurface.Create
        let canvas = surface.Canvas
        use paint = new SKPaint()
        paint.Color <- SKColors.White
        paint.IsAntialias <- false
        paint.StrokeWidth <- strokeWidth
        paint.Style <- SKPaintStyle.Stroke
        let liveCells = grid |> List.where(fun cell -> cell.State = Alive)
        let draw (point:Point) = 
            canvas.DrawRect(
                point |> getX |> float32, 
                point |> getY |> float32, 
                1 |> float32, 
                1 |> float32, 
                paint)

        canvas.Clear SKColors.Black
        liveCells 
        |> List.iter (fun cell ->
            let x = (cell |> cellX) * xScale
            let y = (cell |> cellY) * yScale
            draw (x,y) )
        use image = surface.Snapshot()
        use data = image.Encode(SKEncodedImageFormat.Png, 100)
        new MemoryStream(data.ToArray())
