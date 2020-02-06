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

namespace Daedalic.ProductDatabase.Pages.Releases
{
    public class CreateModel : ReleasePageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public CreateModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["GameId"] = new SelectList(_context.Game.OrderBy(g => g.Name), "Id", "Name");

            return Page();
        }

        [BindProperty]
        public Release Release { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost(int[] selectedLanguages)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./CreateForGame", new RouteValues().Id(Release.GameId).Build());
        }
    }
}
