<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormPedido.aspx.cs" Inherits="GestOn2.ABMS.FormPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    #TextArea1 {
        width: 200px;
        height: 54px;
    }
    #txtDescripcion {
        width: 200px;
        height: 39px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<br />
        <asp:Label ID="Label3" runat="server" Text="Fecha de pedido:" Width="120px"></asp:Label>
        <asp:TextBox ID="txtFechaPedido" runat="server" ReadOnly="True" Width="200px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Dirección:" Width="120px"></asp:Label>
        <asp:TextBox ID="txtDireccion" runat="server" style="margin-left: 0px" Width="200px"></asp:TextBox>
        <asp:Label ID="lblDescripcion" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Descripción:" Width="120px"></asp:Label>
        <textarea id="txtDescripcion" runat="server" name="S1"></textarea><br />
<br />
<br />
        <asp:Button ID="btnNuevoPedido" runat="server" OnClick="btnNuevoPedido_Click" style="margin-left: 122px" Text="Generar pedido" />
        <br />
        <br />
        <asp:Label ID="lblId" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblFecha" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblDireccion" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
        <br />
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
