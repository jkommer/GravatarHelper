namespace GravatarHelper.Tests.Extensions
{
    using System;
    using System.Collections.Specialized;
    using System.Web;

    /// <summary>
    /// Extension methods for <see cref="Uri"/>.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Gets the query parameters as a <see cref="NameValueCollection"/>.
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <returns>A NameValueCollection of all query parameters.</returns>
        public static NameValueCollection GetQueryParameters(this Uri uri)
        {
            return HttpUtility.ParseQueryString(uri.Query);
        }

        /// <summary>
        /// Gets the query parameter.
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The value of the query parameter, null if not specified.</returns>
        public static string GetQueryParameter(this Uri uri, string parameter)
        {
            return GetQueryParameters(uri)[parameter];
        }
    }
}
