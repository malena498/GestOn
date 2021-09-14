<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormOferta.aspx.cs" Inherits="GestOn2.ABMS.FormOferta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/OfertaCss.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <div id="DivFiltros" runat="server" class="form-row border border-info col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-2 ml-1 ">
                    <h1 class="col-md-12 col-lg-12 col-sm-12 col-xl-12">Selección de Filtros</h1>
                    <div class="col-md-3 col-lg-3 col-sm-3 col-xl-3 mt-3">
                        <asp:Label ID="Label12" runat="server" Text="Filtrar por: " class="col-2 col-md-2 col-lg-2" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="ddlSeleccionaFiltro" CssClass="form-select col-7 col-md-7 col-lg-7 col-sm-7" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSeleccionaFiltro_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Fechas</asp:ListItem>
                            <asp:ListItem>Titulo</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-xl-5 col-lg-4 col-md-4 col-sm-4 col-xs-4 mt-3 mb-3 p-0"  id="DivFiltroXFechas" runat="server">
                        <asp:TextBox ID="txtFchDesde" type="datetime" class="col-xs-6 col-lg-6 col-md-6 col-sm-6 col-xl-6 " runat="server" TextMode="Date"></asp:TextBox>
                        <asp:TextBox ID="txtFchHasta" type="datetime" class="col-xs-6 col-lg-6 col-md-6 col-sm-6 col-xl-6 " runat="server" TextMode="Date"></asp:TextBox>  
                    </div>
                    <div class="col-xl-5 col-lg-5 col-md-5 col-sm-5 col-xs-5 mt-3 mb-3" id="DivFiltroXTitulo" runat="server" visible="false">
                        <asp:TextBox ID="txtTitulo" placeholder="Nombre del titulo" class="col-8 col-lg-8 col-md-8 col-sm-8 col-xl-8 ml-2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 col-xs-2  mt-3 mb-3" id="Div3" runat="server">
                        <asp:LinkButton ID="btnBuscar" CssClass="btn btn-info col-md-2 col-lg-2 col-sm-2" OnClick="btnBuscar_Click" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 col-xs-2 mt-3" id="Div2" runat="server">
                        <asp:LinkButton ID="lnkNuevaOferta" runat="server" CssClass="btn btn-outline-danger col-xl-5 col-xs-5 col-sm-5 col-md-5 col-lg-5" OnClick="lnkNuevaOferta_Click"> Agregar Oferta </asp:LinkButton>
                    </div>
                    <asp:Label ID="lblResultadoBusqueda" Visible="false" class="alert alert-danger col-md-3 col-lg-3 col-sm-3 col-xl-3" runat="server"></asp:Label>
                </div>
            
            <div class="col-md-12 col-lg-12 col-sm-12 col-12 col-xl-12 ml-1" runat="server" id="div1">
                        <hr />
                        <asp:GridView ID="GridViewOferta" AutoGenerateColumns="False" EmptyDataText="No hay ofertas ingresadas."
                            AllowPaging="True" runat="server" DataKeyNames="IdOferta" OnRowDataBound="GridViewOferta_RowDataBound"
                            OnRowEditing="GridViewOferta_RowEditing" OnRowUpdating="GridViewOferta_RowUpdated"
                            OnRowCancelingEdit="GridViewOferta_RowCancelingEdit" class="table table-light table-striped table-hover col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-3" OnRowDeleting="GridViewOferta_OnRowDeleting" HorizontalAlign="Center" OnPageIndexChanging="GridViewOferta_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="IdOferta" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdOferta" runat="server" Text='<%# Eval("IdOferta") %>' ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Título" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitulo" runat="server" Text='<%# Eval("OfertaTitulo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTitulo" runat="server" Text='<%# Eval("OfertaTitulo") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha desde">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaDesde" runat="server" Text='<%# Eval("OfertaFechaDesde") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFechaDesde" runat="server" Text='<%# Eval("OfertaFechaDesde") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha hasta">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaHasta" runat="server" Text='<%# Eval("OfertaFechaHasta") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFechaHasta" runat="server" Text='<%# Eval("OfertaFechaHasta") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("OfertaDescripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("OfertaDescripcion") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activo">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActivo" runat="server" Checked='<%# Eval("Activo") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkActivo1" runat="server" Checked='<%# Eval("Activo") %>' Width="140"></asp:CheckBox>
                                    </EditItemTemplate>
                                    <ControlStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Precio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Eval("OfertaPrecio") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPrecio" runat="server" Text='<%# Eval("OfertaPrecio") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ControlStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accion">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" runat="server" class="btn btn-outline-success col-md-5 col-xl-5 col-lg-5 col-sm-5 m-0 text-center" CommandName="Edit">Editar</asp:LinkButton>
                                        <asp:Button ID="btnBorrar" runat="server" class="btn btn-outline-danger col-md-5 col-xl-5 col-lg-5 col-sm-5 m-0 text-center" CommandName="Delete" Text="Borrar"
                                            OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnActualizar" runat="server" CommandName="Update" Text="Actualizar" class="btn btn-outline-success col-md-5 col-xl-5 col-lg-5 col-sm-5 m-0 text-center"></asp:Button>
                                        <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar" class="btn btn-outline-danger col-md-5 col-xl-5 col-lg-5 col-sm-5 m-0 text-center"></asp:Button>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:GridView>
                        <asp:Label ID="lblResultadoGrilla" Visible="false" class="alert alert-danger col-md-12 col-lg-12 col-sm-12 col-xl-12" runat="server"></asp:Label>
                    </div>

            <div class="container-fluid col-4 col-lg-4 col-md-4 col-sm-4 col-xl-4 mt-3" runat="server" id="DivNewOferta" visible ="false">
                <div class="row bg-light border border-dark">
                    <div class="col-md-10 col-lg-10 col-sm-10 col-10 col-xl-10 ml-4" runat="server" id="divNuevaOferta">
                        <asp:TextBox ID="txtIdOferta" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" placeholder="Id oferta" runat="server" Visible=" false"></asp:TextBox>
                        <asp:TextBox ID="txtTituloOferta" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" placeholder="Titulo de la oferta" runat="server"></asp:TextBox>
                        <div class="form-row col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" runat="server" id="DivMensajeFormulario" visible="false">
                            <asp:Label ID="lblResultado" class="alert alert-danger col-md-12 col-lg-12 col-sm-12 col-xl-12" runat="server"></asp:Label>
                        </div>
                        <asp:TextBox ID="txtFechaDesde" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" runat="server" TextMode="Date"></asp:TextBox>
                        <asp:TextBox ID="txtFechaHasta" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" runat="server" TextMode="Date"></asp:TextBox>
                        <asp:TextBox ID="txtDescripcionOferta" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" placeholder="Descripción" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:TextBox ID="txtPrecio" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" placeholder="Precio" runat="server"></asp:TextBox>
                        <asp:FileUpload ID="fuImagenes" class="custom-file bg-light mt-3 bg-light border border-dark" runat="server" AllowMultiple="false" />
                        <asp:Button ID="btnUpload" class="btn btn-outline-success  col-md-12 col-lg-12 col-sm-12 col-12 mt-1" runat="server" OnClick="btnUpload_Click" Text="Subir" />
                        <asp:TextBox ID="txtURLs" Style="display: none;" runat="server"></asp:TextBox>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-CssClass="col-6 p-0" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:ImageField DataImageUrlField="Value" ControlStyle-Width="40%" />
                            </Columns>
                        </asp:GridView>
                        <div class="form-row col-md-12 col-lg-12 col-sm-12 col-xl-12 col-12 mt-2"></div>
                        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" CssClass="btn btn-info  col-md-12 col-lg-12 col-sm-12 col-xl-12 col-xs-12 mt-3" Text="Guardar" />
                        <asp:LinkButton ID="btnVerListado" runat="server" CssClass="btn btn-outline-danger col-xl-12 col-xs-12 col-sm-12 col-md-12 col-lg-12 mt-3 mb-3" OnClick="btnVerListado_Click"> Ver Listado </asp:LinkButton>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
