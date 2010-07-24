using System;

namespace FacebookSharp
{
    /// <summary>
    /// Interface for linking Facebook with Membership Provider
    /// </summary>
    public interface IFacebookMembershipProvider
    {
        /// <summary>
        /// Name of the application
        /// </summary>
        string ApplicationName { get; }

        /// <summary>
        /// Checks if the specified membership user has already linked the Facebook account.
        /// </summary>
        /// <param name="membershipUsername">Membership username.</param>
        /// <returns>True if membership user has been linked, otherwise false.</returns>
        /// <remarks>
        /// Even if the user doesn't exist in the Membership provider, it must return false.
        /// </remarks>
        bool HasLinkedFacebook(string membershipUsername);

        /// <summary>
        /// Checks if the specified membership user has already linked the Facebook account.
        /// </summary>
        /// <param name="membershipProviderUserKey">Membership provider user key.</param>
        /// <returns>True if membership user has been linked, otherwise false.</returns>
        /// <remarks>
        /// Even if the user doesn't exist in the Membership provider, it must return false.
        /// </remarks>
        bool HasLinkedFacebook(object membershipProviderUserKey);

        /// <summary>
        /// Checks if the specified facebook user has already linked with the membership provider.
        /// </summary>
        /// <param name="facebookId">Facebook user id.</param>
        /// <returns>True if the facebook user has been linked, otherwise false.</returns>
        /// <remarks>
        /// Even if the facebook user id doesn't exist in the Membership provider, it must return false.
        /// Incase the facebookId is not a Facebook User, it should still return false.
        /// </remarks>
        bool IsFacebookUserLinked(string facebookId);

        /// <summary>
        /// Links the Facebook User with the Membership Provider.
        /// </summary>
        /// <param name="membershipUsername">Membership username to link.</param>
        /// <param name="facebookId">Facebook user id to link.</param>
        /// <param name="accessToken">Facebook access token.</param>
        /// <remarks>
        /// If the specified membership user doesn't exist, it should not throw error.
        /// Incase the facebookId is not a Facebook User, it should it should not throw error. 
        /// todo: remove this method soon
        /// </remarks>
        [Obsolete("Use LinkFacebook(string membershipUsername, string facebookId, string accessToken, int expiresIn) instead.")]
        void LinkFacebook(string membershipUsername, string facebookId, string accessToken);

        /// <summary>
        /// Links the Facebook User with the Membership Provider.
        /// </summary>
        /// <param name="membershipUsername">Membership username to link.</param>
        /// <param name="facebookId">Facebook user id to link.</param>
        /// <param name="accessToken">Facebook access token.</param>
        /// <param name="expiresIn">Expires in.</param>
        /// <remarks>
        /// If the specified membership user doesn't exist, it should not throw error.
        /// Incase the facebookId is not a Facebook User, it should it should not throw error.
        /// </remarks>
        void LinkFacebook(string membershipUsername, string facebookId, string accessToken, int expiresIn);

        /// <summary>
        /// Links the Facebook User with the Membership Provider.
        /// </summary>
        /// <param name="membershipProviderUserKey">Membership user's unique provider user key.</param>
        /// <param name="facebookId">Facebook user id to link.</param>
        /// <param name="accessToken">Facebook access token.</param>
        /// <remarks>
        /// If the specified membership user doesn't exist, it should not throw error.
        /// Incase the facebookId is not a Facebook User, it should it should not throw error.
        /// todo: remove this method soon
        /// </remarks>
        [Obsolete("Use LinkFacebook(object membershipProviderUserKey, string facebookId, string accessToken, int expiresIn) instead.")]
        void LinkFacebook(object membershipProviderUserKey, string facebookId, string accessToken);

        /// <summary>
        /// Links the Facebook User with the Membership Provider.
        /// </summary>
        /// <param name="membershipProviderUserKey">Membership user's unique provider user key.</param>
        /// <param name="facebookId">Facebook user id to link.</param>
        /// <param name="accessToken">Facebook access token.</param>
        /// <param name="expiresIn">Expires in.</param>
        /// <remarks>
        /// If the specified membership user doesn't exist, it should not throw error.
        /// Incase the facebookId is not a Facebook User, it should it should not throw error.
        /// </remarks>
        void LinkFacebook(object membershipProviderUserKey, string facebookId, string accessToken, int expiresIn);

        /// <summary>
        /// Unlinks Facebook User from the Membership Provider.
        /// </summary>
        /// <param name="membershipUsername">Membership username to unlink.</param>
        void UnlinkFacebook(string membershipUsername);

        /// <summary>
        /// Unlinks Facebook User from the Membership Provider.
        /// </summary>
        /// <param name="membershipProviderUserKey">Membership user's unique provider user key.</param>
        /// <remarks>
        /// If the specified membership user doesn't exist, it should not throw error.
        /// </remarks>
        void UnlinkFacebook(object membershipProviderUserKey);

        /// <summary>
        /// Unlink Facebook User from the Membership Provider.
        /// </summary>
        /// <param name="facebookId">Facebook user id.</param>
        /// <remarks>
        /// If the specified facebook user id doesn't exist, it should not throw error.
        /// </remarks>
        void UnlinkFacebookByFacebookId(string facebookId);

        /// <summary>
        /// Gets the Facebook access token for the specified membership user.
        /// </summary>
        /// <param name="membershipUsername">Membership username.</param>
        /// <returns>Returns the Facebook access token if found, otherwise null.</returns>
        /// <remarks>
        /// In case the membership user doesn't exist it should return null.
        /// In case the membership user hasn't linked with the application, it should return null.
        /// </remarks>
        string GetFacebookAccessToken(string membershipUsername);

        /// <summary>
        /// Gets the Facebook access token for the specified membership user.
        /// </summary>
        /// <param name="membershipProviderUserKey">Memberhip user's unique provider user key.</param>
        /// <returns>Returns the Facebook access token if found, otherwise null.</returns>
        /// <remarks>
        /// In case the membership user doesn't exist it should return null.
        /// In case the membership user hasn't linked with the application, it should return null.
        /// </remarks>
        string GetFacebookAccessToken(object membershipProviderUserKey);

        /// <summary>
        /// Gets the Facebook access token for the specified facebook user id.
        /// </summary>
        /// <param name="facebookId">Facebook user id.</param>
        /// <returns>Returns the Facebook access token if found, otherwise null.</returns>
        /// <remarks>
        /// In case the facebook user doesn't exist it should return null.
        /// In case the facebook user hasn't linked with the application, it should return null.
        /// </remarks>
        string GetFacebookAccessTokenByFacebookId(string facebookId);

        /// <summary>
        /// Gets the Facebook user id for the specified membership user.
        /// </summary>
        /// <param name="membershipUsername">Membership user name.</param>
        /// <returns>Returns the Facebook user id if found, otherwise null.</returns>
        /// <remarks>
        /// In case the membership user doesn't exist it should return null.
        /// In case the membership user hasn't linked with the application, it should return null.
        /// </remarks>
        string GetFacebookId(string membershipUsername);

        /// <summary>
        /// Gets the Facebook user id for the specified membership user.
        /// </summary>
        /// <param name="membershipProviderUserKey">Membership user's unique provider user key.</param>
        /// <returns>Returns the Facebook user id if found, otherwise null.</returns>
        /// <remarks>
        /// In case the membership user doesn't exist it should return null.
        /// In case the membership user hasn't linked with the application, it should return null.
        /// </remarks>
        string GetFacebookId(object membershipProviderUserKey);
    }
}