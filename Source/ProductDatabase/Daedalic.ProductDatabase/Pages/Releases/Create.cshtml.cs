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

namespace Daedalic.ProductDatabase.Releases
{
    public class CreateModel : ReleasePageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;
        private readonly ConfigurationRepository _configurationRepository;

        public CreateModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context, ConfigurationRepository configurationRepository)
        {
            _context = context;
            _configurationRepository = configurationRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            ConfigurationData configuration = await _configurationRepository.Load();

            Release = new Release
            {
                Languages = new List<ImplementedLanguage>(),
                ReleaseDate = DateTime.UtcNow,
                GmcDate = DateTime.UtcNow,
                ReleaseStatusId = configuration.DefaultReleaseStatus
            };

            ViewData["GameId"] = new SelectList(_context.Game.OrderBy(g => g.Name), "Id", "Name");
            ViewData["PlatformId"] = new SelectList(_context.Platform.OrderBy(p => p.Name), "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Publisher.OrderBy(p => p.Name), "Id", "Name");
            ViewData["ReleaseStatusId"] = new SelectList(_context.ReleaseStatus.OrderBy(s => s.Name), "Id", "Name");
            ViewData["StoreId"] = new SelectList(_context.Store.OrderBy(s => s.Name), "Id", "Name");

            Language = _context.Language.ToList();
            LanguageType = _context.LanguageType.ToList();

            return Page();
        }

        [BindProperty]
        public Release Release { get; set; }

        public IList<Language> Language { get; set; }

        public IList<LanguageType> LanguageType { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedLanguages)
        {
            var newRelease = new Release();

            if (selectedLanguages != null)
            {
                newRelease.Languages = new List<ImplementedLanguage>();
                UpdateImplementedLanguages(_context, selectedLanguages, newRelease);
            }

            if (await TryUpdateModelAsync<Release>(newRelease, "Release",
                r => r.GameId, r => r.Summary, r => r.GmcDate, r => r.ReleaseDate, r => r.Version,
                r => r.ReleaseStatusId, r => r.PublisherId, r => r.PlatformId, r => r.StoreId))
            {
                _context.Release.Add(newRelease);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
