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
            <asp:Label ID="Label1" runat="server" Text="Importar Documento"></asp:Label>
            <br />
            <asp:FileUpload ID="fuDocs" runat="server" AllowMultiple="false"/>
            <asp:Button ID="btnSubir" runat="server"  Text="Subir"  />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label>
            <br />
            <asp:TextBox ID="txtNombre" runat="server" Width="185px"></asp:TextBox>
            <asp:CheckBox ID="chkDobleFaz" runat="server" Text="Es doble faz?" />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Formato:"></asp:Label>
            <br />
            <asp:TextBox ID="txtFormato" runat="server"></asp:TextBox>
            <asp:CheckBox ID="chkColor" runat="server" Text="Impresion a color?" />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Descripcion:"></asp:Label>
            <br />
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Label ID="Label6" runat="server" Text="Tipo:"></asp:Label>
            <br />
            <asp:TextBox ID="txtTipo" runat="server"></asp:TextBox>
            <asp:CheckBox ID="chkEsPractico" runat="server" OnCheckedChanged="chkEsPractico_CheckedChanged" Text="Es practico?" />
            <br />
            <asp:Label ID="Label7" runat="server" Text="Grado Liceal:"></asp:Label>
            <br />
            <asp:TextBox ID="txtGradoLiceal" runat="server"></asp:TextBox>
            <asp:CheckBox ID="chkEsEnvio" runat="server" OnCheckedChanged="chkEsEnvio_CheckedChanged" Text="Necesita envio?" />
            <br />
            <asp:Label ID="Label8" runat="server" Text="Número Practico:"></asp:Label>
            <br />
            <asp:TextBox ID="txtNroPractico" runat="server" Visible="False"></asp:TextBox>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Direccion:"></asp:Label>
            <br />
            <asp:TextBox ID="txtDireccion" runat="server" Visible="False"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnUpload" runat="server" CssClass="auto-style1" OnClick="btnUpload_Click" Text="Guardar" Width="120px" />
            <asp:Button ID="btnModificar" runat="server" CssClass="auto-style1" OnClick="btnModificar_Click" Text="Modificar" Width="120px" />
            <br />
        </ContentTemplate>
         <Triggers>
                        <asp:PostBackTrigger ControlID="btnUpload" />
                    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
