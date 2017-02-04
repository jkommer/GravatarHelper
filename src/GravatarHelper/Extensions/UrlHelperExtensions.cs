using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace GravatarHelper.Extensions
{
    /// <summary>
    /// Gravatar UrlHelper extension methods.
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Gets or sets the Func used to retrieve the HttpContext.
        /// </summary>
        /// <value>
        /// The get HTTP context.
        /// </value>
        internal static Func<HttpContextBase> GetHttpContext { get; set; } = () => new HttpContextWrapper(HttpContext.Current);

        /// <summary>
        /// Returns the Gravatar URL for the provided parameters.
        /// </summary>
        /// <param name="helper">The UrlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>#
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>The Gravatar URL for the provided parameters.</returns>
        public static string Gravatar(this UrlHelper helper, string email, int imageSize, bool forceSecureUrl = false)
        {
            return Common.GravatarHelper.CreateGravatarUrl(email, imageSize, null, null, null, null, forceSecureUrl || IsSecureConnection());
        }

        /// <summary>
        /// Returns the Gravatar URL for the provided parameters.
        /// </summary>
        /// <param name="helper">The UrlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>The Gravatar URL for the provided parameters.</returns>
        public static string Gravatar(this UrlHelper helper, string email, int imageSize, string defaultImage, bool forceSecureUrl = false)
        {
            return Common.GravatarHelper.CreateGravatarUrl(email, imageSize, defaultImage, null, null, null, forceSecureUrl || IsSecureConnection());
        }

        /// <summary>
        /// Returns the Gravatar URL for the provided parameters.
        /// </summary>
        /// <param name="helper">The UrlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="rating">The content rating of the images to display.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>The Gravatar URL for the provided parameters.</returns>
        public static string Gravatar(this UrlHelper helper, string email, int imageSize, string defaultImage, GravatarRating? rating, bool forceSecureUrl = false)
        {
            return Common.GravatarHelper.CreateGravatarUrl(email, imageSize, defaultImage, rating, null, null, forceSecureUrl || IsSecureConnection());
        }

        /// <summary>
        /// Returns a URL to the Gravatar profile.
        /// </summary>
        /// <param name="helper">The UrlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>The Gravatar URL for the provided parameters.</returns>
        public static string GravatarProfile(this UrlHelper helper, string email, bool forceSecureUrl = false)
        {
            return Common.GravatarHelper.CreateGravatarProfileUrl(email, null, null, forceSecureUrl);
        }

        /// <summary>
        /// Returns a URL to the Gravatar profile data formatted as JSON.
        /// </summary>
        /// <param name="helper">The UrlHelper to extend.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A URL to the Gravatar profile data formatted as JSON.</returns>
        public static string GravatarProfileAsJSON(this UrlHelper helper, string email, bool forceSecureUrl = false)
        {
            return Common.GravatarHelper.CreateGravatarProfileUrl(email, "json", null, forceSecureUrl);
        }

        /// <summary>
        /// Returns a URL to the Gravatar profile data formatted as JSON.
        /// </summary>
        /// <param name="helper">The UrlHelper to extend.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <param name="callback">Name of the Javascript callback function.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A URL to the Gravatar profile data formatted as JSON.</returns>
        public static string GravatarProfileAsJSON(this UrlHelper helper, string email, string callback, bool forceSecureUrl = false)
        {
            var optionalParameters = new Dictionary<string, string>
            {
                { "Callback", callback}
            };

            return Common.GravatarHelper.CreateGravatarProfileUrl(email, "json", optionalParameters, forceSecureUrl);
        }

        /// <summary>
        /// Returns a URL to the Gravatar profile data formatted as vCard.
        /// </summary>
        /// <param name="helper">The UrlHelper to extend.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A URL to the Gravatar profile data formatted as vCard.</returns>
        public static string GravatarProfileAsVCard(this UrlHelper helper, string email, bool forceSecureUrl = false)
        {
            return Common.GravatarHelper.CreateGravatarProfileUrl(email, "vcf", null, forceSecureUrl);
        }

        /// <summary>
        /// Returns a URL to the Gravatar profile data formatted as XML.
        /// </summary>
        /// <param name="helper">The UrlHelper to extend.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A URL to the Gravatar profile data formatted as XML.</returns>
        public static string GravatarProfileAsXml(this UrlHelper helper, string email, bool forceSecureUrl = false)
        {
            return Common.GravatarHelper.CreateGravatarProfileUrl(email, "xml", null, forceSecureUrl);
        }

        /// <summary>
        /// Returns a URL to an image containing a QR Code link back to the Gravatar profile.
        /// </summary>
        /// <param name="helper">The UrlHelper to extend.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A URL to an image containing a QR Code link back to the Gravatar profile.</returns>
        public static string GravatarProfileAsQRCode(this UrlHelper helper, string email, bool forceSecureUrl = false)
        {
            return Common.GravatarHelper.CreateGravatarProfileUrl(email, "qr", null, forceSecureUrl);
        }

        /// <summary>
        /// Returns a URL to an image containing a QR Code link back to the Gravatar profile.
        /// </summary>
        /// <param name="helper">The UrlHelper to extend.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <param name="imageSize">QR Code size in pixels.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A URL to an image containing a QR Code link back to the Gravatar profile.</returns>
        public static string GravatarProfileAsQRCode(this UrlHelper helper, string email, int imageSize, bool forceSecureUrl = false)
        {
            var optionalParameters = new Dictionary<string, string>
            {
                { "s", imageSize.ToString() }
            };

            return Common.GravatarHelper.CreateGravatarProfileUrl(email, "qr", optionalParameters, forceSecureUrl);
        }

        private static bool IsSecureConnection()
        {
            var httpContext = GetHttpContext();
            var httpRequest = httpContext.Request;

            return httpRequest.IsSecureConnection;
        }
    }
}