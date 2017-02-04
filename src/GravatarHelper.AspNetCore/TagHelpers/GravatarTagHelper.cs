using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GravatarHelper.AspNetCore.TagHelpers
{
    [HtmlTargetElement("img", Attributes = GravatarEmailAttributeName)]
    public class GravatarTagHelper : TagHelper
    {
        private const string GravatarSizeAttributeName = "gravatar-size";
        private const string GravatarEmailAttributeName = "gravatar-email";
        private const string GravatarRatingAttributeName = "gravatar-rating";
        private const string GravatarIncludeExtensionName = "gravatar-include-extension";
        private const string GravatarForceHttpsAttributeName = "gravatar-force-https";
        private const string GravatarDefaultImageAttributeName = "gravatar-default-image";
        private const string GravatarForceDefaultImageAttributeName = "gravatar-force-default";

        [HtmlAttributeName(GravatarEmailAttributeName)]
        public string EmailAddress { get; set; }

        [HtmlAttributeName(GravatarSizeAttributeName)]
        public int ImageSize { get; set; }

        [HtmlAttributeName(GravatarRatingAttributeName)]
        public GravatarRating? Rating { get; set; }

        [HtmlAttributeName(GravatarIncludeExtensionName)]
        public bool IncludeExtension { get; set; }

        [HtmlAttributeName(GravatarForceHttpsAttributeName)]
        public bool UseSecureUrl { get; set; }

        [HtmlAttributeName(GravatarDefaultImageAttributeName)]
        public string DefaultImage { get; set; }

        [HtmlAttributeName(GravatarForceDefaultImageAttributeName)]
        public bool ForceDefaultImage { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var isHttpsRequest = ViewContext?.HttpContext?.Request?.IsHttps ?? false;

            output.Attributes.SetAttribute("src", Common.GravatarHelper.CreateGravatarUrl(EmailAddress, ImageSize, DefaultImage, Rating, IncludeExtension, ForceDefaultImage, isHttpsRequest || UseSecureUrl));
        }
    }
}
