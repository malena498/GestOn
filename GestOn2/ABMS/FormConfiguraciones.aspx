<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormConfiguraciones.aspx.cs" Inherits="GestOn2.ABMS.FormConfiguraciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
            <asp:Label ID="CostoEnvio" runat="server" AccessKey="1" Text="Costo de envios a domicilio:" class="col-12 col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
            <asp:TextBox ID="txtCostoPedido" runat="server" class="col-12 col-md-12 col-lg-12" placeholder="Costo de envio" ></asp:TextBox><br />
            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" class="btn btn-info col-5 col-md-5 col-lg-5" Text="Generar pedido" Font-Bold="True" />
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

