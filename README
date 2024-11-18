using System;

class Program
{
    static void Main()
    {
        
        Console.WriteLine("Welcome to Package Express. Please follow the instructions below.");

        
        Console.WriteLine("Please enter the package weight:");
        double weight = Convert.ToDouble(Console.ReadLine());  // Read user input and convert to double

        
        if (weight > 50)
        {
            
            Console.WriteLine("Package too heavy to be shipped via Package Express. Have a good day.");
            return;  
        }

        
        Console.WriteLine("Please enter the package width:");
        double width = Convert.ToDouble(Console.ReadLine());  // Read and convert input to double

        
        Console.WriteLine("Please enter the package height:");
        double height = Convert.ToDouble(Console.ReadLine());  // Read and convert input to double

       
        Console.WriteLine("Please enter the package length:");
        double length = Convert.ToDouble(Console.ReadLine());  // Read and convert input to double

        
        if ((width + height + length) > 50)
        {
            
            Console.WriteLine("Package too big to be shipped via Package Express.");
            return;  // Exit the program
        }

        
        double quote = (width * height * length * weight) / 100;

        
        Console.WriteLine($"Your estimated total for shipping this package is: ${quote:F2}");

        
        Console.WriteLine("Thank you!");
    }
}
