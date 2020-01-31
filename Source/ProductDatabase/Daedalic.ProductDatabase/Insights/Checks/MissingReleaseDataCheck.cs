using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Insights.Checks
{
    public class MissingReleaseDataCheck : IInsightCheck
    {
        private const string InsightName = "Missing Release Data";

        private const string InsightDescription = "Show all releases along with any data that hasn't been specified yet.";

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

            // Check GMC dates.
            foreach (Release release in context.Release.Include(r => r.Game).Include(r => r.Platform).Where(r => r.GmcDate == null))
            {
                results.Add(new InsightResult
                {
                    Severity = InsightResultSeverity.Warning,
                    Item = release,
                    Text = $"A release for {release.Game.Name} ({release.Platform.Name}) has no GMC date."
                });
            }

            // Check release dates.
            foreach (Release release in context.Release.Include(r => r.Game).Include(r => r.Platform).Where(r => r.ReleaseDate == null))
            {
                results.Add(new InsightResult
                {
                    Severity = InsightResultSeverity.Warning,
                    Item = release,
                    Text = $"A release for {release.Game.Name} ({release.Platform.Name}) has no release date."
                });
            }

            // Check version numbers.
            foreach (Release release in context.Release.Include(r => r.Game).Include(r => r.Platform).Where(r => r.Version.Length == 0))
            {
                results.Add(new InsightResult
                {
                    Severity = InsightResultSeverity.Warning,
                    Item = release,
                    Text = $"A release for {release.Game.Name} ({release.Platform.Name}) has no version number."
                });
            }

            // Check publishers.
            foreach (Release release in context.Release.Include(r => r.Game).Include(r => r.Platform).Include(r => r.Publisher).Where(r => r.Publisher == null))
            {
                results.Add(new InsightResult
                {
                    Severity = InsightResultSeverity.Warning,
                    Item = release,
                    Text = $"A release for {release.Game.Name} ({release.Platform.Name}) has no publisher."
                });
            }

            // Check engines.
            foreach (Release release in context.Release.Include(r => r.Game).Include(r => r.Platform).Include(r => r.Engine).Where(r => r.Engine == null))
            {
                results.Add(new InsightResult
                {
                    Severity = InsightResultSeverity.Warning,
                    Item = release,
                    Text = $"A release for {release.Game.Name} ({release.Platform.Name}) has no engine."
                });
            }

            return results;
        }
    }
}
