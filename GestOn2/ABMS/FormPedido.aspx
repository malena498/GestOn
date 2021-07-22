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
    <div class="container col-lg-4 col-4 col-md-4" runat="server">
        <form class="form-control">
        <div class="form-row">
        <asp:Label ID="Label8" runat="server" Text="Id Pedido : " class="col-12 col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="txtId" class="col-lg-9 col-md-9 col-9" placeholder="Ingrese id" runat="server" Width="100px"></asp:TextBox>
        <asp:Button ID="btnBuscar" class="col-lg-3 col-md-3 col-3 btn btn-info" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Font-Bold="True" />
        </div>
        <asp:Label ID="lblInformativo" Visible="false" runat="server" class="alert-danger col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="Fecha de pedido:" class="col-12 col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="txtFechaPedido" runat="server"  ReadOnly="True" class="col-12 col-md-12 col-lg-12"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Text="Descripción:" class="col-12 col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
        <textarea id="txtDescripcion" class="col-12 col-md-12 col-lg-12" placeholder="Ingrese descripción" runat="server" name="S1" cols="20"></textarea><br />
        <div class="form-row">
        <asp:Label ID="Label6" runat="server" Text="Seleccionar producto" class="col-6 col-md-6 col-lg-6" Font-Bold="True"></asp:Label>
        <asp:Label ID="Label7" runat="server" Text="Productos seleccionados"  class="col-6 col-md-6 col-lg-6"  Font-Bold="True"></asp:Label>
        </div>
        <asp:ListBox ID="ListProductos" runat="server" Height="150px" SelectionMode="Multiple" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ListProductos_SelectedIndexChanged"></asp:ListBox>
        <asp:ListBox ID="ListSeleccionados" runat="server" Height="150px" SelectionMode="Multiple" style="margin-left: 50px" Width="180px" AutoPostBack="True"></asp:ListBox>
        <asp:TextBox ID="txtCantidadProducto" runat="server" placeholder="Ingrese cantidad" class="col-12 col-md-12 col-lg-12" AutoPostBack="True" OnTextChanged="txtCantidadProducto_TextChanged"></asp:TextBox>
        <div class="form-row">
        <asp:Label ID="lblPrecioTexto" runat="server" Text="Precio total del pedido" class="col-8 col-md-8 col-lg-8" Font-Bold="True"></asp:Label>
        <asp:Label ID="lblPrecioResultado" runat="server" Text="" class="col-2 col-md-2 col-lg-2" Font-Bold="True"></asp:Label>
        </div>
        <asp:Button ID="btnAgregarTodo" runat="server" OnClick="btnAgregarTodo_Click" Text="Agregar" class="btn btn-info col-3 col-md-3 col-lg-3" Font-Bold="True" Enabled="False" />
        <asp:Button ID="btnQuitarTodo" runat="server" OnClick="btnQuitarTodo_Click" Text="Quitar" class="btn btn-info col-3 col-md-3 col-lg-3 ml-30" Font-Bold="True" Enabled="False" /><br />
        <asp:Label ID="Label9" runat="server" Text="Envío a domicilo" class="col-12 col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
        <asp:RadioButton ID="RadioBtnNo" runat="server" Checked="True" GroupName="Radiosbtn" OnCheckedChanged="RadioBtnNo_CheckedChanged" Text="No" AutoPostBack="True" Font-Bold="True" />
        <asp:RadioButton ID="RadioBtnSi" runat="server" GroupName="Radiosbtn" OnCheckedChanged="RadioBtnSi_CheckedChanged" Text="Si" Width="40px" AutoPostBack="True" Font-Bold="True" /><br />
        <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" class="col-12 col-md-12 col-lg-12" Visible="False" Font-Bold="True"></asp:Label><br />
        <asp:TextBox ID="txtDireccion" runat="server" class="col-12 col-md-12 col-lg-12" placeholder="Ingrese Dirección" Visible="False"></asp:TextBox><br />
        <asp:Button ID="btnNuevoPedido" runat="server" OnClick="btnNuevoPedido_Click" class="btn btn-info col-5 col-md-5 col-lg-5" Text="Generar pedido" Font-Bold="True" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" class="btn btn-info col-5 col-md-5 col-lg-5" Font-Bold="True" OnClick="btnModificar_Click" />
        <asp:Timer ID="TimerMensajes" runat="server" Enabled="False" Interval="3000" OnTick="TimerMensajes_Tick" />
       </form>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
