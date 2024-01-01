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

        public void CombinedAlgorithm()
        {
            for (int n = 2; n <= 8; n++)
            {
                RecursionStep(n, n, 0, new List<int>(), new HashSet<int>());
            }
        }

        private void RecursionStep(int degree, int maxPvCount, int indexInList, List<int> addedCellsIndexes, HashSet<int> possibleValues)
        {
            degree--;
            for (int i = indexInList; i < cells.Count; i++)
            {
                if (cells[i].HasValue)
                {
                    continue;
                }
                var pvToSet = cells[i].PossibleValues;
                var editedPv = new HashSet<int>(possibleValues);
                var editedAddedCellsIndexes = new List<int>(addedCellsIndexes);
                foreach (var value in pvToSet)
                {
                    editedPv.Add(value);
                }
                editedAddedCellsIndexes.Add(i);

                if (degree <= 0)
                {
                    if (editedPv.Count == maxPvCount)
                    {
                        for (int j = 0; j < cells.Count; j++)
                        {
                            if (!editedAddedCellsIndexes.Contains(j) && !cells[j].HasValue)
                            {
                                cells[j].RemoveSetOfPossibleValues(editedPv);
                            }
                        }
                    }
                }
                else
                {
                    RecursionStep(degree, maxPvCount, i + 1, editedAddedCellsIndexes, editedPv);
                }
            }
            return;
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
