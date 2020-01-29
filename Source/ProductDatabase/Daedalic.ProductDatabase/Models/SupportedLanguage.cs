using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class SupportedLanguage
    {
        public int Id { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        public Language Language { get; set; }

        [Display(Name = "Language Type")]
        public int LanguageTypeId { get; set; }

        public LanguageType LanguageType { get; set; }

        [Display(Name = "Game")]
        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
