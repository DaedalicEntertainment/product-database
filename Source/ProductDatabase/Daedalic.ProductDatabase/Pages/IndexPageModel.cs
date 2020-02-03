using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Pages
{
    public class IndexPageModel<T> : PageModel where T : class
    {
        public string AlertClass { get; set; }

        public string AlertText { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public Dictionary<string, string> SortOrders { get; set; }

        protected void UpdateSortOrders(string currentSortOrder, params string[] columns)
        {
            SortOrders = new Dictionary<string, string>();

            if (columns.Length == 0)
            {
                return;
            }

            SortOrders.Add(columns[0], string.IsNullOrEmpty(currentSortOrder) ? columns[0] + "_desc" : string.Empty);

            for (int i = 1; i < columns.Length; ++i)
            {
                SortOrders.Add(columns[i], currentSortOrder == columns[i] ? columns[i] + "_desc" : columns[i]);
            }
        }

        protected async Task ChangeOrder<TItem>(DaedalicProductDatabaseContext context, Func<DaedalicProductDatabaseContext,
            DbSet<TItem>> dbSetSelector, int? id, int change) where TItem : class, IIndexedModel, IOrderedModel
        {
            // Get current statuses.
            List<TItem> items = dbSetSelector(context).OrderBy(i => i.Order).ToList();

            // Assign current indices (fixes gaps caused by deletions).
            for (int i = 0; i < items.Count; ++i)
            {
                items[i].Order = i;
            }

            // Swap indices as desired.
            for (int i = 0; i < items.Count; ++i)
            {
                if (items[i].Id == id)
                {
                    int otherIndex = i + change;
                    bool otherIndexValid = otherIndex >= 0 && otherIndex < items.Count;

                    if (otherIndexValid)
                    {
                        items[i + change].Order = i;
                        items[i].Order = i + change;
                    }

                    break;
                }
            }

            await context.SaveChangesAsync();
        }

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

        protected List<T> GetFilteredAndSortedItemsSlow(DbSet<T> table, Func<T, string> filteredProperty, string sortOrder, params Ordering[] orderings) 
        {
            // https://docs.microsoft.com/en-us/ef/core/querying/client-eval

            // Apply filters.
            var items = ApplyFilterSlow(table.AsNoTracking(), filteredProperty);

            // Sort results.
            UpdateSortOrders(sortOrder, orderings.Select(o => o.PropertyName).ToArray());
            items = ApplySortOrderSlow(items, sortOrder, orderings);

            return items.ToList();
        }

        protected Ordering OrderBy(string propertyName, Func<T, string> propertyGetter)
        {
            return new Ordering { PropertyName = propertyName, PropertyGetter = propertyGetter };
        }

        private IEnumerable<T> ApplyFilterSlow(IEnumerable<T> items, Func<T, string> filteredProperty)
        {
            if (!string.IsNullOrEmpty(Filter))
            {
                items = items.Where(i => filteredProperty(i).ToLower().Contains(Filter.ToLower()));
            }

            return items;
        }

        private IEnumerable<T> ApplySortOrderSlow(IEnumerable<T> items, string sortOrder, params Ordering[] orderings)
        {
            if (orderings.Length <= 0)
            {
                return items;
            }

            // Check for primary property, descending.
            if (sortOrder == orderings[0].PropertyName + "_desc")
            {
                return items.OrderByDescending(item => orderings[0].PropertyGetter(item));
            }

            // Check all properties, both ascending and descending.
            for (int i = 1; i < orderings.Length; ++i)
            {
                if (sortOrder == orderings[i].PropertyName)
                {
                    return items.OrderBy(item => orderings[i].PropertyGetter(item));
                }

                if (sortOrder == orderings[i].PropertyName + "_desc")
                {
                    return items.OrderByDescending(item => orderings[i].PropertyGetter(item));
                }
            }

            // Default to primary property, ascending.
            return items.OrderBy(item => orderings[0].PropertyGetter(item));
        }

        protected class Ordering
        {
            public string PropertyName { get; set; }

            public Func<T, string> PropertyGetter { get; set; }
        }
    }
}
