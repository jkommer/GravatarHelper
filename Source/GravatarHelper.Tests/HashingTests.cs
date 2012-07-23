﻿namespace GravatarHelper.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Test which verify the functionality of CreateGravatarHash
    /// </summary>
    public class HashingTests
    {
        /// <summary>
        /// Verifies that the casing of an email address does not alter CreateGravatarHash's result.
        /// </summary>
        [Fact(DisplayName = "The casing of an email address is irrelevant for hashing.")]
        public void EmailAddressCaseIsIrrelevantForHash()
        {
            var emails = new List<string>
                {
                    "MyEmailAddress@example.com",
                    "myemailaddress@example.com",
                    "MYEMAILADDRESS@EXAMPLE.COM",
                    "MYEMAILADDRESS@example.com",
                    "myemailaddress@EXAMPLE.COM"
                };

            var hashes = emails.Select(email => GravatarHelper.CreateGravatarHash(email));

            Assert.True(hashes.Any(), "Email addresses should succesfully hash.");

            var firstHash = hashes.First();

            Assert.True(hashes.All(hash => hash == firstHash), "Email addresses should return same hash regardless of case.");
        }

        /// <summary>
        /// Verifies that CreateGravatarHash produces correct results for predetermined values.
        /// </summary>
        [Fact(DisplayName = "Returns correct hashes.")]
        public void HashResult()
        {
            var emailAndExpectedHashes = new Dictionary<string, string>
                { 
                    { "MyEmailAddress@example.com", "0bc83cb571cd1c50ba6f3e8a78ef1346" },
                    { "jsmith@example.org", "5cc22172821c12cd0c014ed7af99ae6f" },
                    { "simplewith+symbol@example.com", "6e98f7d4e24fd2fcb163d1e31aef4359" },
                    { "a.little.more.unusual@dept.example.com", "27b0165c7edc6ca4b0575a3c67622291" },
                    { "user@[IPv6:2001:db8:1ff::a0b:dbd0]", "829fc0ea15e1e4daec2ba90bd23b0d62" },
                };

            foreach (var combination in emailAndExpectedHashes)
            {
                var hash = GravatarHelper.CreateGravatarHash(combination.Key);
                Assert.True(hash == combination.Value, string.Format("Email {0} hashed into {1} but expected {2}", combination.Key, hash, combination.Value));
            }
        }

        /// <summary>
        /// Verifies that the Gravatar hashing ignores any leading and trailing whitespaces. 
        /// </summary>
        [Fact(DisplayName = "Leading and trailing whitespaces are ignored.")]
        public void HashingIgnoresLeadingAndTrailingWhitespaces()
        {
            var emails = new List<string>
                {
                    "MyEmailAddress@example.com",
                    " MyEmailAddress@example.com",
                    " MyEmailAddress@example.com ",
                    "  MyEmailAddress@example.com  "
                };

            var hashes = emails.Select(email => GravatarHelper.CreateGravatarHash(email));

            Assert.True(hashes.Any(), "Email addresses should succesfully hash.");

            var firstHash = hashes.First();

            Assert.True(hashes.All(hash => hash == firstHash), "Email addresses should return same hash regardless of leading and trailing whitespaces.");
        }
    }
}