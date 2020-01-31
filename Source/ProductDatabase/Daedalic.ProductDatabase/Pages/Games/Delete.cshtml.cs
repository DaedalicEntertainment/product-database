using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public DeleteModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Game Game { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game = await _context.Game
                .Include(g => g.Developer)
                .Include(g => g.Genre)
                .Include(g => g.Releases)
                .Include(g => g.SupportedLanguages)
                    .ThenInclude(l => l.Language)
                .Include(g => g.SupportedLanguages)
                    .ThenInclude(l => l.LanguageType)
                .Include(g => g.ImplementedLanguages)
                    .ThenInclude(l => l.Language)
                .Include(g => g.ImplementedLanguages)
                    .ThenInclude(l => l.LanguageStatus)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Game == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch releases along with game to ensure cascading delete.
            Game = await _context.Game.Include(g => g.Releases).FirstOrDefaultAsync(g => g.Id == id);

            if (Game != null)
            {
                _context.Game.Remove(Game);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new RouteValues().AlertDeleted().Build());
        }
    }
}
