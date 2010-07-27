<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Link
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Link your Facebook Account</h2>

    <div class="KO">Oops! seems like your facebook account hasn't been linked with us.</div>
    <p>
        <a href="<%: ViewData["FBLinkUrl"] %>">Click here to link your facebook account.</a>
    </p>
</asp:Content>
