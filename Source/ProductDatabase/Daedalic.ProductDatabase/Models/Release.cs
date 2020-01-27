using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class Release
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Game")]
        public int GameId { get; set; }

        public Game Game { get; set; }

        public string Summary { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public string Version { get; set; }

        [Display(Name = "Status")]
        public int ReleaseStatusId { get; set; }

        public ReleaseStatus Status { get; set; }

        [Display(Name = "Publisher")]
        public int? PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        [Display(Name = "Platform")]
        public int? PlatformId { get; set; }

        public Platform Platform { get; set; }

        [Display(Name = "Store")]
        public int? StoreId { get; set; }

        public Store Store { get; set; }

        public ICollection<ImplementedLanguage> Languages { get; set; }
    }
}
