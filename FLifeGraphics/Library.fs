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
        use color1 = new SKPaint() |> setColor (new SKColor(byte 80, byte 0, byte 0))
        use color2 = new SKPaint() |> setColor (new SKColor(byte 100, byte 0, byte 0))
        use color3 = new SKPaint() |> setColor (new SKColor(byte 130, byte 0, byte 0))
        use color4 = new SKPaint() |> setColor (new SKColor(byte 170, byte 0, byte 0))
        use color5 = new SKPaint() |> setColor (new SKColor(byte 210, byte 0, byte 0))
        use color6 = new SKPaint() |> setColor (new SKColor(byte 250, byte 0, byte 0))

        let liveCells = grid |> List.where(fun cell -> cell.State = Alive)
        let maxGenerations = liveCells |> List.map (fun cell -> cell.Generations) |> List.max
        
        let draw (cell:Cell) = 
            let x = (cell |> cellX) * xScale
            let y = (cell |> cellY) * yScale
            canvas.DrawRect(
                x |> float32, 
                y |> float32, 
                1 |> float32,
                1 |> float32, 
                match cell.Generations with
                | 0 -> color1
                | 1 -> color2
                | 2 -> color3
                | 3 -> color4
                | 4 -> color5
                | _ -> color6
                )

        canvas.Clear SKColors.Black
        liveCells |> List.iter draw
        use image = surface.Snapshot()
        use data = image.Encode(SKEncodedImageFormat.Png, 100)
        new MemoryStream(data.ToArray())
