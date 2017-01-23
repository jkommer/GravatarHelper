namespace GravatarHelper.Tests
{
    /// <summary>
    /// Base class for GravatarHelper unit tests. Sets up the GetHttpContext Func to work without a HttpContext.
    /// </summary>
    public abstract class BaseGravatarTests
    {
        /// <summary>
        /// Default email address for testing.
        /// </summary>
        protected const string DefaultEmailAddress = "MyEmailAddress@example.com";

        /// <summary>
        /// Default image size for testing.
        /// </summary>
        protected const int DefaultImageSize = 80;
    }
}
