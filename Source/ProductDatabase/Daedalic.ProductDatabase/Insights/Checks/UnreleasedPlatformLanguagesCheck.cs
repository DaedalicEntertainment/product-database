using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Insights.Checks
{
    public class UnreleasedPlatformLanguagesCheck : IInsightCheck
    {
        private const string InsightName = "Unreleased Platform Languages";

        private const string InsightDescription = "Shows all languages that are released on at least one platform, but not on all of them.";

        private const string InsightDetailsPage = "/Games/Details";

        public string Name => InsightName;

        public string Description => InsightDescription;

        public List<InsightResult> Run(DaedalicProductDatabaseContext context)
        {
            // Query database.
            List<UnreleasedLanguage> unreleasedLanguages = new List<UnreleasedLanguage>();

            foreach (Game game in context.Game
                .Include(g => g.Releases)
                    .ThenInclude(r => r.Languages)
                .Include(g => g.Releases)
                    .ThenInclude(r => r.Platform)
                .Include(g => g.SupportedLanguages)
                    .ThenInclude(sl => sl.Language))
            {
                if (game.SupportedLanguages != null)
                {
                    foreach (Language language in game.SupportedLanguages.Select(sl => sl.Language).Distinct())
                    {
                        foreach (Platform platform in game.Releases.Select(r => r.Platform).Distinct())
                        {
                            if (!game.Releases.Any(r => r.Platform == platform && r.Languages.Any(l => l.LanguageId == language.Id)))
                            {
                                unreleasedLanguages.Add(new UnreleasedLanguage { Game = game, Platform = platform, Language = language });
                            }
                        }
                    }
                }
            }

            // Collect results.
            List<InsightResult> results = new List<InsightResult>();

            foreach (UnreleasedLanguage unreleasedLanguage in unreleasedLanguages)
            {
                results.Add(new InsightResult
                {
                    Item = unreleasedLanguage.Game,
                    Text = $"{unreleasedLanguage.Game.Name} supports {unreleasedLanguage.Language.Name}, but that language is not being released on {unreleasedLanguage.Platform.Name}."
                });
            }

            return results;
        }

        public string DetailsPage => InsightDetailsPage;

        public int GetDetailsPageRouteId(InsightResult result)
        {
            return ((Game)result.Item).Id;
        }

        private class UnreleasedLanguage
        {
            public Game Game { get; set; }

            public Platform Platform { get; set; }

            public Language Language { get; set; }
        }
    }
}
