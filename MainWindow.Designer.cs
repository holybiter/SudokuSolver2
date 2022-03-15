namespace SudokuSolver2
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.ExampleCellTextBox = new System.Windows.Forms.TextBox();
            this.FinishSetupButton = new System.Windows.Forms.Button();
            this.FillButton = new System.Windows.Forms.Button();
            this.RestartButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.ExamplePossibleValuesLabel = new System.Windows.Forms.Label();
            this.ShowPossibleValuesCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExampleCellTextBox
            // 
            this.ExampleCellTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ExampleCellTextBox.Enabled = false;
            this.ExampleCellTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.ExampleCellTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ExampleCellTextBox.Location = new System.Drawing.Point(17, 12);
            this.ExampleCellTextBox.Name = "ExampleCellTextBox";
            this.ExampleCellTextBox.Size = new System.Drawing.Size(33, 46);
            this.ExampleCellTextBox.TabIndex = 0;
            this.ExampleCellTextBox.Text = "5";
            this.ExampleCellTextBox.Visible = false;
            this.ExampleCellTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlAllowedCharactersInCell);
            // 
            // FinishSetupButton
            // 
            this.FinishSetupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.FinishSetupButton.Location = new System.Drawing.Point(662, 37);
            this.FinishSetupButton.Name = "FinishSetupButton";
            this.FinishSetupButton.Size = new System.Drawing.Size(143, 38);
            this.FinishSetupButton.TabIndex = 1;
            this.FinishSetupButton.Text = "Finish setup";
            this.FinishSetupButton.UseVisualStyleBackColor = true;
            this.FinishSetupButton.Click += new System.EventHandler(this.FinishSetupButton_Click);
            // 
            // FillButton
            // 
            this.FillButton.Enabled = false;
            this.FillButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.FillButton.Location = new System.Drawing.Point(662, 81);
            this.FillButton.Name = "FillButton";
            this.FillButton.Size = new System.Drawing.Size(143, 38);
            this.FillButton.TabIndex = 2;
            this.FillButton.Text = "Fill";
            this.FillButton.UseVisualStyleBackColor = true;
            this.FillButton.Visible = false;
            this.FillButton.Click += new System.EventHandler(this.FillCell);
            // 
            // RestartButton
            // 
            this.RestartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.RestartButton.Location = new System.Drawing.Point(662, 553);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(68, 38);
            this.RestartButton.TabIndex = 2;
            this.RestartButton.Text = "Restart";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.ResetButton.Location = new System.Drawing.Point(737, 553);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(68, 38);
            this.ResetButton.TabIndex = 2;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // ExamplePossibleValuesLabel
            // 
            this.ExamplePossibleValuesLabel.AutoSize = true;
            this.ExamplePossibleValuesLabel.BackColor = System.Drawing.Color.Transparent;
            this.ExamplePossibleValuesLabel.Enabled = false;
            this.ExamplePossibleValuesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold);
            this.ExamplePossibleValuesLabel.Location = new System.Drawing.Point(12, 59);
            this.ExamplePossibleValuesLabel.Name = "ExamplePossibleValuesLabel";
            this.ExamplePossibleValuesLabel.Size = new System.Drawing.Size(50, 9);
            this.ExamplePossibleValuesLabel.TabIndex = 3;
            this.ExamplePossibleValuesLabel.Text = "123456789";
            this.ExamplePossibleValuesLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ExamplePossibleValuesLabel.Visible = false;
            // 
            // ShowPossibleValuesCheckBox
            // 
            this.ShowPossibleValuesCheckBox.AutoSize = true;
            this.ShowPossibleValuesCheckBox.Location = new System.Drawing.Point(662, 125);
            this.ShowPossibleValuesCheckBox.Name = "ShowPossibleValuesCheckBox";
            this.ShowPossibleValuesCheckBox.Size = new System.Drawing.Size(128, 17);
            this.ShowPossibleValuesCheckBox.TabIndex = 4;
            this.ShowPossibleValuesCheckBox.Text = "Show possible values";
            this.ShowPossibleValuesCheckBox.UseVisualStyleBackColor = true;
            this.ShowPossibleValuesCheckBox.Visible = false;
            this.ShowPossibleValuesCheckBox.CheckedChanged += new System.EventHandler(this.ShowPossibleValuesCheckBox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(662, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 38);
            this.button1.TabIndex = 5;
            this.button1.Text = "Update Possible Values";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button0_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(662, 265);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 38);
            this.button2.TabIndex = 6;
            this.button2.Text = "Fill 2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(662, 309);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(143, 38);
            this.button3.TabIndex = 7;
            this.button3.Text = "Fill 3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.button4.Location = new System.Drawing.Point(662, 221);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(143, 38);
            this.button4.TabIndex = 6;
            this.button4.Text = "Fill 1";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.button5.Location = new System.Drawing.Point(662, 353);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(143, 38);
            this.button5.TabIndex = 7;
            this.button5.Text = "Fill 4";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button4_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(866, 621);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ShowPossibleValuesCheckBox);
            this.Controls.Add(this.ExamplePossibleValuesLabel);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.RestartButton);
            this.Controls.Add(this.FillButton);
            this.Controls.Add(this.FinishSetupButton);
            this.Controls.Add(this.ExampleCellTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.RightToLeftLayout = true;
            this.Text = "Sudoku Solver 2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ExampleCellTextBox;
        private System.Windows.Forms.Button FinishSetupButton;
        private System.Windows.Forms.Button FillButton;
        private System.Windows.Forms.Button RestartButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Label ExamplePossibleValuesLabel;
        private System.Windows.Forms.CheckBox ShowPossibleValuesCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

