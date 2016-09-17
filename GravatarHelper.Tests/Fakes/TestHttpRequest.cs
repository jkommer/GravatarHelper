namespace GravatarHelper.Tests.Fakes
{
    using System.Web;

    /// <summary>
    /// HttpRequest class to facilitate testing.
    /// </summary>
    internal class TestHttpRequest : HttpRequestBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether IsSecureConnection should return true or false.
        /// </summary>
        /// <value>
        ///     <c>true</c> if IsSecureConnection; otherwise, <c>false</c>.
        /// </value>
        public bool SecureConnectionResult { get; set; }

        /// <summary>
        /// When overridden in a derived class, gets a value that indicates whether the HTTP connection uses secure sockets (HTTPS protocol).
        /// </summary>
        /// <returns>true if the connection is an SSL connection that uses HTTPS protocol; otherwise, false.</returns>
        public override bool IsSecureConnection
        {
            get
            {
                return this.SecureConnectionResult;
            }
        }
    }
}