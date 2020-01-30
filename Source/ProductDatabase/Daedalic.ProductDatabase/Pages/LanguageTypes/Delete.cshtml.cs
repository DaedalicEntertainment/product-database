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

            LanguageType = await _context.LanguageType.FirstOrDefaultAsync(m => m.Id == id);

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

            LanguageType = await _context.LanguageType.FindAsync(id);

            if (LanguageType != null)
            {
                _context.LanguageType.Remove(LanguageType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new RouteValues().AlertDeleted().Build());
        }
    }
}
