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

namespace Daedalic.ProductDatabase.Languages
{
    public class IndexModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Language> Language { get;set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public Dictionary<string, string> SortOrders { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            var languages = from l in _context.Language select l;

            if (!string.IsNullOrEmpty(Filter))
            {
                languages = languages.Where(l => l.Name.Contains(Filter));
            }

            // Sort results.
            SortOrders = PageHelper.GetNewSortOrders(sortOrder, "name");

            switch (sortOrder)
            {
                case "name_desc":
                    languages = languages.OrderByDescending(l => l.Name);
                    break;
                default:
                    languages = languages.OrderBy(l => l.Name);
                    break;
            }

            Language = await languages.AsNoTracking().ToListAsync();
        }
    }
}
