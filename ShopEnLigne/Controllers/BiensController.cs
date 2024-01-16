using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopEnLigne.Data;
using ShopEnLigne.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace ShopEnLigne.Controllers
{
    public class BiensController : Controller
    {
        private readonly ShopEnLigneContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BiensController(ShopEnLigneContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Pay()
        {
            return View();
        }
        [HttpGet("BiensCount")]
        [Route("api/BiensCount")]
        public IActionResult GetBiensCountCount()
        {
            var count = _context.Bien.Count();
            return Ok(count);
        }

        public async Task<IActionResult> ProductsByCategory(int? categoryId)
        {
            if (categoryId == null)
            {
                return NotFound();
            }

            var category = await _context.Categorie.FindAsync(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            var productsInCategory = await _context.Bien
                .Include(b => b.Categorie)
                .Include(b => b.User)
                .Where(b => b.CategorieId == categoryId)
                .ToListAsync();

            ViewData["CategoryName"] = category.Nom;

            return View("Index", productsInCategory);
        }

        public bool IsUserLoggedIn()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }

        // GET: Biens
        public async Task<IActionResult> Index()
        {
            var shopEnLigneContext = _context.Bien.Include(b => b.Categorie).Include(b => b.User);
            return View(await shopEnLigneContext.ToListAsync());
        }

        // GET: Biens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bien = await _context.Bien
                .Include(b => b.Categorie)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bien == null)
            {
                return NotFound();
            }

            return View(bien);
        }

        // GET: Biens/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Nom");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Username");
            return View();
        }

        // POST: Biens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre,Description,Photo,Prix,IsAvailable,UserId,CategorieId")] Bien bien, IFormFile photoFile)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    if (photoFile != null && photoFile.Length > 0)
                    {
                        // Save the image to the server (e.g., in wwwroot/images)
                        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + photoFile.FileName;
                        var filePath = Path.Combine(uploads, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await photoFile.CopyToAsync(fileStream);
                        }

                        // Update the Photo property of the Bien model
                        bien.Photo = uniqueFileName;
                    }

                    _context.Add(bien);
                    await _context.SaveChangesAsync();
                    // Load the User navigation property
                    bien = _context.Bien.Include(b => b.User).Single(b => b.Id == bien.Id);
                    // Create a new Historique entry
                    var historique = new Historique
                    {
                        Date = DateTime.Now, // Set the date to the current date
                        UserId = bien.UserId, // Set the UserId to the UserId of the Bien
                        BienId = bien.Id // Set the BienId to the Id of the newly created Bien
                    };

                    _context.Add(historique);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index","Users", new { username = bien.User.Username });
                }
                catch (DbUpdateException)
                {
                    // Log the error
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Id", bien.CategorieId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", bien.UserId);
            return View(bien);
        }

        // GET: Biens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bien = await _context.Bien.FindAsync(id);
            if (bien == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Nom", bien.CategorieId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Username", bien.UserId);
            return View(bien);
        }

        // POST: Biens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Description,Photo,Prix,IsAvailable,UserId,CategorieId")] Bien bien)
        {
            if (id != bien.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(bien);
                    await _context.SaveChangesAsync();
                    bien = _context.Bien.Include(b => b.User).Single(b => b.Id == bien.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BienExists(bien.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Users", new { username = bien.User.Username });
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Nom", bien.CategorieId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Username", bien.UserId);
            return View(bien);
        }

        // GET: Biens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bien = await _context.Bien
                .Include(b => b.Categorie)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bien == null)
            {
                return NotFound();
            }

            return View(bien);
        }

        // POST: Biens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bien = await _context.Bien.Include(b => b.User).FirstOrDefaultAsync(b => b.Id == id);
            if (bien != null)
            {
                _context.Bien.Remove(bien);
                await _context.SaveChangesAsync();
            }

            // Redirect to the user's profile
            return RedirectToAction("Index", "Users", new { username = bien.User.Username });
        }

        private bool BienExists(int id)
        {
            return _context.Bien.Any(e => e.Id == id);
        }
    }
}
