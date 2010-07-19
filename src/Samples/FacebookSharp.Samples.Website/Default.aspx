<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Facebook in ASP.NET Website using the Facebook Graph API.
    </h2>
    <p>
        To learn more about Facebook# visit <a href="http://github.com/prabirshrestha/FacebookSharp"
            title="Facebook# @ github" target="_blank">http://bit.ly/facebooksharp</a>.
    </p>
    <p>
        You can also find more information on my blog at <a href="http://www.prabir.me" target="_blank">
            www.prabir.me</a> or catch up with me on twitter <a href="http://twitter.com/prabirshrestha"
                target="_blank">@prabirshrestha</a>
    </p>
    <p>
        <b>
            <a href="<%= FacebookContext.FacebookContext.Settings.FacebookAuthorizeUrl %>">Click here to login to facebook</a>
        </b>
    </p>
</asp:Content>
