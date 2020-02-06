using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.Stores
{
    public class IndexModel : IndexPageModel<Store>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Store> Store { get;set; }

        public void OnGet(string sortOrder, string alert)
        {
            Store = GetFilteredAndSortedItemsSlow(_context.Store, s => s.Name, sortOrder,
                OrderBy("name", s => s.Name)
            );

            // Show alerts.
            UpdateAlerts(alert, "Store");
        }
    }
}
