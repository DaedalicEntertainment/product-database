using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Pages
{
    public class RouteValues
    {
        public const string AlertKey = "alert";
        public const string IdKey = "id";

        public const string AlertValueCreated = "created";
        public const string AlertValueUpdated = "updated";
        public const string AlertValueDeleted = "deleted";

        private readonly Dictionary<string, string> values = new Dictionary<string, string>();

        public RouteValues AlertCreated()
        {
            values.Add(AlertKey, AlertValueCreated);
            return this;
        }

        public RouteValues AlertUpdated()
        {
            values.Add(AlertKey, AlertValueUpdated);
            return this;
        }

        public RouteValues AlertDeleted()
        {
            values.Add(AlertKey, AlertValueDeleted);
            return this;
        }

        public RouteValues Id(int id)
        {
            values.Add(IdKey, id.ToString());
            return this;
        }

        public Dictionary<string, string> Build()
        {
            return values;
        }
    }
}
