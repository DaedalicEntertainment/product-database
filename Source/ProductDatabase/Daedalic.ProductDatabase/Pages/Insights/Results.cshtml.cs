using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daedalic.ProductDatabase.Insights;
using Daedalic.ProductDatabase.Models;
using Daedalic.ProductDatabase.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Daedalic.ProductDatabase.Pages.Insights
{
    public class ResultsModel : PageModel
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;
        private readonly InsightsService _insightsService;
        private readonly ConfigurationRepository _configurationRepository;

        public ResultsModel(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context, InsightsService insightsService,
            ConfigurationRepository configurationRepository)
        {
            _context = context;
            _insightsService = insightsService;
            _configurationRepository = configurationRepository;
        }

        public IList<InsightCheckWithResults> Insights { get; set; }

        public async Task OnGet(int[] selectedChecks)
        {
            ConfigurationData configuration = await _configurationRepository.Load();

            Insights = new List<InsightCheckWithResults>();

            foreach (var check in _insightsService.GetChecks().Where(c => selectedChecks.Contains(c.Id)))
            {
                var results = check.Run(_context, configuration);

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
