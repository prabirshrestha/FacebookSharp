<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Facebook in ASP.NET MVC using the Facebook Graph API.</h2>
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
            <%: Html.ActionLink("Click here to start.","LogOn","Account") %>
        </b>
    </p>
    <p>
        <b>Make sure you have executed the following sql script.</b>
        <pre>
        CREATE TABLE [FacebookUsers](
            [ApplicationName] NVARCHAR(256) NOT NULL,
            [Username] NVARCHAR(60), -- membershipUsername, primary key already enforced as unique and not null
            [FacebookId] VARCHAR(50) NOT NULL UNIQUE,
            [AccessToken] VARCHAR(256),
            PRIMARY KEY ([Username])
        );
        </pre>
    </p>
</asp:Content>
