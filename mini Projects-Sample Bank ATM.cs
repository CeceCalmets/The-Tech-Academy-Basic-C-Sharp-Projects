//Install-Package Microsoft.EntityFrameworkCore
//Add-Migration InitialCreate
//Update-Database

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BankATM
{
    // Define the Account class for the user
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public List<Transaction> Transactions { get; set; }
    }

    // Define the Transaction class
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // "Deposit" or "Withdrawal"
        public DateTime Date { get; set; }

        public Account Account { get; set; }
    }

    // Define the database context class
    public class BankContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database connection string (SQLite or SQL Server)
            optionsBuilder.UseSqlite("Data Source=bank.db");
        }
    }
}




