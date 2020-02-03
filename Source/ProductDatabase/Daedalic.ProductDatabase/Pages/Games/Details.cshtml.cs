using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Daedalic.ProductDatabase.Repositories;
using Microsoft.Extensions.Configuration;

namespace Daedalic.ProductDatabase.Pages.Games
{
    public class DetailsModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;
        private readonly IConfiguration _config;
        private readonly ConfigurationRepository _configurationRepository;

        public DetailsModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context, IConfiguration config,
            ConfigurationRepository configurationRepository)
        {
            _context = context;
            _config = config;
            _configurationRepository = configurationRepository;
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

        public ConfigurationData Configuration { get; private set; }

        public string AssetIndexUrl { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Configuration = await _configurationRepository.Load();

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
                .Include(g => g.Releases)
                    .ThenInclude(r => r.ReleaseStatus)
                .Include(g => g.Releases)
                    .ThenInclude(r => r.Publisher)
                .Include(g => g.Releases)
                    .ThenInclude(r => r.Engine)
                .Include(g => g.Releases)
                    .ThenInclude(r => r.Platform)
                .Include(g => g.Releases)
                    .ThenInclude(r => r.Store)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Game == null)
            {
                return NotFound();
            }

            AssetIndexUrl = _config.GetValue<string>("AssetIndexUrl");

            return Page();
        }
    }
}
