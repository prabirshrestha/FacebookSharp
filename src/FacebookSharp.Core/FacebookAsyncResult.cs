namespace FacebookSharp
{
    using System;

    public class FacebookAsyncResult
    {
        public FacebookAsyncResult(string result, Exception exception)
        {
            Exception = exception;
            RawResponse = result;
        }

        public Exception Exception { get; private set; }

        public string RawResponse { get; private set; }

        public T GetResponseAs<T>()
        {
            return FacebookUtils.DeserializeObject<T>(RawResponse);
        }

        public bool IsSuccessful
        {
            get { return Exception == null; }
        }
    }
}