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
    public class DetailsModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public DetailsModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public Game Game { get; set; }

        public string FacebookUrl
        {
            get
            {
                if (Game == null)
                {
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(Game.FacebookPageName))
                {
                    return string.Empty;
                }

                return "https://www.facebook.com/" + Game.FacebookPageName;
            }
        }

        public string TwitterUrl
        {
            get
            {
                if (Game == null)
                {
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(Game.TwitterHandle))
                {
                    return string.Empty;
                }

                string twitterHandle = Game.TwitterHandle;

                if (twitterHandle.StartsWith("@"))
                {
                    twitterHandle = twitterHandle.Substring(1);
                }

                return "https://twitter.com/" + twitterHandle;
            }
        }

        public string FullTwitterHandle
        {
            get
            {
                if (Game == null)
                {
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(Game.TwitterHandle))
                {
                    return string.Empty;
                }

                string twitterHandle = Game.TwitterHandle;

                if (!twitterHandle.StartsWith("@"))
                {
                    twitterHandle = "@" + twitterHandle;
                }

                return twitterHandle;
            }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game = await _context.Game
                .Include(g => g.Developer)
                .Include(g => g.Genre)
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
    }
}
