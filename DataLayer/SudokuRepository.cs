using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver2
{
    internal static class SudokuRepository
    {
        public static void SaveNewSudoku(List<int> values, string name)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SudokuSolverDataBase.mdf;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            var data = string.Join("", values);
            string queryString = "INSERT INTO Sudoku " +
                                                 "Values (" + (new Random()).Next(1000000, 100000000) + ", '" +
                                                 name + "', '"
                                                 + data + "')";
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.Text;
            myCommand.CommandText = queryString;
            myCommand.Connection = connection;

            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();
        }

        public static List<string> GetAllSudokuNames()
        {
            List<string> sudokuNames = new List<string>();

            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SudokuSolverDataBase.mdf;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString =
                "SELECT Id, Name, CellData FROM dbo.Sudoku";
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

            DataSet sudokuData = new DataSet();
            connection.Open();
            adapter.Fill(sudokuData, "Sudoku");
            connection.Close();

            foreach (DataRow row in sudokuData.Tables[0].Rows)
            {
                sudokuNames.Add(row["Name"].ToString().Trim());
            }

            return sudokuNames;
        }

        public static List<int> GetSudokuDataByName(string name)
        {
            List<int> sudokuData = new List<int>();
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SudokuSolverDataBase.mdf;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            string queryString = "SELECT TOP 1 CellData FROM dbo.Sudoku WHERE [Name] = '" + name + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

            DataSet sudokuDataSet = new DataSet();

            connection.Open();
            adapter.Fill(sudokuDataSet, "Sudoku");
            connection.Close();

            if (sudokuDataSet.Tables[0].Rows.Count < 1)
            {
                return sudokuData;
            }
            var data = sudokuDataSet.Tables[0].Rows[0][0].ToString();

            for (int i = 0; i < data.Length; i++)
            {
                sudokuData.Add(data[i] - '0');
            }
            while (sudokuData.Count < 81)
            {
                sudokuData.Add(0);
            }

            return sudokuData;
        }

        public static void DeleteSudokuDataByName(string name)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SudokuSolverDataBase.mdf;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.Text;
            myCommand.CommandText = "DELETE FROM Sudoku WHERE [Name] = '" + name + "'";
            myCommand.Connection = connection;

            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();
        }
    }
}
