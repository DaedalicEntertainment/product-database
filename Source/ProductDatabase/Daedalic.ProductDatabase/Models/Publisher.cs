using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Release> Releases { get; set; }
    }
}
