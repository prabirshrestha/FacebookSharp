using System.Collections.Generic;
using System.Text;

namespace FacebookSharp
{
    public static class FacebookExtensions
    {
        /// <summary>
        /// Fetches the give object from the graph.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id">Id of the object to fetch.</param>
        /// <param name="parameters">List of arguments.</param>
        /// <returns>The required object</returns>
        public static string GetObject(this Facebook facebook, string id, IDictionary<string, string> parameters)
        {
            return facebook.Request(id, parameters);
        }

        /// <summary>
        /// Fetches all of the give object from the graph.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="parameters">List of arguments.</param>
        /// <param name="ids">Ids of the objects to return.</param>
        /// <returns>A map from ID to object. If any of the IDs are invalid, an exception is raised.</returns>
        public static string GetObjects(this Facebook facebook, IDictionary<string, string> parameters, params  string[] ids)
        {
            StringBuilder joinedIds = new StringBuilder();
            for (int i = 0; i < ids.Length; i++)
            {
                if (i == 0)
                    joinedIds.Append(ids[i]);
                else
                    joinedIds.AppendFormat(",{0}", ids[i]);
            }

            if (parameters == null)
                parameters = new Dictionary<string, string>();
            parameters["ids"] = joinedIds.ToString();

            return facebook.Request(null, parameters);
        }

        /// <summary>
        /// Fetches the connections for given object.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id">Id of the object to fetch.</param>
        /// <param name="connectionName">Name of the connection.</param>
        /// <param name="parameters">List of arguments.</param>
        /// <returns>Returns the connections.</returns>
        public static string GetConnections(this Facebook facebook, string id, string connectionName, IDictionary<string, string> parameters)
        {
            return facebook.Request(id + "/" + connectionName, parameters);
        }

        /// <summary>
        /// Writes the give object to the graph, connected to the give parent.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="parentObject">The parent object.</param>
        /// <param name="connectionName">Name of the connection.</param>
        /// <param name="parameters">List of arguments.</param>
        /// <returns>Result of operation</returns>
        /// <remarks>
        /// For example,
        /// 
        ///     var data = new Dictionary<string, string>();
        ///     data.Add("message", "Hello, world");
        ///     facebook.PutObject("me", "feed", data);
        /// 
        /// writes "Hello, world" to the active user's wall.
        /// 
        /// See http://developers.facebook.com/docs/api#publishing for all of
        /// the supported writeable objects.
        /// 
        /// Most write operations require extended permissions. For example,
        /// publishing wall posts requires the "publish_stream" permission. See
        /// http://developers.facebook.com/docs/authentication/ for details about
        /// extended permissions.
        /// </remarks>
        public static string PutObject(this Facebook facebook, string parentObject, string connectionName, IDictionary<string, string> parameters)
        {
            if (!facebook.IsSessionValid())
                throw new FacebookSharpException("AccessToken required.");
            return facebook.Request(parentObject + "/" + connectionName, parameters, "POST");
        }

        /// <summary>
        /// Writes a wall post to current user wall.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="message">The message to put on the wall.</param>
        /// <param name="parameters">Optional parameters for the message.</param>
        /// <returns>Result of the operation.</returns>
        /// <remarks>
        /// Default to writing to the authenticated user's wall if no
        /// profile_id is specified.
        /// 
        /// attachment adds a structured attachment to the status message being
        /// posted to the Wall. It should be a dictionary of the form:
        /// 
        ///     {"name": "Link name"
        ///      "link": "http://www.example.com/",
        ///      "caption": "{*actor*} posted a new review",
        ///      "description": "This is a longer description of the attachment",
        ///      "picture": "http://www.example.com/thumbnail.jpg"}
        /// </remarks>
        public static string PutWallPost(Facebook facebook, string message, IDictionary<string, string> parameters)
        {
            if (parameters != null)
                parameters.Add("message", "message");
            return facebook.PutObject("me", "feed", parameters);
        }
    }
}