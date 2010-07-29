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

        public static bool AmIAdminOfPage(this Facebook facebook, string pageId)
        {
            AssertRequireAccessToken(facebook);
            var pages = facebook.GetMyPages();

            if (pages == null)
                return false;

            return pages.Data.Find(p => p.ID == pageId) != null;
        }

        /// <summary>
        /// Gets the the list of all facebook user who are members of the specified page.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="pageId">
        /// The page id.
        /// </param>
        /// <returns>
        /// Returns list of users who are members for the page.
        /// </returns>
        public static BasicUserInfoCollection GetPageMembers(this Facebook facebook, string pageId)
        {
            return facebook.GetPageMembers(pageId, null);
        }

        /// <summary>
        /// Gets the the list of all facebook user who are members of the specified page.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="pageId">
        /// The page id.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// Returns list of users who are members for the page.
        /// </returns>
        public static BasicUserInfoCollection GetPageMembers(this Facebook facebook, string pageId, IDictionary<string, string> parameters)
        {
            var likes = facebook.Get<BasicUserInfoCollection>("/" + pageId + "/members", parameters) ?? new BasicUserInfoCollection();

            if (likes.Data == null)
                likes.Data = new List<BasicUserInfo>();

            return likes;
        }

        public static BasicUserInfoCollection GetPageMembers(this Facebook facebook, string pageId, int? limit, int? offset, string until, IDictionary<string, string> parameters)
        {
            parameters = AppendPagingParameters(parameters, limit, offset, until);
            return facebook.GetPageMembers(pageId, parameters);
        }
    }
}
