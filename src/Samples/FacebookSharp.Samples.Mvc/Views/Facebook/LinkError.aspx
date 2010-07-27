<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FacebookSharp.FacebookAuthenticationResult>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	LinkError
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="KO">Oops!</h2>
    <b>Seems like we couldn't link your facebook account.</b>

    <p>
        <b>Reason: </b><%: Model.ErrorReasonText %>
    </p>
</asp:Content>
