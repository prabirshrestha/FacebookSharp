
namespace FacebookSharp
{
    public abstract class GraphObject
    {
        public string ID { get; set; }

        public Metadata Metadata { get; set; }

        public string Paging { get; set; }
    }
}