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

namespace Daedalic.ProductDatabase.ReleaseStatuses
{
    public class IndexModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<ReleaseStatus> ReleaseStatus { get;set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public Dictionary<string, string> SortOrders { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            var statuses = from s in _context.ReleaseStatus select s;

            if (!string.IsNullOrEmpty(Filter))
            {
                statuses = statuses.Where(s => s.Name.Contains(Filter));
            }

            // Sort results.
            SortOrders = PageHelper.GetNewSortOrders(sortOrder, "name");

            switch (sortOrder)
            {
                case "name_desc":
                    statuses = statuses.OrderByDescending(s => s.Name);
                    break;
                default:
                    statuses = statuses.OrderBy(s => s.Name);
                    break;
            }

            ReleaseStatus = await statuses.AsNoTracking().ToListAsync();
        }
    }
}
