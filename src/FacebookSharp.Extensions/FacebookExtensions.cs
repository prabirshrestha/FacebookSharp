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
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using FacebookSharp.Schemas.Graph;
    using RestSharp;

    public static partial class FacebookExtensions
    {
        /// <summary>
        /// Fetches the give object from the graph.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id">Id of the object to fetch.</param>
        /// <param name="parameters">List of parameters.</param>
        /// <returns>The facebook graph object.</returns>
        public static string GetObject(this Facebook facebook, string id, IDictionary<string, string> parameters)
        {
            return facebook.Get("/" + id, parameters);
        }

        /// <summary>
        /// Fetches the give object from the graph.
        /// </summary>
        /// <param name="facebook">The facebook.</param>
        /// <param name="id">Id of the object to fetch.</param>
        /// <param name="parameters">List of parameters.</param>
        /// <typeparam name="T">Type of Object to deserialize to.</typeparam>
        /// <returns>Deserialized Facebook graph object.</returns>
        public static T GetObject<T>(this Facebook facebook, string id, IDictionary<string, string> parameters)
             where T : new()
        {
            return facebook.Get<T>("/" + id, parameters);
        }

        /// <summary>
        /// Fetches all of the give object from the graph.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="parameters">List of arguments.</param>
        /// <param name="ids">Ids of the objects to return.</param>
        /// <returns>A map from ID to object. If any of the IDs are invalid, an exception is raised.</returns>
        public static string GetObjects(this Facebook facebook, IDictionary<string, string> parameters,
                                        params string[] ids)
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

            return facebook.Get(null, parameters);
        }

        /// <summary>
        /// Fetches the connections for given object.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id">Id of the object to fetch.</param>
        /// <param name="connectionName">Name of the connection.</param>
        /// <param name="parameters">List of arguments.</param>
        /// <returns>Returns the connections.</returns>
        public static string GetConnections(this Facebook facebook, string id, string connectionName,
                                            IDictionary<string, string> parameters)
        {
            return facebook.Get("/" + id + "/" + connectionName, parameters);
        }

        /// <summary>
        /// Fetches the connections for given object.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="id">Id of the object to fetch.</param>
        /// <param name="connectionName">Name of the connection.</param>
        /// <param name="parameters">List of arguments.</param>
        /// <returns>Returns the connections.</returns>
        public static T GetConnections<T>(this Facebook facebook, string id, string connectionName,
                                          IDictionary<string, string> parameters)
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
        public static string PutObject(this Facebook facebook, string parentObject, string connectionName,
                                       IDictionary<string, string> parameters)
        {
            if (!facebook.IsSessionValid())
                throw new FacebookSharpException("AccessToken required.");
            return facebook.Post("/" + parentObject + "/" + connectionName, parameters);
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
        public static string PostToWall(this Facebook facebook, string message, IDictionary<string, string> parameters)
        {
            if (parameters != null)
                parameters.Add("message", "message");
            return facebook.PutObject("/me", "feed", parameters);
        }

        [Obsolete("This method is marked for deletion in future releases. Use PostToWall.")]
        public static string PutWallPost(this Facebook facebook, string message, IDictionary<string, string> parameters)
        {
            return facebook.PostToWall(message, parameters);
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
        public static string PostToWall(this Facebook facebook, string message, IDictionary<string, string> parameters,
                                        string profileId)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();
            parameters.Add("message", message);
            return facebook.PutObject(profileId, "feed", parameters);
        }

        [Obsolete("This method is marked for deletion in future releases. Use PostToWall.")]
        public static string PutWallPost(this Facebook facebook, string message, IDictionary<string, string> parameters,
                                         string profileId)
        {
            return facebook.PostToWall(message, parameters, profileId);
        }

        /// <summary>
        /// Writes the given comment on the given post.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="objectId">Id of the object.</param>
        /// <param name="message">Comment Message.</param>
        /// <returns>Result of the operation.</returns>
        [Obsolete("This method is marked for deletion in future releases. Use PostComment.")]
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
            return facebook.Delete("/" + id);
        }

        /// <summary>
        /// Gets the Facebook user id and the name who likes the specified Facebook object.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// Returns collection of Facebook users' id along with names.
        /// </returns>
        /// <remarks>
        ///     Key: Facebook User Id
        ///     Value: Facebook Username
        /// </remarks>
        public static BasicUserInfoCollection GetLikes(this Facebook facebook, string id)
        {
            var likes = facebook.Get<BasicUserInfoCollection>("/" + id + "/likes") ?? new BasicUserInfoCollection();

            if (likes.Data == null)
                likes.Data = new List<BasicUserInfo>();

            return likes;
        }

        /// <summary>
        /// Gets profile url. ex: http://www.facebook.com/profile.php?id=123456
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// Returns the generated facebook profile url
        /// </returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
            "SA1632:DocumentationTextMustMeetMinimumCharacterLength",
            Justification = "Reviewed. Suppression is OK here.")]
        public static string GetProfileUrl(this Facebook facebook, string id)
        {
            return "http://www.facebook.com/profile.php?id=" + id;
        }

        /// <summary>
        /// Gets the profile picture url for the specified id.
        /// </summary>
        /// <param name="facebook">Id of the object to retrieve profile picture for.</param>
        /// <param name="id"></param>
        /// <returns>Url of the picture.</returns>
        /// <remarks>
        /// For security reasons, id cannot be passed as 'me'.
        /// As this will be most probably used in website directly in the <img/> tag,
        /// and passing the access token is quite risky out here.
        /// incase you do want to use it, use GetMyProfilePictureUrl() instead,
        /// this might be useful in desktop apps.
        /// </remarks>
        public static string GetProfilePictureUrl(this Facebook facebook, string id)
        {
            if (id.Equals("me", StringComparison.OrdinalIgnoreCase))
                throw new FacebookSharpException("For security reason passing 'me' as id is not allowed.");
            return string.Format("{0}/{1}/picture", Facebook.GraphBaseUrl, id);
        }

        /// <summary>
        /// Gets the profile picture url for the specified id. Passing the access token.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="profileId">
        /// The profile id.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="FacebookException">
        /// </exception>
        /// <exception cref="FacebookRequestException">
        /// </exception>
        /// <remarks>
        /// This is useful in web pages, if you don't want to show the access token on the client side,
        /// or html page.
        /// </remarks>
        public static string GetProfilePictureUrlSafe(this Facebook facebook, string profileId)
        {
            return facebook.GetProfilePictureUrlSafe(profileId, null);
        }

