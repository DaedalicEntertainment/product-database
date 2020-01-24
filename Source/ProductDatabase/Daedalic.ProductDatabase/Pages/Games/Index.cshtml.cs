using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase
{
    public class IndexModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get;set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public async Task OnGetAsync()
        {
            var games = from g in _context.Game select g;
            if (!string.IsNullOrEmpty(Filter))
            {
                games = games.Where(s => s.Name.Contains(Filter));
            }

            Game = await games.ToListAsync();
        }
    }
}
