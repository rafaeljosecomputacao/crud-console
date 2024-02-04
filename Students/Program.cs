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

            // Creating table
            string createTableSql = "IF OBJECT_ID('students') IS NULL " +
                "CREATE TABLE students (" +
                "id INT IDENTITY(1,1) PRIMARY KEY," +
                "name VARCHAR(100) NOT NULL," +
                "email VARCHAR(100) NOT NULL)";

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand(createTableSql, sqlConnection);

            // Attempt to connect to the database
            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Successfully created table");
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

            // Read
            string readSql = "SELECT * FROM students";
            sqlCommand = new SqlCommand(readSql, sqlConnection);

            SqlDataReader sqlReader;

            // Attempt to connect to the database
            try
            {
                sqlConnection.Open();
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    Console.WriteLine($"Id: {sqlReader["id"]}, Name: {sqlReader["name"]}, Email: {sqlReader["email"]}");
                }
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