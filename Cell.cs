using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver2
{

    internal class Cell
    {
        private TextBox textBox;
        private Label possibleValuesLabel;
        public int Value { get; set; }
        public bool HasValue { get; set; }
        private bool isDefinedfromStart;
        private List<int> possibleValues;

        private int positionInLine;
        private int positionInColumn;
        private int positionInArea;

        private Cluster corresponedLine;
        private Cluster corresponedColumn;
        private Cluster corresponedArea;

        public Cell (TextBox textBox, Label possibleValuesLabel)
        {
            this.textBox = textBox;
            this.possibleValuesLabel = possibleValuesLabel;

            positionInLine = Convert.ToInt32(textBox.Name.Substring(7, 1));
            positionInColumn = Convert.ToInt32(textBox.Name.Substring(8, 1));
            positionInArea = 3 * (positionInLine % 3) + (positionInColumn % 3);

            possibleValues = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                possibleValues.Add(i + 1);
            }
            UpdateValueAfterStart();
            UpdatePossibleValuesLabel();
        }

        public void MakeNoneInteractable()
        {
            UpdateValueAfterStart();
            textBox.Enabled = false;
            textBox.BackColor = Color.White;
        }

        public void MakeInteractable()
        {
            textBox.Enabled = true;
        }

        public void ClearValue()
        {
            HasValue = false;
            Value = 0;
            textBox.Text = String.Empty;
            possibleValues = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                possibleValues.Add(i + 1);
            }
            UpdatePossibleValuesLabel();
        }

        public void ResetToInitialValue()
        {
            if (!isDefinedfromStart)
            {
                ClearValue();
            }
        }

        private void UpdateValueAfterStart()
        {
            if (textBox.Text == String.Empty)
            {
                HasValue = false;
                isDefinedfromStart = false;
            }
            else
            {
                Value = Convert.ToInt32(textBox.Text);
                HasValue = true;
                isDefinedfromStart = true;
            }
            UpdatePossibleValuesLabel();
        }

        public void BindLine(Cluster line)
        {
            corresponedLine = line;
        }

        public void BindColumn(Cluster column)
        {
            corresponedColumn = column;
        }
        public void BindArea(Cluster area)
        {
            corresponedArea = area;
        }

        public void UpdatePossibleValues()
        {
            if (!HasValue)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (corresponedLine.Contains(i + 1))
                    {
                        possibleValues.Remove(i + 1);
                    }
                    if (corresponedColumn.Contains(i + 1))
                    {
                        possibleValues.Remove(i + 1);
                    }
                    if (corresponedArea.Contains(i + 1))
                    {
                        possibleValues.Remove(i + 1);
                    }
                }
            }
            else
            {
                possibleValues.Clear();
            }
            UpdatePossibleValuesLabel();
        }

        private void UpdatePossibleValuesLabel()
        {
            if (HasValue)
            {
                possibleValuesLabel.Text = String.Empty;
            }
            else
            {
                possibleValuesLabel.Text = string.Join("", possibleValues.ToArray());
            }
        }

        /// <summary>
        /// Fills the only possible number for cluster if possible
        /// </summary>
        public void TryFill()
        {
            // Fill the only possioble number for CELL
            if (!HasValue)
            {
                if (possibleValues.Count == 1)
                {
                    SetValue(possibleValues[0]);
                }
            }
            // Fill the only posiiblew number for line/column/area
            if (!HasValue)
            {
                for (int j = 0; j < possibleValues.Count; j++)
                {
                    if (corresponedLine.PossibleValueCount(possibleValues[j], positionInLine) == 0 ||
                        corresponedColumn.PossibleValueCount(possibleValues[j], positionInColumn) == 0 ||
                        corresponedArea.PossibleValueCount(possibleValues[j], positionInArea) == 0)
                    {
                        SetValue(possibleValues[j]);
                    }
                }
            }
        }

        private void SetValue(int value)
        {
            Value = value;
            HasValue = true;
            textBox.Text = Value.ToString();
            UpdatePossibleValues();
        }

        public bool IfPossibleValue(int value)
        {
            for( int i = 0; i < possibleValues.Count; i++)
            {
                if (possibleValues[i] == value)
                {
                    return true;
                }
            }
            return false;
        }

        public void TogglePossibleValuesLabelVisibility(bool show)
        {
            possibleValuesLabel.Visible = show;
        }
    }
}