#if !SILVERLIGHT

        /// <summary>
        /// Gets the profile picture url for the specified id. Passing the access token.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="pictureSizeType">
        /// The picture size type.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="FacebookException">
        /// </exception>
        /// <exception cref="FacebookRequestException">
        /// </exception>
        /// <remarks>
        /// This is useful in web pages, if you don't want to show the access token on the client side,
        /// or html page.
        /// </remarks>
        public static string GetProfilePictureUrlSafe(this Facebook facebook, string profileId, PictureSizeType? pictureSizeType)
        {
            var request = new RestRequest("/" + profileId + "/picture", Method.GET);
            if (pictureSizeType != null)
                request.AddParameter("type", FacebookUtils.ToString(pictureSizeType.Value));

            var response = facebook.Execute(request, true);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                var fbException = (FacebookException)response.Content;
                if (fbException != null)
                    throw fbException;

                return response.ResponseUri.ToString();
            }

            throw new FacebookRequestException(response);
        }

#endif

        /// <summary>
        /// Gets the profile picture url for the current facebook user.
        /// </summary>
        /// <param name="facebook"></param>
        /// <returns>Url of the profile picture.</returns>
        /// <remarks>
        /// Try using GetProfilePictureUrl(id) as far as possible for security reasons.
        /// </remarks>
        public static string GetMyProfilePictureUrl(this Facebook facebook)
        {
            if (string.IsNullOrEmpty(facebook.Settings.AccessToken))
                throw new ArgumentNullException("Settings.AccessToken",
                                                "AccessToken must be specified inorder to invoke this method");
            return string.Format("{0}/me/picture?{1}={2}", Facebook.GraphBaseUrl, Facebook.Token,
                                 facebook.Settings.AccessToken);
        }

        private static void AssertRequireAccessToken(Facebook facebook)
        {
            if (string.IsNullOrEmpty(facebook.Settings.AccessToken))
                throw new ArgumentNullException("Settings.AccessToken",
                                                "AccessToken must be specified inorder to invoke this method");
        }

        /// <summary>
        /// Helper method to add optional parameters for paging.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <param name="limit">
        /// The limit.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <param name="until">
        /// The until.
        /// </param>
        /// <returns>
        /// </returns>
        private static IDictionary<string, string> AppendPagingParameters(IDictionary<string, string> parameters,
                                                                          int? limit, int? offset, string until)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();

            if (limit != null)
                parameters.LimitTo(limit.Value);
            if (offset != null)
                parameters.Offset(offset.Value);
            if (!string.IsNullOrEmpty(until))
                parameters.Until(until);

            return parameters;
        }

        /// <summary>
        /// Converts to specified <see cref="DateTime"/> to ISO-8601 format (yyyy-MM-ddTHH:mm:ssZ).
        /// </summary>
        /// <param name="dateTime">
        /// The date time.
        /// </param>
        /// <returns>
        /// Returns the string representation of date time in ISO-8601 format (yyyy-MM-ddTHH:mm:ssZ).
        /// </returns>
        /// <remarks>
        /// You might want to checkout http://frugalcoder.us/post/2010/01/07/EcmaScript-5s-Date-Extensions.aspx
        /// and http://webreflection.blogspot.com/2009/07/ecmascript-iso-date-for-every-browser.html
        /// for Javascript ISO-8601 dates.
        /// </remarks>
        public static string ToIso8601FormattedDateTime(this DateTime dateTime)
        {
            return FacebookUtils.Date.ToIso8601FormattedDateTime(dateTime);
        }

        /// <summary>
        /// Converts ISO-8601 format (yyyy-MM-ddTHH:mm:ssZ) date time to <see cref="DateTime"/>.
        /// </summary>
        /// <param name="iso8601DateTime">
        /// The iso 8601 formatted date time.
        /// </param>
        /// <returns>
        /// Returns the <see cref="DateTime"/> equivalent to the ISO-8601 formatted date time. 
        /// </returns>
        /// <remarks>
        /// You might want to checkout http://frugalcoder.us/post/2010/01/07/EcmaScript-5s-Date-Extensions.aspx
        /// and http://webreflection.blogspot.com/2009/07/ecmascript-iso-date-for-every-browser.html
        /// for Javascript ISO-8601 dates.
        /// </remarks>
        public static DateTime FromIso8601FormattedDateTime(this string iso8601DateTime)
        {
            return FacebookUtils.Date.FromIso8601FormattedDateTime(iso8601DateTime);
        }

        /// <summary>
        /// Get Unix Timestamp for the specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">Date Time.</param>
        /// <returns>Returns Unix Timestamp.</returns>
        public static double ToUnixTimestamp(this DateTime date)
        {
            return FacebookUtils.Date.ToUnixTimestamp(date);
        }

        /// <summary>
        /// Get <see cref="DateTime"/> from the specified UnixTimestamp.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns>Returns <see cref="DateTime"/>.</returns>
        public static DateTime FromUnixTimestamp(this long timestamp)
        {
            return FacebookUtils.Date.FromUnixTimestamp(timestamp);
        }
    }
}