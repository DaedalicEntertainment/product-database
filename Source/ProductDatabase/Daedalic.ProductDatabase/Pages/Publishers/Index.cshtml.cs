using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.Publishers
{
    public class IndexModel : IndexPageModel<Publisher>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get;set; }

        public async Task OnGetAsync(string sortOrder, string alert)
        {
            var publishers = from p in _context.Publisher select p;

            if (!string.IsNullOrEmpty(Filter))
            {
                publishers = publishers.Where(p => p.Name.Contains(Filter));
            }

            // Sort results.
            UpdateSortOrders(sortOrder, "name");

            switch (sortOrder)
            {
                case "name_desc":
                    publishers = publishers.OrderByDescending(p => p.Name);
                    break;
                default:
                    publishers = publishers.OrderBy(p => p.Name);
                    break;
            }

            Publisher = await publishers.AsNoTracking().ToListAsync();

            // Show alerts.
            UpdateAlerts(alert, "Publisher");
        }
    }
}
