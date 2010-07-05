
namespace FacebookSharp
{
    public abstract class GraphObject
    {
        public Metadata Metadata { get; set; }

        public Paging Paging { get; set; }
    }
}