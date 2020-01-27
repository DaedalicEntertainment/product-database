using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.Releases
{
    public class IndexModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<Release> Release { get;set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public async Task OnGetAsync()
        {
            var releases = from r in  _context.Release
                .Include(r => r.Game)
                .Include(r => r.Platform)
                .Include(r => r.Publisher)
                .Include(r => r.Status)
                .Include(r => r.Store) select r;

            if (!string.IsNullOrEmpty(Filter))
            {
                releases = releases.Where(r => r.Summary.Contains(Filter));
            }

            Release = await releases.ToListAsync();
        }
    }
}
