using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Daedalic.ProductDatabase.Helpers;

namespace Daedalic.ProductDatabase.Pages.Platforms
{
    public class IndexModel : IndexPageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Platform> Platform { get;set; }

        public Dictionary<string, string> SortOrders { get; set; }

        public async Task OnGetAsync(string sortOrder, string alert)
        {
            var platforms = from p in _context.Platform select p;

            if (!string.IsNullOrEmpty(Filter))
            {
                platforms = platforms.Where(p => p.Name.Contains(Filter));
            }

            // Sort results.
            SortOrders = PageHelper.GetNewSortOrders(sortOrder, "name");

            switch (sortOrder)
            {
                case "name_desc":
                    platforms = platforms.OrderByDescending(p => p.Name);
                    break;
                default:
                    platforms = platforms.OrderBy(p => p.Name);
                    break;
            }

            Platform = await platforms.AsNoTracking().ToListAsync();

            // Show alerts.
            UpdateAlerts(alert, "Platform");
        }
    }
}
