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

        [Route("trades", Name = "TradeList")]
        public async Task<IActionResult> Index()
        {
            //ViewData["Message"] = "Lista de Operações";

            return View(await _context.Trades.ToListAsync());
        }

        [HttpGet]
        [Route("trades/update", Name = "UpdateForm")]
        public async Task<IActionResult> Update(int id)
        {
            var trade = await _context.Trades.FirstOrDefaultAsync(t => t.Id == id);

            if (trade == null) return NotFound();

            return View(trade);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("trades/update", Name = "UpdateTrade")]
        public async Task<IActionResult> Update(Trade trade)
        {
            try
            {
                if (Exists(trade.Code, trade.Id))
                    ModelState.AddModelError("Code", "Já existe uma negociação cadastrada com este código.");
                
                if (ModelState.IsValid)
                {
                    _context.Update(trade);

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

        [HttpGet]
        [Route("trades/create", Name = "CreateForm")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("trades/create", Name = "CreateTrade")]
        public async Task<IActionResult> Create(Trade trade)
        {
            try
            {
                if (Exists(trade.Code, trade.Id))
                    ModelState.AddModelError("Code", "Já existe uma negociação cadastrada com este código.");

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
        

        [HttpGet]
        [Route("trades/{id}/display", Name = "DisplayTrade")]
        public async Task<IActionResult> Display(int id)
        {
            var trade = await _context.Trades.FirstOrDefaultAsync(t => t.Id == id);

            if (trade == null) return NotFound();

            return View(trade);
        }
        

        [Route("trades/verify-code")]
        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyCode(string code, int id)
        {
            var error = $"Já existe uma negociação cadastrada com este código: {code}";

            return Exists(code, id) ? Json(data: error) : Json(data: true);
        }

        bool Exists(string code, int id) => 
            _context.Trades.Any(t => t.Code == code && t.Id != id);


        [HttpGet]
        [Route("trades/remove", Name = "RemoveForm")]
        public async Task<IActionResult> Remove(int id)
        {
            var trade = await _context.Trades.FirstOrDefaultAsync(t => t.Id == id);

            if (trade == null)
                return NotFound();
            try
            {
                _context.Remove(trade);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
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

        
    }
}
