using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daedalic.ProductDatabase.Insights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Daedalic.ProductDatabase.Pages.Insights
{
    public class IndexModel : PageModel
    {
        private readonly InsightsService _insightsService;

        public IndexModel(InsightsService insightsService)
        {
            _insightsService = insightsService;
        }

        public IList<IInsightCheck> Checks { get; set; }

        public void OnGet()
        {
            Checks = _insightsService.GetChecks();
        }

        public IActionResult OnPostAsync(string[] selectedChecks)
        {
            return RedirectToPage("./Results", new RouteValues().Custom("selectedChecks", selectedChecks).Build());
        }
    }
}