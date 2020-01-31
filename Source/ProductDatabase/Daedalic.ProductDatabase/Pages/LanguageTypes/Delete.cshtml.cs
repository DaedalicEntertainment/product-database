using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.LanguageTypes
{
    public class DeleteModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public DeleteModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LanguageType LanguageType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LanguageType = await _context.LanguageType.Include(t => t.SupportedLanguages).ThenInclude(sl => sl.Game).FirstOrDefaultAsync(t => t.Id == id);

            if (LanguageType == null)
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

            LanguageType = await _context.LanguageType
                .Include(t => t.SupportedLanguages)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (LanguageType != null)
            {
                // Remove supported languages.
                List<SupportedLanguage> supportedLanguages = LanguageType.SupportedLanguages.Where(sl => sl.LanguageTypeId == LanguageType.Id).ToList();
                        
                foreach (var removedLanguage in supportedLanguages)
                {
                    _context.Remove(removedLanguage);
                }
                
                // Remove type.
                _context.LanguageType.Remove(LanguageType);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new RouteValues().AlertDeleted().Build());
        }
    }
}
