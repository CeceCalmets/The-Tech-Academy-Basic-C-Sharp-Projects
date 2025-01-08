 So here is my coding :

 

Employee.cs (Class Definition)
csharp
Copy code
using System;
public class Employee
{

public int Id { get; set; }
public string FirstName { get; set; }
public string LastName { get; set; }

public Employee(int id, string firstName, string lastName)
{
Id = id;
FirstName = firstName;
LastName = lastName;
}

public static bool operator ==(Employee emp1, Employee emp2)
{

  {

}

{
if (obj is Employee other)
{
return this.Id == other.Id; }
return false; }

}


Program.cs (Main Program)
csharp
Copy code
using System;
class Program
{
static void Main(string[] args)
{
if (emp1 == emp2)
{
Console.WriteLine("The employees are equal (same ID).");
}
else
{
Console.WriteLine("The employees are not equal (different IDs).");
}

}
}
}
