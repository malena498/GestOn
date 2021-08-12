<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FromProductos.aspx.cs" Inherits="GestOn2.ABMS.FromProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-row col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-3">
                <div id="DivFiltros" runat="server" class="form-row border border-info col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-2">
                    <h1 class="col-md-12 col-lg-12 col-sm-12 col-xl-12">Selección de Filtros</h1>
                    <asp:Label ID="Label10" runat="server" Text="Filtrar por: " class="col-1 col-md-1 col-lg-1" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlSeleccionaFiltro" CssClass="form-select col-2 col-md-2 col-lg-2 col-sm-2 mb-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSeleccionaFiltro_SelectedIndexChanged">
                        <asp:ListItem Value="MarcaProducto">Marca Producto</asp:ListItem>
                        <asp:ListItem Value="CategoriaProducto">Categoría Producto</asp:ListItem>
                    </asp:DropDownList>
                    <div id="DivFiltroXMarca" class="col-xl-7 col-lg-7 col-md-7 col-xs-7 col-xs-7" runat="server" visible="true">
                    <asp:Label ID="Label1" runat="server" Text="Marca Producto" class="col-4 col-md-4 col-lg-4" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtMarcaProductoFiltro"  placeholder="Ingrese marca del producto" class="col-lg-4 col-md-4 col-sm-4 col-xl-4 ml-3" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="btnBuscarXMarca"  CssClass="btn btn-outline-success col-lg-2 col-sm-2 col-md-2" runat="server" OnClick="btnBuscarXMarca_Click">Buscar</asp:LinkButton>
                    </div>
                    <div id="DivFiltroXCategoria" class="col-xl-7 col-lg-7 col-md-7 col-xs-7 col-xs-7" runat="server" visible ="false">
                    <asp:Label ID="Label8" runat="server" Text="Categoría Producto" class="col-4 col-md-4 col-lg-4" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtCategoriaFiltro"  placeholder="Ingrese categoría del producto" class="col-lg-4 col-md-4 col-sm-4 col-xl-4 ml-3" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="btnBuscarXCategoria" CssClass="btn btn-outline-success col-xl-2 col-lg-2 col-sm-2 col-md-2" runat="server" OnClick="btnBuscarXCategoria_Click">Buscar</asp:LinkButton>
                 </div>
                <asp:LinkButton ID="btnNuevoProducto" CssClass="btn btn-outline-danger col-xl-2 col-lg-2 col-sm-2 col-md-2" runat="server" OnClick="btnAgregarProducto_Click">Nuevo producto</asp:LinkButton>
                </div>
                <div class="form-row col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-1"  >
                   <asp:Label ID="lblInformativo" runat="server" Visible="False" CssClass="alert alert-danger col-md-12 col-sm-12 col-xl-12 col-xs-12 col-lg-12"></asp:Label>
                    <div id="GVProductos" class="col-lg-12 col-sm-12 col-md-12" runat="server">
                        <asp:GridView ID="GridViewProductos" AutoGenerateColumns="False" EmptyDataText="No hay registros."
                            AllowPaging="True" runat="server" DataKeyNames="ProductoId" OnRowDataBound="GridViewProductos_RowDataBound"
                            OnRowEditing="GridViewProductos_RowEditing" OnRowUpdating="GridViewProductos_RowUpdated" CssClass="table table-light table-striped table-hover col-md-12 col-lg-12 col-sm-12 col-xl-12"
                            OnRowCancelingEdit="GridViewProductos_RowCancelingEdit" OnRowDeleting="GridViewProductos_OnRowDeleting" OnPageIndexChanging="GridViewProductos_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="IdProducto" ItemStyle-Width="150" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdProducto" runat="server" Text='<%# Eval("ProductoId") %>' ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("ProductoNombre") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNombre" runat="server" Text='<%# Eval("ProductoNombre") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marca" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarca" runat="server" Text='<%# Eval("ProductoMarca") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMarca" runat="server" Text='<%# Eval("ProductoMarca") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Categoria" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoria" runat="server" Text='<%# Eval("Categoria.NombreCategoria") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlCategoria" runat="server" Width="140"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio Compra" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrecioCompra" runat="server" Text='<%# Eval("ProductoPrecioCompra") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPrecioCompra" runat="server" Text='<%# Eval("ProductoPrecioCompra") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio Venta" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lbloPrecioVenta" runat="server" Text='<%# Eval("ProductoPrecioVenta") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtoPrecioVenta" runat="server" Text='<%# Eval("ProductoPrecioVenta") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accion" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" CommandName="Edit" Text="Editar" class="btn btn-outline-success col-md-12 col-xl-12 col-lg-12 col-sm-12 text-center"></asp:Button>
                                        <asp:Button ID="btnBorrar" runat="server" CommandName="Delete" Text="Borrar"  class="btn btn-outline-danger col-md-12 col-xl-12 col-lg-12 col-sm-12 text-center"
                                            OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnActualizar" runat="server" CommandName="Update" Text="Actualizar"  class="btn btn-outline-success col-md-12 col-xl-12 col-lg-12 col-sm-12 text-center"></asp:Button>
                                        <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar" class="btn btn-outline-danger col-md-12 col-xl-12 col-lg-12 col-sm-12 text-center" ></asp:Button>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Timer ID="TimerMensajes" runat="server" Enabled="False" Interval="3000" OnTick="TimerMensajes_Tick">
                        </asp:Timer>
                    </div>
                    </div>
                    <div class="col-lg-12 col-sm-12 col-md-12" runat="server" id="DivNuevoProducto" visible="false">
                    <div class="container-fluid col-lg-4 col-sm-4 col-md-4 ">
                        <asp:Label ID="Label2" runat="server" Text="Nombre:" CssClass="col-md-12 col-sm-12 col-xl-12 col-xs-12 col-lg-12"></asp:Label>
                        <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="col-12 col-md-12 col-lg-12 mt-3"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" Text="Precio compra:" CssClass="col-12 col-md-12 col-lg-12 mt-3"></asp:Label>
                        <asp:TextBox ID="txtPrecioCompra" runat="server" CssClass="col-12 col-md-12 col-lg-12 mt-3"></asp:TextBox>
                        <asp:Label ID="Label4" runat="server" Text="Precio venta:" CssClass="col-12 col-md-12 col-lg-12 mt-3"></asp:Label>
                        <asp:TextBox ID="txtPrecioVenta" runat="server" CssClass="col-12 col-md-12 col-lg-12 mt-3"></asp:TextBox>
                        <asp:Label ID="Label6" runat="server" Text="Marca:" CssClass="col-12 col-md-12 col-lg-12 mt-3"></asp:Label>
                        <asp:TextBox ID="txtMarca" runat="server" CssClass="col-12 col-md-12 col-lg-12 mt-3"></asp:TextBox>
                        <asp:Label ID="Label7" runat="server" Text="Cantidad:" CssClass="col-12 col-md-12 col-lg-12 mt-3"></asp:Label>
                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="col-12 col-md-12 col-lg-12 mt-3"></asp:TextBox>
                        <asp:Label ID="Label5" runat="server" Text="Categoría:" CssClass="col-12 col-md-12 col-lg-12 mt-3"></asp:Label>
                        <asp:DropDownList ID="lstCategorias" runat="server" CssClass="col-12 col-md-12 col-lg-12 mt-3">
                        </asp:DropDownList>
                        <asp:LinkButton ID="linkNewCategoria" runat="server" OnClick="linkNewCategoria_Click" CssClass="col-12 col-md-12 col-lg-12 mt-3 ">Nueva Categoría</asp:LinkButton>
                        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" CssClass="btn btn-danger col-lg-12 col-xl-12 col-xs-12 col-md-12 col-sm-12 mt-3" Text="Guardar" />
                        <asp:LinkButton ID="btnListadoProductos" CssClass="btn btn-outline-danger col-xl-12 col-lg-12 col-sm-12 col-md-12 mt-3" runat="server" OnClick="btnListadoProductos_Click">Ver listado</asp:LinkButton>
                    </div>
                    <div id="pnlNuevaCat" runat="server" visible="false" class="border border-secondary bg-light col-lg-3 col-sm-3 col-xl-3 col-xs-3 col-md-3 mt-3">
                        <asp:Label ID="lbllll" runat="server"  CssClass="col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12 mt-3" Text="Id Categoría:" ></asp:Label><br />
                        <asp:TextBox ID="txtIdCat" runat="server" CssClass="col-lg-4 col-sm-4 col-md-4 col-xl-4 col-xs-4 mt-3"></asp:TextBox>
                        <asp:Button ID="btnBuscarCat" runat="server" OnClick="btnBuscarCat_Click" Text="Buscar" CssClass="col-lg-7 col-sm-7 col-md-7 col-xl-7 col-xs-7" />
                        <asp:Label ID="lblCategoriasMsj" runat="server" CssClass="alert alert-danger col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12 mt-3" Visible="False"></asp:Label>
                        <asp:Label ID="Label9" runat="server" Text="Nombre Categoría" CssClass="col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12 mt-3"></asp:Label>
                        <asp:TextBox ID="txtNomCat" runat="server" CssClass="col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12 mt-3"></asp:TextBox>
                        <div class="form-row col-lg-12 col-sm-12 col-xl-12 col-md-12 col-xl-12 col-xs-12 mt-3">
                        <asp:Button ID="btnGuardarCategoria" runat="server" OnClick="btnGuardarCategoria_Click" class="btn btn-outline-success col-md-5 col-xl-5 col-lg-5 col-sm-5 ml-3 " Text="Guardar"/>
                        <asp:Button ID="btnEliminarCategoria" runat="server" OnClick="btnEliminarCategoria_Click" Text="Eliminar" class="btn btn-outline-danger col-md-5 col-xl-5 col-lg-5 col-sm-5 ml-4"/>
                        </div>
                        <asp:Button ID="btnClosePanel" runat="server" CssClass="btn btn-danger col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12 mt-3" Text="Cerrar" OnClick="btnClosePanel_Click" />
                    </div>
                        </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
