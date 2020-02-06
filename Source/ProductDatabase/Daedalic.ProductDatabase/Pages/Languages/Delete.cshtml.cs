using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.Languages
{
    public class DeleteModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public DeleteModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Language Language { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Language = await _context.Language.Include(l => l.SupportedLanguages).ThenInclude(sl => sl.Game).FirstOrDefaultAsync(l => l.Id == id);

            if (Language == null)
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

            Language = await _context.Language
                .Include(l => l.SupportedLanguages)
                    .ThenInclude(sl => sl.Game)
                        .ThenInclude(g => g.Releases)
                            .ThenInclude(r => r.Languages)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (Language != null)
            {
                foreach (SupportedLanguage supportedLanguage in Language.SupportedLanguages)
                {
                    foreach (Release release in supportedLanguage.Game.Releases)
                    {
                        // Remove from releases.
                        List<ReleasedLanguage> releasedLanguages = release.Languages.Where(rl => rl.LanguageId == Language.Id).ToList();
                        
                        foreach (var removedLanguage in releasedLanguages)
                        {
                            release.Languages.Remove(removedLanguage);
                        }
                    }

                    // Remove from games.
                    if (supportedLanguage.Game.ImplementedLanguages != null)
                    {
                        List<ImplementedLanguage> implementedLanguages = supportedLanguage.Game.ImplementedLanguages.Where(il => il.LanguageId == Language.Id).ToList();
                        
                        foreach (var removedLanguage in implementedLanguages)
                        {
                            supportedLanguage.Game.ImplementedLanguages.Remove(removedLanguage);
                        }
                    }
                    
                    List<SupportedLanguage> supportedLanguages = supportedLanguage.Game.SupportedLanguages.Where(sl => sl.LanguageId == Language.Id).ToList();
                        
                    foreach (var removedLanguage in supportedLanguages)
                    {
                        supportedLanguage.Game.SupportedLanguages.Remove(removedLanguage);
                    }
                }
                
                // Remove language.
                _context.Language.Remove(Language);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new RouteValues().AlertDeleted().Build());
        }
    }
}
