using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Daedalic.ProductDatabase.Pages.Games;

namespace Daedalic.ProductDatabase.Games
{
    public class CreateModel : GamePageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public CreateModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Game = new Game
            {
                SupportedLanguages = new List<SupportedLanguage>()
            };

            ViewData["DeveloperId"] = new SelectList(_context.Developer.OrderBy(d => d.Name), "Id", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genre.OrderBy(g => g.Name), "Id", "Name");

            Language = _context.Language.ToList();
            LanguageType = _context.LanguageType.ToList();

            return Page();
        }

        [BindProperty]
        public Game Game { get; set; }

        public IList<Language> Language { get; set; }

        public IList<LanguageType> LanguageType { get; set; }


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] newSupportedLanguages)
        {
            var newGame = new Game();

            if (newSupportedLanguages != null)
            {
                newGame.SupportedLanguages = new List<SupportedLanguage>();
                UpdateSupportedLanguages(_context, newSupportedLanguages, newGame);
            }

            if (await TryUpdateModelAsync<Game>(newGame, "Game",
                g => g.DeveloperId, g  => g.GenreId, g => g.Name, g => g.AssetIndexProjectId))
            {
                _context.Game.Add(newGame);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
