<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="GestOn2.Registrarse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label>
            <br />
            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            <br />
<br />
            <asp:Label ID="Label2" runat="server" Text="E-Mail"></asp:Label>
            <br />
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Documento"></asp:Label>
            <br />
            <asp:TextBox ID="txtDocumento" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label6" runat="server" Text="Telefono"></asp:Label>
            <br />
            <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Contraseña"></asp:Label>
            <br />
            <asp:TextBox ID="txtContrasenia" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Confirmar contraseña"></asp:Label>
            <br />
            <asp:TextBox ID="txtConfirmarContrasenia" runat="server"></asp:TextBox>
            <br />
             <asp:Label ID="Label8" runat="server" Text="Categoría"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlCategoriaUsuario" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btnRegistrarse" runat="server" OnClick="btnRegistrarse_Click" Text="Registrarse" />
            <br />
            <br />
            <asp:Label ID="lblResultado" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="Ya está registrado?  "></asp:Label>
            <asp:LinkButton ID="lnkLogin" runat="server" OnClick="lnkLogin_Click">Ingresar</asp:LinkButton>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
