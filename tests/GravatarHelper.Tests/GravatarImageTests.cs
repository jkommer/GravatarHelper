using System;
using System.Xml.Linq;
using System.Collections.Generic;
using GravatarHelper.Common;
using Xunit;
using GravatarHelper.Extensions;
using GravatarHelper.Tests.Fakes;

namespace GravatarHelper.Tests
{
    /// <summary>
    /// Test which verify the functionality of CreateGravatarImage.
    /// </summary>
    public class GravatarImageTests
    {
        public GravatarImageTests()
        {
            HttpRequest = new TestHttpRequest();
            HttpContext = new TestHttpContext(HttpRequest);

            HtmlHelperExtensions.GetHttpContext = () => HttpContext;
        }

        private TestHttpContext HttpContext { get; }
        private TestHttpRequest HttpRequest { get; }

        /// <summary>
        /// Default email address for testing.
        /// </summary>
        private const string DefaultEmailAddress = "MyEmailAddress@example.com";

        /// <summary>
        /// Default image size for testing.
        /// </summary>
        private const int DefaultImageSize = 80;

        /// <summary>
        /// Verifies that custom attributes can be successfully added to the img tag.
        /// </summary>
        [Fact(DisplayName = "Custom attributes can be successfully added to the img tag.")]
        public void CustomAttributesCanBeAdded()
        {
            var htmlAttributes = new Dictionary<string, object>
            {
                { "style", "display: block;" },
                { "alt", "My Gravatar" },
                { "width", "80px" },
                { "height", "80px" }
            };

            var imageTag = CreateGravatarImageXml(htmlAttributes: htmlAttributes);

            foreach (var attribute in htmlAttributes)
            {
                var tagAttribute = imageTag.Attribute(attribute.Key);

                Assert.NotNull(tagAttribute);
                Assert.True(tagAttribute.Value.Equals(attribute.Value), $"Attribute {attribute.Key}'s value was set to {tagAttribute.Value} expected {attribute.Value}.");
            }
        }

        /// <summary>
        /// Verifies that the src attribute equals CreateGravatarUrl's result regardless of attributes specified.
        /// </summary>
        [Fact(DisplayName = "Src attribute equals CreateGravatarUrl's result regardless of attributes specified.")]
        public void SrcAttributeCannotBeOverriden()
        {
            var htmlAttributes = new Dictionary<string, object>
            {
                { "src", "http://www.google.com" }
            };

            var imageTagWithoutAttributes = CreateGravatarImageXml();
            var imageTagWithAttributes = CreateGravatarImageXml(DefaultEmailAddress, DefaultImageSize, null, null, null, null, htmlAttributes);
            var url = Common.GravatarHelper.CreateGravatarUrl(DefaultEmailAddress, DefaultImageSize, null, null, null, null, false);

            Assert.True(imageTagWithoutAttributes?.Attribute("src")?.Value == url);
            Assert.True(imageTagWithAttributes?.Attribute("src")?.Value == url);
        }

        /// <summary>
        /// Verifies that CreateGravatarImage returns a valid <img /> tag.
        /// </summary>
        [Fact(DisplayName = "Should return an img tag.")]
        public void ShouldReturnImgTag()
        {
            var element = CreateGravatarImageXml();
            Assert.True(element.Name == "img", $"img tag expected but got {element.Name}");
        }

        /// <summary>
        /// Verifies that CreateGravatarUrl automatically switches between http and https depending on IsSecureConnection.
        /// </summary>
        [Fact(DisplayName = "Automatically determine whether to use http or https.")]
        public void AutomaticallyUseHttpAndHttps()
        {
            HttpRequest.SecureConnectionResult = true;
            var secureUri = CreateGravatarImageXml();

            HttpRequest.SecureConnectionResult = false;
            var normalUri = CreateGravatarImageXml();

            Assert.True(GetSrcAsUri(secureUri).Scheme == "https", "Https protocol should be used on secure connections by default.");
            Assert.True(GetSrcAsUri(normalUri).Scheme == "http", "Http protocol should be used on normal connections by default.");
        }

        /// <summary>
        /// Verifies that CreateGravatarUrl can be forced to https based on ForceSecureUrl.
        /// </summary>
        [Fact(DisplayName = "Can force https.")]
        public void CanForceHttps()
        {
            HttpRequest.SecureConnectionResult = true;
            var secureUri = CreateGravatarImageXml(forceHttps: true);

            HttpRequest.SecureConnectionResult = false;
            var normalUri = CreateGravatarImageXml(forceHttps: true);

            Assert.True(GetSrcAsUri(secureUri).Scheme == "https", "Https protocol should be used on secure connections by when ForceSecureUrl = true.");
            Assert.True(GetSrcAsUri(normalUri).Scheme == "https", "Https protocol should be used on normal connections by when ForceSecureUrl = true.");
        }

        /// <summary>
        /// Creates a XmlElement to facilitate unit testing using CreateGravatarImage.
        /// </summary>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="rating">The content rating of the images to display.</param>
        /// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
        /// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// The Gravatar img tag wrapped inside a XmlElement.
        /// </returns>
        private static XElement CreateGravatarImageXml(string email = DefaultEmailAddress, int imageSize = DefaultImageSize, string defaultImage = null, GravatarRating? rating = null, bool? addExtension = null, bool? forceDefault = null, IDictionary<string, object> htmlAttributes = null, bool forceHttps = false)
        {
            var image = HtmlHelperExtensions.CreateGravatarImage(email, imageSize, defaultImage, rating, addExtension, forceDefault, htmlAttributes, forceHttps);

            var doc = XDocument.Parse(image.ToString());

            return doc.Root;
        }

        /// <summary>
        /// Retrieves the src attribute from the <see cref="XElement"/> and returns it as an <see cref="Uri"/>.
        /// </summary>
        /// <param name="element">The element to retrieve the src from.</param>
        /// <returns>
        /// The src as an Uri
        /// </returns>
        private static Uri GetSrcAsUri(XElement element)
        {
            var srcAttribute = element?.Attribute("src");

            Assert.NotNull(srcAttribute);

            return new Uri(srcAttribute.Value);
        }
    }
}