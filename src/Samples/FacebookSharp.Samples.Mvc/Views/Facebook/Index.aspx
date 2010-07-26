<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Facebook/Facebook.Master" Inherits="System.Web.Mvc.ViewPage<FacebookSharp.Schemas.Graph.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Hi <%: Model.Name %></h2>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Navigation" runat="server">
</asp:Content>
