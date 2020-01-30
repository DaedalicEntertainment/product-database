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

        [Display(Name = "Asset Index Project Id",
            Description = "Id of the project in our asset index tool, e.g. for an asset index link like http://d00445:4040/?project=Deponia, enter Deponia.")]
        public string AssetIndexProjectId { get; set; }

        public Developer Developer { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Release> Releases { get; set; }

        [Display(Name = "Supported Languages",
            Description = "Languages the game is supposed to support (on any platform).")]
        public ICollection<SupportedLanguage> SupportedLanguages { get; set; }

        [Display(Name = "Implemented Languages",
            Description = "Current status of localizing the respective languages. See their individual tooltips for details.")]
        public ICollection<ImplementedLanguage> ImplementedLanguages { get; set; }
    }
}
