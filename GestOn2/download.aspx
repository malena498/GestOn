<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/Account.Master" AutoEventWireup="true" CodeBehind="download.aspx.cs" Inherits="GestOn2.download" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
<br />
            <asp:Label ID="Label1" runat="server" Text="Seleccionar documento"></asp:Label>
            <asp:Button ID="bntDescargar" runat="server" OnClick="bntDescargar_Click" Text="Descargar" />
<br />
            <asp:Label CssClass="alert alert-danger col-xl-5 col-md-5 col-lg-5 col-sm-5" ID="lblMensaje" runat="server" Visible="False"></asp:Label>
<br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
