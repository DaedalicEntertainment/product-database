using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daedalic.ProductDatabase.Insights;
using Daedalic.ProductDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Daedalic.ProductDatabase.Pages.Insights
{
    public class ResultsModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;
        private readonly InsightsService _insightsService;

        public ResultsModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context, InsightsService insightsService)
        {
            _context = context;
            _insightsService = insightsService;
        }

        public IList<InsightCheckWithResults> Insights { get; set; }

        public void OnGet(string[] selectedChecks)
        {
            Insights = new List<InsightCheckWithResults>();

            foreach (var check in _insightsService.GetChecks().Where(c => selectedChecks.Contains(c.Name)))
            {
                var results = check.Run(_context);

                Insights.Add(new InsightCheckWithResults { Check = check, Results = results });
            }
        }

        public class InsightCheckWithResults
        {
            public IInsightCheck Check { get; set; }

            public List<InsightResult> Results { get; set; }
        }
    }
}
