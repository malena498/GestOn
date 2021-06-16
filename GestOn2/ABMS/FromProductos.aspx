<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FromProductos.aspx.cs" Inherits="GestOn2.ABMS.FromProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<br />
            <asp:Label ID="Label1" runat="server" Font-Size="13pt" ForeColor="Black" Text="Id:" Width="120px"></asp:Label>
            <asp:TextBox ID="txtIdProducto" runat="server" Width="50px"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="100px" OnClick="btnBuscar_Click" />
<br />
            <asp:Label ID="lblInformativo" runat="server" Font-Size="14pt" ForeColor="#FF3300" style="margin-left: 120px" Visible="False" Width="150px"></asp:Label>
<br />
            <asp:Label ID="Label2" runat="server" Font-Size="13pt" ForeColor="Black" Text="Nombre:" Width="120px"></asp:Label>
            <asp:TextBox ID="txtNombreProducto" runat="server" Width="150px"></asp:TextBox>
<br />
<br />
            <asp:Label ID="Label3" runat="server" Font-Size="13pt" ForeColor="Black" Text="Precio compra:" Width="120px"></asp:Label>
            <asp:TextBox ID="txtPrecioCompra" runat="server" Width="150px"></asp:TextBox>
<br />
<br />
            <asp:Label ID="Label4" runat="server" Font-Size="13pt" ForeColor="Black" Text="Precio venta:" Width="120px"></asp:Label>
            <asp:TextBox ID="txtPrecioVenta" runat="server" Width="150px"></asp:TextBox>
<br />
<br />
            <asp:Label ID="Label5" runat="server" Font-Size="13pt" ForeColor="Black" Text="Categoría:" Width="120px"></asp:Label>
            <asp:TextBox ID="txtCategoria" runat="server" Width="150px"></asp:TextBox>
<br />
<br />
            <asp:Label ID="Label6" runat="server" Font-Size="13pt" ForeColor="Black" Text="Marca:" Width="120px"></asp:Label>
            <asp:TextBox ID="txtMarca" runat="server" Width="150px"></asp:TextBox>
<br />
<br />
            <asp:Label ID="Label7" runat="server" Font-Size="13pt" Text="Cantidad:" Width="120px"></asp:Label>
            <asp:TextBox ID="txtCantidad" runat="server" Width="150px"></asp:TextBox>
<br />
<br />
            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" style="margin-left: 120px" Text="Guardar" Width="75px" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" Width="75px" OnClick="btnModificar_Click" Enabled="False" />
<br />
<br />
            <asp:Timer ID="TimerMensajes" runat="server" Enabled="False" Interval="3000" OnTick="TimerMensajes_Tick">
            </asp:Timer>
            <asp:Label ID="lblprice" runat="server" Width="100px"></asp:Label>
<br />
<br />
<br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
