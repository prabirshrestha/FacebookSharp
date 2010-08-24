namespace FacebookSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static partial class FacebookExtensions
    {
        /// <summary>
        /// Check if the user likes a Facebook object.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="userId">Id of the user (cannot be me())</param>
        /// <param name="objectId"></param>
        /// <returns>
        /// Returns true if the user likes the facebook object, otherwise false.
        /// </returns>
        [Obsolete(Facebook.OldRestApiWarningMessage)]
        public static bool UserLikes(this Facebook facebook, string userId, string objectId)
        {
            AssertRequireAccessToken(facebook);

            if (userId.Equals("me()", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("User ID cannot be me().", userId);

            var result = facebook.Query(
                string.Format("SELECT user_id FROM like WHERE user_id={0} AND object_id={1}",
                              userId, objectId));

            var jsonObj = FacebookUtils.FromJson(result);

            if (jsonObj.Count == 1)
            {
                var users = (IDictionary<string, object>)jsonObj["object"];
                if (users.Count == 1)
                {
                    if (users["user_id"].ToString().Equals(userId, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks who likes the objects.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="objectIds">
        /// List of object ids.
        /// </param>
        /// <returns>
        /// List of object ids that are liked by the user.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        [Obsolete(Facebook.OldRestApiWarningMessage)]
        public static string[] UserLikes(this Facebook facebook, string userId, string[] objectIds)
        {
            AssertRequireAccessToken(facebook);

            if (objectIds == null)
                throw new ArgumentNullException("userIds");

            if (userId.Equals("me()", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("User ID cannot be me()", "userId");

            if (objectIds.Length == 0)
                return new string[] { }; // return empty array of string. no need to query facebook.

            var query = new StringBuilder();

            query.AppendFormat("SELECT object_id FROM like WHERE user_id={0} and (", userId);

            var i = 0;
            foreach (var objectId in objectIds)
            {
                if (i != 0)
                    query.Append(" OR ");

                if (objectId.Equals("me()", StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Object ID cannot contain me()", "objectIds");

                query.AppendFormat("object_id={0}", objectId);
                ++i;
            }

            query.Append(")");

            var result = facebook.Query(query.ToString());

            // json returned by fb in not a valid json, so we need to do some ugly hack before can use FacebookUtils.FromJson
            if (result.StartsWith("["))
            {
                result = result.Substring(1, result.Length - 2);

                var objects = result.Split(',');

                var likedObjects = new List<string>(objectIds.Length);

                foreach (var obj in objects)
                {
                    var jsonObj = FacebookUtils.FromJson(obj);
                    likedObjects.Add(jsonObj["object_id"].ToString());
                }

                return likedObjects.ToArray();
            }

            return new string[] { };
        }


        /// <summary>
        /// Check who likes the object.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="userIds">
        /// List of user ids to check. (Id of the user cannot be me())
        /// </param>
        /// <param name="objectId">
        /// The object id.
        /// </param>
        /// <returns>
        /// Returns list of users who likes the object.
        /// </returns>
        [Obsolete(Facebook.OldRestApiWarningMessage)]
        public static string[] UsersLike(this Facebook facebook, string[] userIds, string objectId)
        {
            AssertRequireAccessToken(facebook);

            if (userIds == null)
                throw new ArgumentNullException("userIds");

            if (userIds.Length == 0)
                return new string[] { }; // return empty array of string. no need to query facebook.

            var query = new StringBuilder();

            query.AppendFormat("SELECT user_id FROM like WHERE object_id={0} and (", objectId);

            var i = 0;
            foreach (var userId in userIds)
            {
                if (i != 0)
                    query.Append(" OR ");

                if (userId.Equals("me()", StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("User ID cannot contain me()", "userIds");

                query.AppendFormat("user_id={0}", userId);
                ++i;
            }

            query.Append(")");

            var result = facebook.Query(query.ToString());

            // json returned by fb in not a valid json, so we need to do some ugly hack before can use FacebookUtils.FromJson
            if (result.StartsWith("["))
            {
                result = result.Substring(1, result.Length - 2);

                var users = result.Split(',');

                var likedUsers = new List<string>(userIds.Length);

                foreach (var user in users)
                {
                    var jsonObj = FacebookUtils.FromJson(user);
                    likedUsers.Add(jsonObj["user_id"].ToString());
                }

                return likedUsers.ToArray();
            }

            return new string[] { };
        }

    }
}