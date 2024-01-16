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
    public class OffreSpecialesController : Controller
    {
        private readonly ShopEnLigneContext _context;

        public OffreSpecialesController(ShopEnLigneContext context)
        {
            _context = context;
        }

        // GET: OffreSpeciales
        public async Task<IActionResult> Index()
        {
            var shopEnLigneContext = _context.OffreSpeciale.Include(o => o.Bien);
            return View(await shopEnLigneContext.ToListAsync());
        }

        // GET: OffreSpeciales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offreSpeciale = await _context.OffreSpeciale
                .Include(o => o.Bien)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offreSpeciale == null)
            {
                return NotFound();
            }

            return View(offreSpeciale);
        }

        // GET: OffreSpeciales/Create
        public IActionResult Create()
        {
            ViewData["BienId"] = new SelectList(_context.Bien, "Id", "Titre");
            var offreSpeciale = new OffreSpeciale { Bien = new Bien() };
            return View(offreSpeciale);
        }

        // POST: OffreSpeciales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TauxRemise,DateDebut,DateFin,BienId")] OffreSpeciale offreSpeciale)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(offreSpeciale);
                await _context.SaveChangesAsync();
                // Load the User navigation property
                offreSpeciale = _context.OffreSpeciale.Include(o => o.Bien).ThenInclude(b => b.User).Single(o => o.Id == offreSpeciale.Id);
                return RedirectToAction("Index", "Users", new { username = offreSpeciale.Bien.User.Username });
            }
            ViewData["BienId"] = new SelectList(_context.Bien, "Id", "Id", offreSpeciale.BienId);
            return View(offreSpeciale);
        }

        // GET: OffreSpeciales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offreSpeciale = await _context.OffreSpeciale.Include(o => o.Bien).FirstOrDefaultAsync(o => o.Id == id);
            if (offreSpeciale == null)
            {
                return NotFound();
            }
            ViewData["BienId"] = new SelectList(_context.Bien, "Id", "Id", offreSpeciale.BienId);
            return View(offreSpeciale);
        }


        // POST: OffreSpeciales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TauxRemise,DateDebut,DateFin,BienId")] OffreSpeciale offreSpeciale)
        {
            if (id != offreSpeciale.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(offreSpeciale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OffreSpecialeExists(offreSpeciale.Id))
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
            ViewData["BienId"] = new SelectList(_context.Bien, "Id", "Id", offreSpeciale.BienId);
            return View(offreSpeciale);
        }

        // GET: OffreSpeciales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offreSpeciale = await _context.OffreSpeciale
                .Include(o => o.Bien)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (offreSpeciale == null)
            {
                return NotFound();
            }

            return View(offreSpeciale);
        }

        // POST: OffreSpeciales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offreSpeciale = await _context.OffreSpeciale.FindAsync(id);
            if (offreSpeciale != null)
            {
                _context.OffreSpeciale.Remove(offreSpeciale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OffreSpecialeExists(int id)
        {
            return _context.OffreSpeciale.Any(e => e.Id == id);
        }
    }
}
