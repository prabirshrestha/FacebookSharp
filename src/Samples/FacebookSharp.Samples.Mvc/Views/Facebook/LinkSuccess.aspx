<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LinkSuccess
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Congrats</h2>
    <b>Your facebook account has been linked successfully.</b>
    <p>
        <%: Html.ActionLink("Click here to continue.","Index","Facebook") %>
    </p>
</asp:Content>
