using System;
using System.Web.Mvc;
using System.Web.Routing;
using GravatarHelper.Extensions;
using Xunit;

namespace GravatarHelper.Tests
{
    public class GravatarProfileTests
    {
        /// <summary>
        /// Default email address for testing.
        /// </summary>
        private const string DefaultEmailAddress = "MyEmailAddress@example.com";

        public GravatarProfileTests()
        {
            UrlHelper = new UrlHelper(new RequestContext());

        }

        private UrlHelper UrlHelper { get; }

        [Fact(DisplayName = "QR Profile urls should end with .qr")]
        public void QRCodesEndWithQRExtension()
        {
            var url = UrlHelper.GravatarProfileAsQRCode(DefaultEmailAddress, false);
            var uri = new Uri(url);

            Assert.EndsWith(".qr", uri.LocalPath);
        }

        [Fact(DisplayName = "JSON Profile urls should end with .json")]
        public void JsonProfilesEndWithJson()
        {
            var url = UrlHelper.GravatarProfileAsJSON(DefaultEmailAddress, false);
            var uri = new Uri(url);

            Assert.EndsWith(".json", uri.LocalPath);
        }
    }
}