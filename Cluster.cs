using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2
{
    internal class Cluster
    {
        private List<Cell> cells = new List<Cell>();
        
        public Cluster () {}

        public void AddCell(Cell cell)
        {
            cells.Add(cell);
        }

        public bool Contains(int value)
        {
            for(int i = 0; i < cells.Count; i++)
            {
                if(cells[i].HasValue)
                {
                    if (cells[i].Value == value)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int PossibleValueCount(int value, int x)
        {
            int count = 0;
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].HasValue == false && i != x)
                {
                    if (cells[i].ContainsPossibleValue(value))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void UpdatePossibleValuesInAllCells()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].UpdatePossibleValues();
            }
        }

        public int PositionOfSecondCellThatContainsOnlyTheseTwoPossibleValues (int firstValue, int secondValue, int excludedPositionInCluster)
        {
            int position = -1;
            for(int i = 0; i < cells.Count; i++)
            {
                if (!cells[i].HasValue && i != excludedPositionInCluster)
                {
                    if (cells[i].ContainsOnlyTheseTwoPossibleValues(firstValue, secondValue))
                    {
                        position = i;
                    }
                }
            }
            return position;
        }

        public void RemoveCertainPossibleValues(List<int> values, List<int> excludepositions)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                if (!cells[i].HasValue && !excludepositions.Contains(i))
                {
                    for (int j = 0; j < values.Count; j++)
                    {
                        cells[i].RemovePossibleValueIfPresent(values[j]);
                    }
                }
            }
        }
    }
}
