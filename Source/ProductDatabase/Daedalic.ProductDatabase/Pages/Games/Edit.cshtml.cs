using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Daedalic.ProductDatabase.Pages.Games;

namespace Daedalic.ProductDatabase.Pages.Games
{
    public class EditModel : GamePageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public EditModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Game Game { get; set; }

        public IList<Language> Language { get; set; }

        public IList<LanguageType> LanguageType { get; set; }

        public IList<LanguageStatus> LanguageStatus { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game game = await GetGameById(id.Value);

            if (game == null)
            {
                return NotFound();
            }

            ViewData["DeveloperId"] = new SelectList(_context.Developer.OrderBy(d => d.Name), "Id", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genre.OrderBy(g => g.Name), "Id", "Name");

            PreparePage(game);

            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] newSupportedLanguages, Dictionary<string, string> newLanguageStatuses)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameToUpdate = await GetGameById(id.Value);

            if (gameToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Game>(
                gameToUpdate,
                "Game",
                g => g.Name, g => g.DeveloperId, g => g.GenreId, g => g.AssetIndexProjectId,
                g => g.WebsiteUrl, g => g.FacebookPageName, g => g.TwitterHandle))
            {
                UpdateSupportedLanguages(_context, newSupportedLanguages, gameToUpdate);
                UpdateImplementedLanguages(_context, newLanguageStatuses, gameToUpdate);

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index", new RouteValues().AlertUpdated().Build());
            }
            
            UpdateSupportedLanguages(_context, newSupportedLanguages, gameToUpdate);
            UpdateImplementedLanguages(_context, newLanguageStatuses, gameToUpdate);

            PreparePage(gameToUpdate);

            return Page(); 
        }

        public bool IsLanguageStatus(Language language, LanguageStatus status)
        {
            LanguageStatus defaultStatus = LanguageStatus.First();

            if (Game.ImplementedLanguages == null)
            {
                return status.Id == defaultStatus.Id;
            }

            ImplementedLanguage implementedLanguage = Game.ImplementedLanguages.FirstOrDefault(il => il.LanguageId == language.Id);

            if (implementedLanguage == null)
            {
                return status.Id == defaultStatus.Id;
            }

            return status.Id == implementedLanguage.LanguageStatusId;
        }

        private void PreparePage(Game game)
        {
            Game = game;
            Language = _context.Language.ToList();
            LanguageType = _context.LanguageType.ToList();
            LanguageStatus = _context.LanguageStatus.ToList();
        }

        private Task<Game> GetGameById(int id)
        {
            return _context.Game
                .Include(g => g.Developer)
                .Include(g => g.Genre)
                .Include(r => r.SupportedLanguages)
                    .ThenInclude(l => l.Language)
                .Include(r => r.SupportedLanguages)
                    .ThenInclude(l => l.LanguageType)
                .Include(r => r.ImplementedLanguages)
                    .ThenInclude(l => l.Language)
                .Include(r => r.ImplementedLanguages)
                    .ThenInclude(l => l.LanguageStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
