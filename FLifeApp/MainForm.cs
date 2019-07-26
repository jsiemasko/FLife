using Microsoft.FSharp.Collections;
using System;
using System.Drawing;
using System.Windows.Forms;
using static FLife.File;
using static FLife.Grid;
using static FLife.Graphics;

namespace FLife.App
{
    public partial class MainForm : Form
    {
        private FSharpList<FLife.Cell.Cell> _grid = notQuiteRandom;
        private static readonly Random _random = new Random();
        private int _rows = 50;
        private int _cols = 50;

        public MainForm()
        {
            _grid = createGridWithRandomState(_rows, _cols, _random);
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            var xScale = Width / _rows;
            var yScale = Height / _cols;
            var stroke = Math.Min(xScale, yScale) - 3;
            displayBox.Image = new Bitmap(renderGrid(Width, Height, stroke, xScale, yScale, _grid));
            _grid = nextGridState(_grid);
        }

        private void DisplayBox_Click(object sender, EventArgs e)
        {
            _grid = createGridWithRandomState(_rows, _cols, _random);
        }
    }
}