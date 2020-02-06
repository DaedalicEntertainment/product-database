using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public interface IOrderedModel
    {
        public int Order { get; set; }
    }
}
