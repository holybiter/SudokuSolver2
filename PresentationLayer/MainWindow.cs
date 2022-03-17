using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SudokuSolver2
{
    public partial class MainWindow : Form
    {
        private Sudoku currentSudoku;

        private List<TextBox> cellTextBoxes;
        private List<Label> cellLabels;

        public MainWindow()
        {
            InitializeComponent();
            cellTextBoxes = new List<TextBox>();
            cellLabels = new List<Label>();
            currentSudoku = new Sudoku();
            InitializeCellTextBoxes();
            InitializeCellLabels();

            UpdateComboBoxContentFromDB();
        }

        private void InitializeCellTextBoxes()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox newCellTextBox = new TextBox();
                    int initialX = 17, initialY = i == 8 ? 10 : 12;
                    double intervalX = 66.6666, intervalY = 66.6666;
                    var location = new Point((int)Math.Round(j * intervalX + initialX), (int)Math.Round(i * intervalY + initialY));

                    newCellTextBox.Location = location;
                    newCellTextBox.Text = String.Empty;
                    newCellTextBox.BorderStyle = BorderStyle.None;
                    newCellTextBox.TextAlign = HorizontalAlignment.Center;
                    newCellTextBox.Font = new Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
                    newCellTextBox.Size = new Size(33, 46);
                    newCellTextBox.Name = "TextBox" + i.ToString() + j.ToString();
                    newCellTextBox.PreviewKeyDown += new PreviewKeyDownEventHandler(ArrowKeyPressedInCellTextbox);
                    newCellTextBox.KeyPress += new KeyPressEventHandler(KeyPressedAllowOnlyDigits);

                    Controls.Add(newCellTextBox);
                    cellTextBoxes.Add(newCellTextBox);
                }
            }
        }

        private void InitializeCellLabels()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Label newCellLabel = new Label();
                    int initialX = 12, initialY = i == 8 ? 55 : 57;
                    double intervalX = 66.6666, intervalY = 66.6666;
                    var location = new Point((int)Math.Round(j * intervalX + initialX), (int)Math.Round(i * intervalY + initialY));

                    newCellLabel.AutoSize = true;
                    newCellLabel.BackColor = Color.Transparent;
                    newCellLabel.TextAlign = ContentAlignment.TopCenter;
                    newCellLabel.Visible = ShowPossibleValuesCheckBox.Checked;
                    newCellLabel.Font = new Font("Microsoft Sans Serif", 6F, FontStyle.Bold);
                    newCellLabel.Location = location;
                    newCellLabel.Name = "PossibleValuesLabel" + i.ToString() + j.ToString();
                    newCellLabel.Size = new Size(50, 9);
                    newCellLabel.TabIndex = 3;
                    newCellLabel.Text = String.Empty;
                    
                    Controls.Add(newCellLabel);
                    cellLabels.Add(newCellLabel);
                }
            }
        }

        private void KeyPressedAllowOnlyDigits(object sender, KeyPressEventArgs e)
        {
            var currentTextBox = (TextBox)sender;
            if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[1-9 ]"))
            {
                e.Handled = false;
                var givenValue = e.KeyChar - '0';
                var currentCellIndex = 9 * (currentTextBox.Name[7] - '0') + (currentTextBox.Name[8] - '0');
                currentSudoku.SetCellValue(currentCellIndex, givenValue);
            }
            else
            {
                if (e.KeyChar.ToString() != " " 
                    && e.KeyChar != (char)Keys.Back 
                    && e.KeyChar != (char)Keys.Delete 
                    && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }

                currentTextBox.Text = String.Empty;
            }
        }

        private void ArrowKeyPressedInCellTextbox(object sender, PreviewKeyDownEventArgs e)
        {
            char keyChar;
            int keyValue = e.KeyValue;
            if (!e.Shift && keyValue >= (int)Keys.A && keyValue <= (int)Keys.Z)
            {
                keyChar = (char)(keyValue + 32);
            }
            else
            {
                keyChar = (char)keyValue;
            }
            var currentTextBox = (TextBox)sender;

            var changeDirection = Direction.None;
            switch (keyChar)
            {
                case (char)Keys.Up:
                    changeDirection = Direction.Up;
                    break;
                case (char)Keys.Down:
                    changeDirection = Direction.Down;
                    break;
                case (char)Keys.Left:
                    changeDirection = Direction.Left;
                    break;
                case (char)Keys.Right:
                    changeDirection = Direction.Right;
                    break;
            }
            MoveFocusToNextControl(currentTextBox, changeDirection);
        }

        private void MoveFocusToNextControl(TextBox sender, Direction direction)
        {
            if (direction == Direction.None)
            {
                return;
            }
            int i = sender.Name[7] - '0';
            int j = sender.Name[8] - '0';
            switch (direction)
            {
                case Direction.Up:
                    i = i <= 0 ? i : i - 1;
                    break;
                case Direction.Down:
                    i = i >=8 ? i : i + 1;
                    break;
                case Direction.Left:
                    j = j <= 0 ? j : j - 1;
                    break;
                case Direction.Right:
                    j = (j >= 8) ? j : j + 1;
                    break;
            }
            var nextTextBox = (TextBox)this.Controls.Find("TextBox" + i + j, false).First();
            nextTextBox.Focus();
        }

        private void FinishSetupButton_Click(object sender, EventArgs e)
        {
            ShowPossibleValuesCheckBox.Visible = true;
            FillButton.Enabled = true;
            FillButton.Visible = true;

            currentSudoku.SetCurrentStateAsInitial();
            foreach(TextBox textBox in cellTextBoxes)
            {
                textBox.Enabled = false;
            }

            Button button = ((Button)sender);
            button.Visible = false;
            button.Enabled = false;

            CurrentSudokuNameTextBox.Enabled = false;
            CurrentSudokuNameTextBox.Visible = false;
            SaveCurrentSudokuToDBButton.Enabled = false;
            SaveCurrentSudokuToDBButton.Visible = false;
            ExistingSudokusComboBox.Enabled = false;
            ExistingSudokusComboBox.Visible = false;
            LoadSelectedSudokuFromDBButton.Enabled = false;
            LoadSelectedSudokuFromDBButton.Visible = false;
            DeleteSelectedSudokuFromDBButton.Enabled = false;
            DeleteSelectedSudokuFromDBButton.Visible = false;
        }

        private void FillCell(object sender, EventArgs e)
        {
            currentSudoku.TryFill();
            UpdateSudokuGraphics();
        }

        private void UpdateSudokuGraphics()
        {
            for (int i = 0; i < cellTextBoxes.Count; i++)
            {
                var value = currentSudoku.GetCellValue(i);
                cellTextBoxes[i].Text = value == 0 ? string.Empty : value.ToString();
            }
            for (int i = 0; i < cellLabels.Count; i++)
            {
                var possibleValues = currentSudoku.GetCellPossibleValues(i);
                cellLabels[i].Text = string.Join("", possibleValues);
            }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            currentSudoku.ResetToInitialState();
            foreach (TextBox textBox in cellTextBoxes)
            {
                textBox.Enabled = true;
            }
            ShowPossibleValuesCheckBox.Checked = false;
            ShowPossibleValuesCheckBox.Visible = false;
            FillButton.Enabled = false;
            FillButton.Visible = false;
            FinishSetupButton.Visible = true;
            FinishSetupButton.Enabled = true;
            CurrentSudokuNameTextBox.Enabled = true;
            CurrentSudokuNameTextBox.Visible = true;
            SaveCurrentSudokuToDBButton.Enabled = true;
            SaveCurrentSudokuToDBButton.Visible = true;
            ExistingSudokusComboBox.Enabled = true;
            ExistingSudokusComboBox.Visible = true;
            LoadSelectedSudokuFromDBButton.Enabled = true;
            LoadSelectedSudokuFromDBButton.Visible = true;
            DeleteSelectedSudokuFromDBButton.Enabled = true;
            DeleteSelectedSudokuFromDBButton.Visible = true;
            HidePossibleValues();
            UpdateSudokuGraphics();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            currentSudoku = new Sudoku();
            foreach (TextBox textBox in cellTextBoxes)
            {
                textBox.Enabled = true;
            }
            ShowPossibleValuesCheckBox.Checked = false;
            ShowPossibleValuesCheckBox.Visible = false;
            FillButton.Enabled = false;
            FillButton.Visible = false;
            FinishSetupButton.Visible = true;
            FinishSetupButton.Enabled = true;
            CurrentSudokuNameTextBox.Enabled = true;
            CurrentSudokuNameTextBox.Visible = true;
            SaveCurrentSudokuToDBButton.Enabled = true;
            SaveCurrentSudokuToDBButton.Visible = true;
            ExistingSudokusComboBox.Enabled = true;
            ExistingSudokusComboBox.Visible = true;
            LoadSelectedSudokuFromDBButton.Enabled = true;
            LoadSelectedSudokuFromDBButton.Visible = true;
            DeleteSelectedSudokuFromDBButton.Enabled = true;
            DeleteSelectedSudokuFromDBButton.Visible = true;
            HidePossibleValues();
            UpdateSudokuGraphics();
        }

        private void ShowPossibleValuesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                ShowPossibleValues();
            }
            else
            {
                HidePossibleValues();
            }
        }

        private void HidePossibleValues()
        {
            ShowPossibleValuesCheckBox.Checked = false;
            foreach (Label label in cellLabels)
            {
                label.Visible = false;
            }
        }

        private void ShowPossibleValues()
        {
            ShowPossibleValuesCheckBox.Checked = true;
            foreach (Label label in cellLabels)
            {
                label.Visible = true;
            }
        }

        private void SaveCurrentSudokuToDB(object sender, EventArgs e)
        {
            string newSudokuName = CurrentSudokuNameTextBox.Text == string.Empty ? "Unknown" + DateTime.Now.Hour.ToString()
                                     + DateTime.Now.Minute.ToString()
                                     : CurrentSudokuNameTextBox.Text;
            SudokuRepository.SaveNewSudoku(currentSudoku.GetAllValues(), newSudokuName);
            
            CurrentSudokuNameTextBox.Text = string.Empty;
            UpdateComboBoxContentFromDB();
        }

        private void UpdateComboBoxContentFromDB()
        {
            ExistingSudokusComboBox.Items.Clear();
            ExistingSudokusComboBox.Items.AddRange(SudokuRepository.GetAllSudokuNames().ToArray());
            ExistingSudokusComboBox.SelectedIndex = -1;
            ExistingSudokusComboBox.SelectedItem = null;
            ExistingSudokusComboBox.ResetText();
        }

        private void LoadSelectedSudokuFromDB(object sender, EventArgs e)
        {
            if (ExistingSudokusComboBox.SelectedIndex != -1)
            {
                var sudokuData = SudokuRepository.GetSudokuDataByName(ExistingSudokusComboBox.SelectedItem.ToString());
                currentSudoku = new Sudoku(sudokuData);
                UpdateSudokuGraphics();

                ExistingSudokusComboBox.SelectedIndex = -1;
                ExistingSudokusComboBox.SelectedItem = null;
                ExistingSudokusComboBox.ResetText();
            }
        }

        private void DeleteSelectedSudokuFromDB(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;
            }
            if (ExistingSudokusComboBox.SelectedIndex != -1)
            {
                var name = ExistingSudokusComboBox.SelectedItem.ToString();
                SudokuRepository.DeleteSudokuDataByName(name);
                UpdateComboBoxContentFromDB();
            }
        }

        //protected override bool IsInputKey(Keys keyData)
        //{
        //    switch (keyData)
        //    {
        //        case Keys.Right:
        //        case Keys.Left:
        //        case Keys.Up:
        //        case Keys.Down:
        //            return true;
        //        case Keys.Shift | Keys.Right:
        //        case Keys.Shift | Keys.Left:
        //        case Keys.Shift | Keys.Up:
        //        case Keys.Shift | Keys.Down:
        //            return true;
        //    }
        //    return base.IsInputKey(keyData);
        //}
        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Left:
        //        case Keys.Right:
        //        case Keys.Up:
        //        case Keys.Down:
        //            if (e.Shift)
        //            {

        //            }
        //            else
        //            {
        //            }
        //            break;
        //    }
        //}
    }
}
