namespace FacebookSharp
{
    using System.Collections.Generic;

    public partial class Facebook
    {

#if !(SILVERLIGHT || WINDOWS_PHONE)  // NOR logic !(A+B) : if its running on desktop version

        /// <summary>
        /// Query Facebook using Facebook Query Language
        /// </summary>
        /// <param name="query">Query (must not be url encoded.)</param>
        /// <param name="parameters">Parameters to add.</param>
        /// <param name="addAccessToken">Whether to add accesstoken or not</param>
        /// <returns>Returns JSON result.</returns>
        public string Query(string query, IDictionary<string, string> parameters, bool addAccessToken)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();

            parameters.Add("query", query);

            return GetUsingRestApi("fql.query", parameters, addAccessToken);
        }

        /// <summary>
        /// Query Facebook using Facebook Query Language
        /// </summary>
        /// <param name="query">Query (must not be url encoded.)</param>
        /// <param name="parameters">Parameters to add.</param>
        /// <returns>Returns JSON result.</returns>
        public string Query(string query, IDictionary<string, string> parameters)
        {
            return Query(query, parameters, true);
        }

        /// <summary>
        /// Query Facebook using Facebook Query Language
        /// </summary>
        /// <param name="query">Query (must not be url encoded.)</param>
        /// <returns>Returns JSON result.</returns>
        public string Query(string query)
        {
            return Query(query, null);
        }

#endif

    }
}