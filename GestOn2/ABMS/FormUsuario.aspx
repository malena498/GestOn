<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormUsuario.aspx.cs" Inherits="GestOn2.FormUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="Label7" runat="server" Text="Id"></asp:Label>
            <br />
            <asp:TextBox ID="txtIdUsuario" runat="server"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" style="margin-left: 14px" Text="Buscar" OnClick="btnBuscar_Click" />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label>
            <br />
            <asp:TextBox ID="txtNombreUser" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="E-Mail"></asp:Label>
            <br />
            <asp:TextBox ID="txtEmailUser" runat="server" TextMode="Email"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Documento"></asp:Label>
            <br />
            <asp:TextBox ID="txtCedulaUser" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Teléfono"></asp:Label>
            <br />
            <asp:TextBox ID="txtTelefonoUser" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Contraseña"></asp:Label>
            <br />
            <asp:TextBox ID="txtPassUser" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label6" runat="server" Text="Categoría"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlCategoriaUsuario" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="lblResultado" runat="server"></asp:Label>
            <br />
<br />
            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" />
            <asp:Button ID="btnModificar" runat="server" OnClick="btnModificar_Click" Text="Modificar" />
            <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Text="Eliminar" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
