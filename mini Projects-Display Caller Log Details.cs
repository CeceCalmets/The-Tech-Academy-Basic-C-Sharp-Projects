using System;
using System.Collections.Generic;

namespace CallerLogApp
{
    // Class to represent the details of a caller log.
    public class CallerLog
    {
        // Properties to hold caller's information
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CallTime { get; set; }

        // Constructor to initialize a caller log with the provided information
        public CallerLog(string name, string phoneNumber, DateTime callTime)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            CallTime = callTime;
        }

        // Method to display the caller log details
        public void DisplayLog()
        {
            Console.WriteLine("Caller Name: " + Name);
            Console.WriteLine("Phone Number: " + PhoneNumber);
            Console.WriteLine("Call Time: " + CallTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine();
        }
    }

    // Class to manage a list of caller logs
    public class CallerLogManager
    {
        // List to store multiple caller logs
        private List<CallerLog> callerLogs = new List<CallerLog>();

        // Method to add a new caller log to the list
        public void AddCallerLog(string name, string phoneNumber)
        {
            // Get the current time of the call
            DateTime callTime = DateTime.Now;

            // Create a new CallerLog object and add it to the list
            CallerLog newCall = new CallerLog(name, phoneNumber, callTime);
            callerLogs.Add(newCall);

            Console.WriteLine("New call log added successfully.");
        }

        // Method to display all caller logs
        public void DisplayAllLogs()
        {
            Console.WriteLine("----- Caller Log Details -----");
            if (callerLogs.Count == 0)
            {
                Console.WriteLine("No caller logs available.");
                return;
            }

            // Loop through the list and display each call log
            foreach (var log in callerLogs)
            {
                log.DisplayLog();
            }
        }
    }

    // Main program to interact with the user and manage caller logs
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of CallerLogManager to manage caller logs
            CallerLogManager logManager = new CallerLogManager();

            // Display menu options to the user
            while (true)
            {
                Console.WriteLine("------ Caller Log System ------");
                Console.WriteLine("1. Add a new call log");
                Console.WriteLine("2. Display all call logs");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option (1-3): ");
                
                string choice = Console.ReadLine();

                // Process the user's choice
                switch (choice)
                {
                    case "1":
                        // Prompt user for caller details
                        Console.Write("Enter the caller's name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter the caller's phone number: ");
                        string phoneNumber = Console.ReadLine();

                        // Add the new caller log
                        logManager.AddCallerLog(name, phoneNumber);
                        break;

                    case "2":
                        // Display all call logs
                        logManager.DisplayAllLogs();
                        break;

                    case "3":
                        // Exit the program
                        Console.WriteLine("Exiting the program...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please select an option between 1 and 3.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
