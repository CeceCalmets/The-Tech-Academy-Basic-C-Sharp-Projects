using System;
public interface IQuittable
{
    void Quit();  
}

public class Employee : IQuittable
{
    public string Name { get; set; }
    public string Position { get; set; }
    public Employee(string name, string position)
    {
        Name = name;
        Position = position;
    }

    public void Quit()
    {
               Console.WriteLine($"{Name} has quit their position as {Position}.");
    }
}

public class Program
{
    public static void Main()
    {        Employee emp = new Employee("John Doe", "Software Developer");
        
                IQuittable quitter = emp; 
        quitter.Quit();  
    }
}
