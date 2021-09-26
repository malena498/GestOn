<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormDocumento.aspx.cs" Inherits="GestOn2.ABMS.FormDocumento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/font-awesome.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-row col-md-12 col-lg-12 col-sm-12 col-xl-12 ">
               <div id="DivGeneral" runat="server" class="form-row border border-info col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-2 p-3">
               <div id="DivFiltros" runat="server" class="form-row col-md-10 col-lg-10 col-sm-10 col-xl-10 mt-1 ">
                    <div class="col-md-4 col-lg-4 col-sm-4 col-xl-4 mt-3">
                        <asp:Label ID="Label12" runat="server" Text="Filtrar por: " class="col-2 col-md-2 col-lg-2" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="ddlSeleccionaFiltro" CssClass="form-select col-7 col-md-7 col-lg-7 col-sm-7" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSeleccionaFiltro_SelectedIndexChanged">
                        <asp:ListItem Value="Fecha">Fecha</asp:ListItem>
                        <asp:ListItem Value="NombreDocumento">Nombre de documento</asp:ListItem>
                        <asp:ListItem>Practico</asp:ListItem>
                        <asp:ListItem Value="NombreUsuario">Nombre de usuario</asp:ListItem>
                        <asp:ListItem Value="NumeroCarpeta">Numero de Carpeta</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-xs-4 mt-3 mb-3" id="DivFiltroPorUsuario"  runat="server" visible ="false">
                    <asp:DropDownList ID="ddlDocumentoNombreUsuario" runat="server" AutoPostBack="True" class="form-select col-10 col-md-10 col-lg-10 col-sm-10 mb-3" OnSelectedIndexChanged="ListPedidoUsuario_SelectedIndexChanged">
                        <asp:ListItem>Todos</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                    <div id="DivFiltroPorDocumento" runat="server" class="col-xl-5 col-lg-5 col-md-5 col-sm-5 col-xs-5 mt-3 mb-3"  visible ="false">
                    <asp:TextBox ID="txtNombreDocumentoFiltro" placeholder="Nombre del documento" class="col-8 col-md-8 col-lg-8 col-sm-8 mb-3" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="btnBuscarFiltro" CssClass="btn btn-outline-success col-xl-3 col-xs-3 col-sm-3 col-md-3 col-lg-3" runat="server" OnClick="btnBuscarFiltro_Click">Buscar</asp:LinkButton>
                    </div>
                    <div id="DivFiltroPorPractico" runat="server" class="col-xl-5 col-lg-5 col-md-5 col-sm-5 col-xs-5 mt-3 mb-3" visible ="false">
                    <asp:Label ID="Label6" runat="server" Text="Practico" class="col-2 col-md-2 col-lg-2" Font-Bold="True"></asp:Label>
                    <asp:CheckBox ID="chkEsPracticoFiltro" runat="server" AutoPostBack="True" OnCheckedChanged="chkEsPracticoFiltro_CheckedChanged" />
                    </div>
                    <div id="DivFiltroPorNroCarpeta" runat="server" class="col-xl-5 col-lg-5 col-md-5 col-sm-5 col-xs-5 mt-3 mb-3" visible ="false">
                    <asp:TextBox ID="txtNroCarpeta" placeholder="Numero de carpeta" TextMode="number" class="col-5 col-md-5 col-lg-5 col-sm-5 mb-3" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="lnkPorNroCarpeta" CssClass="btn btn-outline-success col-lg-3 col-xl-3 col-md-3 col-sm-3" runat="server" OnClick="btnBuscarNroCarpeta_Click">Buscar</asp:LinkButton>
                    </div>
                    <div id="DivFiltroPorFecha" runat="server" class="col-xl-5 col-lg-5 col-md-5 col-sm-5 col-xs-5 mt-3 mb-3">
                    <asp:TextBox ID="txtFchDesde"  placeholder="14/07/2021" class="col-xs-6 col-lg-6 col-md-6 col-sm-6 col-xl-6 ml-1" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:TextBox ID="txtFchHasta"  placeholder="14/08/2021" class="col-xs-6 col-lg-6 col-md-6 col-sm-6 col-xl-6 ml-1" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:LinkButton ID="btnBuscarFecha" CssClass="btn btn-outline-success col-lg-3 col-xl-3 col-md-3 col-sm-3" runat="server" OnClick="btnBuscarFecha_Click">Buscar</asp:LinkButton>
                    </div>
                    <div class=" col-lg-3 col-md-3 col-sm-3 col-xs-3 mt-3 " id="Div2" runat="server">
                 </div>
                    </div>
                   <div id="Div1" runat="server" class="form-row col-md-2 col-lg-2 col-sm-2 col-xl-2 mt-1 ">
                    <asp:LinkButton ID="lnkNuevoProducto" CssClass="btn btn-outline-danger col-lg-12 col-xl-12 col-md-12 col-sm-12" runat="server" OnClick="lnkNuevoProducto_Click">Agregar documento</asp:LinkButton>
               </div>
                       </div>
            <div class="container-fluid col-md-4 col-lg-4 col-sm-4 col-4 col-xl-4 ml-3 mt-3" runat="server" visible="false" id="divNuevaOferta">
                <form id="form1">
                    <div class="row bg-light col-md-12 col-lg-12 col-sm-12 col-xl-12 ">
                        <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre del documento" class="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                        <asp:Label ID="lblMensaje" runat="server" class="alert alert-danger col-md-12 col-lg-12 col-md-12 col-sm-12 mt-3" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtDescripcion" runat="server" placeholder="Descripción" class="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" TextMode="MultiLine"></asp:TextBox>
                       
                        
                        <div class="form-row col-lg-12 col-sm-12 col-md-12 col-xl-12  mt-3" runat="server" id="divDocente" visible="false">
                             <asp:Label ID="Label7" runat="server" class="col-md-4 col-lg-4 col-sm-4 col-md-4 col-xl-4 mt-3" Text="Grado Liceal:"></asp:Label>
                            <asp:DropDownList ID="ddlCurso" class="form-select col-6 col-md-6 col-lg-6 col-sm-6" runat="server"></asp:DropDownList>
                            <asp:CheckBox ID="chkEsPractico" class="col-lg-4 col-sm-4 col-md-4 col-xl-4" runat="server" OnCheckedChanged="chkEsPractico_CheckedChanged" Text=" Es practico?" AutoPostBack="True" />
                            <asp:TextBox ID="txtNroPractico" placeholder="Número de práctico" class="col-md-8 col-lg-8 col-sm-8 col-md-8 col-xl-8" TextMode="number" runat="server" Visible="False"></asp:TextBox>
                        </div>

                        <div class="form-row col-lg-12 col-sm-12 col-md-12 col-xl-12  mt-3">
                            <asp:FileUpload ID="fuDocs" class="file col-md-12 col-lg-12 col-md-12 col-sm-12 mt-3" runat="server" AllowMultiple="false" />
                        <asp:Button ID="btnSubir" class="btn btn-outline-danger col-md-12 col-lg-12 col-md-12 col-sm-12 mt-3" runat="server" Text="Subir" OnClick="btnSubir_Click" />

                            <asp:CheckBox ID="chkEsEnvio" class="col-lg-4 col-sm-4 col-md-4 col-xl-4" runat="server" OnCheckedChanged="chkEsEnvio_CheckedChanged" Text=" Con envio?" AutoPostBack="True" />
                            <asp:TextBox ID="txtDireccion" placeholder="Dirección de envío" class="col-md-8 col-lg-8 col-sm-8 col-md-8 col-xl-8" runat="server" TextMode="MultiLine" Visible="False"></asp:TextBox>
                        </div>
                        <asp:CheckBox ID="chkColor" class="col-lg-6 col-sm-6 col-md-6 col-xl-6 mt-3" runat="server" Text="Impresion a color?" />
                        <asp:CheckBox ID="chkDobleFaz" class="col-lg-6 col-sm-6 col-md-6 col-xl-6 mt-3" runat="server" Text="Es doble faz?" />

                        <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col-md-12 col-lg-12 col-md-12 col-sm-12 mt-3" OnClick="btnUpload_Click" Text="Guardar" Enabled="False" />

                        <!-- Estos label son para guardar ruta y tipo de archivo del documento que selecciona el cliente-->
                        <asp:Label ID="lblrutaarchivo" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblTipoDoc" runat="server" Visible="False"></asp:Label>

                        <asp:LinkButton ID="lnkVerDocumentos" CssClass="btn btn-outline-danger col-lg-12 col-xl-12 col-md-12 col-xs-12 col-sm-12 mt-3" runat="server" OnClick="lnkVerDocumentos_Click"> Listar documentos</asp:LinkButton>
                    </div>
                </form>
            </div>
            <div id="DivGridDocumentos" class="col-md-12 col-lg-12 col-sm-12 col-12 col-xl-12" runat="server">
                
                <asp:GridView ID="GridViewDocumentos" AutoGenerateColumns="False" EmptyDataText="No tiene documentos ingresados."
                    AllowPaging="True" runat="server" DataKeyNames="IdDocumento" OnRowDataBound="GridViewDocumento_RowDataBound"
                    OnRowUpdating="GridViewDocumento_RowUpdated"
                    OnRowCancelingEdit="GridViewDocumento_RowCancelingEdit" OnRowEditing="GridViewDocumento_RowEditing" OnRowDeleting="GridViewDocumento_OnRowDeleting"
                    class="table table-light table-striped table-hover col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-3" HorizontalAlign="Center" OnPageIndexChanging="GridViewDocumentos_PageIndexChanging" PageSize="5">
                    <Columns>
                        <asp:TemplateField HeaderText="IdDocumento" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIdDocumento" runat="server" Text='<%# Eval("IdDocumento") %>' ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lblNombreDoc" runat="server" Text='<%# Eval("NombreDocumento") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNombreDoc" runat="server" Text='<%# Eval("NombreDocumento") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ControlStyle />
                            <ItemStyle HorizontalAlign="Left" Wrap="True" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripción">
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcionDoc" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescripcionDoc" runat="server" Text='<%# Eval("Descripcion") %>' Width="140"></asp:TextBox>
                            </EditItemTemplate>
                            <ControlStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Ingreso">
                            <ItemTemplate>
                                <asp:Label ID="lblFechaIngresoDoc" runat="server" Text='<%# Eval("FechaIngreso") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFechaIngresoDoc" runat="server" Text='<%# Eval("FechaIngreso") %>' Width="140"></asp:TextBox>
                            </EditItemTemplate>
                            <ControlStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="A color">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAColorDoc" runat="server" Checked='<%# Eval("AColor") %>'></asp:CheckBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkAColorDoc1" runat="server" Checked='<%# Eval("AColor") %>' Width="140"></asp:CheckBox>
                            </EditItemTemplate>
                            <ControlStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Doble Faz">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDobleFazDoc" runat="server" Checked='<%# Eval("EsDobleFaz") %>'></asp:CheckBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkDobleFazDoc1" runat="server" Checked='<%# Eval("EsDobleFaz") %>' Width="140"></asp:CheckBox>
                            </EditItemTemplate>
                            <ControlStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Es práctico">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEsPractico" runat="server" Checked='<%# Eval("EsPractico") %>'></asp:CheckBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkEsPractico1" runat="server" Checked='<%# Eval("EsPractico") %>'></asp:CheckBox>
                                <asp:TextBox ID="txtNumeroPracticoDoc" runat="server" Text='<%# Eval("NroPractico") %>' Visible="false"></asp:TextBox>
                            </EditItemTemplate>
                            <ControlStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Domicilio">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkADomicilio" runat="server" Checked='<%# Eval("esEnvio") %>'></asp:CheckBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkADomicilio1" runat="server" Checked='<%# Eval("esEnvio") %>'></asp:CheckBox>
                                <asp:TextBox ID="txtADomicilioDoc" runat="server" Text='<%# Eval("Direccion") %>' Visible="false"></asp:TextBox>
                            </EditItemTemplate>
                            <ControlStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accion">
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-outline-success col-md-5 col-xl-5 col-lg-5 col-sm-5" CommandName="Edit" Text="E"></asp:Button>
                                <asp:Button ID="btnBorrar" runat="server" CssClass="btn btn-outline-danger col-md-5 col-xl-5 col-lg-5 col-sm-5" CommandName="Delete" Text="X"
                                    OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-outline-success col-md-5 col-xl-5 col-lg-5 col-sm-5" CommandName="Update" Text="Actualizar"></asp:Button>
                                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-outline-danger col-md-5 col-xl-5 col-lg-5 col-sm-5" CommandName="Cancel" Text="Cancelar"></asp:Button>
                            </EditItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descargar" ItemStyle-HorizontalAlign="Center"  >
                    <ItemTemplate>
                        <asp:HyperLink ID="descarga" runat="server" NavigateUrl='<%# Eval("IdDocumento", "~/download.aspx?IdDoc={0}") %>'  Text ="Descargar">
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:GridView>
                <asp:Label ID="lblResultado" class="alert alert-danger col-lg-12 col-xl-12 col-md-12 col-sm-12" Visible="false" runat="server" Text=""></asp:Label>
            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnSubir" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
