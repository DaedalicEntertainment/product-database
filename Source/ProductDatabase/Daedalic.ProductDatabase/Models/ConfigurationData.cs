using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class ConfigurationData
    {
        [Display(Name = "Default Release Status", Description = "Default status to select when creating a new release.")]
        public int DefaultReleaseStatus { get; set; }

        [Display(Name = "Finished Release Status", Description = "Status that describes a finished release (game is available for customers).")]
        public int FinishedReleaseStatus { get; set; }

        [Display(Name = "Default Language Status", Description = "Default status to select for new game langauges.")]
        public int DefaultLanguageStatus { get; set; }

        [Display(Name = "Locale", Description = "Locale to use, e.g. for formatting dates.")]
        public string Locale { get; set; }

        public string FormatDate(DateTime? dateTime)
        {
            return dateTime.HasValue ? FormatDate(dateTime.Value) : string.Empty;
        }

        public string FormatDate(DateTime dateTime)
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(Locale ?? "en-US");

            if (cultureInfo == null)
            {
                cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            }

            return dateTime.ToString("d", cultureInfo);
        }
    }
}
