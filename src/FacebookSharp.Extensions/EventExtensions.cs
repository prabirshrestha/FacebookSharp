
namespace FacebookSharp.Extensions
{
    using System.Collections.Generic;
    using FacebookSharp.Schemas.Graph;

    public static partial class FacebookExtensions
    {
        /// <summary>
        /// Gets the specified event as plain json string.
        /// </summary>
        /// <param name="facebook">The facebook.</param>
        /// <param name="eventId">The event id.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Json string representation of the event.
        /// </returns>
        public static string GetEventAsJson(this Facebook facebook, string eventId, IDictionary<string, string> parameters)
        {
            return facebook.GetObject(eventId, parameters);
        }

        /// <summary>
        /// Gets the specified event.
        /// </summary>
        /// <param name="facebook">The facebook.</param>
        /// <param name="eventId">The event id.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Returns the facebook event.
        /// </returns>
        public static Event GetEvent(this Facebook facebook, string eventId, IDictionary<string, string> parameters)
        {
            return facebook.GetObject<Event>(eventId, parameters);
        }

        /// <summary>
        /// Gets the specified event.
        /// </summary>
        /// <param name="facebook">The facebook.</param>
        /// <param name="eventId">The event id.</param>
        /// <returns>
        /// Returns the facebook event.
        /// </returns>
        public static Event GetEvent(this Facebook facebook, string eventId)
        {
            return facebook.GetEvent(eventId, null);
        }
    }
}