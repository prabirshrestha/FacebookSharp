namespace FacebookSharp.Extensions
{
    using System.Collections.Generic;
    using FacebookSharp.Schemas.Graph;

    public static partial class FacebookExtensions
    {
#if !(SILVERLIGHT || WINDOWS_PHONE)
        #region Post specifics

        /// <summary>
        /// Get the feeds for the specified facebook id (application ,event, group, page or user).
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        /// <returns>Returns Json string.</returns>
        /// <remarks>
        /// type of id  | returns
        /// ------------|------------------------
        ///  Application| The application's wall.
        ///  Event      | The event's wall.
        ///  Group      | The group's wall.
        ///  Page       | The page's wall.
        ///  User       | The user's wall. Requires the read_stream permission to see non-public posts.
        /// </remarks>
        public static string GetFeedsAsJson(this Facebook facebook, string id, IDictionary<string, string> parameters)
        {
            return facebook.GetConnections(id, "feed", parameters);
        }

        /// <summary>
        /// Get the feeds for the specified facebook id (application ,event, group, page or user).
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        /// <returns>
        /// Returns PostCollection.
        /// </returns>
        /// <remarks>
        /// type of id  | returns
        /// ------------|-------------------
        ///  Application| The application's wall.
        ///  Event      | The event's wall.
        ///  Group      | The group's wall.
        ///  Page       | The page's wall.
        ///  User       | The user's wall. Requires the read_stream permission to see non-public posts.
        /// </remarks>
        public static PostCollection GetFeeds(this Facebook facebook, string id, IDictionary<string, string> parameters)
        {
            return FacebookUtils.DeserializeObject<PostCollection>(facebook.GetFeedsAsJson(id, parameters));
        }

        /// <summary>
        /// Get the feeds for the specified facebook id (application ,event, group, page or user).
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
        /// ------------|-------------------
        ///  Application| The application's wall.
        ///  Event      | The event's wall.
        ///  Group      | The group's wall.
        ///  Page       | The page's wall.
        ///  User       | The user's wall. Requires the read_stream permission to see non-public posts.
        /// </remarks>
        public static PostCollection GetFeeds(this Facebook facebook, string id, int? limit, int? offset, string until, IDictionary<string, string> parameters)
        {
            parameters = AppendPagingParameters(parameters, limit, offset, until);

            return facebook.GetFeeds(id, parameters);
        }

        #endregion
#endif
    }
}