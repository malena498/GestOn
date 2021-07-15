<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormDocumento.aspx.cs" Inherits="GestOn2.ABMS.FormDocumento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            font-weight: bold;
            font-size: medium;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="Label8" runat="server" Text="ID:"></asp:Label>
            <br />
            <asp:TextBox ID="txtIdDocumento" runat="server"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Importar Documento"></asp:Label>
            <br />
            <asp:FileUpload ID="fuDocs" runat="server" AllowMultiple="false"/>
            <asp:Button ID="btnSubir" runat="server"  Text="Subir" OnClick="btnSubir_Click"  />
            <br />
            <asp:Label ID="lblMensaje" runat="server" CssClass="alert-danger" Width="300px"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label>
            <br />
            <asp:TextBox ID="txtNombre" runat="server" Width="185px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Descripcion:"></asp:Label>
            <br />
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="Grado Liceal:"></asp:Label>
            <br />
            <asp:DropDownList ID="ddlGradoLiceal" runat="server" Width="201px">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:CheckBox ID="chkColor" runat="server" Text="Impresion a color?" />
            <asp:CheckBox ID="chkDobleFaz" runat="server" Text="Es doble faz?" />
            <br />
            <br />
            <asp:CheckBox ID="chkEsPractico" runat="server" OnCheckedChanged="chkEsPractico_CheckedChanged" Text="Es practico?" AutoPostBack="True" />
            <br />
            <asp:Label ID="lblPractico" runat="server" Text="Número Practico:" Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="txtNroPractico" runat="server" Visible="False" Width="193px"></asp:TextBox>
            <br />
            <asp:CheckBox ID="chkEsEnvio" runat="server" OnCheckedChanged="chkEsEnvio_CheckedChanged" Text="Necesita envio?" AutoPostBack="True" />
            <br />
            <asp:Label ID="lblDireccion" runat="server" Text="Direccion:" Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="txtDireccion" runat="server" Visible="False"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnUpload" runat="server" CssClass="auto-style1" OnClick="btnUpload_Click" Text="Guardar" Width="120px" Enabled="False" />
            <asp:Button ID="btnModificar" runat="server" CssClass="auto-style1" OnClick="btnModificar_Click" Text="Modificar" Width="120px" />
            <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Text="Eliminar" />
            <br />
            <asp:Label ID="lblrutaarchivo" runat="server" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="lblTipoDoc" runat="server" Visible="False"></asp:Label>
            <br />
        </ContentTemplate>
         <Triggers>
                        <asp:PostBackTrigger ControlID="btnSubir" />
                    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
