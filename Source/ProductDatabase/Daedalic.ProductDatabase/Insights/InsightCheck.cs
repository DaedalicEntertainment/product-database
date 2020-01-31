using Daedalic.ProductDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Insights
{
    public interface IInsightCheck
    {
        string Name { get; }

        string Description { get; }

        List<InsightResult> Run(DaedalicProductDatabaseContext context);

        string DetailsPage { get; }

        int GetDetailsPageRouteId(InsightResult result);
    }
}
