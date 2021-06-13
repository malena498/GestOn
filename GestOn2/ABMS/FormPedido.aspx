<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormPedido.aspx.cs" Inherits="GestOn2.ABMS.FormPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    #TextArea1 {
        width: 200px;
        height: 54px;
    }
    #txtDescripcion {
        width: 222px;
        height: 58px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <br />
        <asp:Label ID="lblResultado" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Fecha de pedido:" Width="120px"></asp:Label>
        <asp:TextBox ID="txtFechaPedido" runat="server" ReadOnly="True" Width="200px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Dirección:" Width="120px" style="margin-bottom: 0px"></asp:Label>
        <asp:TextBox ID="txtDireccion" runat="server" style="margin-left: 0px" Width="200px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Descripción:" Width="120px"></asp:Label>
        <textarea id="txtDescripcion" runat="server" name="S1" cols="20" rows="1"></textarea><br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Seleccionar producto" Width="150px"></asp:Label>
        <asp:Label ID="Label7" runat="server" style="margin-left: 50px" Text="Productos seleccionados" Width="150px"></asp:Label>
        <br />
        <asp:ListBox ID="ListProductos" runat="server" Height="150px" SelectionMode="Multiple" Width="150px"></asp:ListBox>
        <asp:ListBox ID="ListSeleccionados" runat="server" Height="150px" SelectionMode="Multiple" style="margin-left: 50px" Width="150px"></asp:ListBox>
        <br />
        <asp:Button ID="btnAgregarTodo" runat="server" OnClick="btnAgregarTodo_Click" Text="Agregar" Width="150px" />
        <asp:Button ID="btnQuitarTodo" runat="server" OnClick="btnQuitarTodo_Click" style="margin-left: 50px" Text="Quitar" Width="150px" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="btnNuevoPedido" runat="server" OnClick="btnNuevoPedido_Click" style="margin-left: 122px" Text="Generar pedido" />
        <br />
        <br />
        <br />
        <br />
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
