using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Models;
using Daedalic.ProductDatabase.Repositories;

namespace Daedalic.ProductDatabase.Pages.Configuration
{
    public class IndexModel : IndexPageModel<Daedalic.ProductDatabase.Models.Configuration>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;
        private readonly ConfigurationRepository _configurationRepository;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context, ConfigurationRepository configurationRepository)
        {
            _context = context;
            _configurationRepository = configurationRepository;
        }

        [BindProperty]
        public ConfigurationData Configuration { get; set; }

        public async Task<IActionResult> OnGetAsync(string alert)
        {
            Configuration = await _configurationRepository.Load();
            
            ViewData["ReleaseStatusId"] = new SelectList(_context.ReleaseStatus.OrderBy(s => s.Order).ThenBy(s => s.Name), "Id", "Name");

            // Show alerts.
            UpdateAlerts(alert, "Configuration");

            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            await _configurationRepository.Save(Configuration);

            return RedirectToPage("/Configuration/Index", new RouteValues().AlertUpdated().Build());
        }
    }
}
