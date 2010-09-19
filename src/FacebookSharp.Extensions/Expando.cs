#if !(DOTNET_3_5 || WINDOWS_PHONE)

namespace FacebookSharp.Extensions.Dynamic
{
    using System.Collections.Generic;
    using System.Dynamic;

    public static class Expando
    {
        /// <summary>
        /// Converts Json string to ExpandoObject
        /// </summary>
        /// <param name="json">The json string.</param>
        /// <returns>Expando Object</returns>
        public static ExpandoObject ToExpandoObject(this string json)
        {
            return FacebookUtils.ToExpandoObject(FacebookUtils.FromJson(json, false));
        }

        /// <summary>
        /// Converts Dictionary to ExpandoObject.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>Expando Object</returns>
        public static ExpandoObject ToExpandoObject(this IDictionary<string, object> dictionary)
        {
            return FacebookUtils.ToExpandoObject(dictionary);
        }
    }
}

#endif