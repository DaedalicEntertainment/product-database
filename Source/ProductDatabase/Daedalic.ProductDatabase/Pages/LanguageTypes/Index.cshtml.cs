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

namespace Daedalic.ProductDatabase.Pages.LanguageTypes
{
    public class IndexModel : IndexPageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<LanguageType> LanguageType { get;set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public Dictionary<string, string> SortOrders { get; set; }

        public async Task OnGetAsync(string sortOrder, string alert)
        {
            var languageTypes = from t in _context.LanguageType select t;

            if (!string.IsNullOrEmpty(Filter))
            {
                languageTypes = languageTypes.Where(t => t.Name.Contains(Filter));
            }

            // Sort results.
            SortOrders = PageHelper.GetNewSortOrders(sortOrder, "name");

            switch (sortOrder)
            {
                case "name_desc":
                    languageTypes = languageTypes.OrderByDescending(t => t.Name);
                    break;
                default:
                    languageTypes = languageTypes.OrderBy(t => t.Name);
                    break;
            }

            LanguageType = await languageTypes.AsNoTracking().ToListAsync();

            // Show alerts.
            UpdateAlerts(alert, "Language type");
        }
    }
}
