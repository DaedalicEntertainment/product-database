using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Releases
{
    public class ReleasePageModel : PageModel
    {
        protected void UpdateReleasedLanguages(DaedalicProductDatabaseContext context, 
            int[] selectedLanguages, Release releaseToUpdate)
        {
            var oldLanguages = releaseToUpdate.Languages.ToList();

            foreach (var language in context.Language.ToList())
            {
                if (selectedLanguages.Contains(language.Id))
                {
                    if (!oldLanguages.Any(ol => ol.LanguageId == language.Id))
                    {
                        releaseToUpdate.Languages.Add(new ReleasedLanguage { ReleaseId = releaseToUpdate.Id, LanguageId = language.Id });
                    }
                }
                else
                {
                    ReleasedLanguage oldLanguage = releaseToUpdate.Languages.FirstOrDefault(ol => ol.LanguageId == language.Id);

                    if (oldLanguage != null)
                    {
                        context.Remove(oldLanguage);
                    }
                }
            }
        }
    }
}
