using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Trading.Models
{
    public class Trade
    {
        public int Id { get; set; }

        [Required]
        [Remote(action: "VerifyCode", controller: "Trade")]
        public string Code { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, 99999999999999.99)]
        public decimal Price { get; set; }

        [Required]
        public BusinessType BusinessType { get; set; }
    }

    public enum BusinessType
    {
        [Display(Name = "Venda")]
        Sell = 0,
        [Display(Name = "Compra")]
        Buy = 1
    }
}
