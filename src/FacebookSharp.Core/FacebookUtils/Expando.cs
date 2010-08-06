#if !DOTNET_3_5

namespace FacebookSharp
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;

    public static partial class FacebookUtils
    {
        public static ExpandoObject ToExpandoObject(IDictionary<string, object> dictionary)
        {
            var expando = new ExpandoObject();
            var expandoDic = (IDictionary<string, object>)expando;

            foreach (var item in dictionary)
            {
                bool alreadyProcessed = false;

                if (item.Value is IDictionary<string, object>)
                {
                    expandoDic.Add(item.Key, ToExpandoObject((IDictionary<string, object>)item.Value));
                    alreadyProcessed = true;
                }
                else if (item.Value is ICollection)
                {
                    var itemList = new List<object>();
                    foreach (var item2 in (ICollection)item.Value)
                        if (item2 is IDictionary<string, object>)
                            itemList.Add(ToExpandoObject((IDictionary<string, object>)item2));
                        else
                            itemList.Add(ToExpandoObject(new Dictionary<string, object> { { "Unknown", item2 } }));

                    if (itemList.Count > 0)
                    {
                        expandoDic.Add(item.Key, itemList);
                        alreadyProcessed = true;
                    }
                }

                if (!alreadyProcessed)
                    expandoDic.Add(item);
            }

            return expando;
        }
    }
}

#endif