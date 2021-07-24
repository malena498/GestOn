<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormPedido.aspx.cs" Inherits="GestOn2.ABMS.FormPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/PedidoCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="container col-lg-4 col-4 col-md-4" runat="server">
        <form class="form-control">
        <div class="form-row">

        <asp:TextBox ID="txtId" class="col-lg-9 col-md-9 col-9" placeholder="Ingrese id" runat="server" Width="100px" Visible ="false"></asp:TextBox>

        <asp:Button ID="btnBuscar" class="col-lg-3 col-md-3 col-3 btn btn-info" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Font-Bold="True" />

        </div>
        <asp:Label ID="lblInformativo" Visible="false" runat="server" class="alert-danger col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="Fecha de entrega:" class="col-12 col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="txtFechaEntregaPedido" runat="server"  ReadOnly="True" class="col-12 col-md-12 col-lg-12"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Text="Descripción:" class="col-12 col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
        <textarea id="txtDescripcion" class="col-12 col-md-12 col-lg-12" placeholder="Ingrese descripción" runat="server" name="S1" cols="20"></textarea><br />
        <div class="form-row mt-2">
        <asp:Label ID="Label6" runat="server" Text="Seleccionar producto" class="col-6 col-md-6 col-lg-6" Font-Bold="True"></asp:Label>
         <asp:DropDownList ID="ListProductos1" runat="server" AutoPostBack="True" class="form-select col-6 col-md-6 col-lg-6 col-sm-6"  OnSelectedIndexChanged="ListProductos1_SelectedIndexChanged"></asp:DropDownList>
                    </div>
         <asp:TextBox ID="txtCantidadProducto" runat="server" placeholder="Ingrese cantidad" class="col-12 col-md-12 col-lg-12 mt-3" AutoPostBack="True" OnTextChanged="txtCantidadProducto_TextChanged"></asp:TextBox>
         <asp:Button ID="btnAgregarTodo" runat="server" OnClick="btnAgregarTodo_Click" Text="Agregar" class="btn btn-info col-3 col-md-3 col-lg-3" Font-Bold="True" Enabled="False" />
            <asp:Label ID="Label7" runat="server" class="col-6 col-md-6 col-lg-6" Font-Bold="True" Text="Productos seleccionados"></asp:Label>
            <asp:GridView ID="GridViewProductos" EmptyDataText="No hay registros."
                AllowPaging="True" runat="server" OnRowEditing="GridViewProductos_RowEditing" OnRowUpdating="GridViewProductos_RowUpdated"
                OnRowCancelingEdit="GridViewProductos_RowCancelingEdit" OnRowDeleting="GridViewProductos_OnRowDeleting" AutoGenerateColumns="False">
                <Columns>
                     <asp:TemplateField HeaderText="IdProducto" ItemStyle-Width="150" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblIdProducto" runat="server" Text='<%# Eval("IdProducto") %>' ReadOnly="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Acción" ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"  ShowCancelButton="true" />
                </Columns>
            </asp:GridView>
        <asp:Label ID="Label9" runat="server" Text="Envío a domicilo" class="col-12 col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
        <asp:RadioButton ID="RadioBtnNo" runat="server" Checked="True" GroupName="Radiosbtn" OnCheckedChanged="RadioBtnNo_CheckedChanged" Text="No" AutoPostBack="True" Font-Bold="True" />
        <asp:RadioButton ID="RadioBtnSi" runat="server" GroupName="Radiosbtn" OnCheckedChanged="RadioBtnSi_CheckedChanged" Text="Si" Width="40px" AutoPostBack="True" Font-Bold="True" /><br />
        <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" class="col-12 col-md-12 col-lg-12" Visible="False" Font-Bold="True"></asp:Label><br />
        <asp:TextBox ID="txtDireccion" runat="server" class="col-12 col-md-12 col-lg-12" placeholder="Ingrese Dirección" Visible="False"></asp:TextBox><br />
        <asp:Button ID="btnNuevoPedido" runat="server" OnClick="btnNuevoPedido_Click" class="btn btn-info col-5 col-md-5 col-lg-5" Text="Generar pedido" Font-Bold="True" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" class="btn btn-info col-5 col-md-5 col-lg-5" Font-Bold="True" OnClick="btnModificar_Click" />
        <asp:Timer ID="TimerMensajes" runat="server" Enabled="False" Interval="3000" OnTick="TimerMensajes_Tick" />
        <asp:GridView ID="GridViewPedidos" AutoGenerateColumns="false" EmptyDataText="No hay registros."
                AllowPaging="True" runat="server" DataKeyNames="IdPedido">
                <Columns>
                     <asp:TemplateField HeaderText="IdPedido" ItemStyle-Width="150" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="IdPedido" runat="server" Text='<%# Eval("IdPedido") %>' ReadOnly="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha Entrega" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaEntrega" runat="server" Text='<%# Eval("FechaEntrega") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblPrecio" runat="server" Text='<%# Eval("Precio") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        
        
        </form>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
