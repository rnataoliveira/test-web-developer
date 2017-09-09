using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Trading.Models;

namespace Trading.Data
{
    public class DbInitializer
    {
        public static void Initialize(TradingContext context)
        {
            context.Database.EnsureCreated();

            if (context.Trades.Any())
            {
                return;
            }

            var trades = new Trade[]
            {
                new Trade {
                    Code = "0001",
                    Type = "Alimento",
                    Name = "Banana",
                    Quantity = 1000,
                    Price = 2000,
                    BusinessType = "Compra"
                },
                new Trade {
                    Code = "0001",
                    Type = "Alimento",
                    Name = "Milho",
                    Quantity = 500,
                    Price = 1500,
                    BusinessType = "Venda"
                }
            };

            foreach (Trade n in trades)
            {
                context.Trades.Add(n);
            }
            context.SaveChanges();
        }
    }
}
