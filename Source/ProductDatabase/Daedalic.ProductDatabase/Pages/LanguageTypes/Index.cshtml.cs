using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;

namespace Daedalic.ProductDatabase.LanguageTypes
{
    public class IndexModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public IndexModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public IList<LanguageType> LanguageType { get;set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public async Task OnGetAsync()
        {
            var languageTypes = from t in _context.LanguageType select t;

            if (!string.IsNullOrEmpty(Filter))
            {
                languageTypes = languageTypes.Where(t => t.Name.Contains(Filter));
            }

            LanguageType = await languageTypes.OrderBy(t => t.Name).ToListAsync();
        }
    }
}
