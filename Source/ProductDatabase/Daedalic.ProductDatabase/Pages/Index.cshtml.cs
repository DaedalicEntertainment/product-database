using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Daedalic.ProductDatabase.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public int Developers { get; set; }

        public int Games { get; set; }

        public int Genres { get; set; }

        public int Languages { get; set; }

        public int Platforms { get; set; }

        public int Publishers { get; set; }

        public int Releases { get; set; }

        public int Stores { get; set; }

        public int Engines { get; set;}

        public void OnGet()
        {
            Developers = _context.Developer.Count();
            Games = _context.Game.Count();
            Genres = _context.Genre.Count();
            Languages = _context.Language.Count();
            Platforms = _context.Platform.Count();
            Publishers = _context.Publisher.Count();
            Releases = _context.Release.Count();
            Stores = _context.Store.Count();
            Engines = _context.Engine.Count();
        }
    }
}
