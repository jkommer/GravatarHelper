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
        [Fact(DisplayName = "Forces Gravatar to serve default image if requested.")]
        public void ForcesGravatarToServeDefaultImageIfRequested()
        {
            Func<bool?, string> createGravatarUrl = (forceDefault) =>
            {
                var uri = CreateGravatarUri(forceDefault: forceDefault);
                return GetQueryParameter(uri, "f");
            };

            Assert.True("y".Equals(createGravatarUrl(true)), "Forces the default image when requested to.");
            Assert.True(createGravatarUrl(false) == null, "Does not force the default image when requested not to.");
            Assert.True(createGravatarUrl(null) == null, "Does not force the default image by default.");
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
            var secureUrl = GravatarHelper.CreateGravatarUrl("MyEmailAddress@example.com", 80, null, null, null, null);

            Assert.True(secureUrl.StartsWith("https://"), "Https protocl should be used on secure connections by default.");

            this.httpRequest.SecureConnectionResult = false;
            var normalUrl = GravatarHelper.CreateGravatarUrl("MyEmailAddress@example.com", 80, null, null, null, null);            

            Assert.True(normalUrl.StartsWith("http://"), "Http protocl should be used on normal connections by default.");
        }

        /// <summary>
        /// Verify that the Gravatar size cannot exceed either minimum or maximum size. 
        /// </summary>
        [Fact(DisplayName = "Image size cannot exceed either minimum or maximum size.")]
        public void ImageSizeCannotExceedBounds()
        {
            Func<int, int> createGravatarUrl = (gravatarSize) =>
                {
                    var uri = CreateGravatarUri(imageSize: gravatarSize);
                    var sizeQueryParameter = GetQueryParameter(uri, "s");
                    int size;

                    Assert.True(int.TryParse(sizeQueryParameter, out size), "The query string must contain an integer value for size.");

                    return size;
                };

            var average = (GravatarHelper.MinImageSize + GravatarHelper.MaxImageSize) / 2;

            var minSize = createGravatarUrl(GravatarHelper.MinImageSize - 1);
            var avgSize = createGravatarUrl(average);
            var maxSize = createGravatarUrl(GravatarHelper.MaxImageSize + 1);

            Assert.True(minSize == GravatarHelper.MinImageSize, "Size cannot be smaller than MinImageSize");
            Assert.True(avgSize == average, "Size must remain unaltered if between MinImageSize and MaxImageSize");
            Assert.True(maxSize == GravatarHelper.MaxImageSize, "Size cannot be larger than MaxImageSize.");
        }

        /// <summary>
        /// Verify that the URL generated by CreateGravatarUrl is well-formed.
        /// </summary>
        [Fact(DisplayName = "Generated URL is well-formed.")]
        public void UrlIsWellFormed()
        {
            Func<string, Uri> createGravatarUri = (defaultImage) =>
                {
                    return CreateGravatarUri("MyEmailAddress@example.com", 80, defaultImage, GravatarRating.PG, true, true);
                };

            var identiconUri = createGravatarUri(GravatarHelper.DefaultImageIdenticon);
            var customUri = createGravatarUri("http://example.com/logo.jpg");

            Assert.True(identiconUri.IsWellFormedOriginalString(), "CreateGravatarUrl did not create a well-formed URI using a gravatar default image.");
            Assert.True(customUri.IsWellFormedOriginalString(), "CreateGravatarUrl did not create a well-formed URI using custom default image.");
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