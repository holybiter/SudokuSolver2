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

            // create all cells
            const int initialX = 17, initialY = 12, intervalX = 67, intervalY = 67;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var location = new System.Drawing.Point(j * intervalX + initialX, i * intervalY + initialY);
                    TextBox newCell = new TextBox();
                    newCell.Location = location;
                    newCell.Text = String.Empty;
                    //newCell.Text = "5";
                    //newCell.Enabled = false;
                    //newCell.BackColor = Color.White;
                    newCell.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    newCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
                    newCell.Size = new System.Drawing.Size(33, 46);
                    newCell.Name = "TextBox" + i.ToString() + j.ToString();
                    newCell.KeyPress += new KeyPressEventHandler(ControlAllowedCharactersInCell);
                    this.Controls.Add(newCell);

                    var addedCell = new Cell(newCell);
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

            //var line = new Cluster();
            //addedCell.BindLine(line);
            //line.AddCell(addedCell);
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
            FillButton.Enabled = true;
            FillButton.Visible = true;
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].MakeNoneInteractable();
            }

            Button button = ((Button)sender);
            button.Visible = false;
            button.Enabled = false;
        }

        private void FillCell(object sender, EventArgs e)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].UpdatePossibleValues();
                cells[i].TryFill();
            }
        }
    }
}
