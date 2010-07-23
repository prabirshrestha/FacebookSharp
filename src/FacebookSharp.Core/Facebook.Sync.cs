namespace FacebookSharp
{
    using RestSharp;

    public partial class Facebook
    {
#if !SILVERLIGHT
        private RestResponse Execute(RestRequest request)
        {
            var client = new RestClient();
            client.BaseUrl = GraphBaseUrl;
            return client.Execute(request);
        }

        private RestResponse<T> Execute<T>(RestRequest request)
            where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = GraphBaseUrl;
            return client.Execute<T>(request);
        }
#endif
    }
}