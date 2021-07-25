<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormDocumento.aspx.cs" Inherits="GestOn2.ABMS.FormDocumento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            font-weight: bold;
            font-size: medium;
        }
    </style>
    <link href="../Content/font-awesome.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-4 col-lg-4 col-sm-4 col-4 col-xl-4 ml-3 mt-3" runat="server" id="divNuevaOferta">
                        <form id="form1">
                            <div class="row bg-light col-md-12 col-lg-12 col-sm-12 col-xl-12 ">
                                <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre del documento" class="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                                <asp:Label ID="lblMensaje" runat="server" class="alert alert-danger col-md-12 col-lg-12 col-md-12 col-sm-12 mt-3" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtDescripcion" runat="server" placeholder="Descripción" class="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" TextMode="MultiLine"></asp:TextBox>
                                <asp:Label ID="Label7" runat="server" class="col-md-4 col-lg-4 col-sm-4 col-md-4 col-xl-4 mt-3" Text="Grado Liceal:"></asp:Label>
                                <asp:DropDownList ID="ddlGradoLiceal" runat="server" class="col-lg-8 col-sm-9 col-md-8 col-xl-8 mt-3">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                </asp:DropDownList>

                                <asp:FileUpload ID="fuDocs" class="file col-md-12 col-lg-12 col-md-12 col-sm-12 mt-3" runat="server" AllowMultiple="false" />
                                <asp:Button ID="btnSubir" class="btn btn-outline-danger col-md-12 col-lg-12 col-md-12 col-sm-12 mt-3" runat="server" Text="Subir" OnClick="btnSubir_Click" />

                                <div class="form-row col-lg-12 col-sm-12 col-md-12 col-xl-12  mt-3">
                                    <asp:CheckBox ID="chkEsPractico" class="col-lg-4 col-sm-4 col-md-4 col-xl-4" runat="server" OnCheckedChanged="chkEsPractico_CheckedChanged" Text=" Es practico?" AutoPostBack="True" />
                                    <asp:TextBox ID="txtNroPractico" placeholder="Número de práctico" class="col-md-8 col-lg-8 col-sm-8 col-md-8 col-xl-8" TextMode="number" runat="server" Visible="False"></asp:TextBox>
                                </div>

                                <div class="form-row col-lg-12 col-sm-12 col-md-12 col-xl-12  mt-3">
                                    <asp:CheckBox ID="chkEsEnvio" class="col-lg-4 col-sm-4 col-md-4 col-xl-4" runat="server" OnCheckedChanged="chkEsEnvio_CheckedChanged" Text=" Con envio?" AutoPostBack="True" />
                                    <asp:TextBox ID="txtDireccion" placeholder="Dirección de envío" class="col-md-8 col-lg-8 col-sm-8 col-md-8 col-xl-8" runat="server" TextMode="MultiLine" Visible="False"></asp:TextBox>
                                </div>
                                <asp:CheckBox ID="chkColor" class="col-lg-6 col-sm-6 col-md-6 col-xl-6 mt-3" runat="server" Text="Impresion a color?" />
                                <asp:CheckBox ID="chkDobleFaz" class="col-lg-6 col-sm-6 col-md-6 col-xl-6 mt-3" runat="server" Text="Es doble faz?" />

                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary col-md-12 col-lg-12 col-md-12 col-sm-12 mt-3" OnClick="btnUpload_Click" Text="Guardar" Enabled="False" />

                                <!-- Estos label son para guardar ruta y tipo de archivo del documento que selecciona el cliente-->
                                <asp:Label ID="lblrutaarchivo" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblTipoDoc" runat="server" Visible="False"></asp:Label>
                            </div>
                        </form>
                </div>
                <div class="col-md-8 col-lg-8 col-sm-8 col-8 col-xl-8 ml-1" runat="server">
                    <asp:Label ID="lblResultado" class="alert alert-danger col-lg-12 col-xl-12 col-md-12 col-sm-12" Visible="false" runat="server" Text=""></asp:Label>
                    <asp:GridView ID="GridViewDocumentos" AutoGenerateColumns="False" EmptyDataText="No tiene documentos ingresados."
                        AllowPaging="True" runat="server" DataKeyNames="IdDocumento" OnRowDataBound="GridViewDocumento_RowDataBound"
                        OnRowUpdating="GridViewDocumento_RowUpdated"
                        OnRowCancelingEdit="GridViewDocumento_RowCancelingEdit" OnRowEditing="GridViewDocumento_RowEditing" OnRowDeleting="GridViewDocumento_OnRowDeleting"
                        class="table table-light table-striped table-hover col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-3" HorizontalAlign="Center">
                        <Columns>
                            <asp:TemplateField HeaderText="IdDocumento" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdDocumento" runat="server" Text='<%# Eval("IdDocumento") %>' ReadOnly="True"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombreDoc" ControlStyle-CssClass="col-md-1 col-xl-1 col-lg-1 col-sm-1" runat="server" Text='<%# Eval("NombreDocumento") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNombreDoc" runat="server" Text='<%# Eval("NombreDocumento") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle CssClass="col-md-2 col-xl-2 col-lg-2 col-sm-2 align-content-center" />
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
                            <asp:TemplateField HeaderText="Accion" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditar" runat="server" CommandName="Edit" Text="Editar"></asp:Button>
                                    <asp:Button ID="btnBorrar" runat="server" CommandName="Delete" Text="Borrar" Width="50px"
                                        OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnActualizar" runat="server" CommandName="Update" Text="Actualizar" Width="45px"></asp:Button>
                                    <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar" Width="45px"></asp:Button>
                                </EditItemTemplate>
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnSubir" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
