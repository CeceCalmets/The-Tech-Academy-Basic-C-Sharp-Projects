using System;

namespace MathOperationApp
{
    // Create a class named MathOperations
    public class MathOperations
    {
        // Create a void method named 'PerformOperation'
        // It takes two integers as parameters: num1 and num2
        public void PerformOperation(int num1, int num2)
        {
            // Perform a simple math operation on num1 (adding 10 to num1)
            int result = num1 + 10;
            
            // Display the result of the math operation
            Console.WriteLine("The result of adding 10 to the first number is: " + result);
            
            // Display the second number to the screen
            Console.WriteLine("The second number is: " + num2);
        }
    }

    // Main program entry point
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the MathOperations class to create an object
            MathOperations mathOps = new MathOperations();

            // Call the PerformOperation method, passing two numbers (5 and 7)
            // The method will add 10 to the first number and display both numbers
            mathOps.PerformOperation(5, 7);

            // Call the PerformOperation method again, but this time specify the parameters by name
            // This helps improve readability and allows for more flexibility
            mathOps.PerformOperation(num1: 8, num2: 12);

            // Keep the console window open until the user presses a key
            Console.ReadLine();
        }
    }
}
