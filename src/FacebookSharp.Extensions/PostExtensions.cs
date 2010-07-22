namespace FacebookSharp.Extensions
{
    using System.Collections.Generic;
    using FacebookSharp.Schemas.Graph;

    public static partial class FacebookExtensions
    {
        #region Post specifics

        /// <summary>
        /// Get the posts for the specified facebook id (application, page or user).
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns Json string.</returns>
        /// <remarks>
        /// type of id  | returns
        /// ------------|------------------------
        ///  Application| The applications's own posts.
        ///  Page       | The page's own posts.
        ///  User       | The user's own posts. Requires the read_stream permission to see non-public posts.
        /// </remarks>
        public static string GetPostAsJson(this Facebook facebook, string id, IDictionary<string, string> parameters)
        {
            return facebook.GetConnections(id, "posts", parameters);
        }

        /// <summary>
        /// Get the posts for the specified facebook id (application, page or user).
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        /// <returns>
        /// Returns PostCollection.
        /// </returns>
        /// <remarks>
        /// type of id  | returns
        /// ------------|------------------------
        ///  Application| The applications's own posts.
        ///  Page       | The page's own posts.
        ///  User       | The user's own posts. Requires the read_stream permission to see non-public posts.
        /// </remarks>
        public static PostCollection GetPosts(this Facebook facebook, string id, IDictionary<string, string> parameters)
        {
            return FacebookUtils.DeserializeObject<PostCollection>(facebook.GetPostAsJson(id, parameters));
        }

        #endregion

    }
}