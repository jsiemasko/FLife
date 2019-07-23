using SkiaSharp;
using System;
using System.Drawing;
using System.IO;
using static FLife.Grid;
using static FLife.File;
using static FLife.Cell;
using static FLife.Cell.State.CellState;
using System.Windows.Forms;
using static SkiaSharp.SKColors;
using Microsoft.FSharp.Collections;
using static SkiaSharp.SKPaintStyle;
using System.Linq;

namespace FLifeApp
{
    public partial class MainForm : Form
    {
        private static readonly Random _random = new Random();
        private FSharpList<Cell.Cell> _grid = notQuiteRandom;
        private int _xScale = 15;
        private int _yScale = 15;
        private int _strokeWidth = 12;

        public MainForm()
            => InitializeComponent();

        private void Timer1_Tick(object sender, EventArgs e)
        {
            _grid = nextGridState(_grid);
            DisplayGrid(_grid);
        }

        private void DisplayGrid(FSharpList<Cell.Cell> grid)
        {
            var imageInfo = new SKImageInfo(this.Width, this.Height);
            using (var surface = SKSurface.Create(imageInfo))
            {
                var canvas = surface.Canvas;
                canvas.Clear(Black);
                var colorPredefined = Red;

                using (var paint = new SKPaint())
                {
                    paint.Color = Red;
                    paint.IsAntialias = false;
                    paint.StrokeWidth = _strokeWidth;
                    paint.Style = Stroke;
                    var liveCells = _grid.Where(cell => cell.State == Alive);
                    foreach (var cell in liveCells)
                    {
                        canvas.DrawRect(Cell.cellX(cell) * _xScale, Cell.cellY(cell) * _yScale, 1, 1, paint);
                    }
                }
                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var mStream = new MemoryStream(data.ToArray()))
                {
                    displayBox.Image = new Bitmap(mStream, false);
                }
            }
        }
    }
}