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
    public class FavoriteListsController : Controller
    {
        private readonly ShopEnLigneContext _context;

        public FavoriteListsController(ShopEnLigneContext context)
        {
            _context = context;
        }

        // GET: FavoriteLists
        public async Task<IActionResult> Index()
        {
            var shopEnLigneContext = _context.FavoriteList.Include(f => f.User);
            return View(await shopEnLigneContext.ToListAsync());
        }

        // GET: FavoriteLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteList = await _context.FavoriteList
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteList == null)
            {
                return NotFound();
            }

            return View(favoriteList);
        }

        // GET: FavoriteLists/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Username");
            return View();
        }

        // POST: FavoriteLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId")] FavoriteList favoriteList)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(favoriteList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", favoriteList.UserId);
            return View(favoriteList);
        }

        // GET: FavoriteLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteList = await _context.FavoriteList.FindAsync(id);
            if (favoriteList == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Username", favoriteList.UserId);
            return View(favoriteList);
        }

        // POST: FavoriteLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId")] FavoriteList favoriteList)
        {
            if (id != favoriteList.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoriteList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteListExists(favoriteList.Id))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", favoriteList.UserId);
            return View(favoriteList);
        }

        // GET: FavoriteLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteList = await _context.FavoriteList
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favoriteList == null)
            {
                return NotFound();
            }

            return View(favoriteList);
        }

        // POST: FavoriteLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favoriteList = await _context.FavoriteList.FindAsync(id);
            if (favoriteList != null)
            {
                _context.FavoriteList.Remove(favoriteList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteListExists(int id)
        {
            return _context.FavoriteList.Any(e => e.Id == id);
        }
    }
}
