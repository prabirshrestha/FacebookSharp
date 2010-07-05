using System.Collections.Generic;

namespace FacebookSharp
{
    public abstract class Connection<T> : GraphObject
        where T : GraphObject
    {
        public List<T> Data { get; set; }
    }
}