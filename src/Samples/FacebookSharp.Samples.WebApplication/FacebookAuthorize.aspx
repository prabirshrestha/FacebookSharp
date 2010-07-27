<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="FacebookAuthorize.aspx.cs" Inherits="FacebookSharp.Samples.WebApplication.FacebookAuthorize" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel runat="server" ID="pnlKO" EnableViewState="false" Visible="false">
        Facebook Authorization Failed
        <asp:Literal runat="server" id="lblErrorReason" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlOK" EnableViewState="false" Visible="false">
        You have success fully linked your facebook account. 
        <a href="Facebook.aspx">Click here to continue.</a>

        <p>Your access token <asp:Literal  runat="server" ID="lblAccessToken" /></p>
    </asp:Panel>
</asp:Content>