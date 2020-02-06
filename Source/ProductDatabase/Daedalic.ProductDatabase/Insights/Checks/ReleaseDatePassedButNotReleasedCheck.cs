using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Insights.Checks
{
    public class ReleaseDatePassedButNotReleasedCheck : IInsightCheck
    {
        private const string InsightName = "Passed Release Dates";

        private const string InsightDescription = "Shows all releases whose release dates have passed, but whose status has not been updated. " +
            "(This could indicate we missed an update of the release date.)";

        private const string InsightDetailsPage = "/Releases/Details";

        public int Id { get; set; }

        public string Name => InsightName;

        public string Description => InsightDescription;

        public string DetailsPage => InsightDetailsPage;

        public int GetDetailsPageRouteId(InsightResult result)
        {
            return ((Release)result.Item).Id;
        }

        public List<InsightResult> Run(DaedalicProductDatabaseContext context, ConfigurationData configuration)
        {
            // Query database.
            List<InsightResult> results = new List<InsightResult>();

            foreach (Release release in context.Release
                .Include(r => r.Game)
                .Include(r => r.ReleaseStatus)
                .Include(r => r.Platform)
                .Where(r => r.ReleaseDate.HasValue &&
                r.ReleaseDate.Value < DateTime.UtcNow &&
                r.ReleaseStatusId != configuration.FinishedReleaseStatus))
            {
                // Collect results.
                string platformName = release.Platform != null ? release.Platform.Name : "none";

                results.Add(new InsightResult
                {
                    Severity = InsightResultSeverity.Warning,
                    Item = release,
                    Text = $"{release.Game.Name} ({platformName}) release date {release.ReleaseDate.Value.ToShortDateString()} has passed, but status is still {release.ReleaseStatus.Name}."
                });
            }

            return results;
        }
    }
}
