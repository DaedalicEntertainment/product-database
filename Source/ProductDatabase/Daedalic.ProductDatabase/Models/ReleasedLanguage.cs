using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class ReleasedLanguage
    {
        public int Id { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        public Language Language { get; set; }

        [Display(Name = "Release")]
        public int ReleaseId { get; set; }

        public Release Release { get; set; }
    }
}
