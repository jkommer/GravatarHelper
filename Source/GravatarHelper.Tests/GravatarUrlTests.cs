namespace GravatarHelper.Tests
{
    using System;
    using Extensions;
    using Xunit;
    using Xunit.Extensions;

    /// <summary>
    /// Test which verify the functionality of CreateGravatarUrl.
    /// </summary>
    public class GravatarUrlTests : BaseGravatarTests
    {
        /// <summary>
        /// Verify that the rating query string parameter matches the supplied argument.
        /// </summary>
        /// <param name="rating">The rating.</param>
        [Theory(DisplayName = "Rating query string parameter matches the supplied argument.")]
        [InlineData(GravatarRating.G)]
        [InlineData(GravatarRating.PG)]
        [InlineData(GravatarRating.R)]
        [InlineData(GravatarRating.X)]
        [InlineData(null)]
        public void RatingMatchesProvidedGravatarRating(GravatarRating? rating)
        {
            var uri = CreateGravatarUri(rating: rating);
            var queryParameter = uri.GetQueryParameter("r");

            var result = rating.HasValue ?
                rating.ToString() == queryParameter :
                queryParameter == null;

            Assert.True(result, string.Format("Rating: {0} did not produce expected query value: \"{1}\"", rating, rating.ToString()));
        }

        /// <summary>
        /// Verifies that CreateGravatarUrl forces gravatar to serve default image if requested.
        /// </summary>
        /// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
        /// <param name="expectedValue">The expected value.</param>
        [Theory(DisplayName = "Forces Gravatar to serve default image if requested.")]
        [InlineData(true, "y")]
        [InlineData(false, null)]
        [InlineData(null, null)]
        public void ForcesGravatarToServeDefaultImageIfRequested(bool? forceDefault, string expectedValue)
        {
            var uri = CreateGravatarUri(forceDefault: forceDefault);
            var queryParameter = uri.GetQueryParameter("f");

            Assert.True(queryParameter == expectedValue, string.Format("Query value: {0} did not match expected value: {1}", queryParameter, expectedValue));
        }

        /// <summary>
        /// Verifies that CreateGravatarUrl uses the .jpg file extension if a file extension has been requested.
        /// </summary>
        /// <param name="addExtension">The add extension.</param>
        /// <param name="fileExtensionExpected">Whether a file extension is expected</param>
        [Theory(DisplayName = "File extensions are used if requested.")]
        [InlineData(true, true)]
        [InlineData(false, false)]
        [InlineData(null, false)]
        public void UsesExtensionIfRequested(bool? addExtension, bool fileExtensionExpected)
        {
            var uri = CreateGravatarUri(addExtension: addExtension);
            var result = uri.Segments[2];

            Assert.True(
                result.EndsWith(".jpg") == fileExtensionExpected, 
                string.Format("{0} file extension was expected, result: {1}", fileExtensionExpected ? "A" : "no", result));
        }

        /// <summary>
        /// Verifies that CreateGravatarUrl automatically switches between http and https depending on IsSecureConnection.
        /// </summary>
        [Fact(DisplayName = "Automatically determine whether to use http or https.")]
        public void AutomaticallyUseHttpAndHttps()
        {
            HttpRequest.SecureConnectionResult = true;
            var secureUri = CreateGravatarUri();

            HttpRequest.SecureConnectionResult = false;
            var normalUri = CreateGravatarUri();

            Assert.True(secureUri.Scheme == "https", "Https protocol should be used on secure connections by default.");
            Assert.True(normalUri.Scheme == "http", "Http protocol should be used on normal connections by default.");
        }

        /// <summary>
        /// Verify that the Gravatar size cannot exceed either minimum or maximum size.
        /// </summary>
        /// <param name="imageSize">Size of the image.</param>
        /// <param name="expectedSize">The expected size.</param>
        [Theory(DisplayName = "Image size cannot exceed either minimum or maximum size.")]
        [InlineData(GravatarHelper.MinImageSize - 1, GravatarHelper.MinImageSize)]
        [InlineData((GravatarHelper.MinImageSize + GravatarHelper.MaxImageSize) / 2, (GravatarHelper.MinImageSize + GravatarHelper.MaxImageSize) / 2)]
        [InlineData(GravatarHelper.MaxImageSize + 1, GravatarHelper.MaxImageSize)]
        public void ImageSizeCannotExceedBounds(int imageSize, int expectedSize)
        {
            var uri = CreateGravatarUri(imageSize: imageSize);
            var querySizeParameter = uri.GetQueryParameter("s");

            int querySize;

            Assert.True(int.TryParse(querySizeParameter, out querySize), "The query string must contain an integer value for size.");
            Assert.True(querySize == expectedSize, string.Format("Size query value: {0} did not match expected size: {1}", querySize, expectedSize));
        }

        /// <summary>
        /// Verify that the URL generated by CreateGravatarUrl is well-formed.
        /// </summary>
        /// <param name="defaultImage">The default image.</param>
        [Theory(DisplayName = "Generated URL is well-formed.")]
        [InlineData(GravatarHelper.DefaultImageIdenticon)]
        [InlineData("http://example.com/logo.jpg")]
        public void UrlIsWellFormed(string defaultImage)
        {
            var uri = CreateGravatarUri(defaultImage: defaultImage);
            Assert.True(uri.IsWellFormedOriginalString(), string.Format("CreateGravatarUrl did not create a well-formed URI for: {0}", defaultImage));
        }

        /// <summary>
        /// Creates a URI to facilitate unit testing using CreateGravatarUrl.
        /// </summary>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        ///  can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="rating">The content rating of the images to display.</param>
        /// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
        /// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
        /// <returns>The Gravatar url wrapped inside an Uri.</returns>
        private static Uri CreateGravatarUri(string email = DefaultEmailAddress, int imageSize = DefaultImageSize, string defaultImage = null, GravatarRating? rating = null, bool? addExtension = null, bool? forceDefault = null)
        {
            var url = GravatarHelper.CreateGravatarUrl(email, imageSize, defaultImage, rating, addExtension, forceDefault);
            return new Uri(url);
        }
    }
}