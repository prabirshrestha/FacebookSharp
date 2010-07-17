using System;

namespace FacebookSharp
{
    public class SqlFacebookMembershipProvider : IFacebookMembershipProvider
    {
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