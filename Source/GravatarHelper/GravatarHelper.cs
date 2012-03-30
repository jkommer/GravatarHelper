using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GravatarHelper
{
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
		private const int MinImageSize = 1;

		/// <summary>
		/// Maximum image size supported by gravatar.
		/// </summary>
		private const int MaxImageSize = 512;

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
		/// Returns the Gravatar URL for the provided parameters.
		/// </summary>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup, 
		/// 						   can either be a url to an image or one of the DefaultImage* constants</param>
		/// <param name="rating">The content rating of the images to display.</param>
		/// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
		/// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
		/// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
		/// <returns></returns>
		public static MvcHtmlString CreateGravatarImage(string email, int imageSize, string defaultImage, GravatarRating? rating, bool? addExtension, bool? forceDefault, IDictionary<string, object> htmlAttributes)
		{
			var imgTag = new TagBuilder("img");
			imgTag.MergeAttribute("src", CreateGravatarUrl(email, imageSize, defaultImage, rating, addExtension, forceDefault));

			if (htmlAttributes != null)
			{
				foreach (var htmlAttribute in htmlAttributes)
					imgTag.MergeAttribute(htmlAttribute.Key, htmlAttribute.Value.ToString());
			}

			return MvcHtmlString.Create(imgTag.ToString());
		}

		/// <summary>
		/// Returns the Gravatar URL for the provided parameters. 
		/// </summary>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup, 
		/// 						   can either be a url to an image or one of the DefaultImage* constants</param>
		/// <param name="rating">The content rating of the images to display.</param>
		/// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
		/// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
		/// <returns></returns>
		public static string CreateGravatarUrl(string email, int imageSize, string defaultImage, GravatarRating? rating, bool? addExtension, bool? forceDefault)
		{
			var hash = CreateGravatarHash(email);

			// Limit our Gravatar size to be between the minimum and maximum sizes supported by Gravatar.
			imageSize = Math.Max(imageSize, MinImageSize);
			imageSize = Math.Min(imageSize, MaxImageSize);

			if (!string.IsNullOrEmpty(defaultImage) && defaultImage.Trim().Length > 0)
				defaultImage = string.Format("&d={0}", HttpUtility.UrlEncode(defaultImage));

			return string.Format("{0}{1}{2}?s={3}{4}{5}{6}{7}",
				HttpContext.Current.Request.IsSecureConnection ? GravatarSecureUrl : GravatarUrl,
				GravatarImagePath,
				hash,
				imageSize,
				defaultImage,
				rating.HasValue ? string.Concat("&r=", rating) : string.Empty,
				forceDefault.GetValueOrDefault(false) ? "&f=y" : string.Empty,
				addExtension.GetValueOrDefault(false) ? ".jpg" : string.Empty
			);
		}

		/// <summary>
		/// Returns the Gravatar profile URL for the provided parameters. 
		/// </summary>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <returns></returns>
		public static string CreateGravatarProfileUrl(string email)
		{
			var hash = CreateGravatarHash(email);

			return string.Format("{0}{1}{2}",
				HttpContext.Current.Request.IsSecureConnection ? GravatarSecureUrl : GravatarUrl,
				GravatarProfilePath,
				hash
			);
		}
		
		/// <summary>
		/// Creates a gravatar hash.
		/// </summary>
		/// <param name="email">The email to create the hash for</param>
		/// <returns></returns>
		public static string CreateGravatarHash(string email)
		{
			if (string.IsNullOrEmpty(email) || email.Trim().Length == 0)
				return string.Empty;

			email = email.Trim();
			email = email.ToLower();

			// MD5 returns 128 bits which we represent in hexadecimal using 32 characters. 
			var stringBuilder = new StringBuilder(32);

			using (var md5 = MD5.Create())
			{
				var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(email));

				foreach (var hashedByte in hashedBytes)
					stringBuilder.Append(hashedByte.ToString("x2"));
			}

			return stringBuilder.ToString();
		}
	}
}
