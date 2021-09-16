<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormPedido.aspx.cs" Inherits="GestOn2.ABMS.FormPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/PedidoCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1 class="col-md-12 col-lg-12 col-sm-12 col-xl-12">Pedidos</h1>
            <div class="container-fluid col-md-12 col-lg-12 col-sm-12 col-xl-12" id="DivEncabezado" runat="server">
                <asp:Label ID="lblInformativo" Visible="false" runat="server" class="alert-danger col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
                <asp:LinkButton ID="lnkNuevoPedido" runat="server" CssClass="btn btn-outline-danger offset-lg-1 offset-xl-1 col-xl-2 col-xs-2 col-sm-2 col-md-2 col-lg-2" OnClick="lnkNuevoPedido_Click"> Agregar pedido </asp:LinkButton>
                <hr />
            </div>
            <div class="row-control">
                <form class="form-control col-xl-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div id="DivGridPedido" class="col-md-12 col-lg-12 col-sm-12 col-12 col-xl-12" runat="server">
                        <asp:GridView ID="GridViewPedidos" AutoGenerateColumns="false" EmptyDataText="No hay registros." AllowPaging="True" runat="server"
                            DataKeyNames="IdPedido" class="table table-light table-striped table-hover col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-3" PageSize="5"
                            OnPageIndexChanging="GridViewPedidos_PageIndexChanging"
                            OnRowUpdating="GridViewPedidos_RowUpdated" ShowFooter="True"
                            OnRowDeleting="GridViewPedidos_OnRowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="IdPedido" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="IdPedido" runat="server" Text='<%# Eval("IdPedido") %>' ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Entrega">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaEntrega" runat="server" Text='<%# Eval("FechaEntrega") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hora entrega">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHoraEntrega" runat="server" Text='<%# Eval("HoraEntrega") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Eval("Precio") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accion" ItemStyle-Width="200">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" runat="server" CommandName="Update" class="btn btn-outline-success text-center">Editar</asp:LinkButton>
                                        <asp:Button ID="btnBorrar" runat="server" CommandName="Delete" class="btn btn-outline-danger text-center" Text="Borrar"
                                            OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar"></asp:Button>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                    <div id="divConatiner" class="container-fluid  bg-light border border-dark col-md-4 col-lg-4 col-sm-4 col-4 col-xl-4 p-3" runat="server" visible="false">
                        <div id="divNuevoPedido" visible="false" runat="server">
                            <asp:Label ID="Label1" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Text="Pedido Productos"></asp:Label>
                            <asp:RadioButton ID="chkPedidoProducto" runat="server" OnCheckedChanged="rdbPedidoProductos_CheckedChanged" GroupName="Radiosbtn2" AutoPostBack="True" Font-Bold="True" />
                            <asp:Label ID="Label2" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Text="Impresión de imagen"></asp:Label>
                            <asp:RadioButton ID="chkPedidoImagen" runat="server" OnCheckedChanged="rdbPedidoImagen_CheckedChanged" GroupName="Radiosbtn2" Width="40px" AutoPostBack="True" Font-Bold="True" /><br />
                            <asp:Label ID="Label5" runat="server" Text="Descripción:" class="col-12 col-md-12 col-lg-12" Font-Bold="True"></asp:Label>
                            <textarea id="txtDescripcion" class="col-12 col-md-12 col-lg-12" placeholder="Ingrese descripción" runat="server" name="S1" cols="20"></textarea><br />
                            <asp:Label ID="Label9" runat="server" Text="Envío a domicilo" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Font-Bold="True"></asp:Label>
                            <asp:RadioButton ID="RadioBtnNo" runat="server" Checked="True" GroupName="Radiosbtn" OnCheckedChanged="RadioBtnNo_CheckedChanged" Text="No" AutoPostBack="True" Font-Bold="True" />
                            <asp:RadioButton ID="RadioBtnSi" runat="server" GroupName="Radiosbtn" OnCheckedChanged="RadioBtnSi_CheckedChanged" Text="Si" Width="40px" AutoPostBack="True" Font-Bold="True" /><br />
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" placeholder="Ingrese Dirección" Visible="False"></asp:TextBox><br />
                            <asp:Label ID="Label3" runat="server" Text="Horario preferido para la entrega:" class="col-12 col-md-12 col-lg-12" Font-Bold="True" Visible="false"></asp:Label>
                            <br />
                            <asp:RadioButton ID="RadioBtnTarde" runat="server" Checked="True" GroupName="RadiosbtnE" Text="Entre 14 y 16 p.m." AutoPostBack="True" Font-Bold="True" Visible="false" />
                            <br />
                            <asp:RadioButton ID="RadioBtnNoche" runat="server" GroupName="RadiosbtnE" Text="Después de las 20 p.m." AutoPostBack="True" Font-Bold="True" Visible="false" /><br />
                            <asp:Label ID="lblFechaEstimadaEntrega" runat="server" class="col-12 col-md-12 col-lg-12" Font-Bold="True" Visible="false"></asp:Label>

                        </div>
                        <div id="divProductos" runat="server" visible="false">
                            <h5 class="col-md-12 col-lg-12 col-sm-12 col-xl-12">Agregar productos</h5>
                            <div class="form-row mt-2">
                                <asp:Label ID="Label6" runat="server" Text="Seleccionar producto" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Font-Bold="True"></asp:Label>
                                <asp:DropDownList ID="ListProductos1" runat="server" AutoPostBack="True" class="form-select col-6 col-md-6 col-lg-6 col-sm-6" OnSelectedIndexChanged="ListProductos1_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <asp:TextBox ID="txtCantidadProducto" runat="server" placeholder="Ingrese cantidad" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" AutoPostBack="True" OnTextChanged="txtCantidadProducto_TextChanged"></asp:TextBox>
                            <asp:TextBox ID="txtPrecioPedido" runat="server" placeholder="Total" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Enabled="False"></asp:TextBox>
                            <asp:Button ID="btnAgregarTodo" runat="server" OnClick="btnAgregarTodo_Click" Text="Agregar" CssClass="btn btn-outline-success col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Font-Bold="True" Enabled="False" />
                            <div class="table table-bordered mt-3">
                                <asp:Label ID="Label7" runat="server" class="col-6 col-md-6 col-lg-6" Font-Bold="True" Text="Productos seleccionados"></asp:Label>
                                <asp:GridView ID="GridViewProductos" EmptyDataText="No hay registros."
                                    AllowPaging="True" runat="server" OnRowEditing="GridViewProductos_RowEditing" OnRowUpdating="GridViewProductos_RowUpdated"
                                    OnRowCancelingEdit="GridViewProductos_RowCancelingEdit" class="table table-light table-striped table-hover col-md-12 col-lg-12 col-sm-12 col-xl-12" OnRowDeleting="GridViewProductos_OnRowDeleting" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="IdPedido" ItemStyle-Width="150" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdPedido" runat="server" Text='<%# Eval("IdPedido") %>' ReadOnly="True"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                        <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' Width="140"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField HeaderText="Acción" ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true" ControlStyle-Width="80" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div runat="server" id="divPedidoImagen" visible="false">
                            <asp:FileUpload ID="fuImagenes" class="custom-file bg-light mt-3 bg-light border border-dark" runat="server" AllowMultiple="false" />
                            <asp:Button ID="btnUpload" class="btn btn-outline-danger  col-md-12 col-lg-12 col-sm-12 col-12 mt-3" runat="server" OnClick="btnUpload_Click" Text="Subir" />

                            <asp:TextBox ID="txtURLs" Style="display: none;" runat="server"></asp:TextBox>
                            <asp:GridView ID="GridView1" runat="server" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-CssClass="col-6 p-0" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:ImageField DataImageUrlField="Value" ControlStyle-Width="40%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:Button ID="btnNuevoPedido" runat="server" OnClick="btnNuevoPedido_Click" CssClass="btn btn-outline-success col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Text="Generar pedido" Font-Bold="True" Visible="false" />
                        <asp:Button ID="btnActualizar" runat="server" OnClick="btnActualizar_Click" CssClass="btn btn-outline-success col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Text="Actualizar" Font-Bold="True" Visible="false" />
                        <asp:Button ID="btnAgregarProductos" runat="server" OnClick="btnAgregarProductos_Click" CssClass="btn btn-outline-success col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Text="Agregar Productos" Font-Bold="True" Visible="false" />
                        <asp:Button ID="btnCerrar" runat="server" CssClass="btn btn-outline-danger offset-3 col-md-5 col-lg-5 col-sm-5 col-md-5 col-xl-5 mt-3" OnClick="btnCerrar_Click" Text="Listado de pedidos" Visible="false" />
                        <asp:Timer ID="TimerMensajes" runat="server" Enabled="False" Interval="3000" OnTick="TimerMensajes_Tick" />
                    </div>
                </form>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
