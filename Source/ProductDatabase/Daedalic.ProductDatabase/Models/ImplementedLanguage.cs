using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class ImplementedLanguage
    {
        public int Id { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        public Language Language { get; set; }

        [Display(Name = "Language Status")]
        public int LanguageStatusId { get; set; }

        public LanguageStatus LanguageStatus { get; set; }

        [Display(Name = "Game")]
        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
