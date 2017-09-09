using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Trading.Data
{
    public class TradingContext : DbContext
    {
        public TradingContext(DbContextOptions<TradingContext> options) 
            : base(options){ }

        public DbSet<Trade> Trades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trade>().ToTable("Trades");
        }
    }
}

