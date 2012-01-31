using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace $rootnamespace$.Helpers
{
	/// <summary>
	/// A simple ASP.NET MVC helper for Gravatar.
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
		private const string GravatarUrl = "http://www.gravatar.com/avatar";
		
		/// <summary>
		/// Gravatar HTTPS url.
		/// </summary>
		private const string GravatarSecureUrl = "https://secure.gravatar.com/avatar";

		/// <summary>
		/// Returns a Gravatar img tag for the provided parameters.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="altText">The alt text for the image.</param>
		/// <param name="cssClass">The css class for the image.</param>
		/// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup, 
		/// 						   can either be a url to an image or one of the DefaultImage* constants</param>
		/// <param name="rating">The content rating of the images to display.</param>
		/// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
		/// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
		/// <returns></returns>
		public static IHtmlString GravatarImage(this HtmlHelper helper, string email, int imageSize, string altText = "", string cssClass = "", string defaultImage = "", GravatarRating rating = GravatarRating.G, bool addExtension = false, bool forceDefault = false)
		{
			return GravatarImage(email, imageSize, altText, cssClass, defaultImage, rating, addExtension, forceDefault);
		}


		/// <summary>
		/// Returns a Gravatar img tag for the provided parameters.
		/// </summary>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="altText">The alt text for the image.</param>
		/// <param name="cssClass">The css class for the image.</param>
		/// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup, 
		/// 						   can either be a url to an image or one of the DefaultImage* constants</param>
		/// <param name="rating">The content rating of the images to display.</param>
		/// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
		/// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
		/// <returns></returns>
		public static IHtmlString GravatarImage(string email, int imageSize, string altText = "", string cssClass = "", string defaultImage = "", GravatarRating rating = GravatarRating.G, bool addExtension = false, bool forceDefault = false)
		{
			var imgTag = new TagBuilder("img");
			imgTag.MergeAttribute("src", GravatarImageUrl(email, imageSize, defaultImage, rating, addExtension, forceDefault));

			if (!string.IsNullOrWhiteSpace(altText))
                		imgTag.MergeAttribute("alt", altText);

			if (!string.IsNullOrWhiteSpace(cssClass))
                		imgTag.MergeAttribute("class", cssClass);

			return MvcHtmlString.Create(imgTag.ToString());
		}

		/// <summary>
		/// Returns the Gravatar URL for the provided parameters.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup, 
		/// 						   can either be a url to an image or one of the DefaultImage* constants</param>
		/// <param name="rating">The content rating of the images to display.</param>
		/// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
		/// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
		/// <returns></returns>
		public static string GravatarImageUrl(this HtmlHelper helper, string email, int imageSize, string defaultImage = "", GravatarRating rating = GravatarRating.G, bool addExtension = false, bool forceDefault = false)
		{
			return GravatarImageUrl(email, imageSize, defaultImage, rating, addExtension, forceDefault);
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
		public static string GravatarImageUrl(string email, int imageSize, string defaultImage = "", GravatarRating rating = GravatarRating.G, bool addExtension = false, bool forceDefault = false)
		{
			var hash = CreateGravatarHash(email);

			if (imageSize < MinImageSize)
				imageSize = MinImageSize;

			if (imageSize > MaxImageSize)
				imageSize = MaxImageSize;

			if (!string.IsNullOrWhiteSpace(defaultImage))
				defaultImage = string.Format("&d={0}", HttpUtility.UrlEncode(defaultImage));

			return string.Format("{0}/{1}?s={2}{3}&r={4}{5}{6}",
				HttpContext.Current.Request.IsSecureConnection ? GravatarSecureUrl : GravatarUrl,
				hash,
				imageSize,
				defaultImage,
				rating,
				forceDefault ? "&f=y" : "",
				addExtension ? ".jpg" : ""
			);
		}

		/// <summary>
		/// Creates a gravatar hash.
		/// </summary>
		/// <param name="email">The email to create the hash for</param>
		/// <returns></returns>
		private static string CreateGravatarHash(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return string.Empty;

			email = email.Trim();
			email = email.ToLower();

			var stringBuilder = new StringBuilder();

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