using System.ComponentModel.DataAnnotations;

namespace ShopEnLigne.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string? Username { get; set; }
        public string? nom { get; set; }
        public string? prenom { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        // Propriétés spécifiques à la société
        public string? NomSociete { get; set; }
        public UserType? UserType { get; set; }

        public virtual ICollection<Bien> Bien { get; set; } = new List<Bien>();
        public virtual ICollection<Historique> Historique { get; set; } = new List<Historique>();
        public virtual ICollection<BlackList> BlackLists { get; set; } = new List<BlackList>();
        public virtual ICollection<FavoriteList> FavoriteLists { get; set; } = new List<FavoriteList>();
    }
}
