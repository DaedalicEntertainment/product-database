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
    public class DeleteModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public DeleteModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReleaseStatus ReleaseStatus { get; set; }

        public bool CanDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReleaseStatus = await _context.ReleaseStatus.Include(s => s.Releases).FirstOrDefaultAsync(m => m.Id == id);

            if (ReleaseStatus == null)
            {
                return NotFound();
            }

            CanDelete = ReleaseStatus.Releases == null || ReleaseStatus.Releases.Count == 0;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReleaseStatus = await _context.ReleaseStatus.FindAsync(id);

            if (ReleaseStatus != null)
            {
                _context.ReleaseStatus.Remove(ReleaseStatus);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new RouteValues().AlertDeleted().Build());
        }
    }
}
