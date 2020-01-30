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

namespace Daedalic.ProductDatabase.Pages.Engines
{
    public class IndexModel : IndexPageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Engine> Engine { get;set; }

        public Dictionary<string, string> SortOrders { get; set; }

        public async Task OnGetAsync(string sortOrder, string alert)
        {
            var engines = from e in _context.Engine select e;
            if (!string.IsNullOrEmpty(Filter))
            {
                engines = engines.Where(e => e.Name.Contains(Filter));
            }

            // Sort results.
            SortOrders = PageHelper.GetNewSortOrders(sortOrder, "name", "version");

            switch (sortOrder)
            {
                case "name_desc":
                    engines = engines.OrderByDescending(e => e.Name);
                    break;
                case "version":
                    engines = engines.OrderBy(e => e.Version);
                    break;
                case "version_desc":
                    engines = engines.OrderByDescending(e => e.Version);
                    break;
                default:
                    engines = engines.OrderBy(e => e.Name);
                    break;
            }

            Engine = await engines.AsNoTracking().ToListAsync();

            // Show alerts.
            UpdateAlerts(alert, "Engine");
        }
    }
}
