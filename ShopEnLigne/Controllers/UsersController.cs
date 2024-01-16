using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopEnLigne.Data;
using ShopEnLigne.Models;

namespace ShopEnLigne.Controllers
{
    public class UsersController : Controller
    {
        private readonly ShopEnLigneContext _context;
        public static Boolean IsLoggedIn { get; private set; }

        public UsersController(ShopEnLigneContext context)
        {
            _context = context;
        }
        [HttpGet]
        public bool IsUserLoggedIn()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpGet("inviteUserCount")]
        [Route("api/inviteUserCount")]
        public IActionResult GetInviteUserCount()
        {
            var count = _context.User.Count(u => u.UserType == UserType.Invite);
            return Ok(count);
        }
        [HttpGet("ProprietaireUserCount")]
        [Route("api/ProprietaireUserCount")]
        public IActionResult GetProprietaireUserCount()
        {
            var count = _context.User.Count(u => u.UserType == UserType.Proprietaire);
            return Ok(count);
        }


        // GET: Users
        public async Task<IActionResult> Index(string username)
        {
            var user = await _context.User.Include(u => u.Bien).FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            // Passez l'utilisateur à la vue
            return View(user);
        }



        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult LoginProprietaire()
        {
            return View();
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Username,nom,prenom,Password,Photo,NomSociete")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserType = UserType.Invite;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Biens");
            }
            return View(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateP([Bind("Id,Email,Username,nom,prenom,Password,Photo,NomSociete")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserType = UserType.Proprietaire;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { username = user.Username });
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(string Email, string password)
        {
            // Perform authentication logic here
            var user = _context.User.SingleOrDefault(u =>
                (u.Username == Email || u.Email == Email) &&
                u.Password == password);
            if (user != null)
            {
                // Authentication successful
                // You may want to replace this with your actual authentication logic
                if (user.UserType == UserType.Invite)
                {
                    
                    return RedirectToAction("Index", "Biens");
                }
                else if (user.UserType == UserType.Administrator)
                {
                    
                    return RedirectToAction("Dashboard", "Users");
                }
                else
                {
                    IsLoggedIn = true;
                    return RedirectToAction("Index", new { username = user.Username });
                } // Redirect to the "Index" action in the "Home" controller
            }
            else
            {
                IsLoggedIn = false;
                // Authentication failed
                // You may want to add a model state error or return an error view
                ModelState.AddModelError(string.Empty, "Invalid Email or password");
                return View();
            }
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.Include(u => u.Bien).FirstAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Username,nom,prenom,Password,Photo,NomSociete,UserType")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Users", new { username = user.Username });
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route("api/userbiens")]
        public IActionResult GetUserBiensData()
        {
            var userData = _context.User
                .Where(u => u.UserType == UserType.Proprietaire) // Add this line
                .Include(u => u.Bien)
                .Select(u => new
                {
                    username = u.Username,
                    biensCount = u.Bien.Count()
                })
                .ToList();

            return Json(userData);
        }

    }
}
