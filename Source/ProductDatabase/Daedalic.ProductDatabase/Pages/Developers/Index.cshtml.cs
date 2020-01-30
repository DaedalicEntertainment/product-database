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

namespace Daedalic.ProductDatabase.Pages.Developers
{
    public class IndexModel : IndexPageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Developer> Developer { get;set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public Dictionary<string, string> SortOrders { get; set; }

        public async Task OnGetAsync(string sortOrder, string alert)
        {
            var developers = from d in _context.Developer select d;
            if (!string.IsNullOrEmpty(Filter))
            {
                developers = developers.Where(d => d.Name.Contains(Filter));
            }

            // Sort results.
            SortOrders = PageHelper.GetNewSortOrders(sortOrder, "name");

            switch (sortOrder)
            {
                case "name_desc":
                    developers = developers.OrderByDescending(d => d.Name);
                    break;
                default:
                    developers = developers.OrderBy(d => d.Name);
                    break;
            }

            Developer = await developers.AsNoTracking().ToListAsync();

            // Show alerts.
            UpdateAlerts(alert, "Developer");
        }
    }
}
