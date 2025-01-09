using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        // Console width and height of the game area
        static int screenWidth = 40;
        static int screenHeight = 20;

        // Snake representation (list of coordinates)
        static List<Position> snake = new List<Position>();

        // Food position
        static Position food;

        // Direction the snake is moving
        static string direction = "RIGHT";  // Possible values: "UP", "DOWN", "LEFT", "RIGHT"

        // The current score
        static int score = 0;

        // Game loop control
        static bool gameOver = false;

        static void Main(string[] args)
        {
            // Set up the game screen
            Console.SetWindowSize(screenWidth + 1, screenHeight + 1);
            Console.SetBufferSize(screenWidth + 1, screenHeight + 1);
            Console.CursorVisible = false;  // Hide the cursor

            // Initialize the snake at the starting position
            snake.Add(new Position { X = 5, Y = 5 });  // Snake's head
            snake.Add(new Position { X = 4, Y = 5 });  // Snake's body

            // Generate the first piece of food
            GenerateFood();

            // Main game loop
            while (!gameOver)
            {
                Input();
                Logic();
                Draw();
                Thread.Sleep(100);  // Control game speed (milliseconds)
            }

            Console.SetCursorPosition(screenWidth / 2 - 5, screenHeight / 2);
            Console.WriteLine("Game Over! Score: " + score);
        }

        // Method to handle user input for snake movement
        static void Input()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && direction != "DOWN") direction = "UP";
                if (key == ConsoleKey.DownArrow && direction != "UP") direction = "DOWN";
                if (key == ConsoleKey.LeftArrow && direction != "RIGHT") direction = "LEFT";
                if (key == ConsoleKey.RightArrow && direction != "LEFT") direction = "RIGHT";
            }
        }

        // Method to update the game logic (movement, collision, etc.)
        static void Logic()
        {
            // Move the snake by adding a new head based on the current direction
            Position newHead = new Position();
            Position currentHead = snake[0];

            if (direction == "UP")
                newHead = new Position { X = currentHead.X, Y = currentHead.Y - 1 };
            else if (direction == "DOWN")
                newHead = new Position { X = currentHead.X, Y = currentHead.Y + 1 };
            else if (direction == "LEFT")
                newHead = new Position { X = currentHead.X - 1, Y = currentHead.Y };
            else if (direction == "RIGHT")
                newHead = new Position { X = currentHead.X + 1, Y = currentHead.Y };

            // Check for collisions with walls or the snake's own body
            if (newHead.X < 0 || newHead.X >= screenWidth || newHead.Y < 0 || newHead.Y >= screenHeight ||
                snake.Any(p => p.X == newHead.X && p.Y == newHead.Y))
            {
                gameOver = true;
                return;
            }

            // Check if the snake has eaten food
            if (newHead.X == food.X && newHead.Y == food.Y)
            {
                score += 10;  // Increase score
                snake.Insert(0, newHead);  // Add new head to the snake's body
                GenerateFood();  // Generate new food
            }
            else
            {
                snake.Insert(0, newHead);  // Move the snake forward
                snake.RemoveAt(snake.Count - 1);  // Remove the last part of the tail
            }
        }

        // Method to generate food at a random position
        static void GenerateFood()
        {
            Random rand = new Random();
            food = new Position
            {
                X = rand.Next(0, screenWidth),
                Y = rand.Next(0, screenHeight)
            };

            // Ensure food is not generated on the snake's body
            while (snake.Any(p => p.X == food.X && p.Y == food.Y))
            {
                food.X = rand.Next(0, screenWidth);
                food.Y = rand.Next(0, screenHeight);
            }
        }

        // Method to draw everything on the screen
        static void Draw()
        {
            Console.Clear();

            // Draw the snake
            foreach (var part in snake)
            {
                Console.SetCursorPosition(part.X, part.Y);
                Console.Write("■");
            }

            // Draw the food
            Console.SetCursorPosition(food.X, food.Y);
            Console.Write("★");

            // Display the score
            Console.SetCursorPosition(0, screenHeight);
            Console.WriteLine("Score: " + score);
        }
    }

    // Position class to represent the coordinates of snake parts and food
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
