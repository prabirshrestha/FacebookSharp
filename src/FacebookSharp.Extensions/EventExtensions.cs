
namespace FacebookSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using FacebookSharp.Schemas;
    using FacebookSharp.Schemas.Graph;

    public static partial class FacebookExtensions
    {
#if !(SILVERLIGHT || WINDOWS_PHONE)
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
        
        public static string CreateEvent(this Facebook facebook, Event @event, IDictionary<string, string> parameters)
        {
            IDictionary<string, string> pars = parameters != null
                                                  ? new Dictionary<string, string>(parameters)
                                                  : new Dictionary<string, string>();

            pars.Add("name", @event.Name);
            pars.Add("location", @event.Location);
            pars.Add("start_time", @event.StartTime);
            pars.Add("end_time", @event.EndTime);

            var result = facebook.Post("/events", pars);
            return FacebookUtils.FromJson(result)["id"].ToString();
        }

        /// <summary>
        /// Create event for page.
        /// </summary>
        /// <param name="facebook">
        /// The facebook.
        /// </param>
        /// <param name="pageId">
        /// The page id.
        /// </param>
        /// <param name="event">
        /// The event.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// Page ID of the newly created event.
        /// </returns>
        /// <remarks>
        /// The access token must be of user not of page.
        /// </remarks>
        public static string CreateEventForPage(this Facebook facebook, string pageId, Event @event, IDictionary<string, string> parameters)
        {
            IDictionary<string, string> pars = parameters != null
                                                   ? new Dictionary<string, string>(parameters)
                                                   : new Dictionary<string, string>();

            pars.Add("page_id", pageId);

            return facebook.CreateEvent(@event, pars);
        }

        ///// <summary>
        ///// Create a page event using the old rest api.
        ///// </summary>
        ///// <param name="facebook">
        ///// The facebook.
        ///// </param>
        ///// <param name="pageId">
        ///// The page id.
        ///// </param>
        ///// <param name="eventName">
        ///// The event name.
        ///// </param>
        ///// <param name="startTime">
        ///// The start time.
        ///// </param>
        ///// <param name="eventInfoParameters">
        ///// Optional event info parameters.
        ///// </param>
        ///// <param name="parameters">
        ///// The parameters.
        ///// </param>
        ///// <returns>
        ///// Returns the ID of the newly created event.
        ///// </returns>
        ///// <remarks>
        ///// http://developers.facebook.com/docs/reference/rest/events.create
        ///// </remarks>
        //[Obsolete(Facebook.OldRestApiWarningMessage)]
        //public static string CreateEventForPage(this Facebook facebook, string pageId, string eventName, long startTime, IDictionary<string, object> eventInfoParameters, IDictionary<string, string> parameters)
        //{
        //    IDictionary<string, string> pars = parameters != null
        //                                           ? new Dictionary<string, string>(parameters)
        //                                           : new Dictionary<string, string>();

        //    var eventInfo = new Dictionary<string, object>
        //                        {
        //                            { "name", eventName },
        //                            { "start_time", startTime },
        //                            { "page_id", pageId }
        //                        };

        //    if (eventInfoParameters != null)
        //    {
        //        foreach (var eventInfoParameter in eventInfoParameters)
        //            eventInfo.Add(eventInfoParameter.Key, eventInfoParameter.Value);
        //    }

        //    pars.Add("event_info", FacebookUtils.SerializeObject(eventInfo));

        //    return facebook.GetUsingRestApi("events.create", pars);
        //}

        ///// <summary>
        ///// Create a page event using the old rest api.
        ///// </summary>
        ///// <param name="facebook">
        ///// The facebook.
        ///// </param>
        ///// <param name="pageId">
        ///// The page id.
        ///// </param>
        ///// <param name="eventName">
        ///// The event name.
        ///// </param>
        ///// <param name="startTime">
        ///// The start time.
        ///// </param>
        ///// <param name="eventInfoParameters">
        ///// The event info parameters.
        ///// </param>
        ///// <param name="parameters">
        ///// The parameters.
        ///// </param>
        ///// <returns>
        ///// Returns the ID of the newly created event.
        ///// </returns>
        ///// <remarks>
        ///// http://developers.facebook.com/docs/reference/rest/events.create
        ///// </remarks>
        //[Obsolete(Facebook.OldRestApiWarningMessage)]
        //public static string CreateEventForPage(this Facebook facebook, string pageId, string eventName, DateTime startTime, IDictionary<string, object> eventInfoParameters, IDictionary<string, string> parameters)
        //{
        //    return facebook.CreateEventForPage(pageId, eventName, startTime.ToUnixTimestamp(), eventInfoParameters, parameters);
        //}
#endif
    }
}