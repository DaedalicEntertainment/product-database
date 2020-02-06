using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Daedalic.ProductDatabase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Daedalic.ProductDatabase.Pages.Releases
{
    public class CreateForGameModel : ReleasePageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;
        private readonly ConfigurationRepository _configurationRepository;

        public CreateForGameModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context, ConfigurationRepository configurationRepository)
        {
            _context = context;
            _configurationRepository = configurationRepository;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            ConfigurationData configuration = await _configurationRepository.Load();

            Release = new Release
            {
                Game = _context.Game.Include(g => g.SupportedLanguages).ThenInclude(l => l.Language).FirstOrDefault(g => g.Id == id),
                GameId = id,
                Languages = new List<ReleasedLanguage>(),
                ReleaseDate = DateTime.UtcNow,
                GmcDate = DateTime.UtcNow,
                ReleaseStatusId = configuration.DefaultReleaseStatus
            };

            ViewData["PlatformId"] = new SelectList(_context.Platform.OrderBy(p => p.Name), "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Publisher.OrderBy(p => p.Name), "Id", "Name");
            ViewData["ReleaseStatusId"] = new SelectList(_context.ReleaseStatus.OrderBy(s => s.Order).ThenBy(s => s.Name), "Id", "Name");
            ViewData["StoreId"] = new SelectList(_context.Store.OrderBy(s => s.Name), "Id", "Name");

            Language = Release.Game.SupportedLanguages != null
                ? Release.Game.SupportedLanguages.Select(sl => sl.Language).Distinct().ToList()
                : new List<Language>();
            Engine = _context.Engine.OrderBy(e => e.Name).ThenBy(e => e.Version).ToList();

            return Page();
        }

        [BindProperty]
        public Release Release { get; set; }

        public IList<Language> Language { get; set; }

        public IList<Engine> Engine { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int[] selectedLanguages)
        {
            var newRelease = new Release();

            if (selectedLanguages != null)
            {
                newRelease.Languages = new List<ReleasedLanguage>();
                UpdateReleasedLanguages(_context, selectedLanguages, newRelease);
            }

            if (await TryUpdateModelAsync<Release>(newRelease, "Release",
                r => r.GameId, r => r.Summary, r => r.GmcDate, r => r.ReleaseDate, r => r.Version,
                r => r.ReleaseStatusId, r => r.PublisherId, r => r.PlatformId, r => r.StoreId,
                r => r.EarlyAccess, r => r.EngineId))
            {
                _context.Release.Add(newRelease);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index", new RouteValues().AlertCreated().Build());
            }

            return Page();
        }
    }
}
