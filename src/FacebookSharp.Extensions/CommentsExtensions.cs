
namespace FacebookSharp.Extensions
{
    using System.Collections.Generic;
    using FacebookSharp.Schemas.Graph;

    public static partial class FacebookExtensions
    {
#if !(SILVERLIGHT || WINDOWS_PHONE)
        /// <summary>
        /// Writes the given comment on the given post.
        /// </summary>
        /// <param name="facebook">The facebook.</param>
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
        /// <param name="parameters">The parameters.</param>
        /// <returns>Returns the id of the newly posted comment.</returns>
        public static string PostComment(this Facebook facebook, string objectId, IDictionary<string, string> parameters)
        {
            var result = facebook.PutObject(objectId, "comments", parameters);
            return FacebookUtils.FromJson(result)["id"].ToString();
        }

        /// <summary>
        /// Gets list of comments for the specified facebook id (album,link,note,photo,post,status message and video).
        /// </summary>
        /// <param name="facebook">The facebook.</param>
        /// <param name="objectId">Id of the Facebook Graph Object.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Returns list of comments</returns>
        /// <remarks>
        /// type of id      | returns
        /// ----------------|--------------------------------------
        ///  Album          | The comments made on this album.
        ///  Link           | All of the comments on this link.
        ///  Note           | All of the comments on this note
        ///  Photo          | All of the comments on this photo.
        ///  Post           | All of the comments on this post.
        ///  Status Message | All of the comments on this message.
        ///  Video          | All of the comments on this video.
        /// </remarks>
        public static CommentCollection GetComments(this Facebook facebook, string objectId, IDictionary<string, string> parameters)
        {
            return facebook.GetConnections<CommentCollection>(objectId, "comments", parameters);
        }

        /// <summary>
        /// Gets list of comments for the specified facebook id (album,link,note,photo,post,status message and video).
        /// </summary>
        /// <param name="facebook">The facebook.</param>
        /// <param name="objectId">Id of the Facebook Graph Object.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="until">The until.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Returns list of comments</returns>
        /// <remarks>
        /// type of id      | returns
        /// ----------------|--------------------------------------
        ///  Album          | The comments made on this album.
        ///  Link           | All of the comments on this link.
        ///  Note           | All of the comments on this note
        ///  Photo          | All of the comments on this photo.
        ///  Post           | All of the comments on this post.
        ///  Status Message | All of the comments on this message.
        ///  Video          | All of the comments on this video.
        /// </remarks>
        public static CommentCollection GetComments(this Facebook facebook, string objectId, int? limit, int? offset, string until, IDictionary<string, string> parameters)
        {
            parameters = AppendPagingParameters(parameters, limit, offset, until);
            return facebook.GetComments(objectId, parameters);
        }

#endif
    }
}