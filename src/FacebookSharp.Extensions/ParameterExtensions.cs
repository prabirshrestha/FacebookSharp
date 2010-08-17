namespace FacebookSharp.Extensions
{
    using System.Collections.Generic;

    public static partial class FacebookExtensions
    {
        /// <summary>
        /// Adds 'limit' parameter.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://developers.facebook.com/docs/api#paging
        /// 
        /// https://graph.facebook.com/me/likes?limit=3
        /// </remarks>
        public static IDictionary<string, string> LimitTo(this IDictionary<string, string> parameters, int limit)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            return parameters;
        }

        /// <summary>
        /// Adds 'offset' parameter.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// http://developers.facebook.com/docs/api#paging
        /// 
        /// https://graph.facebook.com/me/likes?offset=3
        /// </remarks>
        public static IDictionary<string, string> Offset(this IDictionary<string, string> parameters, int offset)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();
            parameters.Add("offset", offset.ToString());
            return parameters;
        }

        public static IDictionary<string, string> Until(this IDictionary<string, string> parameters, string until)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();
            parameters.Add("until", until);
            return parameters;
        }
    }
}