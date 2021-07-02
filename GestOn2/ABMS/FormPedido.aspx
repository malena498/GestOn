<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormPedido.aspx.cs" Inherits="GestOn2.ABMS.FormPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    #TextArea1 {
        width: 200px;
        height: 54px;
    }
    #txtDescripcion {
        width: 222px;
        height: 27px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Label ID="Label8" runat="server" Text="Id Pedido : " Width="140px" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="txtId" runat="server" Width="100px"></asp:TextBox>
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="150px" OnClick="btnBuscar_Click" Font-Bold="True" />
        <br />
        <asp:Label ID="lblInformativo" runat="server" Width="345px" CssClass="alert-danger" Font-Bold="True"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Fecha de pedido:" Width="140px" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="txtFechaPedido" runat="server" ReadOnly="True" Width="250px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Descripción:" Width="140px" Font-Bold="True"></asp:Label>
        <textarea id="txtDescripcion" style="width:250px;height:30px" runat="server" name="S1" cols="20"></textarea><br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Seleccionar producto" Width="170px" Font-Bold="True"></asp:Label>
        <asp:Label ID="Label7" runat="server" style="margin-left: 50px" Text="Productos seleccionados" Width="195px" Font-Bold="True"></asp:Label>
        <br />
        <asp:ListBox ID="ListProductos" runat="server" Height="150px" SelectionMode="Multiple" Width="180px"></asp:ListBox>
        <asp:ListBox ID="ListSeleccionados" runat="server" Height="150px" SelectionMode="Multiple" style="margin-left: 50px" Width="180px"></asp:ListBox>
        <br />
        <asp:Button ID="btnAgregarTodo" runat="server" OnClick="btnAgregarTodo_Click" Text="Agregar" Width="180px" Font-Bold="True" />
        <asp:Button ID="btnQuitarTodo" runat="server" OnClick="btnQuitarTodo_Click" style="margin-left: 50px" Text="Quitar" Width="180px" Font-Bold="True" />
        <br />
        <br />
        <asp:Label ID="Label9" runat="server" Text="Envío a domicilo" Width="140px" Font-Bold="True"></asp:Label>
        <asp:RadioButton ID="RadioBtnNo" runat="server" Checked="True" GroupName="Radiosbtn" OnCheckedChanged="RadioBtnNo_CheckedChanged" Text="No" AutoPostBack="True" Font-Bold="True" />
        <asp:RadioButton ID="RadioBtnSi" runat="server" GroupName="Radiosbtn" OnCheckedChanged="RadioBtnSi_CheckedChanged" Text="Si" Width="40px" AutoPostBack="True" Font-Bold="True" />
        <br />
        <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" Width="140px" style="margin-bottom: 0px" Visible="False" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="txtDireccion" runat="server" style="margin-left: 0px" Width="250px" Enabled="False" Visible="False"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnNuevoPedido" runat="server" OnClick="btnNuevoPedido_Click" style="margin-left: 0px" Text="Generar pedido" Width="200px" Font-Bold="True" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" Width="200px" Font-Bold="True" />
        <br />
        <asp:Timer ID="TimerMensajes" runat="server" Enabled="False" Interval="3000" OnTick="TimerMensajes_Tick" />
        <br />
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
