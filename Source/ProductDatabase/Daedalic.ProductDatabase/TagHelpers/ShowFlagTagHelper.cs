using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.TagHelpers
{
    public class ShowFlagTagHelper : TagHelper
    {
        public bool Flag { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p";
            output.Content.SetContent(Flag ? "yes" : "no");
        }
    }
}
