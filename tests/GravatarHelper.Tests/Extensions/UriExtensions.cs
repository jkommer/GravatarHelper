using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.WebUtilities;

namespace GravatarHelper.Tests.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Uri"/>.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Gets the query parameters.
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <returns>A NameValueCollection of all query parameters.</returns>
        public static IDictionary<string, StringValues> GetQueryParameters(this Uri uri)
        {
            return QueryHelpers.ParseQuery(uri.Query);
        }

        /// <summary>
        /// Gets the query parameter.
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The value of the query parameter, null if not specified.</returns>
        public static string GetQueryParameter(this Uri uri, string parameter)
        {
            var parameters = GetQueryParameters(uri);

            if (parameters.TryGetValue(parameter, out StringValues value))
                return value;

            return null;
        }
    }
}
