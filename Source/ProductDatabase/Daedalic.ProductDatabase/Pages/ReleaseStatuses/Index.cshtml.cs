using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.ReleaseStatuses
{
    public class IndexModel : IndexPageModel<ReleaseStatus>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<ReleaseStatus> ReleaseStatus { get;set; }

        public void OnGet(string sortOrder, string alert)
        {
            ReleaseStatus = GetFilteredAndSortedItemsSlow(_context.ReleaseStatus, s => s.Name, sortOrder,
                OrderBy("name", s => s.Name)
            );

            // Show alerts.
            UpdateAlerts(alert, "Release status");
        }
    }
}
