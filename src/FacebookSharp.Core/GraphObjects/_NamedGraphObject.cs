namespace FacebookSharp
{
    public abstract class NamedGraphObject : GraphObject
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}