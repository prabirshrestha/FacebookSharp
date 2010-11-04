<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Facebook.aspx.cs" Inherits="FacebookSharp.Samples.WebApplication.Facebook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblAboutMe" runat="server" />

    <div>
        <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine" />
        <asp:Button runat="server" ID="btnPostMessage"  Text="Post To My Wall" OnClick="btnPostMessage_Click" />
        <br />
        <asp:Label runat="server" EnableViewState="false" ID="lblMessagePostStatus" />
    </div>
</asp:Content>