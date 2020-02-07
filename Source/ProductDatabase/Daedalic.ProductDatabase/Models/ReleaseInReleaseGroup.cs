using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class ReleaseInReleaseGroup
    {
        public int Id { get; set; }

        public int ReleaseId { get; set; }

        public Release Release { get; set; }

        public int ReleaseGroupId { get; set; }

        public ReleaseGroup ReleaseGroup { get; set; }
    }
}
