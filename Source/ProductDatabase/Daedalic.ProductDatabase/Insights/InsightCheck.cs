using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Insights
{
    public interface IInsightCheck
    {
        int Id { get; set; }

        string Name { get; }

        string Description { get; }

        string DetailsPage { get; }

        int GetDetailsPageRouteId(InsightResult result);

        List<InsightResult> Run(DaedalicProductDatabaseContext context, ConfigurationData configuration);
    }
}
