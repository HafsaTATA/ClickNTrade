using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopEnLigne.Models
{
    public class Bien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Titre { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.ImageUrl)]
        public string? Photo { get; set; }
        public double? Prix { get; set; }
        public bool IsAvailable { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int CategorieId { get; set; }
        public virtual Categorie Categorie { get; set; }

        public virtual ICollection<OffreSpeciale> OffreSpeciales { get; set; } = new List<OffreSpeciale>();
        public virtual ICollection<Historique> Historique { get; set; } = new List<Historique>();
    }
}
