<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage.Master" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="HSP_Loader.UploadFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Import Excel File:
<asp:FileUpload ID="FileUpload1" runat="server" />
<br />
<br />
<asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload"  />
    <asp:Label ID="Label2" runat="server" Text="Label2" Visible="False"></asp:Label>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" ForeColor="#00CC00"></asp:Label>
    
<br />
<br />
</asp:Content>
