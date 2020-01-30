using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.LanguageTypes
{
    public class IndexModel : IndexPageModel<LanguageType>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<LanguageType> LanguageType { get;set; }

        public void OnGet(string sortOrder, string alert)
        {
            LanguageType = GetFilteredAndSortedItemsSlow(_context.LanguageType, t => t.Name, sortOrder,
                OrderBy("name", t => t.Name)
            );

            // Show alerts.
            UpdateAlerts(alert, "Language type");
        }
    }
}
