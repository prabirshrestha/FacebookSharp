<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
     <p>
        To learn more about Facebook# visit <a href="http://github.com/prabirshrestha/FacebookSharp"
            title="Facebook# @ github" target="_blank">http://bit.ly/facebooksharp</a>.
    </p>
    <p>
        You can also find more information on my blog at <a href="http://www.prabir.me" target="_blank">
            www.prabir.me</a> or catch up with me on twitter <a href="http://twitter.com/prabirshrestha"
                target="_blank">@prabirshrestha</a>
    </p>
</asp:Content>
