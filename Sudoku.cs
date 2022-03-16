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

    internal class Sudoku
    {
        private List<Cell> initialCells;
        private List<Cell> cells;
        private List<Cluster> lines;
        private List<Cluster> columns;
        private List<Cluster> areas;

        public Sudoku()
        {
            initialCells = new List<Cell>();
            for (int i = 0; i < 81; i++)
            {
                initialCells.Add(new Cell(i));
            }

            cells = new List<Cell>();
            for (int i = 0; i < 81; i++)
            {
                cells.Add(new Cell(i));
            }

            DefineLines();
            DefineColumns();
            DefineAreas();
        }

        public Sudoku(List<int> values)
        {
            initialCells = new List<Cell>();
            for (int i = 0; i < 81; i++)
            {
                initialCells.Add(new Cell(i));
            }

            cells = new List<Cell>();
            for (int i = 0; i < 81; i++)
            {
                cells.Add(new Cell(i, values[i]));
            }

            DefineLines();
            DefineColumns();
            DefineAreas();
        }

        public List<int> GetAllValues()
        {
            var returned = new List<int>();
            foreach (var cell in cells)
            {
                returned.Add(cell.Value);
            }
            return returned;
        }

        public void SetCurrentStateAsInitial()
        {
            initialCells = new List<Cell>();
            for (int i = 0; i < 81; i++)
            {
                initialCells.Add(new Cell(i, cells[i].Value));
            }
        }

        public void TryFill()
        {
            foreach (Cell cell in cells)
            {
                cell.UpdatePossibleValues();
            }
            foreach (Cell cell in cells)
            {
                cell.TryFill();
            }
        }

        public void ResetToInitialState()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].SetValue(initialCells[i].Value);
            }
        }

        public void SetCellValue(int cellIndex, int value)
        {
            cells[cellIndex].SetValue(value);
        }

        public int GetCellValue(int cellIndex)
        {
            return cells[cellIndex].Value;
        }

        public List<int> GetCellPossibleValues(int cellIndex)
        {
            return cells[cellIndex].GetPossibleValues();
        }

        #region Private methods

        private void DefineLines()
        {
            lines = new List<Cluster>();
            for (int i = 0; i < 9; i++)
            {
                var line = new Cluster();
                for (int j = 0; j < 9; j++)
                {
                    line.AddCell(cells[9 * i + j]);
                    cells[9 * i + j].BindLine(line);
                }
                lines.Add(line);
            }
            for (int i = 0; i < 9; i++)
            {
                var line = lines[i];
                for (int j = 0; j < 9; j++)
                {
                    cells[9 * i + j].BindLine(line);
                }
            }
        }

        private void DefineColumns()
        {
            columns = new List<Cluster>();
            for (int i = 0; i < 9; i++)
            {
                var column = new Cluster();
                for (int j = 0; j < 9; j++)
                {
                    column.AddCell(cells[9 * j + i]);
                    cells[9 * j + i].BindColumn(column);
                }
                columns.Add(column);
            }
            for (int i = 0; i < 9; i++)
            {
                var column = columns[i];
                for (int j = 0; j < 9; j++)
                {
                    cells[9 * i + j].BindColumn(column);
                }
            }
        }

        private void DefineAreas()
        {
            areas = new List<Cluster>();
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
                areas.Add(area);
            }
            for (int i = 0; i < 9; i++)
            {
                var area = areas[i];
                for (int j = 0; j < 9; j++)
                {
                    cells[9 * i + j].BindArea(area);
                }
            }
        }
        #endregion
    }
}
