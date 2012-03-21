using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace GravatarHelper.Extensions
{
	/// <summary>
	/// Gravatar HtmlHelper extension methods.
	/// </summary>
	public static class HtmlHelperExtensions
	{
		/// <summary>
		/// Returns a Gravatar img tag for the provided parameters.
		/// </summary>
		/// <param name="helper">HtmlHelper.</param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <returns></returns>
		public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize)
		{
			return GravatarHelper.CreateGravatarImage(email, imageSize, null, null, null, null, null);
		}

		/// <summary>
		/// Returns a Gravatar img tag for the provided parameters.
		/// </summary>
		/// <param name="helper">HtmlHelper.</param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
		/// 						   can either be a url to an image or one of the DefaultImage* constants</param>
		/// <returns></returns>
		public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage)
		{
			return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, null, null, null, null);
		}

		/// <summary>
		/// Returns a Gravatar img tag for the provided parameters.
		/// </summary>
		/// <param name="helper">HtmlHelper.</param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
		/// <returns></returns>
		public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, object htmlAttributes)
		{
			return GravatarHelper.CreateGravatarImage(email, imageSize, null, null, null, null, new RouteValueDictionary(htmlAttributes));
		}

		/// <summary>
		/// Returns a Gravatar img tag for the provided parameters.
		/// </summary>
		/// <param name="helper">HtmlHelper.</param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
		/// <returns></returns>
		public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, IDictionary<string, object> htmlAttributes)
		{
			return GravatarHelper.CreateGravatarImage(email, imageSize, null, null, null, null, htmlAttributes);
		}

		/// <summary>
		/// Returns a Gravatar img tag for the provided parameters.
		/// </summary>
		/// <param name="helper">HtmlHelper.</param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
		/// 						   can either be a url to an image or one of the DefaultImage* constants</param>
		/// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
		/// <returns></returns>
		public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage, object htmlAttributes)
		{
			return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, null, null, null, new RouteValueDictionary(htmlAttributes));
		}

		/// <summary>
		/// Returns a Gravatar img tag for the provided parameters.
		/// </summary>
		/// <param name="helper">HtmlHelper.</param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
		/// 						   can either be a url to an image or one of the DefaultImage* constants</param>
		/// <param name="rating">The content rating of the images to display.</param>
		/// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
		/// <returns></returns>
		public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage, GravatarRating? rating, object htmlAttributes)
		{
			return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, rating, null, null, new RouteValueDictionary(htmlAttributes));
		}

		/// <summary>
		/// Returns a Gravatar img tag for the provided parameters.
		/// </summary>
		/// <param name="helper">HtmlHelper.</param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
		/// 						   can either be a url to an image or one of the DefaultImage* constants</param>
		/// <param name="rating">The content rating of the images to display.</param>
		/// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
		/// <returns></returns>
		public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage, GravatarRating? rating, IDictionary<string, object> htmlAttributes)
		{
			return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, rating, null, null, htmlAttributes);
		}

		/// <summary>
		/// Returns a Gravatar img tag for the provided parameters.
		/// </summary>
		/// <param name="helper">HtmlHelper.</param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
		/// 						   can either be a url to an image or one of the DefaultImage* constants</param>
		/// <param name="rating">The content rating of the images to display.</param>
		/// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
		/// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
		/// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
		/// <returns></returns>
		public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage, GravatarRating? rating, bool addExtension, bool forceDefault, object htmlAttributes)
		{
			return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, rating, addExtension, forceDefault, new RouteValueDictionary(htmlAttributes));
		}

		/// <summary>
		/// Returns a Gravatar img tag for the provided parameters.
		/// </summary>
		/// <param name="helper">HtmlHelper.</param>
		/// <param name="email">Email address to generate the Gravatar for.</param>
		/// <param name="imageSize">Gravatar size in pixels.</param>
		/// <param name="defaultImage">The default image to use if the user does not have a Gravatar setup,
		/// 						   can either be a url to an image or one of the DefaultImage* constants</param>
		/// <param name="rating">The content rating of the images to display.</param>
		/// <param name="addExtension">Whether to add the .jpg extension to the provided Gravatar.</param>
		/// <param name="forceDefault">Forces Gravatar to always serve the default image.</param>
		/// <param name="htmlAttributes">Object containing the HTML attributes to set for the img element.</param>
		/// <returns></returns>
		public static MvcHtmlString Gravatar(this HtmlHelper helper, string email, int imageSize, string defaultImage, GravatarRating? rating, bool addExtension, bool forceDefault, IDictionary<string, object> htmlAttributes)
		{
			return GravatarHelper.CreateGravatarImage(email, imageSize, defaultImage, rating, addExtension, forceDefault, htmlAttributes);
		}
	}
}