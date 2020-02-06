using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Daedalic.ProductDatabase.TagHelpers
{
    public class HelpTextTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // This is where we get out data from. See the following source for references:
            // https://github.com/dotnet/aspnetcore/blob/master/src/Mvc/Mvc.TagHelpers/src/LabelTagHelper.cs
            // https://github.com/dotnet/aspnetcore/blob/master/src/Mvc/Mvc.ViewFeatures/src/DefaultHtmlGenerator.cs
            string content = For.ModelExplorer.Metadata.Description;

            if (string.IsNullOrEmpty(content))
            {
                output.SuppressOutput();
            }

            // https://getbootstrap.com/docs/4.4/components/forms/#help-text
            output.TagName = "small";
            output.Attributes.SetAttribute("class", "form-text text-muted");
            output.Content.SetContent(content);
        }
    }
}
