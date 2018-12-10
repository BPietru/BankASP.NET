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
    public class KlientsController : Controller
    {
        private readonly SBContext _context;

        public KlientsController(SBContext context)
        {
            _context = context;
        }

        // GET: Klients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Klient.Include(x => x.Pozyczki).ToListAsync());
        }

        // GET: Klients/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klient = await _context.Klient
                .FirstOrDefaultAsync(m => m.KId == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // GET: Klients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KId,Imie,Nazwisko,Pesel,DataUr")] Klient klient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klient);
        }

        // GET: Klients/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klient = await _context.Klient.FindAsync(id);
            if (klient == null)
            {
                return NotFound();
            }
            return View(klient);
        }

        // POST: Klients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("KId,Imie,Nazwisko,Pesel,DataUr")] Klient klient)
        {
            if (id != klient.KId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlientExists(klient.KId))
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
            return View(klient);
        }

        // GET: Klients/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klient = await _context.Klient
                .FirstOrDefaultAsync(m => m.KId == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // POST: Klients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {



            var klient = await _context.Klient.FindAsync(id);
            var pozyczki = klient.Pozyczki;
            _context.Pozyczki.RemoveRange(pozyczki);
            //  klient.Pozyczki.ToList().ForEach(x =>
            //  {
            //      var pozyczka = await _context.Pozyczki.FindAsync(x.PId).
            //   })
            _context.Klient.Remove(klient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlientExists(decimal id)
        {
            return _context.Klient.Any(e => e.KId == id);
        }
    }
}
