
namespace FacebookSharp
{
    using System.Web.Security;
    using MySql.Data.MySqlClient;

    /// <remarks>
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
        private readonly MembershipProvider _membershipProvider;
        private readonly string _connectionString;

        public MySqlFacebookMembershipProvider(string connectionString)
            : this(connectionString, "facebook_users", null)
        {
        }

        public MySqlFacebookMembershipProvider(string connectionString, string tableName)
            : this(connectionString, tableName, null)
        {
        }

        public MySqlFacebookMembershipProvider(string connectionString, string tableName, MembershipProvider membershipProvider)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _membershipProvider = membershipProvider;
        }

        #region Implementation of IFacebookMembershipProvider

        public bool HasLinkedFacebook(string membershipUsername)
        {
            using (MySqlConnection cn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd =
                    new MySqlCommand(string.Format("SELECT COUNT(*) FROM {0} WHERE user_name=@user_name", _tableName), cn);
                cmd.Parameters.AddWithValue("@user_name", membershipUsername);
                cn.Open();

                return (long)cmd.ExecuteScalar() == 1;
            }
        }

        public bool HasLinkedFacebook(object membershipProviderUserKey)
        {
            MembershipUser user = _membershipProvider.GetUser(membershipProviderUserKey, false);
            if (user == null)
                throw new FacebookSharpException("User with given membershipProviderUserKey not found.");
            return HasLinkedFacebook(user.UserName);
        }

        public bool IsFacebookUserLinked(string facebookId)
        {
            using (MySqlConnection cn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd =
                    new MySqlCommand(string.Format("SELECT COUNT(*) FROM {0} WHERE facebook_id=@facebook_id", _tableName), cn);
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

        public void LinkFacebook(string membershipUsername, string facebookId, string accessToken, int expiresIn)
        {
            // todo: add expires in
            LinkFacebook(membershipUsername, facebookId, accessToken);
        }

        public void LinkFacebook(object membershipProviderUserKey, string facebookId, string accessToken)
        {
            MembershipUser user = _membershipProvider.GetUser(membershipProviderUserKey, false);
            if (user == null)
                throw new FacebookSharpException("User with given membershipProviderUserKey not found.");

            LinkFacebook(user.UserName, facebookId, accessToken);
        }

        public void LinkFacebook(object membershipProviderUserKey, string facebookId, string accessToken, int expiresIn)
        {
            // todo: add expires in
            LinkFacebook(membershipProviderUserKey, facebookId, accessToken);
        }

        public void UnlinkFacebook(string membershipUsername)
        {
            using (MySqlConnection cn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(
                    string.Format("DELETE FROM {0} WHERE user_name=@user_name", _tableName), cn);
                cmd.Parameters.AddWithValue("@user_name", membershipUsername);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UnlinkFacebook(object membershipProviderUserKey)
        {
            MembershipUser user = _membershipProvider.GetUser(membershipProviderUserKey, false);
            if (user == null)
                throw new FacebookSharpException("User with given membershipProviderUserKey not found.");

            UnlinkFacebook(user.UserName);
        }

        public void UnlinkFacebookByFacebookId(string facebookId)
        {
            using (MySqlConnection cn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(
                    string.Format("DELETE FROM {0} WHERE facebook_id=@facebook_id", _tableName), cn);
                cmd.Parameters.AddWithValue("@facebook_id", facebookId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public string GetFacebookAccessToken(string membershipUsername)
        {
            using (MySqlConnection cn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd =
                    new MySqlCommand(
                        string.Format("SELECT access_token FROM {0} WHERE user_name=@user_name", _tableName), cn);
                cmd.Parameters.AddWithValue("@user_name", membershipUsername);

                cn.Open();
                var result = cmd.ExecuteScalar();
                return result == null ? "" : result.ToString();
            }
        }

        public string GetFacebookAccessToken(object membershipProviderUserKey)
        {
            MembershipUser user = _membershipProvider.GetUser(membershipProviderUserKey, false);
            if (user == null)
                throw new FacebookSharpException("User with given membershipProviderUserKey not found.");
            return GetFacebookAccessToken(user.UserName);
        }

        public string GetFacebookAccessTokenByFacebookId(string facebookId)
        {
            using (MySqlConnection cn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd =
                    new MySqlCommand(
                        string.Format("SELECT access_token FROM {0} WHERE facebookId=@facebookId", _tableName), cn);
                cmd.Parameters.AddWithValue("@facebookId", facebookId);

                cn.Open();
                var result = cmd.ExecuteScalar();
                return result == null ? "" : result.ToString();
            }
        }

        public string GetFacebookId(string membershipUsername)
        {
            using (MySqlConnection cn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd =
                    new MySqlCommand(
                        string.Format("SELECT facebook_id FROM {0} WHERE user_name=@user_name", _tableName), cn);
                cmd.Parameters.AddWithValue("@user_name", membershipUsername);

                cn.Open();
                return cmd.ExecuteScalar().ToString();
            }
        }

        public string GetFacebookId(object membershipProviderUserKey)
        {
            MembershipUser user = _membershipProvider.GetUser(membershipProviderUserKey, false);
            if (user == null)
                throw new FacebookSharpException("User with given membershipProviderUserKey not found.");
            return GetFacebookId(user.UserName);
        }

        #endregion
    }
}
