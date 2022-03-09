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
            this.textBox00 = new System.Windows.Forms.TextBox();
            this.FinishSetupButton = new System.Windows.Forms.Button();
            this.FillButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox00
            // 
            this.textBox00.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox00.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.textBox00.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox00.Location = new System.Drawing.Point(17, 12);
            this.textBox00.Name = "textBox00";
            this.textBox00.Size = new System.Drawing.Size(33, 46);
            this.textBox00.TabIndex = 0;
            this.textBox00.Visible = false;
            this.textBox00.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ControlAllowedCharactersInCell);
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
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(866, 621);
            this.Controls.Add(this.FillButton);
            this.Controls.Add(this.FinishSetupButton);
            this.Controls.Add(this.textBox00);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.RightToLeftLayout = true;
            this.Text = "Sudoku Solver 2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox00;
        private System.Windows.Forms.Button FinishSetupButton;
        private System.Windows.Forms.Button FillButton;
    }
}

