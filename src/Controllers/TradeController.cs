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
        public async Task<IActionResult> Create(Trade trade)
        {
            try
            {
                if (_context.Trades.Any(t => t.Code == trade.Code))
                {
                    ModelState.AddModelError("Code", "Já existe uma negociação cadastrada com este código.");
                }


                if (ModelState.IsValid)
                {
                    _context.Add(trade);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(trade);
        }
        
        [Route("trades/verify-code")]
        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyCode(string code)
        {
            if (_context.Trades.Any(t => t.Code == code))
            {
                return Json(data: $"Já existe uma negociação cadastrada com este código: {code}");
            }
            return Json(data: true);
        }
    }
}
