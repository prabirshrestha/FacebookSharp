using System;
using System.Text;

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

        /// <summary>
        /// Checks whether the current user is an admin of the sepcified page.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="pageId">
        /// The page id.
        /// </param>
        /// <returns>
        /// </returns>
        [Obsolete(Facebook.OldRestApiWarningMessage)]
        public static bool AmIAdminOfPage(this Facebook facebook, string pageId)
        {
            return facebook.IsAdminOfPage(string.Empty, pageId);
        }

        /// <summary>
        /// Checkes whether the sepcified user is the admin of the page.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="pageId">
        /// The page id.
        /// </param>
        /// <returns>
        /// </returns>
        [Obsolete(Facebook.OldRestApiWarningMessage)]
        public static bool IsAdminOfPage(this Facebook facebook, string userId, string pageId)
        {
            return facebook.IsAdminOfPage(userId, pageId, false);
        }

        /// <summary>
        /// Checks if the specified user is the admin of the page or not.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="pageId">
        /// The page id.
        /// </param>
        /// <param name="ignoreAccessTokenException">
        /// The ignore access token exception.
        /// </param>
        /// <returns>
        /// </returns>
        /// <remarks>
        ///     ignoreAccessTokenException is true, it will not throw OAuthException if the user doesn't have enough permission,
        /// rather it will return false.
        /// </remarks>
        [Obsolete(Facebook.OldRestApiWarningMessage)]
        public static bool IsAdminOfPage(this Facebook facebook, string userId, string pageId, bool ignoreAccessTokenException)
        {
            AssertRequireAccessToken(facebook);

            var parameters = new Dictionary<string, string>
                                 {
                                     {"page_id", pageId}
                                 };

            // http://developers.facebook.com/docs/reference/rest/pages.isAdmin
            if (!string.IsNullOrEmpty(userId))
                parameters.Add("uid", userId);

            if (ignoreAccessTokenException)
            {
                try
                {
                    var result = facebook.GetUsingRestApi("pages.isAdmin", parameters);

                    return result.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                catch (OAuthException)
                {
                    return false;
                }

            }
            else
            {
                var result = facebook.GetUsingRestApi("pages.isAdmin", parameters);

                return result.Equals("true", StringComparison.OrdinalIgnoreCase);
            }
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
