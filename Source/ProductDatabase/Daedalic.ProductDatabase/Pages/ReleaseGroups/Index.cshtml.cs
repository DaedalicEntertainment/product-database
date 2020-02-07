using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.ReleaseGroups
{
    public class IndexModel : IndexPageModel<ReleaseGroup>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<ReleaseGroup> ReleaseGroup { get;set; }

        public async Task OnGetAsync(string sortOrder, string alert)
        {
            var releaseGroups = from g in _context.ReleaseGroup select g;

            if (!string.IsNullOrEmpty(Filter))
            {
                releaseGroups = releaseGroups.Where(g => g.Name.Contains(Filter));
            }

            // Sort results.
            UpdateSortOrders(sortOrder, "name");

            switch (sortOrder)
            {
                case "name_desc":
                    releaseGroups = releaseGroups.OrderByDescending(g => g.Name);
                    break;
                default:
                    releaseGroups = releaseGroups.OrderBy(g => g.Name);
                    break;
            }

            ReleaseGroup = await releaseGroups.AsNoTracking().ToListAsync();

            // Show alerts.
            UpdateAlerts(alert, "Release group");
        }
    }
}
