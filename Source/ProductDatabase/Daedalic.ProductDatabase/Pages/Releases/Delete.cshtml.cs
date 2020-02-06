using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Daedalic.ProductDatabase.Repositories;

namespace Daedalic.ProductDatabase.Pages.Releases
{
    public class DeleteModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;
        private readonly ConfigurationRepository _configurationRepository;

        public DeleteModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context, ConfigurationRepository configurationRepository)
        {
            _context = context;
            _configurationRepository = configurationRepository;
        }

        [BindProperty]
        public Release Release { get; set; }

        public List<Release> Releases { get; set; }

        public ConfigurationData Configuration { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Release = await _context.Release
                .Include(r => r.Game)
                .Include(r => r.Platform)
                .Include(r => r.Publisher)
                .Include(r => r.ReleaseStatus)
                .Include(r => r.Store)
                .Include(r => r.Languages)
                    .ThenInclude(l => l.Language)
                .Include(r => r.Engine)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Release == null)
            {
                return NotFound();
            }

            Configuration = await _configurationRepository.Load();
            Releases = await _context.Release
                .Include(r => r.Game)
                .Include(r => r.Platform)
                .Include(r => r.Store)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Release = await _context.Release.FindAsync(id);

            if (Release != null)
            {
                _context.Release.Remove(Release);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new RouteValues().AlertDeleted().Build());
        }
    }
}
