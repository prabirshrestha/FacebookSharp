namespace FacebookSharp
{
    using System;
    using System.Web.Security;

    public class SQLiteFacebookMembershipProvider : IFacebookMembershipProvider
    {
        private readonly string _connectionString;
        private readonly string _tableName;
        private readonly MembershipProvider _membershipProvider;

        public SQLiteFacebookMembershipProvider(string connectionString)
            : this(connectionString, "facebook_users", null)
        {

        }

        public SQLiteFacebookMembershipProvider(string connectionString, string tableName)
            : this(connectionString, tableName, null)
        {

        }

        public SQLiteFacebookMembershipProvider(string connectionString, string tableName, MembershipProvider membershipProvider)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _membershipProvider = membershipProvider;
        }

        #region Implementation of IFacebookMembershipProvider

        public bool HasLinkedFacebook(string membershipUsername)
        {
            throw new NotImplementedException();
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