namespace FacebookSharp
{
    public class Event : GraphObject
    {
        public EventOwner Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
        public string Updated_Time { get; set; }
        public string Location { get; set; }
        public string Privacy { get; set; }
        public Location Venue { get; set; }
    }

    public class EventCollection : Connection<Event>
    {

    }
}