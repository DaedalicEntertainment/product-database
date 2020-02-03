using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.LanguageStatuses
{
    public class IndexModel : IndexPageModel<LanguageStatus>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<LanguageStatus> LanguageStatus { get;set; }

        public void OnGet(string sortOrder, string alert)
        {
            LanguageStatus = GetFilteredAndSortedItemsSlow(_context.LanguageStatus, s => s.Name, sortOrder,
                OrderBy("order", s => s.Order.ToString()),
                OrderBy("name", s => s.Name)
            );

            // Show alerts.
            UpdateAlerts(alert, "Language status");
        }

        public async Task<IActionResult> OnPostUpAsync(int? id)
        {
            await ChangeOrder(_context, context => context.LanguageStatus, id, -1);
            return RedirectToPage("./Index", new RouteValues().AlertUpdated().Build());
        }

        public async Task<IActionResult> OnPostDownAsync(int? id)
        {
            await ChangeOrder(_context, context => context.LanguageStatus, id, +1);
            return RedirectToPage("./Index", new RouteValues().AlertUpdated().Build());
        }
    }
}
