using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Pages
{
    public class IndexPageModel : PageModel
    {
        public string AlertClass { get; set; }

        public string AlertText { get; set; }

        protected void UpdateAlerts(string alert, string modelName)
        {
            switch (alert)
            {
                case RouteValues.AlertValueCreated:
                    AlertClass = "success";
                    AlertText = modelName + " created.";
                    break;

                case RouteValues.AlertValueUpdated:
                    AlertClass = "success";
                    AlertText = modelName + " updated.";
                    break;

                case RouteValues.AlertValueDeleted:
                    AlertClass = "success";
                    AlertText = modelName + " deleted.";
                    break;

                default:
                    AlertClass = null;
                    AlertText = null;
                    break;
            }
        }
    }
}
