using System;
using MySql.Data.MySqlClient;
using System.Web.Security;

namespace FacebookSharp.MySqlProvider
{

    /// <remarks>
    /// 
    /// Table structure for MySqlFacebookMembershipProvider
    ///     CREATE TABLE `facebook_users` (
    ///		  `user_name` VARCHAR(60), -- membershipUsername, primary key already enforced as unique and not null
    ///		  `facebook_id` VARCHAR(50) NOT NULL UNIQUE,
    ///		  `access_token` VARCHAR(256),
    ///		  PRIMARY KEY (`user_name`)
    ///		);
    /// </remarks>
    public class MySqlFacebookMembershipProvider : IFacebookMembershipProvider
    {
        private readonly string _tableName;
        private readonly string _connectionString;

        public MySqlFacebookMembershipProvider(string connectionString)
            : this(connectionString, "facebook_users")
        {
        }

        public MySqlFacebookMembershipProvider(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }

        #region Implementation of IFacebookMembershipProvider

        public bool HasLinkedFacebook(string membershipUsername)
        {
            using (MySqlConnection cn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd =
                    new MySqlCommand(string.Format("SELECT COUNT(*) FROM {0} WHERE user_name=@user_name", _tableName));
                cmd.Parameters.AddWithValue("@user_name", membershipUsername);
                cn.Open();

                return (long)cmd.ExecuteScalar() == 1;
            }
        }

        public bool HasLinkedFacebook(object membershipProviderUserKey)
        {
            MembershipUser user = Membership.GetUser(membershipProviderUserKey);
            if (user == null)
                throw new FacebookSharpException("User with given membershipProviderUserKey not found.");
            return HasLinkedFacebook(user.UserName);
        }

        public bool IsFacebookUserLinked(string facebookId)
        {
            using (MySqlConnection cn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd =
                    new MySqlCommand(string.Format("SELECT COUNT(*) FROM {0} WHERE facebook_id=@facebook_id", _tableName));
                cmd.Parameters.AddWithValue("@facebook_id", facebookId);
                cn.Open();

                return (long)cmd.ExecuteScalar() == 1;
            }
        }

        public void LinkFacebook(string membershipUsername, string facebookId, string accessToken)
        {
            using (MySqlConnection cn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd =
                    new MySqlCommand(
                        string.Format(
                            "INSERT INTO {0} (user_name,facebook_id,access_token) VALUES (@user_name,@facebook_id,@access_token)",
                            _tableName), cn);
                cmd.Parameters.AddWithValue("@user_name", membershipUsername);
                cmd.Parameters.AddWithValue("@facebook_id", facebookId);
                cmd.Parameters.AddWithValue("@access_token", accessToken);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void LinkFacebook(object membershipProviderUserKey, string facebookId, string accessToken)
        {
            MembershipUser user = Membership.GetUser(membershipProviderUserKey);
            if (user == null)
                throw new FacebookSharpException("User with given membershipProviderUserKey not found.");

            LinkFacebook(user.UserName, facebookId, accessToken);
        }

        public void UnlinkFacebook(string membershipUsername)
        {
            throw new NotImplementedException();
        }

        public void UnlinkFacebook(object membershipProviderUserKey)
        {
            throw new NotImplementedException();
        }

        public void UnlinkFacebookByFacebookId(string facebookId)
        {
            throw new NotImplementedException();
        }

        public string GetFacebookAccessToken(string membershipUsername)
        {
            throw new NotImplementedException();
        }

        public string GetFacebookAccessToken(object membershipProviderUserKey)
        {
            throw new NotImplementedException();
        }

        public string GetFacebookAccessTokenByFacebookId(string facebookId)
        {
            throw new NotImplementedException();
        }

        public string GetFacebookId(string membershipUsername)
        {
            throw new NotImplementedException();
        }

        public string GetFacebookId(object membershipProviderUserKey)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
