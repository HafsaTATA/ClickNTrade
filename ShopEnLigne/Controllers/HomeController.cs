using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopEnLigne.Data;
using ShopEnLigne.Models;
using System.Diagnostics;

namespace ShopEnLigne.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopEnLigneContext _shopEnLigneContext;

        public HomeController(ILogger<HomeController> logger,ShopEnLigneContext shopEnLigneContext)
        {
            _logger = logger;
            _shopEnLigneContext = shopEnLigneContext;
        }
        public IActionResult Pay()
        {
            return View();
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            // Perform the search based on the searchTerm
            var searchResults = await _shopEnLigneContext.Bien
                .Where(o => o.Titre.Contains(searchTerm)) // Replace with your actual search criteria
                .ToListAsync();

            // Pass the search results to a partial view (create a partial view if needed)
            return PartialView("_SearchResultPartial", searchResults);
        }
        public IActionResult Index()
        {
            var offreSpecialData = _shopEnLigneContext.OffreSpeciale.Include(o => o.Bien).ToList(); // Adjust based on your actual data retrieval logic

            // Pass the data to the view
            return View(offreSpecialData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        

        // GET: Users/Create
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
