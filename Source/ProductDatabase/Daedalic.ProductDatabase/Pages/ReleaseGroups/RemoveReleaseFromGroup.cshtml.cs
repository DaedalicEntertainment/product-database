using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daedalic.ProductDatabase.Models;
using Daedalic.ProductDatabase.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Daedalic.ProductDatabase
{
    public class RemoveReleaseFromGroupModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public RemoveReleaseFromGroupModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public Release Release { get; set; }

        public ReleaseGroup ReleaseGroup { get; set; }

        public async Task<IActionResult> OnGetAsync(int? releaseId, int? groupId)
        {
            if (releaseId == null || groupId == null)
            {
                return NotFound();
            }

            Release = await _context.Release
                .Include(r => r.Game)
                .Include(r => r.Platform)
                .Include(r => r.Store)
                .FirstOrDefaultAsync(r => r.Id == releaseId);

            ReleaseGroup = await _context.ReleaseGroup.FirstOrDefaultAsync(g => g.Id == groupId);

            if (Release == null || ReleaseGroup == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? releaseId, int? groupId)
        {
            if (releaseId == null || groupId == null)
            {
                return NotFound();
            }

            ReleaseGroup = await _context.ReleaseGroup
                .Include(g => g.Releases)
                    .ThenInclude(rirg => rirg.Release)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            if (ReleaseGroup == null)
            {
                return NotFound();
            }

            ReleaseInReleaseGroup releaseInReleaseGroup = ReleaseGroup.Releases.FirstOrDefault(rirg => rirg.Release.Id == releaseId);

            if (releaseInReleaseGroup != null)
            {
                _context.Remove(releaseInReleaseGroup);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Details", new RouteValues().Id(ReleaseGroup.Id).Build());
        }
    }
}