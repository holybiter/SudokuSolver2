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
        public int Value { get; set; }
        public bool HasValue { get; set; }
        private List<int> possibleValues;

        private Cluster corresponedLine;
        private Cluster corresponedColumn;
        private Cluster corresponedArea;

        public Cell (TextBox textBox)
        {
            this.textBox = textBox;
            UpdateValue();
            possibleValues = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                possibleValues.Add(i + 1);
            }
        }

        public void MakeNoneInteractable()
        {
            UpdateValue();
            textBox.Enabled = false;
            textBox.BackColor = Color.White;
        }

        private void UpdateValue()
        {
            if (textBox.Text == "")
            {
                HasValue = false;
            }
            else
            {
                Value = Convert.ToInt32(textBox.Text);
                HasValue = true;
            }
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
        }

        /// <summary>
        /// Fills the only possible number for row/column/area if possible
        /// </summary>
        public void TryFill()
        {
            if (!HasValue)
            {
                if (possibleValues.Count == 1)
                {
                    Value = possibleValues[0];
                    HasValue = true;
                    textBox.Text = Value.ToString();
                }
            }
        }
    }
}
