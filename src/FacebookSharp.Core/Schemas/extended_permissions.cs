using System;

namespace FacebookSharp.Schemas
{
    [Flags]
    public enum extended_permissions : long
    {
        #region Publishing Permissions
        publish_stream = 1,
        create_event = 2,
        rsvp_event = 4,
        sms = 8,
        offline_access = 16,
        #endregion

        #region  Page Permissions
        manage_pages = 32,
        #endregion

        #region Data Permissions
        email = 64,

        read_insights = 128,

        read_stream = 256,

        read_mailbox = 512,

        ads_management = 1024,

        xmpp_login = 2048,

        user_about_me = 4096,
        friends_activities = 8192,

        user_birthday = 16384,
        friends_birthday = 32768,

        user_education_history = 65536,
        friends_education_history = 131072,

        user_events = 262144,
        friends_events = 524288,

        user_groups = 1048576,
        friends_groups = 2097152,

        user_hometown = 4194304,
        friends_hometown = 8388608,

        user_interests = 16777216,
        friends_interests = 33554432,

        user_likes = 67108864,
        friends_likes = 134217728,

        user_location = 268435456,
        friends_location = 536870912,

        user_notes = 1073741824,
        friends_notes = 2147483648,

        user_online_presense = 4294967296,
        friends_online_presense = 8589934592,

        user_photo_video_tags = 17179869184,
        friends_photo_video_tags = 34359738368,

        user_photos = 68719476736,
        friends_photos = 137438953472,

        user_relationships = 274877906944,
        friends_relationships = 549755813888,

        user_religion_politics = 1099511627776,
        friends_religion_politics = 2199023255552,

        user_status = 4398046511104,
        friends_status = 8796093022208,

        user_videos = 17592186044416,
        friends_videos = 35184372088832,

        user_website = 70368744177664,
        friends_website = 140737488355328,

        user_work_history = 281474976710656,
        friends_work_history = 562949953421312,

        read_friendslists = 1125899906842624,

        read_requests = 2251799813685248

        #endregion


    }
}