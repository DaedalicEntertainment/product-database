using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class LanguageStatus
    {
         public int Id { get; set; }

        [Required]
        [Display(Description = "Name of the localization status of a language.")]
        public string Name { get; set; }

        [Display(Description = "Tooltip to show when selecting this status.")]
        public string Summary { get; set; }
    }
}
