using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GravatarHelper.AspNetCore
{
    [HtmlTargetElement("img", Attributes = GravatarEmailAttributeName)]
    public class GravatarTagHelper : TagHelper
    {
        private const string GravatarSizeAttributeName = "gravatar-size";
        private const string GravatarEmailAttributeName = "gravatar-email";
        
        [HtmlAttributeName(GravatarEmailAttributeName)]
        public string GravatarEmail { get; set; }

        [HtmlAttributeName(GravatarSizeAttributeName)]
        public int GravatarSize { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public string DefaultImage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("src", Common.GravatarHelper.CreateGravatarUrl(GravatarEmail, GravatarSize, null, null, false, false, false));
        }
    }
}
