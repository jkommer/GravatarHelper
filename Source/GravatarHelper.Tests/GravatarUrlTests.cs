namespace GravatarHelper.Tests
{
    using System;
    using System.Collections.Specialized;
    using System.Web;
    using Xunit;
    using Xunit.Extensions;

    /// <summary>
    /// Test which verify the functionality of CreateGravatarUrl.
    /// </summary>
    public class GravatarUrlTests
    {
        /// <summary>
        /// Our testing HttpRequest; allowing us to switch between secure / non-secure connection. 
        /// </summary>
        private TestHttpRequest httpRequest = new TestHttpRequest();

        /// <summary>
        /// Initializes a new instance of the <see cref="GravatarUrlTests"/> class.
        /// </summary>
        public GravatarUrlTests()
        {
            GravatarHelper.GetHttpContext = () => new TestHttpContext(this.httpRequest);
        }

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
            var uri = this.CreateGravatarUri(rating: rating);
            var queryParameter = this.GetQueryParameter(uri, "r");

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
            var queryParameter =  GetQueryParameter(uri, "f");

            Assert.True(queryParameter == expectedValue, string.Format("Query value: {0} did not match expected value: {1}", queryParameter, expectedValue));
        }

        /// <summary>
        /// Verifies that CreateGravatarUrl uses the .jpg file extension if a file extension has been requested.
        /// </summary>
        [Fact(DisplayName = "File extensions are used if requested.")]
        public void UsesExtensionIfRequested()
        {
            Func<bool?, string> createGravatarUrl = (addExtension) =>
                {
                    var uri = CreateGravatarUri(addExtension: addExtension);
                    return uri.Segments[2];
                };

            Assert.True(createGravatarUrl(true).EndsWith(".jpg"), "Uses a file extension when requested to.");
            Assert.True(!createGravatarUrl(false).EndsWith(".jpg"), "Does not use a file extension when requested not to.");
            Assert.True(!createGravatarUrl(null).EndsWith(".jpg"), "Does not use extensions by default.");
        }

        /// <summary>
        /// Verifies that CreateGravatarUrl automatically switches between http and https depending on IsSecureConnection.
        /// </summary>
        [Fact(DisplayName = "Automatically determine whether to use http or https.")]
        public void AutomaticallyUseHttpAndHttps()
        {
            this.httpRequest.SecureConnectionResult = true;
            var secureUri = this.CreateGravatarUri();

            this.httpRequest.SecureConnectionResult = false;
            var normalUri = this.CreateGravatarUri();

            Assert.True(secureUri.Scheme == "https", "Https protocl should be used on secure connections by default.");
            Assert.True(normalUri.Scheme == "http", "Http protocl should be used on normal connections by default.");
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
            var uri = this.CreateGravatarUri(imageSize: imageSize);
            var querySizeParameter = this.GetQueryParameter(uri, "s");

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
            var uri = this.CreateGravatarUri(defaultImage: defaultImage);
            Assert.True(uri.IsWellFormedOriginalString(), string.Format("CreateGravatarUrl did not create a well-formed URI for: {0}", defaultImage));
        }

        /// <summary>
        /// Gets the query parameters as a <see cref="NameValueCollection"/>.
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <returns>A NameValueCollection of all query parameters.</returns>
        private NameValueCollection GetQueryParameters(Uri uri)
        {
            return HttpUtility.ParseQueryString(uri.Query);
        }

        /// <summary>
        /// Gets the query parameter.
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The value of the query parameter, null if not specified.</returns>
        private string GetQueryParameter(Uri uri, string parameter)
        {
            return this.GetQueryParameters(uri)[parameter];
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
        private Uri CreateGravatarUri(string email = "MyEmailAddress@example.com", int imageSize = 80, string defaultImage = null, GravatarRating? rating = null, bool? addExtension = null, bool? forceDefault = null)
        {
            var url = GravatarHelper.CreateGravatarUrl(email, imageSize, defaultImage, rating, addExtension, forceDefault);
            return new Uri(url);
        }
    }
}