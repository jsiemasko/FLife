namespace FLife.App
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.displayBox = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnGenerate = new System.Windows.Forms.Button();
            this.hsFrame = new System.Windows.Forms.HScrollBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lblCurrentFrameLabel = new System.Windows.Forms.Label();
            this.lblCurrentFrameValue = new System.Windows.Forms.Label();
            this.nudGridSize = new System.Windows.Forms.NumericUpDown();
            this.lblGridSize = new System.Windows.Forms.Label();
            this.pbRenderingProgress = new System.Windows.Forms.ProgressBar();
            this.lblNumOfFrames = new System.Windows.Forms.Label();
            this.nudNumOfFrames = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGridSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFrames)).BeginInit();
            this.SuspendLayout();
            // 
            // displayBox
            // 
            this.displayBox.Location = new System.Drawing.Point(12, 12);
            this.displayBox.Name = "displayBox";
            this.displayBox.Size = new System.Drawing.Size(500, 500);
            this.displayBox.TabIndex = 0;
            this.displayBox.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(400, 569);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(112, 23);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // hsFrame
            // 
            this.hsFrame.LargeChange = 1;
            this.hsFrame.Location = new System.Drawing.Point(15, 513);
            this.hsFrame.Maximum = 0;
            this.hsFrame.Name = "hsFrame";
            this.hsFrame.Size = new System.Drawing.Size(497, 22);
            this.hsFrame.TabIndex = 2;
            this.hsFrame.ValueChanged += new System.EventHandler(this.HsFrame_ValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(486, 538);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(23, 23);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = ">";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // lblCurrentFrameLabel
            // 
            this.lblCurrentFrameLabel.AutoSize = true;
            this.lblCurrentFrameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentFrameLabel.Location = new System.Drawing.Point(15, 548);
            this.lblCurrentFrameLabel.Name = "lblCurrentFrameLabel";
            this.lblCurrentFrameLabel.Size = new System.Drawing.Size(45, 13);
            this.lblCurrentFrameLabel.TabIndex = 4;
            this.lblCurrentFrameLabel.Text = "Frame:";
            // 
            // lblCurrentFrameValue
            // 
            this.lblCurrentFrameValue.AutoSize = true;
            this.lblCurrentFrameValue.Location = new System.Drawing.Point(96, 548);
            this.lblCurrentFrameValue.Name = "lblCurrentFrameValue";
            this.lblCurrentFrameValue.Size = new System.Drawing.Size(24, 13);
            this.lblCurrentFrameValue.TabIndex = 5;
            this.lblCurrentFrameValue.Text = "0/0";
            // 
            // nudGridSize
            // 
            this.nudGridSize.Location = new System.Drawing.Point(88, 572);
            this.nudGridSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudGridSize.Name = "nudGridSize";
            this.nudGridSize.Size = new System.Drawing.Size(47, 20);
            this.nudGridSize.TabIndex = 6;
            this.nudGridSize.Value = new decimal(new int[] {
            35,
            0,
            0,
            0});
            // 
            // lblGridSize
            // 
            this.lblGridSize.AutoSize = true;
            this.lblGridSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridSize.Location = new System.Drawing.Point(20, 574);
            this.lblGridSize.Name = "lblGridSize";
            this.lblGridSize.Size = new System.Drawing.Size(62, 13);
            this.lblGridSize.TabIndex = 7;
            this.lblGridSize.Text = "Grid Size:";
            // 
            // pbRenderingProgress
            // 
            this.pbRenderingProgress.Location = new System.Drawing.Point(12, 254);
            this.pbRenderingProgress.Name = "pbRenderingProgress";
            this.pbRenderingProgress.Size = new System.Drawing.Size(500, 23);
            this.pbRenderingProgress.TabIndex = 8;
            this.pbRenderingProgress.Visible = false;
            // 
            // lblNumOfFrames
            // 
            this.lblNumOfFrames.AutoSize = true;
            this.lblNumOfFrames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumOfFrames.Location = new System.Drawing.Point(192, 574);
            this.lblNumOfFrames.Name = "lblNumOfFrames";
            this.lblNumOfFrames.Size = new System.Drawing.Size(78, 13);
            this.lblNumOfFrames.TabIndex = 10;
            this.lblNumOfFrames.Text = "# of Frames:";
            // 
            // nudNumOfFrames
            // 
            this.nudNumOfFrames.Location = new System.Drawing.Point(276, 572);
            this.nudNumOfFrames.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudNumOfFrames.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumOfFrames.Name = "nudNumOfFrames";
            this.nudNumOfFrames.Size = new System.Drawing.Size(47, 20);
            this.nudNumOfFrames.TabIndex = 9;
            this.nudNumOfFrames.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 599);
            this.Controls.Add(this.lblNumOfFrames);
            this.Controls.Add(this.nudNumOfFrames);
            this.Controls.Add(this.pbRenderingProgress);
            this.Controls.Add(this.lblGridSize);
            this.Controls.Add(this.nudGridSize);
            this.Controls.Add(this.lblCurrentFrameValue);
            this.Controls.Add(this.lblCurrentFrameLabel);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.hsFrame);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.displayBox);
            this.Name = "MainForm";
            this.Text = "F# Game of Life";
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGridSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumOfFrames)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox displayBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.HScrollBar hsFrame;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lblCurrentFrameLabel;
        private System.Windows.Forms.Label lblCurrentFrameValue;
        private System.Windows.Forms.NumericUpDown nudGridSize;
        private System.Windows.Forms.Label lblGridSize;
        private System.Windows.Forms.ProgressBar pbRenderingProgress;
        private System.Windows.Forms.Label lblNumOfFrames;
        private System.Windows.Forms.NumericUpDown nudNumOfFrames;
    }
}

