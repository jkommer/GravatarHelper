﻿namespace GravatarHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// A simple ASP.NET MVC helper for Gravatar providing extension methods to HtmlHelper and UrlHelper.
    /// </summary>
    public static partial class GravatarHelper
    {
        #region Default Images

        /// <summary>
        /// 404: do not load any image if none is associated with the email hash, instead return an HTTP 404 (File Not Found) response.
        /// </summary>
        public const string DefaultImage404 = "404";

        /// <summary>
        /// mm: (mystery-man) a simple, cartoon-style silhouetted outline of a person (does not vary by email hash).
        /// </summary>
        public const string DefaultImageMysteryMan = "mm";

        /// <summary>
        /// identicon: a geometric pattern based on an email hash.
        /// </summary>
        public const string DefaultImageIdenticon = "identicon";

        /// <summary>
        /// monsterid: a generated 'monster' with different colors, faces, etc.
        /// </summary>
        public const string DefaultImageMonsterId = "monsterid";

        /// <summary>
        /// wavatar: generated faces with differing features and backgrounds.
        /// </summary>
        public const string DefaultImageWavatar = "wavatar";

        /// <summary>
        /// retro: awesome generated, 8-bit arcade-style pixelated faces.
        /// </summary>
        public const string DefaultImageRetro = "retro";

        #endregion Default Images

        /// <summary>
        /// Minimum image size supported by gravatar.
        /// </summary>
        public const int MinImageSize = 1;

        /// <summary>
        /// Maximum image size supported by gravatar.
        /// </summary>
        public const int MaxImageSize = 512;

        /// <summary>
        /// Gravatar HTTP url.
        /// </summary>
        private const string GravatarUrl = "http://www.gravatar.com";

        /// <summary>
        /// Gravatar HTTPS url.
        /// </summary>
        private const string GravatarSecureUrl = "https://secure.gravatar.com";

        /// <summary>
        /// Gravatar image path.
        /// </summary>
        private const string GravatarImagePath = "/avatar/";

        /// <summary>
        /// Gravatar profile path.
        /// </summary>
        private const string GravatarProfilePath = "/";

        /// <summary>
        /// Func used to retrieve the HttpContext to facilitate unit-testing.
        /// </summary>
        private static Func<HttpContextBase> getHttpContext = () => new HttpContextWrapper(HttpContext.Current);

        /// <summary>
        /// Gets or sets the Func used to retrieve the HttpContext.
        /// </summary>
        /// <value>
        /// The get HTTP context.
        /// </value>
        internal static Func<HttpContextBase> GetHttpContext
        {
            get
            { 
                return GravatarHelper.getHttpContext;
            }

            set
            {
                GravatarHelper.getHttpContext = value;
            }
        }

        /// <summary>
        /// Returns the Gravatar URL for the provided parameters.
        /// </summary>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        /// can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="rating">The content rating of the images to display.</param>
        /// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
        /// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
        /// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
        /// <returns>The Gravatar URL for the provided parameters.</returns>
        public static MvcHtmlString CreateGravatarImage(string email, int imageSize, string defaultImage, GravatarRating? rating, bool? addExtension, bool? forceDefault, IDictionary<string, object> htmlAttributes)
        {
            var imgTag = new TagBuilder("img");
            imgTag.MergeAttribute("src", CreateGravatarUrl(email, imageSize, defaultImage, rating, addExtension, forceDefault));

            if (htmlAttributes != null)
            {
                foreach (var htmlAttribute in htmlAttributes)
                {
                    imgTag.MergeAttribute(htmlAttribute.Key, htmlAttribute.Value.ToString());
                }
            }

            return MvcHtmlString.Create(imgTag.ToString());
        }

        /// <summary>
        /// Returns the Gravatar URL for the provided parameters.
        /// </summary>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="imageSize">Gravatar size in pixels.</param>
        /// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
        ///  can either be a url to an image or one of the DefaultImage* constants</param>
        /// <param name="rating">The content rating of the images to display.</param>
        /// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
        /// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
        /// <returns>The Gravatar URL for the provided parameters.</returns>
        public static string CreateGravatarUrl(string email, int imageSize, string defaultImage, GravatarRating? rating, bool? addExtension, bool? forceDefault)
        {
            var hash = CreateGravatarHash(email);

            // Limit our Gravatar size to be between the minimum and maximum sizes supported by Gravatar.
            imageSize = Math.Max(imageSize, MinImageSize);
            imageSize = Math.Min(imageSize, MaxImageSize);

            if (!string.IsNullOrWhiteSpace(defaultImage))
            {
                defaultImage = string.Format("&d={0}", HttpUtility.UrlEncode(defaultImage));
            }

            return string.Format(
                "{0}{1}{2}{3}?s={4}{5}{6}{7}",
                GetHttpContext().Request.IsSecureConnection ? GravatarSecureUrl : GravatarUrl,
                GravatarImagePath,
                hash,
                addExtension.GetValueOrDefault(false) ? ".jpg" : string.Empty,
                imageSize,
                defaultImage,
                rating.HasValue ? string.Concat("&r=", rating) : string.Empty,
                forceDefault.GetValueOrDefault(false) ? "&f=y" : string.Empty);
        }

        /// <summary>
        /// Returns the Gravatar profile URL for the provided parameters.
        /// </summary>
        /// <param name="email">Email address to generate the Gravatar for.</param>
        /// <param name="extension">Format extension to add to the url. Default is none, which creates a link to the profile page.</param>
        /// <param name="optionalParameters">Optional parameters to add to the url.</param>
        /// <returns>The Gravatar profile URL for the provided parameters.</returns>
        public static string CreateGravatarProfileUrl(string email, string extension, object optionalParameters)
        {
            var hash = CreateGravatarHash(email);

            var queryStringParameters = optionalParameters != null ?
                    "?" + string.Join(
                        "&",
                        new RouteValueDictionary(optionalParameters)
                        .Select(parameter => string.Format("{0}={1}", parameter.Key, HttpUtility.UrlEncode(parameter.Value.ToString()))))
                    : string.Empty;

            return string.Format(
                "{0}{1}{2}{3}{4}",
                GetHttpContext().Request.IsSecureConnection ? GravatarSecureUrl : GravatarUrl,
                GravatarProfilePath,
                hash,
                extension != null ? string.Concat(".", extension) : string.Empty,
                queryStringParameters);
        }

        /// <summary>
        /// Creates a Gravatar hash for the provided email.
        /// </summary>
        /// <param name="email">The email to create the hash for</param>
        /// <returns>A Gravatar hash for the provided email.</returns>
        public static string CreateGravatarHash(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return string.Empty;
            }

            email = email.Trim();
            email = email.ToLower();

            // MD5 returns 128 bits which we represent in hexadecimal using 32 characters.
            var stringBuilder = new StringBuilder(32);

            using (var md5 = MD5.Create())
            {
                var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(email));

                foreach (var hashedByte in hashedBytes)
                {
                    stringBuilder.Append(hashedByte.ToString("x2"));
                }
            }

            return stringBuilder.ToString();
        }
    }
}