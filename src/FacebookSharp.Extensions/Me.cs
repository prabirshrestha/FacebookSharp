namespace FacebookSharp.Extensions
{
    using System.Collections.Generic;
    using FacebookSharp.Schemas.Graph;

    public static partial class FacebookExtensions
    {
        /// <summary>
        /// Get list of pages for the current Facebook User
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetMyPagesAsJsonString(this Facebook facebook, IDictionary<string, string> parameters)
        {
            AssertRequireAccessToken(facebook);
            return GetConnections(facebook, "me", "accounts", parameters);
        }

        public static string GetMyPagesAsJsonString(this Facebook facebook)
        {
            return GetMyPagesAsJsonString(facebook);
        }

        public static AccountCollection GetMyPages(this Facebook facebook, IDictionary<string, string> parameters)
        {
            AssertRequireAccessToken(facebook);
            return GetConnections<AccountCollection>(facebook, "me", "accounts", parameters);
        }

        public static AccountCollection GetMyPages(this Facebook facebook)
        {
            return GetMyPages(facebook, null);
        }
    }
}
