namespace FacebookSharp
{
    using System;

    public class FacebookAsyncResult
    {
        public FacebookAsyncResult(string result, Exception exception)
        {
            Exception = exception;
            Response = result;
        }

        public Exception Exception { get; private set; }

        public string Response { get; private set; }

        public T GetResponseAs<T>()
        {
            return FacebookUtils.DeserializeObject<T>(Response);
        }

        public bool IsSuccessful { get { return Exception != null; } }
    }
}