using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.Platforms
{
    public class IndexModel : IndexPageModel<Platform>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Platform> Platform { get;set; }

        public void OnGet(string sortOrder, string alert)
        {
            Platform = GetFilteredAndSortedItemsSlow(_context.Platform, p => p.Name, sortOrder,
                OrderBy("name", p => p.Name)
            );

            // Show alerts.
            UpdateAlerts(alert, "Platform");
        }
    }
}
