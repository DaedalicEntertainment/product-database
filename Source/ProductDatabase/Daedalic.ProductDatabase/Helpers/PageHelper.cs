using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Helpers
{
    public static class PageHelper
    {
        public static Dictionary<string, string> GetNewSortOrders(string currentSortOrder, params string[] columns)
        {
            Dictionary<string, string> sortOrders = new Dictionary<string, string>();

            if (columns.Length == 0)
            {
                return sortOrders;
            }

            sortOrders.Add(columns[0], string.IsNullOrEmpty(currentSortOrder) ? columns[0] + "_desc" : string.Empty);

            for (int i = 1; i < columns.Length; ++i)
            {
                sortOrders.Add(columns[i], currentSortOrder == columns[i] ? columns[i] + "_desc" : columns[i]);
            }

            return sortOrders;
        }
    }
}
