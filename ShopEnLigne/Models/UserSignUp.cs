using System.ComponentModel.DataAnnotations;

namespace ShopEnLigne.Models
{
    public class UserSignUp
    {
        [Required(ErrorMessage = "L'adresse e-mail est obligatoire.")]
        [EmailAddress(ErrorMessage = "Veuillez entrer une adresse e-mail valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        public string prenom { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit comporter au moins 6 caractères.")]
        public string Password { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Le rôle est obligatoire.")]
        public string Role { get; set; }

    }
}
