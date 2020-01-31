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

        public IList<Insight<Game>> Insights { get;set; }

        public void OnGet()
        {
            List<UnreleasedLanguage> unreleasedLanguages = new List<UnreleasedLanguage>();

            foreach (Game game in _context.Game
                .Include(g => g.Releases)
                    .ThenInclude(r => r.Languages)
                .Include(g => g.Releases)
                    .ThenInclude(r => r.Platform)
                .Include(g => g.SupportedLanguages)
                    .ThenInclude(sl => sl.Language))
            {
                if (game.SupportedLanguages != null)
                {
                    foreach (Language language in game.SupportedLanguages.Select(sl => sl.Language).Distinct())
                    {
                        foreach (Platform platform in game.Releases.Select(r => r.Platform).Distinct())
                        {
                            if (!game.Releases.Any(r => r.Platform == platform && r.Languages.Any(l => l.LanguageId == language.Id)))
                            {
                                unreleasedLanguages.Add(new UnreleasedLanguage { Game = game, Platform = platform, Language = language });
                            }
                        }
                    }
                }
            }

            Insights = new List<Insight<Game>>();

            foreach (UnreleasedLanguage unreleasedLanguage in unreleasedLanguages)
            {
                Insights.Add(new Insight<Game>
                {
                    Item = unreleasedLanguage.Game,
                    Text = $"{unreleasedLanguage.Game.Name} supports {unreleasedLanguage.Language.Name}, but that language is not being released on {unreleasedLanguage.Platform.Name}."
                });
            }
        }

        private class UnreleasedLanguage
        {
            public Game Game { get; set; }

            public Platform Platform { get; set; }

            public Language Language { get; set; }
        }
    }
}
