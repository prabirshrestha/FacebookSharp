
namespace FacebookSharp
{
    using System.Data.SqlClient;
    using System.Web.Security;

    /// <remarks>
    /// CREATE TABLE [FacebookUsers](
    ///     [ApplicationName] NVARCHAR(256) NOT NULL,
    ///     [Username] NVARCHAR(60), -- membershipUsername, primary key already enforced as unique and not null
    ///     [FacebookId] VARCHAR(50) NOT NULL UNIQUE,
    ///     [AccessToken] VARCHAR(256),
    ///     PRIMARY KEY ([Username])
    /// );
    /// </remarks>
    public class SqlFacebookMembershipProvider : IFacebookMembershipProvider
    {
        private readonly string _connectionString;
        private readonly string _tableName;
        private readonly MembershipProvider _membershipProvider;

        public SqlFacebookMembershipProvider(string connectionString)
            : this(connectionString, "FacebookUsers", null)
        {
        }

        public SqlFacebookMembershipProvider(string connectionString, string tableName)
            : this(connectionString, tableName, null)
        {
        }

        public SqlFacebookMembershipProvider(string connectionString, string tableName, MembershipProvider membershipProvider)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _membershipProvider = membershipProvider;
            // we cound had done _membershipProvider = membershipProvider ?? Membership.Provider
            // but that wouldn't allow to work under client profile
        }

        #region Implementation of IFacebookMembershipProvider

        /// <summary>
        /// Name of the application
        /// </summary>
        public string ApplicationName
        {
            get { return _membershipProvider == null ? string.Empty : _membershipProvider.ApplicationName; }
        }

        public bool HasLinkedFacebook(string membershipUsername)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand(string.Format("SELECT COUNT(*) FROM {0} WHERE ApplicationName=@ApplicationName AND Username=@Username", _tableName), cn);
                cmd.Parameters.AddWithValue("@Username", membershipUsername);
                cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName);
                cn.Open();

                return (int)cmd.ExecuteScalar() == 1;
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
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand(string.Format("SELECT COUNT(*) FROM {0} WHERE ApplicationName=@ApplicationName AND FacebookId=@FacebookId", _tableName), cn);
                cmd.Parameters.AddWithValue("@FacebookId", facebookId);
                cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName);
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
                            "INSERT INTO {0} (ApplicationName,Username,FacebookId,AccessToken) VALUES (@ApplicationName,@Username,@FacebookId,@AccessToken)",
                            _tableName), cn);
                cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName);
                cmd.Parameters.AddWithValue("@Username", membershipUsername);
                cmd.Parameters.AddWithValue("@FacebookId", facebookId);
                cmd.Parameters.AddWithValue("@AccessToken", accessToken);

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
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    string.Format("DELETE FROM {0} WHERE ApplicationName=@ApplicationName AND Username=@Username", _tableName), cn);
                cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName);
                cmd.Parameters.AddWithValue("@Username", membershipUsername);

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
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    string.Format("DELETE FROM {0} WHERE ApplicationName=@ApplicationName AND FacebookId=@FacebookId", _tableName), cn);
                cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName);
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
                        string.Format("SELECT AccessToken FROM {0} WHERE ApplicationName=@ApplicationName AND Username=@Username", _tableName), cn);
                cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName);
                cmd.Parameters.AddWithValue("@user_Usernamename", membershipUsername);

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
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd =
                    new SqlCommand(
                        string.Format("SELECT AccessToken FROM {0} WHERE ApplicationName=@ApplicationName AND FacebookId=@FacebookId", _tableName), cn);
                cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName);
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
                        string.Format("SELECT FacebookId FROM {0} WHERE ApplicationName=@ApplicationName  AND Username=@Username", _tableName), cn);
                cmd.Parameters.AddWithValue("ApplicationName", ApplicationName);
                cmd.Parameters.AddWithValue("@Username", membershipUsername);

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