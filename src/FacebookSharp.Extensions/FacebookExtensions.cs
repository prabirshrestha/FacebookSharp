// Most of the codes of this FacebookExtensions.cs file is taken from
// http://hernan.amiune.com/labs/facebook/c-sharp-facebook-graph-api-library.html
//
//
// Copyright 2010 Hernan Amiune (hernan.amiune.com)
// Licensed under MIT license:
// http://www.opensource.org/licenses/mit-license.php
// 
// Requires Newtonsoft.Json.Linq.JObject
// http://james.newtonking.com/projects/json-net.aspx
// Based on the Official Python client library for the Facebook Platform
// http://github.com/facebook/python-sdk/
// 
// C# client library for the Facebook Platform
// 
// This client library is designed to support the Graph API and the official
// Facebook JavaScript SDK, which is the canonical way to implement
// Facebook authentication. Read more about the Graph API at
// http://developers.facebook.com/docs/api. You can download the Facebook
// JavaScript SDK at http://github.com/facebook/connect-js/.

namespace FacebookSharp.Extensions
{
    using System.Collections.Generic;
    using System.Text;

    public static partial class FacebookExtensions
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
        /// Fetches the connections for given object.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id">Id of the object to fetch.</param>
        /// <param name="connectionName">Name of the connection.</param>
        /// <param name="parameters">List of arguments.</param>
        /// <returns>Returns the connections.</returns>
        public static T GetConnections<T>(this Facebook facebook, string id, string connectionName, IDictionary<string, string> parameters)
        {
            var jsonString = GetConnections(facebook, id, connectionName, parameters);
            FacebookUtils.ThrowIfFacebookException(jsonString);
            return FacebookUtils.DeserializeObject<T>(jsonString);
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
        public static string PutWallPost(this Facebook facebook, string message, IDictionary<string, string> parameters)
        {
            if (parameters != null)
                parameters.Add("message", "message");
            return facebook.PutObject("me", "feed", parameters);
        }

        /// <summary>
        /// Writes a wall post to the given profile's wall.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="message">The message to put on the wall.</param>
        /// <param name="parameters">Optional parameters for the message.</param>
        /// <param name="profileId">The profile where the message is goin to be put.</param>
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
        public static string PutWallPost(this Facebook facebook, string message, IDictionary<string, string> parameters, string profileId)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();
            parameters.Add("message", message);
            return facebook.PutObject(profileId, "feed", parameters);
        }

        /// <summary>
        /// Writes the given comment on the given post.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="objectId">Id of the object.</param>
        /// <param name="message">Comment Message.</param>
        /// <returns>Result of the operation.</returns>
        public static string PutComment(this Facebook facebook, string objectId, string message)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("message", message);
            return facebook.PutObject(objectId, "comments", parameters);
        }

        /// <summary>
        /// Likes the given post.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="objectId">Id of the object to like.</param>
        /// <returns>Result of the operation.</returns>
        public static string PutLike(this Facebook facebook, string objectId)
        {
            return facebook.PutObject(objectId, "likes", null);
        }

        /// <summary>
        /// Deletes the object with the given ID from the graph.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id">Id of the object to delete.</param>
        /// <returns>Result of the operation.</returns>
        public static string DeleteObject(this Facebook facebook, string id)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "delete");
            return facebook.Request(id, parameters, "POST");
        }

    }
}