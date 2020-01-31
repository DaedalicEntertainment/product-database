using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Pages.Insights
{
    public class Insight<T>
    {
        public T Item { get; set; }

        public string Text { get; set; }
    }
}
