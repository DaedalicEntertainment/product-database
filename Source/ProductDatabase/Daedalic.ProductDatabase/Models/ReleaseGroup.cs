﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Models
{
    public class ReleaseGroup
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<ReleaseInReleaseGroup> Releases { get; set; }
    }
}
