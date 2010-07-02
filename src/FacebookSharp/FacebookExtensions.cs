using System.Collections.Generic;
using System.Text;

namespace FacebookSharp
{
    public static class FacebookExtensions
    {
        public static string GetObject(this Facebook facebook, string id, IDictionary<string, string> parameters)
        {
            return facebook.Request(id, parameters);
        }

       
    }
}