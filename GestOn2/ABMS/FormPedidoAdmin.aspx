<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormPedidoAdmin.aspx.cs" Inherits="GestOn2.ABMS.FormPedidoAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/PedidoAdminCss.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-3">
                <div class="container border border-info col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-2">
                    <h1 class="col-md-12 col-lg-12 col-sm-12 col-xl-12">Selección de Filtros</h1>
                    <asp:Label ID="Label1" runat="server" Text="Nombre Cliente" class="col-2 col-md-2 col-lg-2" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ListPedidoUsuario" runat="server" AutoPostBack="True" class="form-select col-3 col-md-3 col-lg-3 col-sm-3 mb-3" OnSelectedIndexChanged="ListPedidoUsuario_SelectedIndexChanged">
                        <asp:ListItem>Todos</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label6" runat="server" Text="Estado del pedido" class="col-2 col-md-2 col-lg-2" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ListPedidoEstado" runat="server" AutoPostBack="True" class="form-select col-3 col-md-3 col-lg-3 col-sm-3 mb-3" OnSelectedIndexChanged="ListPedidoEstado_SelectedIndexChanged">
                        <asp:ListItem>Todos</asp:ListItem>
                        <asp:ListItem>Pendiente</asp:ListItem>
                        <asp:ListItem>Realizado</asp:ListItem>
                        <asp:ListItem>Cancelado</asp:ListItem>
                    </asp:DropDownList>
                    
                </div>
                <div class="form-row col-md-12 col-lg-12 col-sm-12 col-xl-12 ml-0 ml-15 mt-3">
                    <div runat="server" id="DivVisualizarPedidos" class="col-md-7 col-lg-7 col-sm-7 col-xl-7  " visible="true">
                        <asp:Label ID="lblInformativo" runat="server" Visible="false" Text="" CssClass="alert alert-danger"></asp:Label>
                        <asp:GridView ID="GridViewPedidos" AutoGenerateColumns="false" EmptyDataText="No hay registros."
                            AllowPaging="True" runat="server" DataKeyNames="IdPedido" class="table table-light table-striped table-hover col-md-8 col-lg-8 col-sm-8 col-xl-8"
                            PageSize="4" ShowHeaderWhenEmpty="True" OnPageIndexChanging="GridViewPedidos_PageIndexChanging" OnRowDeleting="GridViewPedidos_OnRowDeleting"
                            OnRowEditing="GridViewPedidos_RowEditing" OnRowUpdating="GridViewPedidos_RowUpdated"
                            OnRowCancelingEdit="GridViewPedidos_RowCancelingEdit">
                            <Columns>
                                <asp:TemplateField HeaderText="IdPedido" ItemStyle-Width="150" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="IdPedido" runat="server" Text='<%# Eval("IdPedido") %>' ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Pedido" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaPedido" runat="server" Text='<%# Eval("FechaPedido") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Entrega" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaEntrega" runat="server" Text='<%# Eval("FechaEntrega") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Direccion" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDirección" runat="server" Text='<%# Eval("Direccion") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio" ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Eval("Precio") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accion" ItemStyle-Width="90">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" runat="server" class="btn btn-outline-success col-md-12 col-xl-12 col-lg-12 col-sm-12 text-center" Visible="false" CommandName="Edit">EDITAR</asp:LinkButton>
                                        <asp:Button ID="btnBorrar" runat="server" class="btn btn-outline-danger col-md-4 col-xl-4 col-lg-4 col-sm-4 text-center" Visible="false" CommandName="Delete" Text="X"
                                            OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                                        <asp:LinkButton ID="btnActualizar" runat="server" class="btn btn-outline-success col-md-12 col-xl-12 col-lg-12 col-sm-12 text-center" CommandName="Update">EDITAR</asp:LinkButton>
                                        <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar" Visible="false"></asp:Button>
                                    </ItemTemplate>

                                    <ItemStyle Width="90px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="DivVisualizarProductos" visible="false" runat="server" class="table table-bordered col-md-7 col-lg-7 col-sm-7 col-xl-7 mt-3">
                        <asp:Label ID="Label7" runat="server" class="col-6 col-md-6 col-lg-6" Font-Bold="True" Text="Productos seleccionados"></asp:Label>
                        <asp:GridView ID="GridViewProductos" EmptyDataText="No hay productos"
                            AllowPaging="True" runat="server" Css="table table-light table-striped table-hover col-md-12 col-lg-12 col-sm-12 col-xl-12" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="IdProducto" ItemStyle-Width="150" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdProducto" runat="server" Text='<%# Eval("ProductoId") %>' ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("producto.ProductoNombre") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio venta" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrecioVenta" runat="server" Text='<%# Eval("producto.ProductoPrecioVenta") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Button ID="btnCerrarDivProductos" CssClass="btn btn-outline-info col-lg-3 col-md-3 col-xl-3 col-sm-3" Text="Cerrar" runat="server" OnClick="btnCerrarDivProductos_Click"/>
                    </div>
                    <div class="offset-md-1 offset-xl-1 offset-lg-1 offset-sm-1 col-lg-4 col-sm-4 col-md-4 border border-dark bg-light">
                        <form>
                            <h3>Datos del pedido</h3>
                            <asp:TextBox ID="txtIdPedidoA" Visible="false" ReadOnly="true" class="col-12 col-md-12 col-lg-12" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtDescripcionPedidoA" class="col-12 col-md-12 col-lg-12" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtDireccionPedidoA" class="col-12 col-md-12 col-lg-12 mt-3" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtFechaPedido" class="col-12 col-md-12 col-lg-12 mt-3" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtFechaEntrega" class="col-12 col-md-12 col-lg-12 mt-3" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtNombreUsuarioA" class="col-12 col-md-12 col-lg-12 mt-3" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtPrecioA" class="col-12 col-md-12 col-lg-12 mt-3" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:DropDownList ID="ddlEstado" class="col-12 col-md-12 col-lg-12 mt-3" runat="server">
                                <asp:ListItem>Realizado</asp:ListItem>
                                <asp:ListItem Selected="True">Pendiente</asp:ListItem>
                                <asp:ListItem>Cancelado</asp:ListItem>
                            </asp:DropDownList>
                            <div class="container-fluid col-12 col-md-12 col-lg-12 mt-3 ">
                                <asp:LinkButton ID="lnkProductos" class="col-12 col-md-12 col-lg-12 align-content-center " runat="server" OnClick="lnkProductos_Click">Ver Productos Seleccionados</asp:LinkButton>
                            </div>
                            <asp:Button ID="btnActualizarPedido" Enabled="false" CssClass="btn btn-info col-lg-12 col-xl-12 col-xs-12 col-md-12 col-sm-12 mt-3" runat="server" Text="Actualizar" OnClick="btnActualizarPedido_Click" />
                        </form>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
