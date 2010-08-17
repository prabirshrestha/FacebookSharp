namespace FacebookSharp.Extensions
{
    using System.Collections.Generic;
    using System.Text;

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

        /// <summary>
        /// Add's fields parameter, if fields already exisits appends it.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="fieldNames">The field names.</param>
        /// <returns>
        /// </returns>
        public static IDictionary<string, string> SelectFields(this IDictionary<string, string> parameters, string[] fieldNames)
        {
            if (fieldNames == null || fieldNames.Length == 0) // don't do anything
                return parameters;

            if (parameters == null)
                parameters = new Dictionary<string, string>();

            StringBuilder oldFields;

            if (parameters.ContainsKey("fields"))
            {
                oldFields = new StringBuilder(parameters["fields"]);
                parameters.Remove("fields");
            }
            else
                oldFields = new StringBuilder();

            if (oldFields.Length > 0)
                oldFields.Append(',');

            foreach (var field in fieldNames)
            {
                oldFields.Append(field);
                oldFields.Append(',');
            }

            oldFields.Remove(oldFields.Length - 1, 1); // remove the last comma

            parameters.Add("fields", oldFields.ToString());

            return parameters;
        }

        /// <summary>
        /// Add's fields parameter, if fields already exisits appends it.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="fieldName">The field name.</param>
        /// <returns>
        /// </returns>
        public static IDictionary<string, string> SelectField(this IDictionary<string, string> parameters, string fieldName)
        {
            return parameters.SelectFields(new[] { fieldName });
        }
    }
}