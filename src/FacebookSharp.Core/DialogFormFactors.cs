namespace FacebookSharp
{
    /// <summary>
    /// Dialog form factors to use when displaying facebook login.
    /// </summary>
    /// <remarks>
    /// For more information visit http://developers.facebook.com/docs/authentication/
    /// </remarks>
    public enum AuthenticationDisplayStyle
    {
        /// <summary>
        /// Display a full-page authorization screen (the default)
        /// </summary>
        Page,

        /// <summary>
        /// Display a compact dialog optimized for web popup windows
        /// </summary>
        Popup,

        /// <summary>
        ///  Display a WAP / mobile-optimized version of the dialog
        /// </summary>
        Wap,

        /// <summary>
        /// Display an iPhone/Android/smartphone-optimized version of the dialog
        /// </summary>
        Touch
    }
}