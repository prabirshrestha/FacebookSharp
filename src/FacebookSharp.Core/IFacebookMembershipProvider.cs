namespace FacebookSharp
{
    ///<summary>
    /// Interface for linking Facebook with Membership Provider
    ///</summary>
    public interface IFacebookMembershipProvider
    {
        bool HasLinkedFacebook(string membershipUsername);
        bool HasLinkedFacebook(object membershipProviderUserKey);
        bool IsFacebookUserLinked(string facebookId);

        void LinkFacebook(string membershipUsername, string facebookId, string accessToken);
        void LinkFacebook(object membershipProviderUserKey, string facebookId, string accessToken);

        void UnlinkFacebook(string membershipUsername);
        void UnlinkFacebook(object membershipProviderUserKey);
        void UnlinkFacebookByFacebookId(string facebookId);

        string GetFacebookAccessToken(string membershipUsername);
        string GetFacebookAccessToken(object membershipProviderUserKey);
        string GetFacebookAccessTokenByFacebookId(string facebookId);

        string GetFacebookId(string membershipUsername);
        string GetFacebookId(object membershipProviderUserKey);
    }
}