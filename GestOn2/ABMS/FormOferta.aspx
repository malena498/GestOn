<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormOferta.aspx.cs" Inherits="GestOn2.ABMS.FormOferta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/OfertaCss.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3">
                <div class="row">
                    <div class="col-md-3 col-lg-3 col-sm-3 col-3 col-xl-3 ml-3" runat="server" id="divNuevaOferta">

                        <asp:TextBox ID="txtIdOferta" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" placeholder="Id oferta" runat="server" Visible=" false"></asp:TextBox>

                        <asp:TextBox ID="txtTituloOferta" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" placeholder="Titulo de la oferta" runat="server"></asp:TextBox>

                        <div class="form-row col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3">
                            <asp:Label ID="lblResultado" Visible="false" class="alert alert-danger col-md-12 col-lg-12 col-sm-12 col-xl-12" runat="server"></asp:Label>
                        </div>

                        <asp:TextBox ID="txtFechaDesde" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" placeholder="Fecha inicio" runat="server" TextMode="DateTime"></asp:TextBox>

                        <asp:TextBox ID="txtFechaHasta" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" placeholder="Fecha fin" runat="server" TextMode="DateTime"></asp:TextBox>

                        <asp:TextBox ID="txtDescripcionOferta" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" placeholder="Descripción" runat="server" TextMode="MultiLine"></asp:TextBox>

                        <asp:TextBox ID="txtPrecio" class="col-12 col-lg-12 col-md-12 col-sm-12 col-xl-12 mt-3" placeholder="Precio" runat="server"></asp:TextBox>

                        <asp:FileUpload ID="fuImagenes" class="custom-file bg-light mt-3 bg-light border border-dark" runat="server" AllowMultiple="false" />
                        <asp:Button ID="btnUpload" class="btn btn-outline-danger  col-md-12 col-lg-12 col-sm-12 col-12 mt-3" runat="server" OnClick="btnUpload_Click" Text="Subir" />

                        <asp:TextBox ID="txtURLs" Style="display: none;" runat="server"></asp:TextBox>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-CssClass="col-6 p-0" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:ImageField DataImageUrlField="Value" ControlStyle-Width="40%" />
                            </Columns>
                        </asp:GridView>
                        <div class="form-row col-md-12 col-lg-12 col-sm-12 col-xl-12 col-12 mt-2"></div>
                        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" class="btn btn-info offset-md-1 offset-lg-1 offset-xl-1 offset-sm-1 col-md-5 col-lg-5 col-sm-5 col-5" Text="Guardar" />
                        <asp:Button ID="btnModificar" runat="server" OnClick="btnModificar_Click" class="btn btn-info  col-md-5 col-lg-5 col-sm-5 col-5 ml-1" Text="Modificar" />
                    </div>
                    <div class="col-md-8 col-lg-8 col-sm-8 col-8 col-xl-8 ml-4" runat="server" id="div1">
                            <asp:TextBox ID="txtFchDesde" placeholder="Fecha desde" class="col-lg-3 col-md-3 col-sm-3 col-xl-3 ml-3" runat="server" TextMode="DateTime"></asp:TextBox>
                            <asp:TextBox ID="txtFchHasta" placeholder="Fecha hasta" class="col-3 col-lg-3 col-md-3 col-sm-3 col-xl-3 ml-2" runat="server" TextMode="DateTime"></asp:TextBox>
                            <asp:TextBox ID="txtTitulo" placeholder="Nombre del titulo" class="col-3 col-lg-3 col-md-3 col-sm-3 col-xl-3 ml-2" runat="server"></asp:TextBox>
                            <asp:Button ID="btnBuscar" runat="server" class="btn btn-info col-md-2 col-lg-2 col-sm-2  ml-2" Text="Buscar" OnClick="btnBuscar_Click" />

                            <asp:Label ID="lblResultadoBusqueda" Visible="false" class="alert alert-danger col-md-8 col-lg-8 col-sm-8 col-xl-8" runat="server"></asp:Label>
                            <hr />
                            <asp:GridView ID="GridViewOferta" AutoGenerateColumns="False" EmptyDataText="No hay ofertas ingresadas."
                                AllowPaging="True" runat="server" DataKeyNames="IdOferta" OnRowDataBound="GridViewOferta_RowDataBound"
                                OnRowEditing="GridViewOferta_RowEditing" OnRowUpdating="GridViewOferta_RowUpdated"
                                OnRowCancelingEdit="GridViewOferta_RowCancelingEdit" class="table table-light table-striped table-hover col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-3" OnRowDeleting="GridViewOferta_OnRowDeleting" HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField HeaderText="IdOferta" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdOferta" runat="server" Text='<%# Eval("IdOferta") %>' ReadOnly="True"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Título" ControlStyle-CssClass="col-md-2 col-xl-2 col-lg-2 col-sm-2 align-content-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTitulo" runat="server" Text='<%# Eval("OfertaTitulo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtTitulo" runat="server" Text='<%# Eval("OfertaTitulo") %>' Width="140"></asp:TextBox>
                                        </EditItemTemplate>
                                        <controlstyle cssclass="col-md-2 col-xl-2 col-lg-2 col-sm-2 align-content-center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha desde" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaDesde" runat="server" Text='<%# Eval("OfertaFechaDesde") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFechaDesde" runat="server" Text='<%# Eval("OfertaFechaDesde") %>' Width="140"></asp:TextBox>
                                        </EditItemTemplate>
                                        <controlstyle  />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha hasta" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaHasta" runat="server" Text='<%# Eval("OfertaFechaHasta") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFechaHasta" runat="server" Text='<%# Eval("OfertaFechaHasta") %>' Width="140"></asp:TextBox>
                                        </EditItemTemplate>
                                        <controlstyle />
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
                                        <controlstyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrecio" runat="server" Text='<%# Eval("OfertaPrecio") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPrecio" runat="server" Text='<%# Eval("OfertaPrecio") %>' ></asp:TextBox>
                                        </EditItemTemplate>
                                        <controlstyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accion">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" class="btn btn-outline-success col-md-5 col-xl-5 col-lg-5 col-sm-5 m-0 text-center" CommandName="Edit"><i class="fas fa-edit"></i></asp:LinkButton>
                                            <%--<asp:Button ID="btnEditar" runat="server" class="btn btn-outline-success col-md-3 col-xl-3 col-lg-3 col-sm-3" CommandName="Edit" Text="Editar"></asp:Button>--%>
                                            <asp:Button ID="btnBorrar" runat="server" class="btn btn-outline-danger col-md-5 col-xl-5 col-lg-5 col-sm-5 m-0 text-center" CommandName="Delete" Text="X"
                                                OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                                            <asp:Button ID="btnActualizar" runat="server" CommandName="Update" Text="Actualizar" Visible="false" Width="45px"></asp:Button>
                                            <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar" Visible="false" Width="45px"></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
