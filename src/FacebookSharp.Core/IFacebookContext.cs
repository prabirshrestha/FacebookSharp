namespace FacebookSharp
{
    public interface IFacebookContext
    {
        /// <summary>
        /// Gets the Facebook object for the current logged in user.
        /// </summary>
        /// <remarks>
        /// If the FacebookMembershipProvider is not null, it will set the AccessToken if the user
        /// has one. 
        /// </remarks>
        Facebook FacebookContext { get; }

        /// <summary>
        /// Gets the Facebook MemershipProvider.
        /// </summary>
        /// <remarks>
        /// Returns null if IFacebookMembershipProvider has not been implemented.
        /// </remarks>
        IFacebookMembershipProvider FacebookMembershipProvider { get; }
    }
}