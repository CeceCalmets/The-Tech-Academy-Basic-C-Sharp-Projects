using System;

namespace RockPaperScissorsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Introduction message
            Console.WriteLine("Welcome to Rock, Paper, Scissors!");
            Console.WriteLine("Type 'Rock', 'Paper', or 'Scissors' to make your choice.");
            Console.WriteLine("To exit the game, type 'Exit'.\n");

            // Initialize the random number generator
            Random random = new Random();

            // Loop for continuous play until the user types 'Exit'
            while (true)
            {
                // Prompt the user to make a move
                Console.Write("Enter your choice (Rock, Paper, Scissors, or Exit): ");
                string userChoice = Console.ReadLine().Trim();

                // Check if the user wants to exit the game
                if (userChoice.Equals("Exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Thanks for playing!");
                    break;
                }

                // Validate if the user's choice is correct
                if (userChoice != "Rock" && userChoice != "Paper" && userChoice != "Scissors")
                {
                    Console.WriteLine("Invalid choice. Please choose 'Rock', 'Paper', or 'Scissors'.");
                    continue;
                }

                // Generate the computer's choice
                int computerChoiceIndex = random.Next(0, 3); // Randomly choose between 0, 1, 2
                string computerChoice = computerChoiceIndex == 0 ? "Rock" : computerChoiceIndex == 1 ? "Paper" : "Scissors";

                // Display the computer's choice
                Console.WriteLine($"Computer chose: {computerChoice}");

                // Determine the winner
                string result = DetermineWinner(userChoice, computerChoice);

                // Display the result of the game
                Console.WriteLine(result);
                Console.WriteLine();
            }
        }

        // Method to determine the winner based on user and computer choices
        static string DetermineWinner(string userChoice, string computerChoice)
        {
            // Case when the user and computer make the same choice
            if (userChoice == computerChoice)
            {
                return "It's a tie!";
            }

            // Case when the user wins
            if ((userChoice == "Rock" && computerChoice == "Scissors") ||
                (userChoice == "Paper" && computerChoice == "Rock") ||
                (userChoice == "Scissors" && computerChoice == "Paper"))
            {
                return "You win!";
            }

            // If it's neither a tie nor a win for the user, the computer wins
            return "You lose!";
        }
    }
}
