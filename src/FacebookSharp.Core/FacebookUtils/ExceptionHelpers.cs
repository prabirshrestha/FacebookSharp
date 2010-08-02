namespace FacebookSharp
{

    public static partial class FacebookUtils
    {
        /// <summary>
        /// Throws an exception if the json string contains facebook exception.
        /// </summary>
        /// <param name="jsonString">Json string</param>
        public static void ThrowIfFacebookException(string jsonString)
        {
            var ex = (FacebookException)jsonString;
            if (ex != null)
                throw ex;
        }

    }
}
