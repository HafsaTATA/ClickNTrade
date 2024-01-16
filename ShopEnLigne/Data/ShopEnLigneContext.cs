using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopEnLigne.Models;

namespace ShopEnLigne.Data
{
    public class ShopEnLigneContext : DbContext
    {
        public ShopEnLigneContext (DbContextOptions<ShopEnLigneContext> options)
            : base(options)
        {
        }

        public DbSet<ShopEnLigne.Models.User> User { get; set; }
        public DbSet<ShopEnLigne.Models.Categorie> Categorie { get; set; }
        public DbSet<ShopEnLigne.Models.Bien> Bien { get; set; }
        public DbSet<ShopEnLigne.Models.BlackList> BlackList { get; set; }
        public DbSet<ShopEnLigne.Models.FavoriteList> FavoriteList { get; set; }
        public DbSet<ShopEnLigne.Models.Historique> Historique { get; set; }
        public DbSet<ShopEnLigne.Models.OffreSpeciale> OffreSpeciale { get; set; }
    }
}
