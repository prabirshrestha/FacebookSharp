using System;
using System.Data.SqlClient;

namespace FacebookSharp
{
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
                cmd.Parameters.AddWithValue("@user_name", membershipUsername);
                cn.Open();

                return (int)cmd.ExecuteScalar() == 1;
            }
        }

        public bool HasLinkedFacebook(object membershipProviderUserKey)
        {
            throw new NotImplementedException();
        }

        public bool IsFacebookUserLinked(string facebookId)
        {
            throw new NotImplementedException();
        }

        public void LinkFacebook(string membershipUsername, string facebookId, string accessToken)
        {
            throw new NotImplementedException();
        }

        public void LinkFacebook(object membershipProviderUserKey, string facebookId, string accessToken)
        {
            throw new NotImplementedException();
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