using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.Genres
{
    public class IndexModel : IndexPageModel<Genre>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Genre> Genre { get;set; }

        public void OnGet(string sortOrder, string alert)
        {
            Genre = GetFilteredAndSortedItemsSlow(_context.Genre, g => g.Name, sortOrder,
                OrderBy("name", g => g.Name)
            );

            // Show alerts.
            UpdateAlerts(alert, "Genre");
        }
    }
}
