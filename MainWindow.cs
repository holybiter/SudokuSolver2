using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver2
{
    public partial class MainWindow : Form
    {
        private List<Cell> cells; 

        public MainWindow()
        {
            InitializeComponent();
            cells = new List<Cell>();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var cellTextBox = CreateTextBoxForCell(i, j);
                    var possibleValuesLabel = CreateLabelForCell(i, j);
                    var addedCell = new Cell(cellTextBox, possibleValuesLabel);
                    cells.Add(addedCell);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                var line = new Cluster();
                for (int j = 0; j < 9; j++)
                {
                    line.AddCell(cells[9 * i + j]);
                    cells[9 * i + j].BindLine(line);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                var column = new Cluster();
                for (int j = 0; j < 9; j++)
                {
                    column.AddCell(cells[9 * j + i]);
                    cells[9 * j + i].BindColumn(column);
                }
            }

            for (int w = 0; w < 9; w++)
            {
                var area = new Cluster();
                for (int v = 0; v < 9; v++)
                {
                    int i = (w / 3) * 3 + (v / 3);
                    int j = (v % 3) + (w % 3) * 3;
                    area.AddCell(cells[9 * i + j]);
                    cells[9 * i + j].BindArea(area);
                }
            }
        }

        private TextBox CreateTextBoxForCell(int i, int j)
        {
            TextBox newCell = new TextBox();
            int initialX = 17, initialY = i == 8 ? 10 : 12;
            double intervalX = 66.6666, intervalY = 66.6666;
            var location = new Point((int)Math.Round(j * intervalX + initialX), (int)Math.Round(i * intervalY + initialY));

            newCell.Location = location;
            newCell.Text = String.Empty;
            newCell.BorderStyle = BorderStyle.None;
            newCell.TextAlign = HorizontalAlignment.Center;
            newCell.Font = new Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            newCell.Size = new Size(33, 46);
            newCell.Name = "TextBox" + i.ToString() + j.ToString();
            newCell.KeyPress += new KeyPressEventHandler(ControlAllowedCharactersInCell);
            this.Controls.Add(newCell);

            return newCell;
        }

        private Label CreateLabelForCell(int i, int j)
        {
            Label newLabel = new Label();
            int initialX = 12, initialY = i == 8 ? 55 : 57;
            double intervalX = 66.6666, intervalY = 66.6666;
            var location = new Point((int)Math.Round(j * intervalX + initialX), (int)Math.Round(i * intervalY + initialY));

            newLabel.AutoSize = true;
            newLabel.BackColor = Color.Transparent;
            newLabel.TextAlign = ContentAlignment.TopCenter;
            newLabel.Visible = ShowPossibleValuesCheckBox.Checked;
            newLabel.Font = new Font("Microsoft Sans Serif", 6F, FontStyle.Bold);
            newLabel.Location = location;
            newLabel.Name = "PossibleValuesLabel" + i.ToString() + j.ToString();
            newLabel.Size = new Size(50, 9);
            newLabel.TabIndex = 3;
            newLabel.Text = String.Empty;
            this.Controls.Add(newLabel);

            return newLabel;
        }

        private void ControlAllowedCharactersInCell(object sender, KeyPressEventArgs e)
        {
            var currentTextBox = (TextBox)sender;
            // Check for a naughty character in the KeyDown event.
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[1-9 ]") && e.KeyChar != (char)Keys.Back)
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }
            else
            {
                if (e.KeyChar.ToString() != " " && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

                MoveFocusToNextControl(currentTextBox);

                currentTextBox.Text = String.Empty;
            }
        }

        private void MoveFocusToNextControl(TextBox currentTextBox)
        {
            int currentI = currentTextBox.Name[7] - '0';
            int currentJ = currentTextBox.Name[8] - '0';
            if (currentI < 8 || currentJ < 8)
            {
                var nextI = currentI;
                var nextJ = currentJ;
                if (currentJ == 8)
                {
                    nextI++;
                    nextJ = 0;
                }
                else
                {
                    nextJ++;
                }
                var nextTextBox = (TextBox)this.Controls.Find("TextBox" + nextI + nextJ, false).First();
                nextTextBox.Focus();
            }
            else
            {
                FinishSetupButton.Focus();
            }
        }

        private void FinishSetupButton_Click(object sender, EventArgs e)
        {
            ShowPossibleValuesCheckBox.Visible = true;
            FillButton.Enabled = true;
            FillButton.Visible = true;
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].MakeNoneInteractable();
            }

            Button button = ((Button)sender);
            button.Visible = false;
            button.Enabled = false;
            //for (int i = 0; i < cells.Count; i++)
            //{
            //    cells[i].UpdatePossibleValues();
            //}
        }

        private void FillCell(object sender, EventArgs e)
        {
            //for (int i = 0; i < cells.Count; i++)
            //{
            //    //cells[i].UpdatePossibleValues();
            //    cells[i].TryFill();
            //}

            button0_Click(sender, e);
            button1_Click(sender, e);
            button0_Click(sender, e);

            button0_Click(sender, e);
            button2_Click(sender, e);
            button0_Click(sender, e);

            button0_Click(sender, e);
            button3_Click(sender, e);
            button0_Click(sender, e);
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            ShowPossibleValuesCheckBox.Checked = false;
            ShowPossibleValuesCheckBox.Visible = false;
            FillButton.Enabled = false;
            FillButton.Visible = false;
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].MakeInteractable();
                cells[i].ResetToInitialValue();
            }

            FinishSetupButton.Visible = true;
            FinishSetupButton.Enabled = true;
        }



        private void ResetButton_Click(object sender, EventArgs e)
        {
            ShowPossibleValuesCheckBox.Checked = false;
            ShowPossibleValuesCheckBox.Visible = false;
            FillButton.Enabled = false;
            FillButton.Visible = false;
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].MakeInteractable();
                cells[i].ClearValue();
            }

            FinishSetupButton.Visible = true;
            FinishSetupButton.Enabled = true;
        }

        private void ShowPossibleValuesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].TogglePossibleValuesLabelVisibility(((CheckBox)sender).Checked);
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].UpdatePossibleValues();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].FillTheOnlyPossibleNumber();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].FillTheOnlyPossibleNumberForCluster();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].UpdatePossibleValuesToExcludeBlaBla();
            }
        }
    }
}
