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
                if (context.Game.Any())
                {
                    // DB already seeded.
                    return;   
                }

                context.Game.AddRange(
                    new Game
                    {
                        Name = "Anna's Quest"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
