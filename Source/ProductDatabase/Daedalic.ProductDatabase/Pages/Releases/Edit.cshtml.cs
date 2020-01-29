﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Releases
{
    public class EditModel : ReleasePageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public EditModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Release Release { get; set; }

        public IList<Language> Language { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Release = await GetReleaseById(id.Value);

            if (Release == null)
            {
                return NotFound();
            }

            ViewData["GameId"] = new SelectList(_context.Game.OrderBy(g => g.Name), "Id", "Name");
            ViewData["PlatformId"] = new SelectList(_context.Platform.OrderBy(p => p.Name), "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Publisher.OrderBy(p => p.Name), "Id", "Name");
            ViewData["ReleaseStatusId"] = new SelectList(_context.ReleaseStatus.OrderBy(s => s.Name), "Id", "Name");
            ViewData["StoreId"] = new SelectList(_context.Store.OrderBy(s => s.Name), "Id", "Name");

            Language = Release.Game.SupportedLanguages != null
                ? Release.Game.SupportedLanguages.Select(sl => sl.Language).Distinct().ToList()
                : new List<Language>();

            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, int[] selectedLanguages)
        {
            if (id == null)
            {
                return NotFound();
            }

            var releaseToUpdate = await GetReleaseById(id.Value);

            if (releaseToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Release>(
                releaseToUpdate,
                "Release",
                r => r.GameId, r => r.Summary, r => r.GmcDate, r => r.ReleaseDate, r => r.Version,
                r => r.ReleaseStatusId, r => r.PublisherId, r => r.PlatformId, r => r.StoreId))
            {
                UpdateReleasedLanguages(_context, selectedLanguages, releaseToUpdate);

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            
            UpdateReleasedLanguages(_context, selectedLanguages, releaseToUpdate);
            return Page(); 
        }

        private Task<Release> GetReleaseById(int id)
        {
            return _context.Release
                .Include(r => r.Game)
                    .ThenInclude(g => g.SupportedLanguages)
                        .ThenInclude(l => l.Language)
                .Include(r => r.Platform)
                .Include(r => r.Publisher)
                .Include(r => r.Status)
                .Include(r => r.Store)
                .Include(r => r.Languages)
                    .ThenInclude(l => l.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
