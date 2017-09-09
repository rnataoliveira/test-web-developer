using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Trading.Models
{
    public class Trade
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string BusinessType { get; set; }
    }
}
