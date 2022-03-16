using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver2
{
    interface ICell
    {
        /// <summary>
        /// Binds to the given cell the horizontal line in Sudoku that contains current cell.
        /// </summary>
        /// <param name="line">The line to remember in this cell.</param>
        void BindLine(Cluster line);

        /// <summary>
        /// Binds to the given cell the vertical column in Sudoku that contains current cell.
        /// </summary>
        /// <param name="column">The column to remember in this cell.</param>
        void BindColumn(Cluster column);

        /// <summary>
        /// Binds to the given cell the vertical column in Sudoku that contains current cell.
        /// </summary>
        /// <param name="column">The column to remember in this cell.</param>
        void BindArea(Cluster area);

        /// <summary>
        /// Clears the value inside the cell. The possible values are also updated.
        /// </summary>
        void ClearValue();

        /// <summary>
        /// Checking if a cell contains a given possible value. That is, is it possible, according to 
        /// the basic rules, to write this value into a cell.
        /// </summary>
        /// <param name="value">The integer value to check.</param>
        /// <returns>True if the value is available.</returns>
        bool ContainsPossibleValue(int value);

        /// <summary>
        /// Makes this cell editable. The user will be able to enter data after calling this method.
        /// </summary>
        void MakeInteractable();

        /// <summary>
        /// Makes this cell uneditable. The user will not be able to enter data after calling this method.
        /// </summary>
        void MakeNoneInteractable();

        /// <summary>
        /// A method that automatically fills the cell based on data received from the corresponding 
        /// lines, columns, or areas. If it is not possible to determine a new cell value from this data, 
        /// the method will simply update the possible values for the cell.
        /// </summary>
        void TryFill();

        /// <summary>
        /// A method that updates data about the possible values of the cell.
        /// The calculation is based only on the basic rules (i.e., the method traverses the corresponding line, column, and area).
        /// Changes are also immediately reflected in the corresponding label.
        /// </summary>
        void UpdatePossibleValues();
    }
}
