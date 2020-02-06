using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Daedalic.ProductDatabase.Repositories;
using System.Globalization;

namespace Daedalic.ProductDatabase.Pages.Releases
{
    public class IndexModel : IndexPageModel<Release>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;
        private readonly ConfigurationRepository _configurationRepository;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context, ConfigurationRepository configurationRepository)
        {
            _context = context;
            _configurationRepository = configurationRepository;
        }

        public IList<Release> Release { get;set; }

        [BindProperty(SupportsGet = true)]
        public string ShowReleased { get; set; }

        public ConfigurationData Configuration { get; set; }

        public async Task OnGetAsync(string sortOrder, string alert)
        {
            Configuration = await _configurationRepository.Load();

            var releases = from r in  _context.Release
                .Include(r => r.Game)
                .Include(r => r.Platform)
                .Include(r => r.Publisher)
                .Include(r => r.ReleaseStatus)
                .Include(r => r.Store) select r;

            // Apply filters.
            if (!string.IsNullOrEmpty(Filter))
            {
                string[] filters = Filter.Split(' ');

                foreach (string f in filters)
                {
                    releases = releases.Where(r =>
                        r.Game.Name.Contains(f) ||
                        r.Platform.Name.Contains(f) ||
                        r.Store.Name.Contains(f));
                }
            }

            if (ShowReleased != "on")
            {
                releases = releases.Where(r => r.ReleaseStatusId != Configuration.FinishedReleaseStatus);
            }

            // Sort results.
            UpdateSortOrders(sortOrder, "game", "platform", "gmcdate", "readyforreleasedate", "releasedate", "publisher", "status");

            switch (sortOrder)
            {
                case "game_desc":
                    releases = releases.OrderByDescending(r => r.Game.Name);
                    break;
                case "platform":
                    releases = releases.OrderBy(r => r.Platform.Name).ThenBy(r => r.Store.Name);
                    break;
                case "platform_desc":
                    releases = releases.OrderByDescending(r => r.Platform.Name).ThenByDescending(r => r.Store.Name);
                    break;
                case "gmcdate":
                    releases = releases.OrderBy(r => r.GmcDate);
                    break;
                case "gmcdate_desc":
                    releases = releases.OrderByDescending(r => r.GmcDate);
                    break;
                case "readyforreleasedate":
                    releases = releases.OrderBy(r => r.ReadyForReleaseDate);
                    break;
                case "readyforreleasedate_desc":
                    releases = releases.OrderByDescending(r => r.ReadyForReleaseDate);
                    break;
                case "releasedate":
                    releases = releases.OrderBy(r => r.ReleaseDate);
                    break;
                case "releasedate_desc":
                    releases = releases.OrderByDescending(r => r.ReleaseDate);
                    break;
                case "status":
                    releases = releases.OrderBy(r => r.ReleaseStatus.Name);
                    break;
                case "status_desc":
                    releases = releases.OrderByDescending(r => r.ReleaseStatus.Name);
                    break;
                default:
                    releases = releases
                                .OrderBy(r => r.Game.Name)
                                .ThenBy(r => r.Platform.Name)
                                .ThenBy(r => r.Store.Name);
                    break;
            }

            Release = await releases.AsNoTracking().ToListAsync();

            // Show alerts.
            UpdateAlerts(alert, "Release");
        }
    }
}
