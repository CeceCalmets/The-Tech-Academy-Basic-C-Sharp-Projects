using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main()
    {
        BlackjackGame game = new BlackjackGame();
        game.StartGame();
    }
}

public class BlackjackGame
{
    private Deck deck;
    private Hand playerHand;
    private Hand dealerHand;
    private bool isGameOver;

    public BlackjackGame()
    {
        deck = new Deck();
        playerHand = new Hand();
        dealerHand = new Hand();
        isGameOver = false;
    }

    public void StartGame()
    {
        Console.WriteLine("Welcome to Blackjack!\n");

        // Initial Deal
        deck.Shuffle();
        DealInitialCards();

        while (!isGameOver)
        {
            DisplayHands();

            // Player's turn
            if (PlayerTurn())
            {
                // Dealer's turn if player didn't bust
                DealerTurn();
                DisplayHands();
                DetermineWinner();
                isGameOver = true;
            }
        }

        Console.WriteLine("Game Over. Press any key to exit...");
        Console.ReadKey();
    }

    private void DealInitialCards()
    {
        playerHand.AddCard(deck.DrawCard());
        dealerHand.AddCard(deck.DrawCard());
        playerHand.AddCard(deck.DrawCard());
        dealerHand.AddCard(deck.DrawCard());
    }

    private void DisplayHands()
    {
        Console.Clear();
        Console.WriteLine($"Player's hand: {playerHand.Display()} (Total: {playerHand.TotalValue})");
        Console.WriteLine($"Dealer's hand: {dealerHand.Display()} (Total: {dealerHand.TotalValue})");
    }

    private bool PlayerTurn()
    {
        while (playerHand.TotalValue < 21)
        {
            Console.WriteLine("\nDo you want to [H]it or [S]tand?");
            string choice = Console.ReadLine().ToLower();

            if (choice == "h")
            {
                playerHand.AddCard(deck.DrawCard());
                Console.WriteLine("You chose to hit.");
                DisplayHands();
            }
            else if (choice == "s")
            {
                Console.WriteLine("You chose to stand.");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter [H] for Hit or [S] for Stand.");
            }
        }

        // Player has busted if their total value exceeds 21
        Console.WriteLine("\nYou busted! Dealer wins.");
        return true;
    }

    private void DealerTurn()
    {
        while (dealerHand.TotalValue < 17)
        {
            Console.WriteLine("\nDealer draws a card.");
            dealerHand.AddCard(deck.DrawCard());
            DisplayHands();
        }

        if (dealerHand.TotalValue > 21)
        {
            Console.WriteLine("\nDealer busted! You win.");
        }
    }

    private void DetermineWinner()
    {
        if (playerHand.TotalValue > 21)
        {
            Console.WriteLine("\nYou busted! Dealer wins.");
        }
        else if (dealerHand.TotalValue > 21)
        {
            Console.WriteLine("\nDealer busted! You win.");
        }
        else if (playerHand.TotalValue > dealerHand.TotalValue)
        {
            Console.WriteLine("\nYou win!");
        }
        else if (playerHand.TotalValue < dealerHand.TotalValue)
        {
            Console.WriteLine("\nDealer wins.");
        }
        else
        {
            Console.WriteLine("\nIt's a tie!");
        }
    }
}

public class Deck
{
    private List<Card> cards;
    private Random random;

    public Deck()
    {
        cards = new List<Card>();
        random = new Random();
        InitializeDeck();
    }

    private void InitializeDeck()
    {
        foreach (var suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (var rank in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card((Rank)rank, (Suit)suit));
            }
        }
    }

    public void Shuffle()
    {
        cards = cards.OrderBy(c => random.Next()).ToList();
    }

    public Card DrawCard()
    {
        var card = cards.First();
        cards.RemoveAt(0);
        return card;
    }
}

public class Hand
{
    private List<Card> cards;

    public Hand()
    {
        cards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public int TotalValue
    {
        get
        {
            int total = 0;
            int aceCount = 0;

            foreach (var card in cards)
            {
                if (card.Rank == Rank.Ace)
                {
                    aceCount++;
                    total += 11;
                }
                else
                {
                    total += (int)card.Rank;
                }
            }

            // Adjust for aces being worth 1 if total > 21
            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }
    }

    public string Display()
    {
        return string.Join(", ", cards.Select(card => card.ToString()));
    }
}

public class Card
{
    public Rank Rank { get; }
    public Suit Suit { get; }

    public Card(Rank rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

public enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

public enum Rank
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 10,
    Queen = 10,
    King = 10,
    Ace = 11
}
