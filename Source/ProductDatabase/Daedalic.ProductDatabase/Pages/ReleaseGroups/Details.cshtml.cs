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

namespace Daedalic.ProductDatabase.Pages.ReleaseGroups
{
    public class DetailsModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;
        private readonly ConfigurationRepository _configurationRepository;

        public DetailsModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context, ConfigurationRepository configurationRepository)
        {
            _context = context;
            _configurationRepository = configurationRepository;
        }

        public ReleaseGroup ReleaseGroup { get; set; }

        public DateTime? AllGMCsFinishedDate { get; private set; }

        public DateTime? AllReadyForReleaseDate { get; private set; }

        public DateTime? AllReleasedDate { get; private set; }

        public ConfigurationData Configuration { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Find release group.
            if (id == null)
            {
                return NotFound();
            }

            ReleaseGroup = await _context.ReleaseGroup
                .Include(g => g.Releases)
                    .ThenInclude(rirg => rirg.Release)
                        .ThenInclude(r => r.Game)
                .Include(g => g.Releases)
                    .ThenInclude(rirg => rirg.Release)
                        .ThenInclude(r => r.Platform)
                .Include(g => g.Releases)
                    .ThenInclude(rirg => rirg.Release)
                        .ThenInclude(r => r.ReleaseStatus)
                .Include(g => g.Releases)
                    .ThenInclude(rirg => rirg.Release)
                        .ThenInclude(r => r.Store)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ReleaseGroup == null)
            {
                return NotFound();
            }

            // Calculate release dates.
            AllGMCsFinishedDate = GetLatestDate(r => r.Release.GmcDate);
            AllReadyForReleaseDate = GetLatestDate(r => r.Release.ReadyForReleaseDate);
            AllReleasedDate = GetLatestDate(r => r.Release.ReleaseDate);

            // Load configuration.
            Configuration = await _configurationRepository.Load();

            return Page();
        }

        private DateTime? GetLatestDate(Func<ReleaseInReleaseGroup, DateTime?> dateSelector)
        {
            if (ReleaseGroup.Releases.Count == 0)
            {
                return null;
            }

            DateTime? latestDate = dateSelector(ReleaseGroup.Releases[0]);

            if (ReleaseGroup.Releases.Count == 1 || latestDate == null)
            {
                return latestDate;
            }

            for (int i = 1; i < ReleaseGroup.Releases.Count; ++i)
            {
                DateTime? otherDate = dateSelector(ReleaseGroup.Releases[i]);

                if (otherDate == null)
                {
                    return null;
                }

                if (otherDate > latestDate)
                {
                    latestDate = otherDate;
                }
            }

            return latestDate;
        }
    }
}
