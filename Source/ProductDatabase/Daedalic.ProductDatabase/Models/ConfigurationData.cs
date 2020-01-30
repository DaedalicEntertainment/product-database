using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class ConfigurationData
    {
        [Display(Name = "Default Release Status", Description = "Default status to select when creating a new release.")]
        public int DefaultReleaseStatus { get; set; }
    }
}
