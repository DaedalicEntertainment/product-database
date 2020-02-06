using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class Release : IValidatableObject
    {
        private static readonly ReleaseType ReleaseTypeRelease = new ReleaseType
        { 
            Name = "Release",
            Description = "First release for a game with this platform and store."
        };

        private static readonly ReleaseType ReleaseTypePatch =
            new ReleaseType
            { 
                Name = "Patch", 
                Description = "Release for an existing game with the same platform and store, but earlier release/ready for release/GMC date." 
            };

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
        [Display(Name = "Ready For Release Date", Description = "Date when all tests and submissions are supposted to be finished.")]
        public DateTime? ReadyForReleaseDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date", Description = "Date when this release is supposed to be live on the platform.")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Description = "Version number of the build to release.")]
        public string Version { get; set; }

        [Display(Name = "Status")]
        public int ReleaseStatusId { get; set; }

        [Display(Name = "Status")]
        public ReleaseStatus ReleaseStatus { get; set; }

        [Display(Name = "Publisher")]
        public int? PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        [Required]
        [Display(Name = "Platform")]
        public int PlatformId { get; set; }

        public Platform Platform { get; set; }

        [Display(Name = "Store", Description = "Store the game is supposed to be released in. Leave this empty if obvious/not applicable (e.g. for consoles).")]
        public int? StoreId { get; set; }

        public Store Store { get; set; }

        [Display(Name = "Early Access")]
        public bool EarlyAccess { get; set; }

        [Display(Name = "Engine")]
        public int? EngineId { get; set; }

        public Engine Engine { get; set; }

        [Display(Description = "New languags to be included in this release. It's not necessary to repeat all languages for every release.")]
        public ICollection<ReleasedLanguage> Languages { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (GmcDate != null && ReadyForReleaseDate != null && ReadyForReleaseDate < GmcDate)
            {
                yield return new ValidationResult("Ready For Release must be after GMC.", new[] { nameof(ReleaseDate) });
            }

            if (GmcDate != null && ReleaseDate != null && ReleaseDate < GmcDate)
            {
                yield return new ValidationResult("Release must be after GMC.", new[] { nameof(ReleaseDate) });
            }

            if (ReadyForReleaseDate != null && ReleaseDate != null && ReleaseDate < ReadyForReleaseDate)
            {
                yield return new ValidationResult("Release must be after Ready For Release.", new[] { nameof(ReleaseDate) });
            }
        }

        public bool IsPatch(IEnumerable<Release> allReleases)
        {
            // If there's any other release satisfying the following criteria, this release must be a patch.
            return allReleases.Any(other =>
                // Only consider other releases.
                other.Id != this.Id &&

                // Must be for same game, platform and store.
                other.GameId == this.GameId &&
                other.PlatformId == this.PlatformId &&
                other.StoreId == this.StoreId &&

                // Must be released before us.
                other.IsBefore(this));
        }

        public ReleaseType GetReleaseType(IEnumerable<Release> allReleases)
        {
            return IsPatch(allReleases) ? ReleaseTypePatch : ReleaseTypeRelease;
        }

        public bool IsBefore(Release other)
        {
            if (other == null)
            {
                return true;
            }

            if (this.ReleaseDate != null && other.ReleaseDate != null)
            {
                return this.ReleaseDate < other.ReleaseDate;
            }

            if (this.ReadyForReleaseDate != null && other.ReadyForReleaseDate != null)
            {
                return this.ReadyForReleaseDate < other.ReadyForReleaseDate;
            }

            if (this.GmcDate != null)
            {
                return other.GmcDate != null && this.GmcDate < other.GmcDate;
            }

            return this.Id < other.Id;
        }

        public ReleaseQuarter GetReleaseQuarter()
        {
            return ReleaseDate.HasValue ? new ReleaseQuarter(ReleaseDate.Value) : null;
        }

        public class ReleaseType
        {
            public string Name { get; set; }

            public string Description { get; set; }
        }

        public class ReleaseQuarter
        {
            public int Year { get; set; }

            public int Quarter { get; set; }

            public ReleaseQuarter()
            {
            }

            public ReleaseQuarter(DateTime releaseDate)
            {
                Year = releaseDate.Year;
                Quarter = (releaseDate.Month / 4) + 1;
            }

            public override bool Equals(object obj)
            {
                if (obj is ReleaseQuarter)
                {
                    ReleaseQuarter other = (ReleaseQuarter)obj;
                    return this.Year == other.Year && this.Quarter == other.Quarter;
                }

                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                int hash = 23;
                hash = hash * 31 + Year.GetHashCode();
                hash = hash * 31 + Quarter.GetHashCode();
                return hash;
            }

            public override string ToString()
            {
                return $"Q{Quarter} {Year}";
            }
        }
    }
}
