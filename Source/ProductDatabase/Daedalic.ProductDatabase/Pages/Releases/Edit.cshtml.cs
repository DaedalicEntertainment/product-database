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

namespace Daedalic.ProductDatabase.Releases
{
    public class EditModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public EditModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Release Release { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Release = await _context.Release
                .Include(r => r.Game)
                .Include(r => r.Platform)
                .Include(r => r.Publisher)
                .Include(r => r.Status)
                .Include(r => r.Store).FirstOrDefaultAsync(m => m.Id == id);

            if (Release == null)
            {
                return NotFound();
            }
           ViewData["GameId"] = new SelectList(_context.Game, "Id", "Name");
           ViewData["PlatformId"] = new SelectList(_context.Platform, "Id", "Name");
           ViewData["PublisherId"] = new SelectList(_context.Publisher, "Id", "Name");
           ViewData["ReleaseStatusId"] = new SelectList(_context.ReleaseStatus, "Id", "Name");
           ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Release).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReleaseExists(Release.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReleaseExists(int id)
        {
            return _context.Release.Any(e => e.Id == id);
        }
    }
}
