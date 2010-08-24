
using System.Text;

namespace FacebookSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using FacebookSharp.Schemas;
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

        [Obsolete(Facebook.OldRestApiWarningMessage)]
        public static string GetEvents(this Facebook facebook, string userId, string[] eventIds, int? startTime, int? endTime, RsvpStatus? rsvpStatus, IDictionary<string, string> parameters)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(userId))
                parameters.Add("uid", userId);

            var eids = FacebookUtils.ToCommaSeperatedValues(eventIds);

            if (!string.IsNullOrEmpty(eids))
                parameters.Add("eids", eids);

            if (startTime != null)
                parameters.Add("start_time", startTime.Value.ToString());

            if (endTime != null)
                parameters.Add("end_time", endTime.Value.ToString());

            if (rsvpStatus != null)
                parameters.Add("rsvp_status", FacebookUtils.ToString(rsvpStatus.Value));

            var result = facebook.GetUsingRestApi("events.get", parameters);

            return result;
        }

        // note: there's a problem with facebook graph api, so till then will just comment this.
        ///// <summary>
        ///// Creates a new event.
        ///// </summary>
        ///// <param name="facebook">
        ///// The facebook.
        ///// </param>
        ///// <param name="event">
        ///// The event.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //public static string CreateEvent(this Facebook facebook, Event @event)
        //{
        //    return facebook.CreateEvent(@event, null);
        //}

        //public static string CreateEvent(this Facebook facebook, Event @event, IDictionary<string, string> parameters)
        //{
        //    var p = parameters ?? new Dictionary<string, string>();

        //    p.Add("name", @event.Name);
        //    p.Add("location", @event.Location);
        //    p.Add("start_time", @event.StartTime);
        //    p.Add("end_time", @event.EndTime);

        //    var result = facebook.Post("/events", p);
        //    return FacebookUtils.FromJson(result)["id"].ToString();
        //}
    }
}