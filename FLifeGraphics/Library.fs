namespace FLife

open SkiaSharp
open FLife.Cell
open System.IO

module Graphics =
    let renderGrid width height strokeWidth (xScale : int) (yScale : int) (grid:Cell list) =
        use surface = new SKImageInfo(width, height) |> SKSurface.Create
        let canvas = surface.Canvas

        let setColor (color:SKColor) (paint:SKPaint) =
            paint.Color <- color
            paint.IsAntialias <- false
            paint.StrokeWidth <- strokeWidth
            paint.Style <- SKPaintStyle.Stroke
            paint
        let palette = 
            [80; 100; 130; 170; 210; 230] 
            |> List.map (fun red -> 
                new SKPaint() 
                |> setColor (new SKColor(byte red, byte 0, byte 0)))

        let liveCells = grid |> List.where(fun cell -> cell.State = true)
        
        let draw (cell:Cell) = 
            let x = ((cell |> cellX) * xScale) |> float32
            let y = ((cell |> cellY) * yScale) |> float32
            let height = 1 |> float32
            let width = 1 |> float32
            let generationColor = 
                match cell.Generations with
                | g when g < 1 -> palette.[0]
                | g when g < 2 -> palette.[1]
                | g when g < 4 -> palette.[2]
                | g when g < 6 -> palette.[3]
                | g when g < 10 -> palette.[4]
                | _ -> palette.[5]
            canvas.DrawRect(x, y, height, width, generationColor)

        canvas.Clear SKColors.Black
        liveCells |> List.iter draw
        use image = surface.Snapshot()
        use data = image.Encode(SKEncodedImageFormat.Png, 100)
        new MemoryStream(data.ToArray())
