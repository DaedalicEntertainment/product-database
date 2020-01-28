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

namespace Daedalic.ProductDatabase.Genres
{
    public class IndexModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Genre> Genre { get;set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public Dictionary<string, string> SortOrders { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            var genres = from g in _context.Genre select g;

            if (!string.IsNullOrEmpty(Filter))
            {
                genres = genres.Where(g => g.Name.Contains(Filter));
            }

            // Sort results.
            SortOrders = PageHelper.GetNewSortOrders(sortOrder, "name");

            switch (sortOrder)
            {
                case "name_desc":
                    genres = genres.OrderByDescending(g => g.Name);
                    break;
                default:
                    genres = genres.OrderBy(g => g.Name);
                    break;
            }

            Genre = await genres.AsNoTracking().ToListAsync();
        }
    }
}
