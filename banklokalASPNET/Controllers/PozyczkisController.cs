using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using banklokalASPNET.Models;

namespace banklokalASPNET.Controllers
{
    public class PozyczkisController : Controller
    {
        private readonly SBContext _context;

        public PozyczkisController(SBContext context)
        {
            _context = context;
        }

        // GET: Pozyczkis
        [HttpGet("/Pozyczkis/")]
        [HttpGet("/Pozyczkis/Filter/{id?}")]
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.Klienci = new SelectList(_context.Klient, "KId", "PelneImie", -1).Append(new SelectListItem("Wybierz klienta", "-1", true, true));

            var sBContext = _context.Pozyczki.Include(p => p.K);
            var results = await sBContext.ToListAsync();

            if (id != null)
                results = results.Where(x => x.KId == id).ToList();

            return View(results);
        }

        // GET: Pozyczkis/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pozyczki = await _context.Pozyczki
                .Include(p => p.K)
                .FirstOrDefaultAsync(m => m.PId == id);
            if (pozyczki == null)
            {
                return NotFound();
            }

            return View(pozyczki);
        }

        // GET: Pozyczkis/Create
        public IActionResult Create()
        {
            ViewData["KId"] = new SelectList(_context.Klient, "KId", "Imie");
            return View();
        }

        // POST: Pozyczkis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PId,KId,Wartosc,DataSplaty")] Pozyczki pozyczki)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pozyczki);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KId"] = new SelectList(_context.Klient, "KId", "Imie", pozyczki.KId);
            return View(pozyczki);
        }

        // GET: Pozyczkis/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pozyczki = await _context.Pozyczki.FindAsync(id);
            if (pozyczki == null)
            {
                return NotFound();
            }
            ViewData["KId"] = new SelectList(_context.Klient, "KId", "Imie", pozyczki.KId);
            return View(pozyczki);
        }

        // POST: Pozyczkis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("PId,KId,Wartosc,DataSplaty")] Pozyczki pozyczki)
        {
            if (id != pozyczki.PId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pozyczki);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PozyczkiExists(pozyczki.PId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KId"] = new SelectList(_context.Klient, "KId", "Imie", pozyczki.KId);
            return View(pozyczki);
        }

        // GET: Pozyczkis/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pozyczki = await _context.Pozyczki
                .Include(p => p.K)
                .FirstOrDefaultAsync(m => m.PId == id);
            if (pozyczki == null)
            {
                return NotFound();
            }

            return View(pozyczki);
        }

        // POST: Pozyczkis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var pozyczki = await _context.Pozyczki.FindAsync(id);
            _context.Pozyczki.Remove(pozyczki);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PozyczkiExists(decimal id)
        {
            return _context.Pozyczki.Any(e => e.PId == id);
        }
    }
}
