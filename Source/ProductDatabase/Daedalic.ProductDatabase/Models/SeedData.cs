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
                        new Genre { Name = "Adventure" }
                    );
                }

                if (!context.Developer.Any())
                {
                    context.Developer.AddRange(
                        new Developer { Name = "Daedalic Entertainment" }
                    );
                }

                if (!context.Publisher.Any())
                {
                    context.Publisher.AddRange(
                        new Publisher { Name = "Daedalic Entertainment" }
                    );
                }

                if (!context.Platform.Any())
                {
                    context.Platform.AddRange(
                        new Platform { Name = "Windows (64-bit)" }
                    );
                }

                if (!context.Store.Any())
                {
                    context.Store.AddRange(
                        new Store { Name = "Steam" }
                    );
                }

                if (!context.Language.Any())
                {
                    context.Language.AddRange(
                        new Language { Name = "English" },
                        new Language { Name = "French" },
                        new Language { Name = "Italian" },
                        new Language { Name = "German" },
                        new Language { Name = "Spanish" },
                        new Language { Name = "Russian" },
                        new Language { Name = "Japanese" },
                        new Language { Name = "Korean" },
                        new Language { Name = "Chinese (Simplified)" },
                        new Language { Name = "Chinese (Traditional)" }
                    );
                }

                if (!context.LanguageType.Any())
                {
                    context.LanguageType.AddRange(
                        new LanguageType {  Name = "Interface" },
                        new LanguageType {  Name = "Full Audio" },
                        new LanguageType {  Name = "Subtitles" }
                    );
                }

                if (!context.LanguageStatus.Any())
                {
                    context.LanguageStatus.AddRange(
                        new LanguageStatus {  Name = "Not Started" },
                        new LanguageStatus {  Name = "Localization" },
                        new LanguageStatus {  Name = "Integration" },
                        new LanguageStatus {  Name = "Testing" },
                        new LanguageStatus {  Name = "Finished" }
                    );
                }

                if (!context.ReleaseStatus.Any())
                {
                    context.ReleaseStatus.AddRange(
                        new ReleaseStatus {  Name = "Scheduled "},
                        new ReleaseStatus {  Name = "Released "},
                        new ReleaseStatus {  Name = "Cancelled "}
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
