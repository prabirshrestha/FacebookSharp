<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Facebook/Facebook.Master"
    Inherits="System.Web.Mvc.ViewPage<FacebookSharp.Schemas.Graph.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Hi
        <%: Model.Name %></h2>
    <table>
        <tr>
            <td>
                First Name:
            </td>
            <td>
                <%: Model.FirstName %>
            </td>
        </tr>
        <tr>
            <td>
                Last Name:
            </td>
            <td>
                <%: Model.LastName %>
            </td>
        </tr>
        <tr>
            <td>
                Birthday:
            </td>
            <td>
                <%: Model.Birthday %>
            </td>
        </tr>
        <tr>
            <td>
                Link:
            </td>
            <td>
                <a href="<%: Model.Link %>"><%: Model.Link %></a>
            </td>
        </tr>
    </table>
    <img src="<%: ViewData["ProfilePic"] %>" alt="<%: Model.Name %>" />
    <%:ViewData["ProfilePic"]%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Navigation" runat="server">
</asp:Content>
