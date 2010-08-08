using System.Security.Cryptography;
using System.Text;

namespace FacebookSharp
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	
	// todo: convert time properties to something other than strings...
	public abstract class FacebookPostCallback
	{
		public List<int> LinkedAccountIds { get; private set; } // fb_sig_linked_account_id
		public int UserId { get; private set; } // fb_sig_user
		public string RequestedAt { get; private set; } // fb_sig_time
		public string ApiKey { get; private set; } // fb_sig_api_key
		
		public FacebookPostCallback(NameValueCollection vars)
		{
			ApiKey = vars["fb_sig_api_key"];
			RequestedAt = vars["fb_sig_time"];
			UserId = Convert.ToInt32(vars["fb_sig_user"]);
			LinkedAccountIds = (List<int>)FacebookUtils.FromJson(vars["fb_sig_linked_account_id"])["array"];
		}
		
		public class FacebookPostRemovalCallback : FacebookPostCallback
		{			
			public bool Blocked { get; private set; } // fb_sig_blocked == 1
			public bool RemovedByUser { get; private set; } // fb_sig_added == 0
			public bool RemovedByAdmin { get; private set; } // fb_sig_page_added == 0
			
			public FacebookPostRemovalCallback(NameValueCollection vars) : base(vars)
			{
				Blocked = Convert.ToBoolean(vars["fb_sig_blocked"]);
				if (vars["fb_sig_added"] != null)
				{
					RemovedByUser = true;
					RemovedByAdmin = false;
				}
				else if (vars["fb_sig_page_added"] != null)
				{
					RemovedByAdmin = true;
					RemovedByUser = false;
				}
			}
		}
		
		public class FacebookPostAuthorizeCallback : FacebookPostCallback
		{
			public string ProfileUpdatedAt { get; private set; } // fb_sig_profile_update_time
			public string SessionKey { get; private set; } // fb_sig_session_key
			public int ExpireTime { get; private set; } // fb_sig_expires
			
			public FacebookPostAuthorizeCallback(NameValueCollection vars) : base(vars)
			{
				SessionKey = vars["fb_sig_session_key"];
				ProfileUpdatedAt = vars["fb_sig_profile_update_time"];
				ExpireTime = Convert.ToInt32(vars["fb_sig_expires"]);
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
		public static bool ValidateSignature(NameValueCollection variables, string applicationSecret)
		{
			string sig = "";
			string[] sorted_keys = variables.AllKeys;
			Array.Sort(sorted_keys);
			foreach (string field in sorted_keys)
			{
				if (field.Substring(0,7).Equals("fb_sig_"))
					sig += field.Substring(7) + "=" + variables[field];
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
        /// <param name="post_variables">
        /// The post variables from Request.Form
        /// </param>
        /// <param name="applicationSecret">
        /// The application secret.
        /// </param>
        /// <returns>
        /// Returns a FacebookPostCallback object if validation passes, else null.
        /// </returns>
		public static FacebookPostCallback Parse(NameValueCollection post_variables, string applicationSecret)
		{
			if (ValidateSignature(post_variables,applicationSecret))
			{
				if (post_variables.GetValues("fb_sig_authorize")[0] == "1")
					return new FacebookPostAuthorizeCallback(post_variables);
				else if (post_variables.GetValues("fb_sig_uninstall")[0] == "1")
					return new FacebookPostRemovalCallback(post_variables);
			}
			return null;
		}
	}
}

