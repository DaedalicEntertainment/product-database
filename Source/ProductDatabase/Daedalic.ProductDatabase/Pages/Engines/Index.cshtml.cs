using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.Engines
{
    public class IndexModel : IndexPageModel<Engine>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Engine> Engine { get;set; }

        public void OnGet(string sortOrder, string alert)
        {
            Engine = GetFilteredAndSortedItemsSlow(_context.Engine, e => e.Name, sortOrder,
                OrderBy("name", e => e.Name),
                OrderBy("version", e => e.Version)
            );

            // Show alerts.
            UpdateAlerts(alert, "Engine");
        }
    }
}
