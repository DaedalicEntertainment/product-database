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
        protected void UpdateImplementedLanguages(DaedalicProductDatabaseContext context, 
            string[] selectedLanguages, Release releaseToUpdate)
        {
            var oldLanguages = releaseToUpdate.Languages.ToList();
            var newLanguages = new List<ImplementedLanguage>();
            
            foreach (string selectedLanguage in selectedLanguages)
            {
                string[] languageParts = selectedLanguage.Split('/');
                
                if (languageParts.Length < 2)
                {
                    continue;
                }

                int languageId;
                int languageTypeId;

                if (!int.TryParse(languageParts[0], out languageId))
                {
                    continue;
                }

                if (!int.TryParse(languageParts[1], out languageTypeId))
                {
                    continue;
                }

                newLanguages.Add(new ImplementedLanguage
                {
                    ReleaseId = releaseToUpdate.Id,
                    LanguageId = languageId,
                    LanguageTypeId = languageTypeId
                });
            }

            foreach (var language in context.Language.ToList())
            {
                foreach (var languageType in context.LanguageType.ToList())
                {
                    ImplementedLanguage newLanguage = newLanguages.FirstOrDefault(nl => nl.LanguageId == language.Id && nl.LanguageTypeId == languageType.Id);

                    if (newLanguage != null)
                    {
                        if (!oldLanguages.Any(ol => ol.LanguageId == language.Id && ol.LanguageTypeId == languageType.Id))
                        {
                            releaseToUpdate.Languages.Add(newLanguage);
                        }
                    }
                    else
                    {
                        ImplementedLanguage oldLanguage = releaseToUpdate.Languages.FirstOrDefault(ol => ol.LanguageId == language.Id && ol.LanguageTypeId == languageType.Id);

                        if (oldLanguage != null)
                        {
                            context.Remove(oldLanguage);
                        }
                    }
                }
            }
        }
    }
}
