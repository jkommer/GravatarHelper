using System.Web.Mvc;

namespace GravatarHelper.Extensions
{
    /// <summary>
    /// Gravatar UrlHelper extension methods.
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Returns the Gravatar URL for the provided parameters.
        /// </summary>
        /// <param name="helper">UrlHelper.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <returns></returns>
        public static string Gravatar(this UrlHelper helper, string email, int imageSize)
        {
            return GravatarHelper.CreateGravatarUrl(email, imageSize, null, null, null, null);
        }

        /// <summary>
        /// Returns the Gravatar URL for the provided parameters.
        /// </summary>
        /// <param name="helper">UrlHelper.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// 						   can either be a url to an image or one of the DefaultImage* constants</param>
        /// <returns></returns>
        public static string Gravatar(this UrlHelper helper, string email, int imageSize, string defaultImage)
        {
            return GravatarHelper.CreateGravatarUrl(email, imageSize, defaultImage, null, null, null);
        }

        /// <summary>
        /// Returns the Gravatar URL for the provided parameters.
        /// </summary>
        /// <param name="helper">UrlHelper.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// 						   can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="rating">The content rating of the images to display.</param>
        /// <returns></returns>
        public static string Gravatar(this UrlHelper helper, string email, int imageSize, string defaultImage, GravatarRating? rating)
        {
            return GravatarHelper.CreateGravatarUrl(email, imageSize, defaultImage, rating, null, null);
        }

        /// <summary>
        /// Returns a URL to the Gravatar profile.
        /// </summary>
        /// <param name="helper">UrlHelper.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <returns></returns>
        public static string GravatarProfile(this UrlHelper helper, string email)
        {
            return GravatarHelper.CreateGravatarProfileUrl(email, null, null);
        }

        /// <summary>
        /// Returns a URL to the Gravatar profile data formatted as JSON.
        /// </summary>
        /// <param name="helper">UrlHelper.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <returns></returns>
        public static string GravatarProfileAsJSON(this UrlHelper helper, string email)
        {
            return GravatarHelper.CreateGravatarProfileUrl(email, "json", null);
        }

        /// <summary>
        /// Returns a URL to the Gravatar profile data formatted as JSON.
        /// </summary>
        /// <param name="helper">UrlHelper.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <param name="callback">Name of the Javascript callback function.</param>
        /// <returns></returns>
        public static string GravatarProfileAsJSON(this UrlHelper helper, string email, string callback)
        {
            return GravatarHelper.CreateGravatarProfileUrl(email, "json", new { callback = callback });
        }

        /// <summary>
        /// Returns a URL to the Gravatar profile data formatted as vCard.
        /// </summary>
        /// <param name="helper">UrlHelper.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <returns></returns>
        public static string GravatarProfileAsVCard(this UrlHelper helper, string email)
        {
            return GravatarHelper.CreateGravatarProfileUrl(email, "vcf", null);
        }

        /// <summary>
        /// Returns a URL to the Gravatar profile data formatted as XML.
        /// </summary>
        /// <param name="helper">UrlHelper.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <returns></returns>
        public static string GravatarProfileAsXml(this UrlHelper helper, string email)
        {
            return GravatarHelper.CreateGravatarProfileUrl(email, "xml", null);
        }

        /// <summary>
        /// Returns a URL to an image containing a QR Code link back to the Gravatar profile.
        /// </summary>
        /// <param name="helper">UrlHelper.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <returns></returns>
        public static string GravatarProfileAsQRCode(this UrlHelper helper, string email)
        {
            return GravatarHelper.CreateGravatarProfileUrl(email, "qr", null);
        }

        /// <summary>
        /// Returns a URL to an image containing a QR Code link back to the Gravatar profile.
        /// </summary>
        /// <param name="helper">UrlHelper.</param>
        /// <param name="email">Email address to generate the link for.</param>
        /// <param name="imageSize">QR Code size in pixels.</param>
        /// <returns></returns>
        public static string GravatarProfileAsQRCode(this UrlHelper helper, string email, int imageSize)
        {
            return GravatarHelper.CreateGravatarProfileUrl(email, "qr", new { s = imageSize });
        }
    }
}