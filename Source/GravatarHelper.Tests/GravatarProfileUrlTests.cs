namespace GravatarHelper.Tests
{
    using System;
    using Extensions;
    using Xunit;

    /// <summary>
    /// Test which verify the functionality of CreateGravatarProfileUrl.
    /// </summary>
    public class GravatarProfileUrlTests : BaseGravatarTests
    {
        /// <summary>
        /// Verifies that custom query parameters can be added to the profile url.
        /// </summary>
        [Fact(DisplayName = "Custom query parameters can be added to the profile url.")]
        public void CanAddCustomQueryParameters()
        {
            var parameters = new
            {
                Callback = "javascripCallback",
                Size = "80"
            };

            var uri = CreateGravatarProfileUri(optionalParameters: parameters);

            var sizeParameter = uri.GetQueryParameter("Size");
            var callbackParameter = uri.GetQueryParameter("Callback");

            Assert.True(sizeParameter == parameters.Size, string.Format("Callback parameter {0} expected but recieved {1}.", parameters.Size, sizeParameter));
            Assert.True(callbackParameter == parameters.Callback, string.Format("Callback parameter {0} expected but recieved {1}.", parameters.Callback, callbackParameter));
        }

        /// <summary>
        /// Creates the gravatar profile URI.
        /// </summary>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="extension">Format extension to add to the url. Default is none, which creates a link to the profile page.</param>
        /// <param name="optionalParameters">Optional parameters to add to the url.</param>
        /// <returns>The Gravatar profile url wrapped inside an Uri.</returns>
        private static Uri CreateGravatarProfileUri(string email = DefaultEmailAddress, string extension = null, object optionalParameters = null)
        {
            var url = GravatarHelper.CreateGravatarProfileUrl(email, extension, optionalParameters);
            return new Uri(url);
        }
    }
}
