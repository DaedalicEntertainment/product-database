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
                        new Platform { Name = "Nintendo Switch" },
                        new Platform { Name = "PlayStation 4" },
                        new Platform { Name = "Xbox One" },
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
                        new Language { Name = "Chinese (Simplified)" },
                        new Language { Name = "Chinese (Traditional)" },
                        new Language { Name = "English" },
                        new Language { Name = "French" },
                        new Language { Name = "German" },
                        new Language { Name = "Italian" },
                        new Language { Name = "Japanese" },
                        new Language { Name = "Korean" },
                        new Language { Name = "Polish" },
                        new Language { Name = "Russian" },
                        new Language { Name = "Spanish" },
                        new Language { Name = "Turkish" }
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
                        new LanguageStatus {  Name = "Not Started", Order = 0, Summary = "Localizing for this language hasn't started yet." },
                        new LanguageStatus {  Name = "Localization", Order = 1, Summary = "We're currently waiting for our external partners to translate the texts."},
                        new LanguageStatus {  Name = "Integration", Order = 2, Summary = "Translations are finished and are being integrated in the game build." },
                        new LanguageStatus {  Name = "Testing", Order = 3, Summary = "Texts have been integrated and are being tested by the localization QA." },
                        new LanguageStatus {  Name = "Finished", Order = 4, Summary = "All texts have been translated, integrated and verified." }
                    );
                }

                if (!context.ReleaseStatus.Any())
                {
                    context.ReleaseStatus.AddRange(
                        new ReleaseStatus {  Name = "To Be Evaluated", Order = 0 },
                        new ReleaseStatus {  Name = "Scheduled", Order = 1 },
                        new ReleaseStatus {  Name = "On Hold", Order = 2 },
                        new ReleaseStatus { Name = "Released", Order = 3 } ,
                        new ReleaseStatus {  Name = "Cancelled", Order = 4 }
                    );
                }

                if (!context.Engine.Any())
                {
                    context.Engine.AddRange(
                        new Engine { Name = "Visionaire", Version = "4" },
                        new Engine { Name = "Visionaire", Version = "5" }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
