namespace FacebookSharp
{
    public interface IFacebookContext
    {
        Facebook Facebook { get; }
        IFacebookMembershipProvider FacebookMembershipProvider { get; }
    }
}