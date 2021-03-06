﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Pages.LanguageStatuses
{
    public class DeleteModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public DeleteModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LanguageStatus LanguageStatus { get; set; }

        public bool CanDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LanguageStatus = await _context.LanguageStatus.Include(s => s.ImplementedLanguages).ThenInclude(il => il.Game).FirstOrDefaultAsync(m => m.Id == id);

            if (LanguageStatus == null)
            {
                return NotFound();
            }

            CanDelete = LanguageStatus.ImplementedLanguages == null || LanguageStatus.ImplementedLanguages.Count == 0;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LanguageStatus = await _context.LanguageStatus.FindAsync(id);

            if (LanguageStatus != null)
            {
                _context.LanguageStatus.Remove(LanguageStatus);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new RouteValues().AlertDeleted().Build());
        }
    }
}
