namespace FacebookSharp
{
    using System.Data.SqlClient;
    using System.Web.Security;

    /// <remarks>
    /// CREATE TABLE [FacebookUsers](
    ///     [Username] VARCHAR(60) -- membershipUsername, primary key already enforced as unique and not null
    ///     [FacebookId] VARCHAR(50) NOT NULL UNIQUE,
    ///     [AccessToken] VARCHAR(256),
    ///     PRIMARY KEY ([Username])
    /// );
    /// </remarks>
    public class SqlFacebookMembershipProvider : IFacebookMembershipProvider
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public SqlFacebookMembershipProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlFacebookMembershipProvider(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }

        #region Implementation of IFacebookMembershipProvider

        public bool HasLinkedFacebook(string membershipUsername)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand(string.Format("SELECT COUNT(*) FROM {0} WHERE Username=@Username", _tableName), cn);
                cmd.Parameters.AddWithValue("@Username", membershipUsername);
                cn.Open();

                return (int)cmd.ExecuteScalar() == 1;
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
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand(string.Format("SELECT COUNT(*) FROM {0} WHERE FacebookId=@FacebookId", _tableName), cn);
                cmd.Parameters.AddWithValue("@FacebookId", facebookId);
                cn.Open();

                return (int)cmd.ExecuteScalar() == 1;
            }
        }

        public void LinkFacebook(string membershipUsername, string facebookId, string accessToken)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand(
                        string.Format(
                            "INSERT INTO {0} (Username,FacebookId,AccessToken) VALUES (@Username,@FacebookId,@AccessToken)",
                            _tableName), cn);
                cmd.Parameters.AddWithValue("@Username", membershipUsername);
                cmd.Parameters.AddWithValue("@FacebookId", facebookId);
                cmd.Parameters.AddWithValue("@AccessToken", accessToken);

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
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    string.Format("DELETE FROM {0} WHERE Username=@Username", _tableName), cn);
                cmd.Parameters.AddWithValue("@Username", membershipUsername);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UnlinkFacebook(object membershipProviderUserKey)
        {
            MembershipUser user = Membership.GetUser(membershipProviderUserKey);
            if (user == null)
                throw new FacebookSharpException("User with given membershipProviderUserKey not found.");

            UnlinkFacebook(user.UserName);
        }

        public void UnlinkFacebookByFacebookId(string facebookId)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    string.Format("DELETE FROM {0} WHERE FacebookId=@FacebookId", _tableName), cn);
                cmd.Parameters.AddWithValue("@FacebookId", facebookId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public string GetFacebookAccessToken(string membershipUsername)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand(
                        string.Format("SELECT AccessToken FROM {0} WHERE Username=@Username", _tableName), cn);
                cmd.Parameters.AddWithValue("@user_Usernamename", membershipUsername);

                cn.Open();
                var result = cmd.ExecuteScalar();
                return result == null ? "" : result.ToString();
            }
        }

        public string GetFacebookAccessToken(object membershipProviderUserKey)
        {
            MembershipUser user = Membership.GetUser(membershipProviderUserKey);
            if (user == null)
                throw new FacebookSharpException("User with given membershipProviderUserKey not found.");
            return GetFacebookAccessToken(user.UserName);
        }

        public string GetFacebookAccessTokenByFacebookId(string facebookId)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand(
                        string.Format("SELECT AccessToken FROM {0} WHERE FacebookId=@FacebookId", _tableName), cn);
                cmd.Parameters.AddWithValue("@FacebookId", facebookId);

                cn.Open();
                var result = cmd.ExecuteScalar();
                return result == null ? "" : result.ToString();
            }
        }

        public string GetFacebookId(string membershipUsername)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand(
                        string.Format("SELECT FacebookId FROM {0} WHERE Username=@Username", _tableName), cn);
                cmd.Parameters.AddWithValue("@Username", membershipUsername);

                cn.Open();
                return cmd.ExecuteScalar().ToString();
            }
        }

        public string GetFacebookId(object membershipProviderUserKey)
        {
            MembershipUser user = Membership.GetUser(membershipProviderUserKey);
            if (user == null)
                throw new FacebookSharpException("User with given membershipProviderUserKey not found.");
            return GetFacebookId(user.UserName);
        }

        #endregion
    }
}