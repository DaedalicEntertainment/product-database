using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.Releases
{
    public class DetailsModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public DetailsModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public Release Release { get; set; }

        public List<Release> Releases { get; set; }

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

            Releases = await _context.Release
                .Include(r => r.Game)
                .Include(r => r.Platform)
                .Include(r => r.Store)
                .ToListAsync();

            return Page();
        }
    }
}
