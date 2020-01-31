using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.Releases
{
    public class IndexModel : IndexPageModel<Release>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Release> Release { get;set; }

        public async Task OnGetAsync(string sortOrder, string alert)
        {
            var releases = from r in  _context.Release
                .Include(r => r.Game)
                .Include(r => r.Platform)
                .Include(r => r.Publisher)
                .Include(r => r.ReleaseStatus)
                .Include(r => r.Store) select r;

            if (!string.IsNullOrEmpty(Filter))
            {
                releases = releases.Where(r => r.Summary.Contains(Filter));
            }

            // Sort results.
            UpdateSortOrders(sortOrder, "game", "platform", "store", "gmcdate", "releasedate", "publisher", "status");

            switch (sortOrder)
            {
                case "game_desc":
                    releases = releases.OrderByDescending(r => r.Game.Name);
                    break;
                case "platform":
                    releases = releases.OrderBy(r => r.Platform.Name);
                    break;
                case "platform_desc":
                    releases = releases.OrderByDescending(r => r.Platform.Name);
                    break;
                case "store":
                    releases = releases.OrderBy(r => r.Store.Name);
                    break;
                case "store_desc":
                    releases = releases.OrderByDescending(r => r.Store.Name);
                    break;
                case "gmcdate":
                    releases = releases.OrderBy(r => r.GmcDate);
                    break;
                case "gmcdate_desc":
                    releases = releases.OrderByDescending(r => r.GmcDate);
                    break;
                case "releasedate":
                    releases = releases.OrderBy(r => r.ReleaseDate);
                    break;
                case "releasedate_desc":
                    releases = releases.OrderByDescending(r => r.ReleaseDate);
                    break;
                case "publisher":
                    releases = releases.OrderBy(r => r.Publisher.Name);
                    break;
                case "publisher_desc":
                    releases = releases.OrderByDescending(r => r.Publisher.Name);
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
