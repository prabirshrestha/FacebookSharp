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

        public static string GetObjects(this Facebook facebook, IDictionary<string, string> parameters, params  string[] ids)
        {
            StringBuilder joinedIds = new StringBuilder();
            for (int i = 0; i < ids.Length; i++)
            {
                if (i == 0)
                    joinedIds.Append(ids[i]);
                else
                    joinedIds.AppendFormat(",{0}", ids[i]);
            }

            if (parameters == null)
                parameters = new Dictionary<string, string>();
            parameters["ids"] = joinedIds.ToString();

            return facebook.Request(null, parameters);
        }
    }
}