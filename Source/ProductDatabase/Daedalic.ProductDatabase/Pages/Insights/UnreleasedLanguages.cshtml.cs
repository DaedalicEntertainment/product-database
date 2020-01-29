using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daedalic.ProductDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Daedalic.ProductDatabase.Pages.Insights
{
    public class UnreleasedLanguagesModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public UnreleasedLanguagesModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<UnreleasedLanguage> UnreleasedLanguages { get;set; }

        public void OnGet()
        {
            UnreleasedLanguages = new List<UnreleasedLanguage>();

            foreach (Game game in _context.Game
                .Include(g => g.Releases)
                    .ThenInclude(r => r.Languages)
                .Include(g => g.SupportedLanguages)
                    .ThenInclude(sl => sl.Language))
            {
                if (game.SupportedLanguages != null)
                {
                    foreach (Language language in game.SupportedLanguages.Select(sl => sl.Language).Distinct())
                    {
                        if (!game.Releases.Any(r => r.Languages.Any(l => l.LanguageId == language.Id)))
                        {
                            UnreleasedLanguages.Add(new UnreleasedLanguage { Game = game, Language = language });
                        }
                    }
                }
            }
        }

        public class UnreleasedLanguage
        {
            public Game Game { get; set; }

            public Language Language { get; set; }
        }
    }
}