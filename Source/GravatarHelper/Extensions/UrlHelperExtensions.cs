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
		/// Returns the Gravatar profile URL for the provided parameters.
		/// </summary>
		/// <param name="helper">UrlHelper.</param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <returns></returns>
		public static string GravatarProfile(this UrlHelper helper, string email)
		{
			return GravatarHelper.CreateGravatarProfileUrl(email);
		}
	}
}