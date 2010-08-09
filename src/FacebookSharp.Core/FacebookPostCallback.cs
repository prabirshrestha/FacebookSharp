#if !SILVERLIGHT
namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

    // REFERENCE: http://github.com/rnewman/clj-facebook/blob/master/AUTH
    // ATTENTION CANVAS USERS!
    // Do not use this class for your callbacks!
    // Use FacebookAuthenticationResult.ValidateSignedRequest (assuming OAuth 2.0 beta is enabled in app migrations)

    // todo: convert time properties to something other than strings...
    public abstract class FacebookPostCallback
    {
        public List<string> LinkedAccountIds { get; private set; } // fb_sig_linked_account_id
        public string UserId { get; private set; } // fb_sig_user
        public bool UsingNewFacebook { get; private set; } // fb_sig_in_new_facebook
        public string RequestedAt { get; private set; } // fb_sig_time
        public string ApiKey { get; private set; } // fb_sig_api_key
        public string AppId { get; private set; } // fb_sig_app_id
        public string Locale { get; private set; } // fb_sig_locale
        public string Country { get; private set; } // fb_sig_country

        protected FacebookPostCallback(IDictionary<string, string> vars)
        {
            ApiKey = vars["fb_sig_api_key"];
            AppId = vars["fb_sig_app_id"];
            RequestedAt = vars["fb_sig_time"];
            UserId = vars["fb_sig_user"];
            LinkedAccountIds = (List<string>)FacebookUtils.FromJson(vars["fb_sig_linked_account_id"])["array"];
            UsingNewFacebook = Convert.ToBoolean(vars["fb_sig_in_new_facebook"]);
            Country = vars["fb_sig_country"];
        }

        public class Authorize : FacebookPostCallback
        {
            public string ProfileUpdatedAt { get; private set; } // fb_sig_profile_update_time
            public string SessionKey { get; private set; } // fb_sig_session_key
            public string SessionSecret { get; private set; } // fb_sig_ss
            public int ExpireTime { get; private set; } // fb_sig_expires
            public bool OfflineAccess { get { return (ExpireTime == 0); } } // fb_sig_expires == 0
            public string[] ExtendedPermissions { get; private set; } // fb_sig_ext_perms
            public string CookieSignature { get; private set; } // fb_sig_cookie_sig

            internal Authorize(IDictionary<string, string> vars)
                : base(vars)
            {
                SessionKey = vars["fb_sig_session_key"];
                SessionSecret = vars["fb_sig_ss"];
                ProfileUpdatedAt = vars["fb_sig_profile_update_time"];
                ExpireTime = Convert.ToInt32(vars["fb_sig_expires"]);
                ExtendedPermissions = vars["fb_sig_ext_perms"].Split(',');
                CookieSignature = vars["fb_sig_cookie_sig"];
            }
        }

        public class Removal : FacebookPostCallback
        {
            public bool Blocked { get; private set; } // fb_sig_blocked == 1
            public bool RemovedByUser { get; private set; } // fb_sig_added == 0
            public bool RemovedByAdmin { get; private set; } // fb_sig_page_added == 0

            internal Removal(IDictionary<string, string> vars)
                : base(vars)
            {
                Blocked = Convert.ToBoolean(vars["fb_sig_blocked"]);
                if (vars.ContainsKey("fb_sig_added"))
                {
                    RemovedByUser = true;
                    RemovedByAdmin = false;
                }
                else if (vars.ContainsKey("fb_sig_page_added"))
                {
                    RemovedByAdmin = true;
                    RemovedByUser = false;
                }
            }
        }

        /// <summary>
        /// Validate incoming POST request using application secret
        /// </summary>
        /// <param name="variables">
        /// The post variables in dictionary format
        /// </param>
        /// <param name="applicationSecret">
        /// The application secret.
        /// </param>
        /// <returns>
        /// Returns true if validation passes, else false.
        /// </returns>
        public static bool ValidateSignature(IDictionary<string, string> variables, string applicationSecret)
        {
            string sig = "";
            SortedDictionary<string, string> sorted_variables = new SortedDictionary<string, string>(variables);
            foreach (KeyValuePair<string, string> param in sorted_variables)
            {
                if (param.Key.Substring(0, 7).Equals("fb_sig_"))
                    sig += param.Key.Substring(7) + "=" + param.Value;
            }

            sig += applicationSecret;
            MD5 md5Hasher = MD5.Create();
            string expected_sig = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(sig)).ToString();
            if (expected_sig.Equals(variables["fb_sig"]))
                return true;
            return false;
        }

        /// <summary>
        /// Parse incoming POST request
        /// </summary>
        /// <param name="post_data">
        /// The raw post data from Request.Form
        /// </param>
        /// <param name="applicationSecret">
        /// The application secret.
        /// </param>
        /// <returns>
        /// Returns a FacebookPostCallback object if validation passes, else null.
        /// </returns>
        public static FacebookPostCallback Parse(string post_data, string applicationSecret)
        {
            var post_vars = FacebookUtils.ParseUrlQueryString(post_data);
            IDictionary<string, string> post_variables = new Dictionary<string, string>();
            foreach (var p in post_vars)
            {
                post_variables.Add(p.Key, p.Value[0].ToString());
            }
            if (ValidateSignature(post_variables, applicationSecret))
            {
                if (post_variables.ContainsKey("fb_sig_authorize"))
                    return new Authorize(post_variables);
                else if (post_variables.ContainsKey("fb_sig_uninstall"))
                    return new Removal(post_variables);
            }
            return null;
        }
    }
}
#endif