using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Colorful;
using Console = Colorful.Console;
using Students.Models;

namespace Students
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Initial display
            Console.Title = "Orange System";
            Console.Write("Welcome to the ", Color.WhiteSmoke);
            Console.WriteLine("Orange System", Color.Orange);
            Console.WriteLine();
            FinalizeDisplay();

            // Creating database connection
            ConnectionString connectionString = new ConnectionString();
            string sqlString = connectionString.GetConnectionString();

            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(sqlString);

            // Attempt to connect to the database
            try
            {
                sqlConnection.Open();
                Console.WriteLine("Connected to the database", Color.WhiteSmoke);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}", Color.WhiteSmoke);
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
                Console.WriteLine("Successfully created table", Color.WhiteSmoke);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}", Color.WhiteSmoke);
            }
            finally
            {
                sqlConnection.Close();
            }

            FinalizeDisplay();

            // Main display
            bool finished = false;

            SqlDataReader sqlReader;

            do
            {
                Title();
                Menu("1", "Create");
                Menu("2", "Read");
                Menu("3", "Update");
                Menu("4", "Delete");
                Console.WriteLine();

                Console.Write("Choose an operation: ", Color.WhiteSmoke);
                var menuOption = Console.ReadKey();

                Console.Clear();

                bool defaultOperation = true;

                if (menuOption.Key != ConsoleKey.Enter)
                {
                    char operation = menuOption.KeyChar;

                    switch (operation)
                    {
                        case '1':
                            Title();
                            Subtitle("Create", Color.ForestGreen);

                            Console.Write("Name: ", Color.WhiteSmoke);
                            string inputName = Console.ReadLine();
                            Console.Write("Email: ", Color.WhiteSmoke);
                            string inputEmail = Console.ReadLine();
                            Console.WriteLine();

                            // Create
                            string createSql = "INSERT INTO students(name,email) VALUES ('" + inputName + "','" + inputEmail + "')";
                            sqlCommand = new SqlCommand(createSql, sqlConnection);

                            // Attempt to connect to the database
                            try
                            {
                                sqlConnection.Open();
                                sqlCommand.ExecuteNonQuery();
                                Console.WriteLine("Successfully created student", Color.WhiteSmoke);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}", Color.WhiteSmoke);
                            }
                            finally
                            {
                                sqlConnection.Close();
                            }

                            Console.WriteLine();
                            defaultOperation = false;
                            FinalizeDisplay();
                            break;
                        case '2':
                            Title();
                            Subtitle("Read", Color.DodgerBlue);

                            // Read
                            string readSql = "SELECT * FROM students";
                            sqlCommand = new SqlCommand(readSql, sqlConnection);

                            // List of students
                            List<Student> students = new List<Student>();

                            // Attempt to connect to the database
                            try
                            {
                                sqlConnection.Open();
                                sqlReader = sqlCommand.ExecuteReader();

                                while (sqlReader.Read())
                                {
                                    // Forcing an object type conversion to integer and string
                                    int id = (int)sqlReader["id"];
                                    string name = (string)sqlReader["name"];
                                    string email = (string)sqlReader["email"];
                                       
                                    // Adding student to students list
                                    students.Add(new Student(id, name, email));
                                   
                                    // Showing the object directly
                                    //Console.WriteLine($"Id: {sqlReader["id"]}, Name: {sqlReader["name"]}, Email: {sqlReader["email"]}", Color.WhiteSmoke);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}", Color.WhiteSmoke);
                            }
                            finally
                            {
                                sqlConnection.Close();
                            }

                            // Showing the object using a list
                            if (students.Count > 0)
                            {                     
                                foreach (Student student in students)
                                {
                                    Console.Write("Id: ", Color.DodgerBlue);
                                    Console.Write(student.Id + ", ", Color.WhiteSmoke);
                                    Console.Write("Name: ", Color.DodgerBlue);
                                    Console.Write(student.Name + ", ", Color.WhiteSmoke);
                                    Console.Write("Email: ", Color.DodgerBlue);
                                    Console.WriteLine(student.Email, Color.WhiteSmoke);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Empty list", Color.WhiteSmoke);
                            }

                            Console.WriteLine();
                            defaultOperation = false;
                            FinalizeDisplay();
                            break;
                        case '3':
                            Title();
                            Subtitle("Update", Color.Yellow);

                            Console.Write("Id: ", Color.WhiteSmoke);
                            int idUpdate = int.Parse(Console.ReadLine());

                            bool update = false;

                            // Read to check if the id exists
                            readSql = "SELECT * FROM students";
                            sqlCommand = new SqlCommand(readSql, sqlConnection);

                            // Attempt to connect to the database
                            try
                            {
                                sqlConnection.Open();
                                sqlReader = sqlCommand.ExecuteReader();

                                while (sqlReader.Read())
                                {
                                    // Forcing an object type conversion to integer and string
                                    int id = (int)sqlReader["id"];

                                    if (id == idUpdate)
                                    {
                                        update = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}", Color.WhiteSmoke);
                            }
                            finally
                            {
                                sqlConnection.Close();
                            }

                            if (update == true)
                            {
                                Console.Write("New email: ", Color.WhiteSmoke);
                                string newEmail = Console.ReadLine();
                                Console.WriteLine();

                                // Update
                                string updateSql = "UPDATE students SET email = '" + newEmail + "' WHERE id = '" + idUpdate + "'";
                                sqlCommand = new SqlCommand(updateSql, sqlConnection);

                                // Attempt to connect to the database
                                try
                                {
                                    sqlConnection.Open();
                                    sqlCommand.ExecuteNonQuery();
                                    Console.WriteLine("Successfully updated student", Color.WhiteSmoke);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}", Color.WhiteSmoke);
                                }
                                finally
                                {
                                    sqlConnection.Close();
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid Id, this student doesn't exist", Color.WhiteSmoke);
                            }
                           
                            Console.WriteLine();
                            defaultOperation = false;
                            FinalizeDisplay();
                            break;
                        case '4':
                            Title();
                            Subtitle("Delete", Color.OrangeRed);

                            Console.Write("Id: ", Color.WhiteSmoke);
                            int idDelete = int.Parse(Console.ReadLine());
                            Console.WriteLine();

                            bool delete = false;

                            // Read to check if the id exists
                            readSql = "SELECT * FROM students";
                            sqlCommand = new SqlCommand(readSql, sqlConnection);

                            // Attempt to connect to the database
                            try
                            {
                                sqlConnection.Open();
                                sqlReader = sqlCommand.ExecuteReader();

                                while (sqlReader.Read())
                                {
                                    // Forcing an object type conversion to integer and string
                                    int id = (int)sqlReader["id"];

                                    if (id == idDelete)
                                    {
                                        delete = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}", Color.WhiteSmoke);
                            }
                            finally
                            {
                                sqlConnection.Close();
                            }

                            if (delete == true)
                            {
                                // Delete
                                string deleteSql = "DELETE students WHERE id = '" + idDelete + "'";
                                sqlCommand = new SqlCommand(deleteSql, sqlConnection);

                                // Attempt to connect to the database
                                try
                                {
                                    sqlConnection.Open();
                                    sqlCommand.ExecuteNonQuery();
                                    Console.WriteLine("Successfully deleted student", Color.WhiteSmoke);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}", Color.WhiteSmoke);
                                }
                                finally
                                {
                                    sqlConnection.Close();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Id, this student doesn't exist", Color.WhiteSmoke);
                            }

                            Console.WriteLine();
                            defaultOperation = false;
                            FinalizeDisplay();
                            break;
                        default:
                            Console.WriteLine("Invalid operation", Color.WhiteSmoke);
                            Console.WriteLine();
                            defaultOperation = true;
                            FinalizeDisplay();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid operation", Color.WhiteSmoke);
                    Console.WriteLine();
                    FinalizeDisplay();
                }

                if (defaultOperation == false)
                {
                    Console.WriteLine("Do you want to perform another operation?", Color.WhiteSmoke);
                    Console.Write("Yes: ", Color.Green);
                    Console.WriteLine("Press y", Color.WhiteSmoke);
                    Console.Write("No: ", Color.Red);
                    Console.WriteLine("Press any key", Color.WhiteSmoke);

                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        Console.Clear();
                        finished = false;                  
                    }
                    else
                    {
                        Console.Clear();
                        Console.Write("Thank you for using the ", Color.WhiteSmoke);
                        Console.WriteLine("Orange System", Color.Orange);
                        Console.ReadKey();
                        finished = true;
                    }
                }
            } while (finished == false);
        }

        // Function to finalize the display
        static void FinalizeDisplay()
        {
            Console.WriteLine("Press any key to continue", Color.WhiteSmoke);
            Console.ReadKey();
            Console.Clear();
        }

        // Display title function
        static void Title()
        {
            Console.Write("Management of the ", Color.WhiteSmoke);
            Console.WriteLine("Orange School", Color.Orange);
            Console.WriteLine();
        }

        // Display subtitle function
        static void Subtitle(string title, Color color)
        {
            Console.Write(title, color);
            Console.WriteLine(" student", Color.WhiteSmoke);
            Console.WriteLine();
        }

        // Function to show the main menu
        static void Menu(string prefix, string message)
        {
            Console.Write("[", Color.WhiteSmoke);
            Console.Write(prefix, Color.Orange);
            Console.WriteLine("] " + message, Color.WhiteSmoke);
        }
    }
}