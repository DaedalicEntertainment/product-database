using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Microsoft.Extensions.Configuration;

namespace Daedalic.ProductDatabase.Pages.Games
{
    public class IndexModel : IndexPageModel<Game>
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;
        private readonly IConfiguration _config;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IList<Game> Game { get;set; }

        public string AssetIndexUrl { get; set; }

        public async Task OnGetAsync(string sortOrder, string alert)
        {
            var games = from g in _context.Game
                        .Include(g => g.Developer)
                        .Include(g => g.Genre) select g;

            if (!string.IsNullOrEmpty(Filter))
            {
                games = games.Where(g => g.Name.Contains(Filter));
            }

            // Sort results.
            UpdateSortOrders(sortOrder, "name", "developer", "genre");

            switch (sortOrder)
            {
                case "name_desc":
                    games = games.OrderByDescending(g => g.Name);
                    break;
                case "developer":
                    games = games.OrderBy(g => g.Developer.Name);
                    break;
                case "developer_desc":
                    games = games.OrderByDescending(g => g.Developer.Name);
                    break;
                case "genre":
                    games = games.OrderBy(g => g.Genre.Name);
                    break;
                case "genre_desc":
                    games = games.OrderByDescending(g => g.Genre.Name);
                    break;
                default:
                    games = games.OrderBy(g => g.Name);
                    break;
            }

            Game = await games.AsNoTracking().ToListAsync();

            AssetIndexUrl = _config.GetValue<string>("AssetIndexUrl");

            // Show alerts.
            UpdateAlerts(alert, "Game");
        }
    }
}
