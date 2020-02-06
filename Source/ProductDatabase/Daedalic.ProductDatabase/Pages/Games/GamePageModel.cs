using Daedalic.ProductDatabase.Data;
using Daedalic.ProductDatabase.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Pages.Games
{
    public class GamePageModel : PageModel
    {
        protected void UpdateSupportedLanguages(DaedalicProductDatabaseContext context, 
            string[] selectedLanguages, Game gameToUpdate)
        {
            var oldLanguages = gameToUpdate.SupportedLanguages.ToList();
            var newLanguages = new List<SupportedLanguage>();
            
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

                newLanguages.Add(new SupportedLanguage
                {
                    GameId = gameToUpdate.Id,
                    LanguageId = languageId,
                    LanguageTypeId = languageTypeId
                });
            }

            foreach (var language in context.Language.ToList())
            {
                foreach (var languageType in context.LanguageType.ToList())
                {
                    SupportedLanguage newLanguage = newLanguages.FirstOrDefault(nl => nl.LanguageId == language.Id && nl.LanguageTypeId == languageType.Id);

                    if (newLanguage != null)
                    {
                        if (!oldLanguages.Any(ol => ol.LanguageId == language.Id && ol.LanguageTypeId == languageType.Id))
                        {
                            gameToUpdate.SupportedLanguages.Add(newLanguage);
                        }
                    }
                    else
                    {
                        SupportedLanguage oldLanguage = gameToUpdate.SupportedLanguages.FirstOrDefault(ol => ol.LanguageId == language.Id && ol.LanguageTypeId == languageType.Id);

                        if (oldLanguage != null)
                        {
                            context.Remove(oldLanguage);
                        }
                    }
                }
            }
        }

        protected void UpdateImplementedLanguages(DaedalicProductDatabaseContext context, 
            Dictionary<string, string> selectedLanguages, Game gameToUpdate)
        {
            var oldLanguages = gameToUpdate.ImplementedLanguages.ToList();
            var newLanguages = new List<ImplementedLanguage>();
            
            foreach (var language in context.Language.ToList())
            {
                string newLanguageStatus;

                if (!selectedLanguages.TryGetValue(language.Id.ToString(), out newLanguageStatus))
                {
                    continue;
                }

                int newLanguageStatusId;

                if (!int.TryParse(newLanguageStatus, out newLanguageStatusId))
                {
                    continue;
                }

                ImplementedLanguage implementedLanguage = gameToUpdate.ImplementedLanguages.FirstOrDefault(il => il.LanguageId == language.Id);

                if (implementedLanguage != null)
                {
                    implementedLanguage.LanguageStatusId = newLanguageStatusId;
                }
                else
                {
                    implementedLanguage = new ImplementedLanguage { GameId = gameToUpdate.Id, LanguageId = language.Id, LanguageStatusId = newLanguageStatusId };
                    gameToUpdate.ImplementedLanguages.Add(implementedLanguage);
                }
            }
        }
    }
}
