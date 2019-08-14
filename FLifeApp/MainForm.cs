using Microsoft.FSharp.Collections;
using System;
using System.Drawing;
using System.Windows.Forms;
using static FLife.Graphics;
using static FLife.Grid;

namespace FLife.App
{
    public partial class MainForm : Form
    {
        private static readonly Random _random = new Random();
        private FSharpList<FLife.Cell.Cell>[] _gridFrames = new FSharpList<FLife.Cell.Cell>[0];
        private int _numOfFramesToGenerate = 100;
        private int _gridSize = 10;

        public MainForm()
            => InitializeComponent();

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (_gridFrames.Length == 0)
                GenerateFrames();
            hsFrame.Value = (hsFrame.Value + 1) % _gridFrames.Length;
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
            => GenerateFrames();

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
            => SetRunState(checkBox1.Checked);

        private void HsFrame_ValueChanged(object sender, EventArgs e)
            => DisplayFrame(_gridFrames[hsFrame.Value]);

        private void DisplayFrame(FSharpList<Cell.Cell> frame)
        {
            int xScale = (int)(displayBox.Width / _gridSize);
            int yScale = (int)(displayBox.Height / _gridSize);
            int stroke = Math.Max(xScale, yScale) - 3;
            lblCurrentFrameValue.Text = $"{hsFrame.Value}/{_gridFrames.Length - 1}";
            displayBox.Image = new Bitmap(renderGrid(displayBox.Width, displayBox.Height, stroke, xScale, yScale, frame));
        }

        private void SetRunState(bool runState)
        {
            checkBox1.Checked = runState;
            timer1.Enabled = runState;
        }

        private void GenerateFrames()
        {
            _gridSize = (int)nudGridSize.Value;
            _numOfFramesToGenerate = (int)nudNumOfFrames.Value;
            SetRunState(false);
            pbRenderingProgress.Visible = true;
            pbRenderingProgress.Maximum = _numOfFramesToGenerate;
            var startingGrid = createGridWithRandomState(_gridSize, _gridSize, _random);
            _gridFrames = new FSharpList<FLife.Cell.Cell>[_numOfFramesToGenerate];
            _gridFrames[0] = startingGrid;
            for (int currentFrame = 1; currentFrame < _numOfFramesToGenerate; currentFrame++)
            {
                pbRenderingProgress.Value = currentFrame;
                var newFrame = nextGridState(_gridFrames[currentFrame - 1]);
                DisplayFrame(newFrame);
                _gridFrames[currentFrame] = newFrame;
                hsFrame.Maximum = currentFrame;
                hsFrame.LargeChange = 1;
                hsFrame.SmallChange = 1;
                hsFrame.Value = currentFrame;
                displayBox.Invalidate();
                displayBox.Update();
                displayBox.Refresh();
                Application.DoEvents();
            }
            pbRenderingProgress.Visible = false;
            SetRunState(true);
        }
    }
}