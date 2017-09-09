using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trading.Data;
using Trading.Models;

namespace Trading.Controllers
{
    public class TradeController : Controller
    {
        private readonly TradingContext _context;

        public TradeController(TradingContext context)
        {
            _context = context;
        }

        [Route("trades")]
        public async Task<IActionResult> Index()
        {
            //ViewData["Message"] = "Lista de Operações";

            return View(await _context.Trades.ToListAsync());
        }

        [HttpGet]
        [Route("trades/create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("trades/create",Name = "CreateTrade")]
        public async Task<IActionResult> Create([Bind("Code, Type, Name, Quantity, Price, BusinessType")] Trade trade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(trade);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(trade);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
