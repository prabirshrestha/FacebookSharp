using System.Reflection;
using FacebookSharp.Schemas.Graph;

namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static partial class FacebookUtils
    {

        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        /// <summary>
        /// Convert <see cref="PictureSizeType"/> to facebook string.
        /// </summary>
        /// <param name="pictureSizeType">
        /// The picture size type.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static string ToString(PictureSizeType pictureSizeType)
        {
            switch (pictureSizeType)
            {
                case PictureSizeType.Square:
                    return "square";
                case PictureSizeType.Small:
                    return "small";
                case PictureSizeType.Large:
                    return "large";
                default:
                    throw new ArgumentOutOfRangeException("pictureSizeType");
            }
        }

        #region Json Converter Utils

        /// <summary>
        /// Parse Json string to IDictionary&lt;string, object>.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IDictionary<string, object> FromJson(string json)
        {
            return JsonParser.FromJson(json);
        }

        /// <summary>
        /// Parse Json string to IDictionary&lt;string, object>. 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="throwFacebookException"></param>
        /// <returns></returns>
        public static IDictionary<string, object> FromJson(string json, bool throwFacebookException)
        {
            IDictionary<string, object> jsonBag = FromJson(json);

            FacebookException ex = ToFacebookException(json);

            if (!throwFacebookException)    // i think sometimes, it shouldn't throw error,
                return jsonBag;             // rather user should have more control over the behavior.

            if (ex != null)
                throw ex;

            return jsonBag;
        }


        /// <summary>
        /// Parse IDictionary&lt;string, object> to json string.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string ToJson(IDictionary<string, object> bag)
        {
            return JsonParser.ToJson(bag);
        }


        #endregion

        /// <summary>
        /// Parse a server response into a JSON object.
        /// </summary>
        /// <param name="response">String representation of the response.</param>
        /// <returns>Returns the response as a JSON object.</returns>
        /// <remarks>
        /// This is a basic implementation using Newtonsoft.JSON.
        /// 
        /// The parsed JSON is checked for a variety of error fields and
        /// a <see cref="FacebookException"/> is thrown if an error condition is set,
        /// populated with the error message and error type or code if available. 
        /// </remarks>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("use FromJson method instead.")]
        public static JToken ParseJson(string response)
        {
            return ParseJson(response, true); // this is the default behavior in facebook android sdk.
        }

        /// <summary>
        /// Parse a server response into a JSON object.
        /// </summary>
        /// <param name="response">String representation of the response.</param>
        /// <param name="throwException">If set to true, then it converts to FacebookException and throws error, else returns JToken always.</param>
        /// <returns>Returns the response as a JSON object.</returns>
        /// <remarks>
        /// This is a basic implementation using Newtonsoft.JSON.
        /// 
        /// The parsed JSON is checked for a variety of error fields and
        /// a <see cref="FacebookException"/> is thrown if an error condition is set,
        /// populated with the error message and error type or code if available. 
        /// </remarks>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("use FromJson method instead.")]
        public static JToken ParseJson(string response, bool throwException)
        {
            JToken json;

            FacebookException ex = ToFacebookException(response, out json);

            if (!throwException) // i think sometimes, it shouldn't throw error,
                return json;     // rather user should have more control over the behavior.

            if (ex != null)
                throw ex;

            return json;
        }

        /// <summary>
        /// Deserializes the specified object to JSON object.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="json">The object to deserialize.</param>
        /// <returns>Deserialized object.</returns>
        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Serializes the specified object ot JSON string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized json string.</returns>
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}