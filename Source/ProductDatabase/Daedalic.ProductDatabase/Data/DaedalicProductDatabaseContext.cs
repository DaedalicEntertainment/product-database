using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Data
{
    public class DaedalicProductDatabaseContext : DbContext
    {
        public DaedalicProductDatabaseContext (DbContextOptions<DaedalicProductDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Daedalic.ProductDatabase.Models.Game> Game { get; set; }

        public DbSet<Daedalic.ProductDatabase.Models.Developer> Developer { get; set; }

        public DbSet<Daedalic.ProductDatabase.Models.Genre> Genre { get; set; }

        public DbSet<Daedalic.ProductDatabase.Models.Publisher> Publisher { get; set; }

        public DbSet<Daedalic.ProductDatabase.Models.Platform> Platform { get; set; }

        public DbSet<Daedalic.ProductDatabase.Models.Store> Store { get; set; }

        public DbSet<Daedalic.ProductDatabase.Models.Language> Language { get; set; }
    }
}
