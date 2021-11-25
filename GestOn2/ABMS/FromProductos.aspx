<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FromProductos.aspx.cs" Inherits="GestOn2.ABMS.FromProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-row col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-2">
                <div id="DivFiltros" runat="server" class="form-row border border-info col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-2 p-2">
                    <div id="Div1" class="col-xl-3 col-lg-3 col-md-3 col-xs-3 col-sm-3 mt-2" runat="server">
                        <asp:Label ID="Label10" runat="server" Text="Filtrar por: " class="col-xl-2 col-md-2 col-lg-2 col-sm-2" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="DropFiltros" CssClass="form-select col-xl-7 col-md-7 col-lg-7 col-sm-7 " runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSeleccionaFiltro_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Marca</asp:ListItem>
                            <asp:ListItem>Categoría</asp:ListItem>
                            <asp:ListItem>Ambas</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div id="DivFiltroXMarca" class="col-xl-3 col-lg-3 col-md-3 col-xs-3 col-xs-3 mt-2" runat="server">
                        <asp:Label ID="Label1" runat="server" Text="Marca Producto" class="col-4 col-md-4 col-lg-4" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="ddlMarcaProductoFiltro" CssClass="col-xl-6 col-md-6 col-lg-6 col-sm-6 col-xs-6" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSeleccionaFiltro_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Faber Castell</asp:ListItem>
                            <asp:ListItem>Papíro</asp:ListItem>
                            <asp:ListItem>Milka</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div id="DivFiltroXCategoria" class="col-xl-4 col-lg-4 col-md-4 col-xs-4 col-xs-4 mt-2" runat="server" visible="false">
                        <asp:Label ID="Label8" runat="server" Text="Categoría Producto" class="col-xl-4 col-xs-4 col-sm-4 col-md-4 col-lg-4" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="ddlCategoriaFiltro" class="col-xl-6 col-md-6 col-lg-6 col-sm-6 col-xs-6" runat="server">
                            <asp:ListItem Selected="True">Utiles</asp:ListItem>
                            <asp:ListItem>Regalería</asp:ListItem>
                            <asp:ListItem>Fotocopias</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div id="DivBtnBuscar" class="col-xl-2 col-lg-2 col-md-2 col-xs-2 col-xs-2" runat="server">
                        <asp:LinkButton ID="btnNuevoProducto" CssClass="btn btn-outline-primary col-xl-12 col-lg-12 col-md-12 col-xs-12 col-xs-12 float-right mb-2" ToolTip="Nuevo Producto" runat="server" OnClick="btnAgregarProducto_Click">Nuevo producto</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="container-fluid">
                <div id="DivOrdenFiltro" class="col-xl-4 col-lg-4 col-md-4 col-xs-4 col-sm-4  mt-4" runat="server">
                    <asp:Label ID="Label12" class="col-xl-2 col-lg-2 col-md-2 col-xs-2 col-sm-2" runat="server" Text="Ordenar grilla por: " Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlOrden" class="col-xl-5 col-lg-5 col-md-5 col-xs-5 col-sm-5" runat="server"></asp:DropDownList>
                    <asp:LinkButton ID="LinkButton1" ToolTip="Ordenar" CssClass="btn-warning col-xl-1 col-lg-1 col-sm-1 col-md-1" runat="server" OnClick="btnBuscarXCategoria_Click">
                            <i class="fa fa-sort"></i>
                    </asp:LinkButton>
                    <hr />
                </div>
                <div class="form-row col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-1">
                    <asp:Label ID="lblInformativo" runat="server" Visible="False" CssClass="alert alert-danger col-md-12 col-sm-12 col-xl-12 col-xs-12 col-lg-12"></asp:Label>
                    <div id="GVProductos" class="col-lg-12 col-sm-12 col-md-12" runat="server">
                        <asp:GridView ID="GridViewProductos" AutoGenerateColumns="False" EmptyDataText="No hay registros."
                            AllowPaging="True" runat="server" DataKeyNames="CodigoProducto" OnRowDataBound="GridViewProductos_RowDataBound"
                            OnRowEditing="GridViewProductos_RowEditing" OnRowUpdating="GridViewProductos_RowUpdated" CssClass="table table-light table-striped table-hover col-md-12 col-lg-12 col-sm-12 col-xl-12"
                            OnRowCancelingEdit="GridViewProductos_RowCancelingEdit" OnRowDeleting="GridViewProductos_OnRowDeleting" OnPageIndexChanging="GridViewProductos_PageIndexChanging" PageSize="5">
                            <Columns>
                                <asp:TemplateField HeaderText="CodigoProducto" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdProducto" runat="server" Text='<%# Eval("CodigoProducto") %>' ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("ProductoNombre") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNombre" runat="server" Text='<%# Eval("ProductoNombre") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marca">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarca" runat="server" Text='<%# Eval("Marca.NombreMarca") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlMarca" runat="server"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Categoria">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoria" runat="server" Text='<%# Eval("Categoria.NombreCategoria") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlCategoria" runat="server"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio Compra">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrecioCompra" runat="server" Text='<%# Eval("ProductoPrecioCompra") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPrecioCompra" runat="server" Text='<%# Eval("ProductoPrecioCompra") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio Venta">
                                    <ItemTemplate>
                                        <asp:Label ID="lbloPrecioVenta" runat="server" Text='<%# Eval("ProductoPrecioVenta") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtoPrecioVenta" runat="server" Text='<%# Eval("ProductoPrecioVenta") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accion">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" CommandName="Edit" Text="Editar" class="btn btn-outline-success col-md-5 col-xl-5 col-lg-5 col-sm-5 text-center"></asp:Button>
                                        <asp:Button ID="btnBorrar" runat="server" CommandName="Delete" Text="Borrar" class="btn btn-outline-danger col-md-5 col-xl-5 col-lg-5 col-sm-5 text-center"
                                            OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnActualizar" runat="server" CommandName="Update" Text="Actualizar" class="btn btn-outline-success col-md-5 col-xl-5 col-lg-5 col-sm-5 text-center"></asp:Button>
                                        <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar" class="btn btn-outline-danger col-md-5 col-xl-5 col-lg-5 col-sm-5 text-center"></asp:Button>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Timer ID="TimerMensajes" runat="server" Enabled="False" Interval="3000" OnTick="TimerMensajes_Tick">
                        </asp:Timer>
                    </div>
                </div>
            </div>
            </div>
            <div class="row text-center bg-light border border-dark offset-2 col-lg-8 col-sm-8 col-md-8 col-xl-8 col-xs-8" runat="server" id="DivNuevoProducto" visible="false">
                <div class="form-row col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12 mt-2 p-2 ">
                    <h2 class="text-md-center col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12">Registrar Productos </h2>
                    <div class="form-row col col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12">
                        <div class="form-row col col-lg-6 col-sm-6 col-md-6 col-xl-6 col-xs-6">
                            <asp:Label ID="Label11" runat="server" Text="Código Producto:" CssClass="col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 font-weight-bold text-left mt-2"></asp:Label>
                            <asp:TextBox ID="txtCodigoProducto" runat="server" CssClass="col-md-7 col-lg-7 col-sm-7 col-md-7 col-xl-7 mt-2"></asp:TextBox>
                        </div>
                        <div class="form-row col col-lg-6 col-sm-6 col-md-6 col-xl-6 col-xs-6 ml-2">
                            <asp:Label ID="Label14" runat="server" Text="Unidad de Medida:" CssClass="col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 font-weight-bold text-left mt-2"></asp:Label>
                            <asp:DropDownList ID="ddlUnidadMedida" runat="server" CssClass="col-md-7 col-lg-7 col-sm-7 col-md-7 col-xl-7 mt-2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-row col col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12">
                        <div class="form-row col col-lg-6 col-sm-6 col-md-6 col-xl-6 col-xs-6">
                            <asp:Label ID="Label2" runat="server" Text="Nombre:" CssClass="col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 font-weight-bold text-left mt-2"></asp:Label>
                            <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="col-md-7 col-lg-7 col-sm-7 col-md-7 col-xl-7 mt-2"></asp:TextBox>
                        </div>
                        <div class="form-row col col-lg-6 col-sm-6 col-md-6 col-xl-6 col-xs-6 ml-2">
                            <asp:Label ID="Label7" runat="server" Text="Cantidad:" CssClass="col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 font-weight-bold text-left mt-2"></asp:Label>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="col-md-7 col-lg-7 col-sm-7 col-md-7 col-xl-7 mt-2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row col col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12">
                        <div class="form-row col col-lg-6 col-sm-6 col-md-6 col-xl-6 col-xs-6">
                            <asp:Label ID="Label6" runat="server" Text="Marca:" CssClass="col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 font-weight-bold text-left mt-2"></asp:Label>
                            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control  col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 mt-2">
                                <asp:ListItem Value="1">Faber Castell</asp:ListItem>
                                <asp:ListItem Value="2">VIC</asp:ListItem>
                            </asp:DropDownList>
                            <asp:LinkButton ID="linkNewMarca" type="button" data-bs-toggle="modal" data-bs-target="#pnlNuevaMarca" runat="server" OnClick="linkNewMarca_Click" ToolTip="Nueva marca" CssClass="btn-outline-primary col-xl-2 col-sm-2 col-md-2 col-lg-2 mt-2"><i class="fa fa-plus"></i></asp:LinkButton>
                        </div>
                        <div class="form-row col col-lg-6 col-sm-6 col-md-6 col-xl-6 col-xs-6 ml-2">
                            <asp:Label ID="lblIVA" runat="server" Text="Tipo de IVA:" CssClass="col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 font-weight-bold text-left mt-2"></asp:Label>
                            <asp:DropDownList ID="ddlTipoIVA" runat="server" CssClass="form-control col-lg-7 col-sm-7 col-md-7 col-xl-7 mt-2">
                                <asp:ListItem Value="1">IVA MÍNIMO</asp:ListItem>
                                <asp:ListItem Value="2">IVA BÁSICO</asp:ListItem>
                                <asp:ListItem Value="3">OTRO</asp:ListItem>
                                <asp:ListItem Value="4">EXENTO DE IVA</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-row col col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12">
                        <div class="form-row col col-lg-6 col-sm-6 col-md-6 col-xl-6 col-xs-6">
                            <asp:Label ID="Label13" runat="server" Text="Categoria:" CssClass="col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 font-weight-bold text-left mt-2"></asp:Label>
                            <asp:DropDownList ID="lstCategorias" runat="server" CssClass="form-control  col-lg-5 col-sm-5 col-md-5 col-xl-5 mt-2">
                                <asp:ListItem Value="1">Regalería</asp:ListItem>
                                <asp:ListItem Value="3">Útiles</asp:ListItem>
                            </asp:DropDownList>
                            <asp:LinkButton ID="linkNewCategoria" type="button" data-bs-toggle="modal" data-bs-target="#pnlNuevaCat" runat="server" OnClick="linkNewCategoria_Click" ToolTip="Nueva categoría" CssClass="btn-outline-primary col-xl-2 col-sm-2 col-md-2 col-lg-2 mt-2"><i class="fa fa-plus"></i></asp:LinkButton>
                        </div>
                        <div class="form-row col col-lg-6 col-sm-6 col-md-6 col-xl-6 col-xs-6 ml-2">
                            <asp:Label ID="Label3" runat="server" Text="Precio compra:" CssClass="col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 font-weight-bold text-left mt-2"></asp:Label>
                            <asp:TextBox ID="txtPrecioCompra" runat="server" CssClass="col-md-7 col-lg-7 col-sm-7 col-md-7 col-xl-7 mt-2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row col col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12">
                        <div class="form-row col col-lg-6 col-sm-6 col-md-6 col-xl-6 col-xs-6">
                            <asp:Label ID="lblStockMinimo" runat="server" Text="Stock Mínimo:" CssClass="col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 font-weight-bold text-left mt-2"></asp:Label>
                            <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="col-md-7 col-lg-7 col-sm-7 col-md-7 col-xl-7 mt-2"></asp:TextBox>
                        </div>
                        <div class="form-row col col-lg-6 col-sm-6 col-md-6 col-xl-6 col-xs-6 ml-2">
                            <asp:Label ID="Label4" runat="server" Text="Precio venta:" CssClass="col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 font-weight-bold text-left mt-2"></asp:Label>
                            <asp:TextBox ID="txtPrecioVenta" runat="server" CssClass="col-md-7 col-lg-7 col-sm-7 col-md-7 col-xl-7 mt-2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row col-lg-12 col-sm-12 col-md-12 col-xl-12 col-xs-12">
                        <asp:Label ID="Label5" runat="server" Text="Imagenes:" CssClass="col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 font-weight-bold text-left mt-2"></asp:Label>
                        <asp:FileUpload ID="fuImagenes" class="custom-file bg-light mt-3 bg-light border border-dark" runat="server" AllowMultiple="false" />
                        <asp:Button ID="btnUpload" class="btn btn-outline-success  col-md-12 col-lg-12 col-sm-12 col-12 mt-1" runat="server" OnClick="btnUpload_Click" Text="Subir" />
                        <asp:TextBox ID="txtURLs" Style="display: none;" runat="server"></asp:TextBox>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-CssClass="col-6 p-0" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:ImageField DataImageUrlField="Value" ControlStyle-Width="40%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:LinkButton ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" CssClass="btn btn-outline-primary offset-3 col-lg-3 col-xl-3 col-xs-3 col-md-3 col-sm-3 mt-2">Guardar</asp:LinkButton>
                    <asp:LinkButton ID="btnListadoProductos" CssClass="btn btn-outline-danger col-xl-3 col-lg-3 col-sm-3 col-md-3 mt-2 ml-2" runat="server" OnClick="btnListadoProductos_Click">Ver listado</asp:LinkButton>
                </div>
                <!-- Agrego un nuevo modal para las categorías-->
                <div class="modal fade" id="pnlNuevaCat" visible="false" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Categoría de productos</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                <div class="form-row">
                                    <asp:Label ID="Label15" runat="server" CssClass="font-weight-bold text-left col-lg-5 col-sm-5 col-md-5 col-xl-5 col-xs-5 mt-2" Text="Id Categoría:"></asp:Label>
                                    <asp:TextBox ID="txtIdCat" TextMode="Number" runat="server" CssClass="col-lg-3 col-sm-3 col-md-3 col-xl-3 col-xs-3"></asp:TextBox>
                                    <asp:LinkButton ID="btnBuscar" CssClass="btn btn-info col-md-3 col-lg-3 col-sm-3 col-xl-3 col-xs-3 ml-3" OnClick="btnBuscarCat_Click" runat="server"><i class="fa fa-search"></i> Buscar</asp:LinkButton>
                                    <asp:Label ID="lblCategoriasMsj" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 font-weight-bold text-left mt-2" Visible="False"></asp:Label>
                                </div>
                                <div class="form-row">
                                    <asp:Label ID="Label17" runat="server" Text="Nombre Categoría:" CssClass="font-weight-bold text-left col-lg-5 col-sm-5 col-md-5 col-xl-5 col-xs-5 mt-2"></asp:Label>
                                    <asp:TextBox ID="txtNomCat" runat="server" CssClass="col-lg-7 col-sm-7 col-md-7 col-xl-7 col-xs-7 mt-2"></asp:TextBox>
                                </div>
                                <div class="form-row">
                                    <asp:Label ID="Label19" runat="server" Text="Porcentaje ganancia:" CssClass="font-weight-bold text-left col-lg-5 col-sm-5 col-md-5 col-xl-5 col-xs-5 mt-2"></asp:Label>
                                    <asp:TextBox ID="TextBox1" runat="server" TextMode="number" CssClass="col-lg-7 col-sm-7 col-md-7 col-xl-7 col-xs-7 mt-2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnGuardarCategoria" type="button" runat="server" OnClick="btnGuardarCategoria_Click" class="btn btn-outline-primary col-md-5 col-xl-5 col-lg-5 col-sm-5 ml-2" Text="Guardar" />
                                <asp:Button ID="btnEliminarCategoria" type="button" runat="server" OnClick="btnEliminarCategoria_Click" Text="Eliminar" class="btn btn-outline-info col-md-5 col-xl-5 col-lg-5 col-sm-5 ml-2" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Termina el modal de categprías-->
                <!-- Agrego un nuevo modal para las marcas de los productos -->
                <div class="modal fade" id="pnlNuevaMarca" visible="false" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="lblTitulo">Marcas de productos</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                <div class="form-row">
                                    <asp:Label ID="Label9" runat="server" CssClass="font-weight-bold text-left col-lg-5 col-sm-5 col-md-5 col-xl-5 col-xs-5 mt-2" Text="Id Marca:"></asp:Label>
                                    <asp:TextBox ID="txtIdMarca" TextMode="Number" runat="server" CssClass="col-lg-3 col-sm-3 col-md-3 col-xl-3 col-xs-3"></asp:TextBox>
                                    <asp:LinkButton ID="btnBuscarMarca" CssClass="btn btn-info col-md-3 col-lg-3 col-sm-3 col-xl-3 col-xs-3 ml-3" OnClick="btnBuscarCat_Click" runat="server"><i class="fa fa-search"></i> Buscar</asp:LinkButton>
                                    <asp:Label ID="Label16" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 font-weight-bold text-left mt-2" Visible="False"></asp:Label>
                                </div>
                                <div class="form-row">
                                    <asp:Label ID="Label18" runat="server" Text="Nombre Marca: " CssClass="font-weight-bold text-left col-lg-5 col-sm-5 col-md-5 col-xl-5 col-xs-5 mt-2"></asp:Label>
                                    <asp:TextBox ID="txtNombreMarca" runat="server" CssClass="col-lg-7 col-sm-7 col-md-7 col-xl-7 col-xs-7 mt-2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button1" type="button" runat="server" OnClick="btnGuardarCategoria_Click" class="btn btn-outline-primary col-md-5 col-xl-5 col-lg-5 col-sm-5 ml-2" Text="Guardar" />
                                <asp:Button ID="Button2" type="button" runat="server" OnClick="btnEliminarCategoria_Click" Text="Eliminar" class="btn btn-outline-info col-md-5 col-xl-5 col-lg-5 col-sm-5 ml-2" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Termina el modal de categprías-->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
