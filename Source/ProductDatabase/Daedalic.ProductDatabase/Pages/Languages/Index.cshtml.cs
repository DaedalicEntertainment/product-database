using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.Languages
{
    public class IndexModel : IndexPageModel<Language>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Language> Language { get;set; }

        public void OnGet(string sortOrder, string alert)
        {
            Language = GetFilteredAndSortedItemsSlow(_context.Language, l => l.Name, sortOrder,
                OrderBy("name", l => l.Name)
            );

            // Show alerts.
            UpdateAlerts(alert, "Language");
        }
    }
}
