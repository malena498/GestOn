<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormOferta.aspx.cs" Inherits="GestOn2.ABMS.FormOferta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="Label1" runat="server" Text="Id:"></asp:Label>
            <br />
            <asp:TextBox ID="txtIdOferta" runat="server"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
<br />
            <asp:Label ID="Label2" runat="server" Text="Titulo:"></asp:Label>
            <br />
            <asp:TextBox ID="txtTituloOferta" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Fecha desde:"></asp:Label>
            <br />
            <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="DateTime"></asp:TextBox>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Fecha hasta:"></asp:Label>
            <br />
            <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="DateTime"></asp:TextBox>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Imagenes:"></asp:Label>
            <br />
            
            <asp:Label ID="Label6" runat="server" Text="Descripción:"></asp:Label>
            <br />
            <asp:TextBox ID="txtDescripcionOferta" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Label ID="Label7" runat="server" Text="Precio:"></asp:Label>
<br />
            <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblResultado" runat="server"></asp:Label>
            <br />
            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" />
            <asp:Button ID="btnEiminar" runat="server" OnClick="btnEiminar_Click" style="margin-left: 20px" Text="Eliminar" />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
