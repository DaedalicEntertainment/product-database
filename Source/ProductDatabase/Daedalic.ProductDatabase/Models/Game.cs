using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Display(Name = "Developer")]
        public int? DeveloperId { get; set; }

        [Display(Name = "Genre")]
        public int? GenreId { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Asset Index Project Id")]
        public string AssetIndexProjectId { get; set; }

        public Developer Developer { get; set; }

        public Genre Genre { get; set; }
    }
}
