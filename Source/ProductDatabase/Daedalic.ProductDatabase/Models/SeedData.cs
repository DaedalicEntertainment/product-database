using Daedalic.ProductDatabase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DaedalicProductDatabaseContext
                (serviceProvider.GetRequiredService<DbContextOptions<DaedalicProductDatabaseContext>>()))
            {
                if (!context.Genre.Any())
                {
                    context.Genre.AddRange(
                        new Genre
                        {
                            Name = "Adventure"
                        }
                    );
                }

                if (!context.Developer.Any())
                {
                    context.Developer.AddRange(
                        new Developer
                        {
                            Name = "Daedalic Entertainment"
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
