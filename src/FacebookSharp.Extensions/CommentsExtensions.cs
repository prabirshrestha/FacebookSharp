namespace FacebookSharp.Extensions
{
    using System.Collections.Generic;

    public static partial class FacebookExtensions
    {
        /// <summary>
        /// Writes the given comment on the given post.
        /// </summary>
        /// <param name="facebook"></param>
        /// <param name="objectId">Id of the object.</param>
        /// <param name="message">Comment Message.</param>
        /// <returns>Returns the id of the newly posted comment.</returns>
        public static string PostComment(this Facebook facebook, string objectId, string message)
        {
            return facebook.PostComment(objectId, new Dictionary<string, string> { { "message", message } });
        }

        /// <summary>
        /// Writes the given comment on the given post.    
        /// </summary>
        /// <param name="facebook">The facebook.</param>
        /// <param name="objectId">Id of the object.</param>
        /// <param name="parameters"> The parameters.</param>
        /// <returns>Returns the id of the newly posted comment.</returns>
        public static string PostComment(this Facebook facebook, string objectId, IDictionary<string, string> parameters)
        {
            var result = facebook.PutObject(objectId, "comments", parameters);
            return FacebookUtils.FromJson(result)["id"].ToString();
        }
    }
}