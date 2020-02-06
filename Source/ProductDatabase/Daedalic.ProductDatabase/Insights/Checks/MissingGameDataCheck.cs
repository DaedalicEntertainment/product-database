using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Insights.Checks
{
    public class MissingGameDataCheck : IInsightCheck
    {
        private const string InsightName = "Missing Game Data";

        private const string InsightDescription = "Show all games along with any data that hasn't been specified yet.";

        private const string InsightDetailsPage = "/Games/Details";

        public int Id { get; set; }

        public string Name => InsightName;

        public string Description => InsightDescription;

        public string DetailsPage => InsightDetailsPage;

        public int GetDetailsPageRouteId(InsightResult result)
        {
            return ((Game)result.Item).Id;
        }

        public List<InsightResult> Run(DaedalicProductDatabaseContext context, ConfigurationData configuration)
        {
            // Query database.
            List<InsightResult> results = new List<InsightResult>();

            // Check developers.
            foreach (Game game in context.Game.Include(g => g.Developer).Where(g => g.Developer == null))
            {
                results.Add(new InsightResult
                {
                    Severity = InsightResultSeverity.Warning,
                    Item = game,
                    Text = $"{game.Name} has no developer."
                });
            }

            // Check genres.
            foreach (Game game in context.Game.Include(g => g.Genre).Where(g => g.Genre == null))
            {
                results.Add(new InsightResult
                {
                    Severity = InsightResultSeverity.Warning,
                    Item = game,
                    Text = $"{game.Name} has no genre."
                });
            }

            // Check releases.
            foreach (Game game in context.Game.Include(g => g.Releases).Where(g => g.Releases.Count == 0))
            {
                results.Add(new InsightResult
                {
                    Severity = InsightResultSeverity.Warning,
                    Item = game,
                    Text = $"{game.Name} has no releases."
                });
            }

            // Check languages.
            foreach (Game game in context.Game.Include(g => g.SupportedLanguages).Where(g => g.SupportedLanguages.Count == 0))
            {
                results.Add(new InsightResult
                {
                    Severity = InsightResultSeverity.Warning,
                    Item = game,
                    Text = $"{game.Name} has no languages."
                });
            }

            return results;
        }
    }
}
