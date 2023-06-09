﻿using FinalsProjectAPI.Features.Stocks.Domain;
using FinalsProjectAPI.Features.Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace FinalsProjectAPI.Data
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Watchlist> UserStocks { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            //modelBuilder.Entity<UserStock>().ToTable("UserStock");
            modelBuilder.Entity<Stock>().ToTable("Stock");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    ID = 1,
                    FirstName = "Karlo",
                    LastName = "Husak",
                    Email = "hk@gmail.com",
                    Password = "1234"
                },
                new User
                {
                    ID = 2,
                    FirstName = "Ivan",
                    LastName = "Ivić",
                    Email = "ivan23@yahoo.com",
                    Password = "1234"
                }
            );

            modelBuilder.Entity<Stock>().HasData(
                new Stock { ID = 1, Ticker = "GME", Company = "GameStop" },
                new Stock { ID = 2, Ticker = "AAPL", Company = "Apple" },
                new Stock { ID = 3, Ticker = "TSLA", Company = "Tesla" },
                new Stock { ID = 4, Ticker = "NOK", Company = "Nokia" }
            );

            modelBuilder.Entity<Watchlist>().HasData(
                new Watchlist { ID = 1, UserID = 1, StockID = 1 },
                new Watchlist { ID = 2, UserID = 1, StockID = 2 },
                new Watchlist { ID = 3, UserID = 1, StockID = 3 },
                new Watchlist { ID = 4, UserID = 2, StockID = 1 },
                new Watchlist { ID = 5, UserID = 2, StockID = 3 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
