namespace FacebookSharp
{
    public interface IFacebookContext
    {
        Facebook FacebookContext { get; }
        IFacebookMembershipProvider FacebookMembershipProvider { get; }
    }
}