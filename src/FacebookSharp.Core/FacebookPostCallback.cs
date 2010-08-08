using System.Security.Cryptography;
using System.Text;

namespace FacebookSharp
{
	using System;
	using System.Collections.Generic;
	
	// todo: convert time properties to something other than strings...
	public abstract class FacebookPostCallback
	{
		public List<string> LinkedAccountIds { get; private set; } // fb_sig_linked_account_id
		public string UserId { get; private set; } // fb_sig_user
		public string RequestedAt { get; private set; } // fb_sig_time
		public string ApiKey { get; private set; } // fb_sig_api_key
		
		protected FacebookPostCallback(IDictionary<string, string> vars)
		{
			ApiKey = vars["fb_sig_api_key"];
			RequestedAt = vars["fb_sig_time"];
			UserId = vars["fb_sig_user"];
			LinkedAccountIds = (List<string>)FacebookUtils.FromJson(vars["fb_sig_linked_account_id"])["array"];
		}
		
		public class FacebookPostAuthorizeCallback : FacebookPostCallback
		{
			public string ProfileUpdatedAt { get; private set; } // fb_sig_profile_update_time
			public string SessionKey { get; private set; } // fb_sig_session_key
			public int ExpireTime { get; private set; } // fb_sig_expires
			
			internal FacebookPostAuthorizeCallback(IDictionary<string, string> vars) : base(vars)
			{
				SessionKey = vars["fb_sig_session_key"];
				ProfileUpdatedAt = vars["fb_sig_profile_update_time"];
				ExpireTime = Convert.ToInt32(vars["fb_sig_expires"]);
			}
		}
	
		public class FacebookPostRemovalCallback : FacebookPostCallback
		{			
			public bool Blocked { get; private set; } // fb_sig_blocked == 1
			public bool RemovedByUser { get; private set; } // fb_sig_added == 0
			public bool RemovedByAdmin { get; private set; } // fb_sig_page_added == 0
			
			internal FacebookPostRemovalCallback(IDictionary<string, string> vars) : base(vars)
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
        /// <param name="post_variables">
        /// The post variables from Request.Form
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
				if (param.Key.Substring(0,7).Equals("fb_sig_"))
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
                post_variables.Add(p.Key, p.Value[0]);
            }
			if (ValidateSignature(post_variables,applicationSecret))
			{
				if (post_variables.ContainsKey("fb_sig_authorize"))
					return new FacebookPostAuthorizeCallback(post_variables);
				else if (post_variables.ContainsKey("fb_sig_uninstall"))
					return new FacebookPostRemovalCallback(post_variables);
			}
			return null;
		}
	}
}

