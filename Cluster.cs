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
    }
}
