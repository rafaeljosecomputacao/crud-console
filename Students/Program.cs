using System;
using System.Data;
using System.Data.SqlClient;
using Students.Models;

namespace Students
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating database connection
            ConnectionString connectionString = new ConnectionString();
            string sqlString = connectionString.GetConnectionString();

            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(sqlString);

            // Attempt to connect to the database
            try
            {
                sqlConnection.Open();
                Console.WriteLine("Connected to the database");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}