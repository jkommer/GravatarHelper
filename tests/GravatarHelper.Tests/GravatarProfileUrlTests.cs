using System;
using System.Collections.Generic;
using GravatarHelper.Tests.Extensions;
using Xunit;

namespace GravatarHelper.Tests
{
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
            var parameters = new Dictionary<string, string>
            {
                { "Callback", "javascripCallback" },
                { "Size", "80" }
            };

            var uri = CreateGravatarProfileUri(optionalParameters: parameters);

            var sizeParameter = uri.GetQueryParameter("Size");
            var callbackParameter = uri.GetQueryParameter("Callback");

            Assert.True(sizeParameter == parameters["Size"], string.Format("Callback parameter {0} expected but recieved {1}.", parameters["Size"], sizeParameter));
            Assert.True(callbackParameter == parameters["Callback"], string.Format("Callback parameter {0} expected but recieved {1}.", parameters["Callback"], callbackParameter));
        }

        /// <summary>
        /// Creates the gravatar profile URI.
        /// </summary>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="extension">Format extension to add to the url. Default is none, which creates a link to the profile page.</param>
        /// <param name="optionalParameters">Optional parameters to add to the url.</param>
        /// <param name="useSecureUrl">Whether to request the Gravatar over https.</param>
        /// <returns>The Gravatar profile url wrapped inside an Uri.</returns>
        private static Uri CreateGravatarProfileUri(string email = DefaultEmailAddress, string extension = null, IDictionary<string, string> optionalParameters = null, bool useSecureUrl = false)
        {
            var url = Common.GravatarHelper.CreateGravatarProfileUrl(email, extension, optionalParameters, useSecureUrl);
            return new Uri(url);
        }
    }
}
