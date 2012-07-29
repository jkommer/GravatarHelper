namespace GravatarHelper.Tests.Fakes
{
    using System.Web;

    /// <summary>
    /// HttpContext class to facilitate testing.
    /// </summary>
    internal class TestHttpContext : HttpContextBase
    {
        /// <summary>
        /// Backing field for Request.
        /// </summary>
        private HttpRequestBase httpRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestHttpContext"/> class.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        public TestHttpContext(HttpRequestBase httpRequest)
        {
            this.httpRequest = httpRequest;
        }

        /// <summary>
        /// When overridden in a derived class, gets the <see cref="T:System.Web.HttpRequest"/> object for the current HTTP request.
        /// </summary>
        /// <returns>The current HTTP request.</returns>
        public override HttpRequestBase Request
        {
            get { return this.httpRequest; }
        }
    }
}