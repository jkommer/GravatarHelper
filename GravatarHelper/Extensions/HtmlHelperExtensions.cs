namespace GravatarHelper.Extensions
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Gravatar HtmlHelper extension methods.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns a Gravatar img tag for the provided parameters.
        /// </summary>
        /// <param name="helper">The HtmlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A Gravatar img tag for the provided parameters.</returns>
        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, bool forceSecureUrl = false)
        {
            return GravatarHelper.CreateGravatarImage(email, imageSize, null, null, null, null, null, forceSecureUrl);
        }

        /// <summary>
        /// Returns a Gravatar img tag for the provided parameters.
        /// </summary>
        /// <param name="helper">The HtmlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A Gravatar img tag for the provided parameters.</returns>
        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage, bool forceSecureUrl = false)
        {
            return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, null, null, null, null, forceSecureUrl);
        }

        /// <summary>
        /// Returns a Gravatar img tag for the provided parameters.
        /// </summary>
        /// <param name="helper">The HtmlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A Gravatar img tag for the provided parameters.</returns>
        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, object htmlAttributes, bool forceSecureUrl = false)
        {
            return GravatarHelper.CreateGravatarImage(email, imageSize, null, null, null, null, new RouteValueDictionary(htmlAttributes), forceSecureUrl);
        }

        /// <summary>
        /// Returns a Gravatar img tag for the provided parameters.
        /// </summary>
        /// <param name="helper">The HtmlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A Gravatar img tag for the provided parameters.</returns>
        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, IDictionary<string, object> htmlAttributes, bool forceSecureUrl = false)
        {
            return GravatarHelper.CreateGravatarImage(email, imageSize, null, null, null, null, htmlAttributes, forceSecureUrl);
        }

        /// <summary>
        /// Returns a Gravatar img tag for the provided parameters.
        /// </summary>
        /// <param name="helper">The HtmlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A Gravatar img tag for the provided parameters.</returns>
        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage, object htmlAttributes, bool forceSecureUrl = false)
        {
            return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, null, null, null, new RouteValueDictionary(htmlAttributes), forceSecureUrl);
        }

        /// <summary>
        /// Returns a Gravatar img tag for the provided parameters.
        /// </summary>
        /// <param name="helper">The HtmlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="rating">The content rating of the images to display.</param>
        /// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A Gravatar img tag for the provided parameters.</returns>
        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage, GravatarRating? rating, object htmlAttributes, bool forceSecureUrl = false)
        {
            return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, rating, null, null, new RouteValueDictionary(htmlAttributes), forceSecureUrl);
        }

        /// <summary>
        /// Returns a Gravatar img tag for the provided parameters.
        /// </summary>
        /// <param name="helper">The HtmlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="rating">The content rating of the images to display.</param>
        /// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A Gravatar img tag for the provided parameters.</returns>
        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage, GravatarRating? rating, IDictionary<string, object> htmlAttributes, bool forceSecureUrl = false)
        {
            return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, rating, null, null, htmlAttributes, forceSecureUrl);
        }

        /// <summary>
        /// Returns a Gravatar img tag for the provided parameters.
        /// </summary>
        /// <param name="helper">The HtmlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="rating">The content rating of the images to display.</param>
        /// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
        /// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
        /// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A Gravatar img tag for the provided parameters.</returns>
        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage, GravatarRating? rating, bool addExtension, bool forceDefault, object htmlAttributes, bool forceSecureUrl = false)
        {
            return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, rating, addExtension, forceDefault, new RouteValueDictionary(htmlAttributes), forceSecureUrl);
        }

        /// <summary>
        /// Returns a Gravatar img tag for the provided parameters.
        /// </summary>
        /// <param name="helper">The HtmlHelper to extend.</param>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="rating">The content rating of the images to display.</param>
        /// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
        /// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
        /// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
        /// <param name="forceSecureUrl">Forces the use of https</param>
        /// <returns>A Gravatar img tag for the provided parameters.</returns>
        public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage, GravatarRating? rating, bool addExtension, bool forceDefault, IDictionary<string, object> htmlAttributes, bool forceSecureUrl = false)
        {
            return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, rating, addExtension, forceDefault, htmlAttributes, forceSecureUrl);
        }
    }
}