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

        [Display(Description = "Optional description of this release. Include any special information, such as new features or crucial bugfixes in case of a patch.")]
        public string Summary { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "GMC Date", Description = "Date when this release is supposed to enter Goldmaster testing with QA.")]
        public DateTime? GmcDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date", Description = "Date when this release is supposed to be live on the platform.")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Description = "Version number of the build to release.")]
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

        [Display(Name = "Store", Description = "Store the game is supposed to be released in. Leave this empty if obvious/not applicable (e.g. for consoles).")]
        public int? StoreId { get; set; }

        public Store Store { get; set; }

        [Display(Description = "New languags to be included in this release. It's not necessary to repeat all languages for every release.")]
        public ICollection<ReleasedLanguage> Languages { get; set; }
    }
}
