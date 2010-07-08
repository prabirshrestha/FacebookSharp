namespace FacebookSharp
{
    public class QueryParseException : FacebookException
    {
        public QueryParseException(string message)
            : base(message, "QueryParseException", 0)
        {
        }
    }
}