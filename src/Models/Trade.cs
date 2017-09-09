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

        [Required(ErrorMessage = "O Código é obrigatório.")]
        [Remote(action: "VerifyCode", controller: "Trade")]
        public string Code { get; set; }

        [Required(ErrorMessage = "O Tipo de Mercadoria é obrigatório.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "O Nome da Mercadoria é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A Quantidade de Mercadoria é obrigatória.")]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "O Preço da mercadoria é obrigatório.")]
        [Range(0.01, 99999999999999.99)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O Tipo do Negócio é obrigatório.")]
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
