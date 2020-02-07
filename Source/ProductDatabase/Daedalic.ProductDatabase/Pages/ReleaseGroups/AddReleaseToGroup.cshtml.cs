using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daedalic.ProductDatabase.Models;
using Daedalic.ProductDatabase.Pages;
using Daedalic.ProductDatabase.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Daedalic.ProductDatabase
{
    public class AddReleaseToGroupModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;
        private readonly ConfigurationRepository _configurationRepository;

        public AddReleaseToGroupModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context, ConfigurationRepository configurationRepository)
        {
            _context = context;
            _configurationRepository = configurationRepository;
        }

        public Release Release { get; set; }

        public IList<ReleaseGroup> ReleaseGroups { get; set; }

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
                .Include(r => r.Store)
                .Include(r => r.ReleaseStatus)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Release == null)
            {
                return NotFound();
            }

            ReleaseGroups = await _context.ReleaseGroup
                .Include(g => g.Releases)
                    .ThenInclude(rirg => rirg.Release)  
                .Where(g => !g.Releases.Any(rirg => rirg.Release.Id == Release.Id))
                .OrderBy(g => g.Name)
                .ToListAsync();

            Configuration = await _configurationRepository.Load();

            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? releaseId, int? groupId)
        {
            Release release = _context.Release.Find(releaseId);

            if (release == null)
            {
                return NotFound();
            }

            ReleaseGroup releaseGroup = await _context.ReleaseGroup
                .Include(g => g.Releases)
                .ThenInclude(rirg => rirg.Release)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            if (releaseGroup == null)
            {
                return NotFound();
            }

            if (!releaseGroup.Releases.Any(rirg => rirg.Release.Id == release.Id))
            {
                ReleaseInReleaseGroup releaseInReleaseGroup = new ReleaseInReleaseGroup
                {
                    Release = release,
                    ReleaseGroup = releaseGroup
                };

                releaseGroup.Releases.Add(releaseInReleaseGroup);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Details", new RouteValues().Id(releaseGroup.Id).Build());
        }
    }
}
