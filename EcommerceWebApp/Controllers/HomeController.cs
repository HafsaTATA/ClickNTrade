using EcommerceWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult item()
        {
            return View();
        }
        public IActionResult Clothes()
        {
            return View();
        }
        public IActionResult Vehicles()
        {
            return View();
        }
        public IActionResult Electronics()
        {
            return View();
        }
        public IActionResult Profil()
        {
            return View();
        }
        public IActionResult itemEdit()
        {
            return View();
        }
        public IActionResult LoginProprietaire()
        {
            return View();
        }
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
