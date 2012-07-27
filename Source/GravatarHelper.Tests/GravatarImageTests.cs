namespace GravatarHelper.Tests
{
    using System.Collections.Generic;
    using System.Xml;
    using Xunit;

    /// <summary>
    /// Test which verify the functionality of CreateGravatarImage.
    /// </summary>
    public class GravatarImageTests : BaseGravatarTests
    {
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

            var imageTagWithoutAttributes = CreateGravatarImageXml(DefaultEmailAddress, DefaultImageSize, null, null, null, null);
            var imageTagWithAttributes = CreateGravatarImageXml(DefaultEmailAddress, DefaultImageSize, null, null, null, null, htmlAttributes);
            var url = GravatarHelper.CreateGravatarUrl(DefaultEmailAddress, DefaultImageSize, null, null, null, null);

            Assert.True(imageTagWithoutAttributes.Attributes["src"].Value == url);
            Assert.True(imageTagWithAttributes.Attributes["src"].Value == url);
        }

        /// <summary>
        /// Verifies that CreateGravatarImage returns a valid <img /> tag.
        /// </summary>
        [Fact(DisplayName = "Should return an img tag.")]
        public void ShouldReturnImgTag()
        {
            var element = CreateGravatarImageXml();
            Assert.True(element.Name == "img", string.Format("img tag expected but got {0}", element.Name));
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
        private static XmlElement CreateGravatarImageXml(string email = DefaultEmailAddress, int imageSize = DefaultImageSize, string defaultImage = null, GravatarRating? rating = null, bool? addExtension = null, bool? forceDefault = null, IDictionary<string, object> htmlAttributes = null)
        {
            var image = GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, rating, addExtension, forceDefault, htmlAttributes);

            var doc = new XmlDocument();
            doc.LoadXml(image.ToString());

            return doc.DocumentElement;
        }
    }
}