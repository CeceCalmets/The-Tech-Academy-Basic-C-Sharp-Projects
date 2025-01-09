public class Student
{
    public int StudentId { get; set; }   // Primary Key
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}
using Microsoft.EntityFrameworkCore;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    // Define connection string (SQL Server in this case)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=StudentDB;Trusted_Connection=True;");
    }
}
using System;

namespace EF_CodeFirst_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ensure the database is created (Code-First will automatically create it based on the model)
            using (var context = new SchoolContext())
            {
                // Create the database if it doesn't exist
                context.Database.EnsureCreated();

                // Create a new student
                var student = new Student
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(2000, 1, 1)
                };

                // Add the student to the database
                context.Students.Add(student);

                // Save the changes to the database
                context.SaveChanges();

                // Confirm student was added
                Console.WriteLine("Student added:");
                Console.WriteLine($"ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Date of Birth: {student.DateOfBirth.ToShortDateString()}");
            }

            Console.WriteLine("Database created and student added successfully!");
            Console.ReadLine();
        }
    }
}
