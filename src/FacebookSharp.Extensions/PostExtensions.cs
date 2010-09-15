namespace FacebookSharp.Extensions
{
    using System.Collections.Generic;
    using FacebookSharp.Schemas.Graph;

    public static partial class FacebookExtensions
    {
#if !(SILVERLIGHT || WINDOWS_PHONE)
        #region Post specifics

        /// <summary>
        /// Gets the specified post by id.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// </returns>
        public static string GetPostAsJson(this Facebook facebook, string id, IDictionary<string, string> parameters)
        {
            return facebook.GetObject(id, parameters);
        }

        /// <summary>
        /// Gets the specified post by id.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="postId">
        /// The id.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// </returns>
        public static Post GetPost(this Facebook facebook, string postId, IDictionary<string, string> parameters)
        {
            var json = facebook.GetPostAsJson(postId, parameters);

            // Facebook seemed to have changed the api on 9/15/2010. small hack to DeserializeObject properly.
            json = json.Replace("\"comments\":[]", "\"comments\":{data:null,count:0}");

            return FacebookUtils.DeserializeObject<Post>(json);
        }

        /// <summary>
        /// Gets the post with the specified id.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="postId">
        /// The post id.
        /// </param>
        /// <returns>
        /// </returns>
        public static Post GetPost(this Facebook facebook, string postId)
        {
            return facebook.GetPost(postId, null);
        }

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
        public static string GetPostsAsJson(this Facebook facebook, string id, IDictionary<string, string> parameters)
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
            var json = facebook.GetPostsAsJson(id, parameters);

            // Facebook seemed to have changed the api on 9/15/2010. small hack to DeserializeObject properly.
            json = json.Replace("\"comments\":[]", "\"comments\":{data:null,count:0}");

            return FacebookUtils.DeserializeObject<PostCollection>(json);
        }

        /// <summary>
        /// Get the posts for the specified facebook id (application, page or user).
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="until"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns PostCollection.</returns>
        /// <remarks>
        /// type of id  | returns
        /// ------------|------------------------
        ///  Application| The applications's own posts.
        ///  Page       | The page's own posts.
        ///  User       | The user's own posts. Requires the read_stream permission to see non-public posts.
        /// </remarks>
        public static PostCollection GetPosts(this Facebook facebook, string id, int? limit, int? offset, string until, IDictionary<string, string> parameters)
        {
            parameters = AppendPagingParameters(parameters, limit, offset, until);
            return facebook.GetPosts(id, parameters);
        }

        #endregion
#endif
    }
}