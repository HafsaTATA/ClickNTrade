using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.IdentityModel.Tokens;
using ShopEnLigne.Data;
using ShopEnLigne.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt;

namespace ShopEnLigne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ShopEnLigneContext _context;
        private readonly IConfiguration Configuration;

        public AuthController(ShopEnLigneContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        //Login
        [HttpPost("login")]
        public IActionResult Login(UserLogin user)
        {
            // Vérifiez l'existence de l'utilisateur et validez le mot de passe
            var loggedInUser = _context.User.Where(u => u.Username == user.Username)
        .FirstOrDefault();

            if (loggedInUser == null || !IsPasswordValid(loggedInUser.Password, user.Password))
            {
                return Unauthorized("Nom d'utilisateur ou mot de passe incorrect");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loggedInUser.Username),
                new Claim(ClaimTypes.NameIdentifier, loggedInUser.Id.ToString()),
                new Claim(ClaimTypes.Email, loggedInUser.Email),
            };

            var token = GenerateJwtToken(claims);

            return Ok(new { Token = token });
        }

        //Sign Up
        [HttpPost("signup")]
        public IActionResult SignUp(UserSignUp user)
        {

            if (_context.User.Any(u => u.Username == user.Username))
            {
                return Conflict("Nom d'utilisateur déjà utilisé");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Créez un nouvel utilisateur
            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                nom = user.nom,
                prenom = user.prenom,
                Photo = user.Photo,
                Password = hashedPassword,

            };

            // Ajoutez l'utilisateur à la base de données
            _context.User.Add(newUser);
            _context.SaveChanges();

            return Ok(new { Message = "Utilisateur enregistré avec succès" });

        }

        private bool IsPasswordValid(string hashedPassword, string providedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
