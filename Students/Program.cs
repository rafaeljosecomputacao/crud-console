using System;
using Students.Models;

namespace Students
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student();

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();

            student.Name = name;
            student.Email = email;

            Console.WriteLine("Name: " + student.Name);
            Console.WriteLine("Email: " + student.Email);
        }
    }
}