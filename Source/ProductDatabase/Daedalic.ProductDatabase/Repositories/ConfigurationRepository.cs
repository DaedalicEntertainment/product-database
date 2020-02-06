using Daedalic.ProductDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Repositories
{
    public class ConfigurationRepository
    {
        private readonly Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext _context;

        public ConfigurationRepository(Daedalic.ProductDatabase.Data.DaedalicProductDatabaseContext context)
        {
            _context = context;
        }

        public async Task<ConfigurationData> Load()
        {
            Daedalic.ProductDatabase.Models.Configuration configurationModel = await _context.Configuration.FirstOrDefaultAsync();

            if (configurationModel == null)
            {
                configurationModel = new Daedalic.ProductDatabase.Models.Configuration();
            }

            ConfigurationData configuration;

            if (string.IsNullOrEmpty(configurationModel.Data))
            {
                configuration = new ConfigurationData();
            }
            else
            {
                configuration = JsonConvert.DeserializeObject<ConfigurationData>(configurationModel.Data);
            }

            return configuration;
        }

        public async Task Save(ConfigurationData configuration)
        {
            var configurationModel = await _context.Configuration.FirstOrDefaultAsync();

            if (configurationModel == null)
            {
                configurationModel = new Daedalic.ProductDatabase.Models.Configuration();
                configurationModel.Data = JsonConvert.SerializeObject(configuration);
                _context.Configuration.Add(configurationModel);
            }
            else
            {
                configurationModel.Data = JsonConvert.SerializeObject(configuration);
                _context.Attach(configurationModel).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }
    }
}
