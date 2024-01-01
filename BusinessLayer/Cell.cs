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
        public int Value { get; private set; }
        public bool HasValue { get; private set; }
        
        private List<int> possibleValues;

        private int positionInLine;
        private int positionInColumn;
        private int positionInArea;

        private Cluster corresponedLine;
        private Cluster corresponedColumn;
        private Cluster corresponedArea;

        public List<int> PossibleValues { get { return new List<int>(possibleValues); } private set { possibleValues = value; } }

        public void ResetValue(int value)
        {
            if (value < 1 || value > 9)
            {
                Value = 0;
                HasValue = false;
                possibleValues = new List<int>();
                for (int i = 0; i < 9; i++)
                {
                    possibleValues.Add(i + 1);
                }
            }
            else
            {
                Value = value;
                HasValue = true;
                possibleValues = new List<int>();
                for (int i = 0; i < 9; i++)
                {
                    possibleValues.Add(i + 1);
                }
            }
        }

        public void SetValue(int value)
        {
            if (value < 1 || value > 9)
            {
                Value = 0;
                HasValue = false;
                possibleValues = new List<int>();
                for (int i = 0; i < 9; i++)
                {
                    possibleValues.Add(i + 1);
                }
            }
            else
            {
                Value = value;
                HasValue = true;
                possibleValues = new List<int>();
                for (int i = 0; i < 9; i++)
                {
                    possibleValues.Add(i + 1);
                }
            }
        }

        public Cell(int cellId)
        {
            positionInLine = cellId % 9;
            positionInColumn = cellId / 9;
            positionInArea = (positionInLine % 3) + 3 * (positionInColumn % 3);

            possibleValues = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                possibleValues.Add(i + 1);
            }
        }

        public Cell(int cellId, int value)
        {
            positionInLine = cellId % 9;
            positionInColumn = cellId / 9;
            positionInArea = (positionInLine % 3) + 3 * (positionInColumn % 3);

            possibleValues = new List<int>();
            if (value >= 1 && value <= 9)
            {
                this.Value = value;
                HasValue = true;
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    possibleValues.Add(i + 1);
                }
            }
        }

        public List<int> GetPossibleValues()
        {
            return new List<int>(possibleValues);
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
        }

        /// <summary>
        /// Fills the only possible number for cluster if possible
        /// </summary>
        public void TryFill()
        {
            // Update possible numbers to exclude existing combinations (2, 3 numbers)
            UpdatePossibleValuesToExcludeExtra();
            // Fill the only possible number for CELL
            FillTheOnlyPossibleNumber();
            // Fill the only possible number for line/column/area
            FillTheOnlyPossibleNumberForCluster();
            // algorithm 4
            CombinedAlgorithm();
        }
        public void FillTheOnlyPossibleNumber()
        {
            if (!HasValue)
            {
                if (possibleValues.Count == 1)
                {
                    SetValue(possibleValues[0]);
                }
            }
        }

        public void FillTheOnlyPossibleNumberForCluster()
        {
            if (!HasValue)
            {
                for (int j = 0; j < possibleValues.Count; j++)
                {
                    if (corresponedLine.PossibleValueCount(possibleValues[j], positionInLine) == 0 ||
                        corresponedColumn.PossibleValueCount(possibleValues[j], positionInColumn) == 0 ||
                        corresponedArea.PossibleValueCount(possibleValues[j], positionInArea) == 0)
                    {
                        SetValue(possibleValues[j]);
                        break;
                    }
                }
            }
        }


        public void CombinedAlgorithm()
        {
            corresponedLine.CombinedAlgorithm();
            corresponedColumn.CombinedAlgorithm();
            corresponedArea.CombinedAlgorithm();
        }

        public void UpdatePossibleValuesToExcludeExtra()
        {
            if (!HasValue)
            {
                if (possibleValues.Count == 2)
                {
                    if (corresponedLine.PositionOfSecondCellThatContainsOnlyTheseTwoPossibleValues(possibleValues[0], possibleValues[1], positionInLine) != -1)
                    {
                        List<int> positions = new List<int>();
                        positions.Add(positionInLine);
                        positions.Add(corresponedLine.PositionOfSecondCellThatContainsOnlyTheseTwoPossibleValues(possibleValues[0], possibleValues[1], positionInLine));
                        corresponedLine.RemoveCertainPossibleValues(possibleValues, positions);
                    }
                    if (corresponedColumn.PositionOfSecondCellThatContainsOnlyTheseTwoPossibleValues(possibleValues[0], possibleValues[1], positionInColumn) != -1)
                    {
                        List<int> positions = new List<int>();
                        positions.Add(positionInColumn);
                        positions.Add(corresponedColumn.PositionOfSecondCellThatContainsOnlyTheseTwoPossibleValues(possibleValues[0], possibleValues[1], positionInColumn));
                        corresponedColumn.RemoveCertainPossibleValues(possibleValues, positions);
                    }
                    if (corresponedArea.PositionOfSecondCellThatContainsOnlyTheseTwoPossibleValues(possibleValues[0], possibleValues[1], positionInArea) != -1)
                    {
                        List<int> positions = new List<int>();
                        positions.Add(positionInArea);
                        positions.Add(corresponedArea.PositionOfSecondCellThatContainsOnlyTheseTwoPossibleValues(possibleValues[0], possibleValues[1], positionInArea));
                        corresponedArea.RemoveCertainPossibleValues(possibleValues, positions);
                    }
                }
            }
        }

        public bool ContainsPossibleValue(int value)
        {
            for (int i = 0; i < possibleValues.Count; i++)
            {
                if (possibleValues[i] == value)
                {
                    return true;
                }
            }
            return false;
        }

        public void RemovePossibleValueIfPresent(int value)
        {
            possibleValues.Remove(value);
        }

        public void RemoveSetOfPossibleValues(HashSet<int> hash)
        {
            foreach(var value in hash)
            {
                RemovePossibleValueIfPresent((int)value);
            }
        }

        public bool ContainsOnlyTheseTwoPossibleValues(int value1, int value2)
        {
            bool contains = false;
            if (possibleValues.Count == 2)
            {
                if (ContainsPossibleValue(value1) && ContainsPossibleValue(value2))
                {
                    contains = true;
                }
            }
            return contains;
        }
    }
}
