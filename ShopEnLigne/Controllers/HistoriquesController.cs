using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopEnLigne.Data;
using ShopEnLigne.Models;

namespace ShopEnLigne.Controllers
{
    public class HistoriquesController : Controller
    {
        private readonly ShopEnLigneContext _context;

        public HistoriquesController(ShopEnLigneContext context)
        {
            _context = context;
        }

        // GET: Historiques
        public async Task<IActionResult> Index()
        {
            var shopEnLigneContext = _context.Historique.Include(h => h.Bien).Include(h => h.User);
            return View(await shopEnLigneContext.ToListAsync());
        }

        // GET: Historiques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historique = await _context.Historique
                .Include(h => h.Bien)
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historique == null)
            {
                return NotFound();
            }

            return View(historique);
        }

        // GET: Historiques/Create
        public IActionResult Create()
        {
            ViewData["Bien"] = new SelectList(_context.Bien, "Id", "Titre");
            ViewData["User"] = new SelectList(_context.User, "Id", "Username");
            return View();
        }

        // POST: Historiques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,UserId,BienId")] Historique historique)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(historique);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BienId"] = new SelectList(_context.Bien, "Id", "Id", historique.BienId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", historique.UserId);
            return View(historique);
        }

        // GET: Historiques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historique = await _context.Historique.FindAsync(id);
            if (historique == null)
            {
                return NotFound();
            }
            ViewData["BienId"] = new SelectList(_context.Bien, "Id", "Id", historique.BienId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", historique.UserId);
            return View(historique);
        }

        // POST: Historiques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,UserId,BienId")] Historique historique)
        {
            if (id != historique.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(historique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoriqueExists(historique.Id))
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
            ViewData["BienId"] = new SelectList(_context.Bien, "Id", "Id", historique.BienId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", historique.UserId);
            return View(historique);
        }

        // GET: Historiques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historique = await _context.Historique
                .Include(h => h.Bien)
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historique == null)
            {
                return NotFound();
            }

            return View(historique);
        }

        // POST: Historiques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historique = await _context.Historique.FindAsync(id);
            if (historique != null)
            {
                _context.Historique.Remove(historique);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoriqueExists(int id)
        {
            return _context.Historique.Any(e => e.Id == id);
        }
    }
}
