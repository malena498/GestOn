<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormOferta.aspx.cs" Inherits="GestOn2.ABMS.FormOferta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row" runat="server" id="div1">
                <div class="col-12" runat="server">
                    <asp:Label runat="server">Fecha Desde</asp:Label>
                    <asp:TextBox ID="txtFchDesde" runat="server" TextMode="DateTime"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Fecha hasta:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtFchHasta" runat="server" TextMode="DateTime"></asp:TextBox>
                    <br />
                    <asp:Label runat="server">Titulo</asp:Label>
                    <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Style="margin-left: 14px" Text="Buscar" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnNuevo" runat="server" Style="margin-left: 14px" Text="Nuevo" OnClick="btnNuevo_Click" />


                </div>
            </div>

            <hr />
            <asp:GridView ID="GridViewOferta" AutoGenerateColumns="False" EmptyDataText="No hay registros."
                AllowPaging="True" runat="server" DataKeyNames="UserId" OnRowDataBound="GridViewOferta_RowDataBound"
                OnRowEditing="GridViewOferta_RowEditing" OnRowUpdating="GridViewOferta_RowUpdated"
                OnRowCancelingEdit="GridViewOferta_RowCancelingEdit" OnRowDeleting="GridViewOferta_OnRowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="IdOferta" ItemStyle-Width="150" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblIdOferta" runat="server" Text='<%# Eval("IdOferta") %>' ReadOnly="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Título" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblTitulo" runat="server" Text='<%# Eval("OfertaTitulo") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTitulo" runat="server" Text='<%# Eval("OfertaTitulo") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha desde" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaDesde" runat="server" Text='<%# Eval("OfertaFechaDesde") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFechaDesde" runat="server" Text='<%# Eval("OfertaFechaDesde") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha hasta" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaHasta" runat="server" Text='<%# Eval("OfertaFechaHasta") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFechaHasta" runat="server" Text='<%# Eval("OfertaFechaHasta") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("OfertaDescripcion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("OfertaDescripcion") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Activo" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkActivo" runat="server" Checked='<%# Eval("Activo") %>'></asp:CheckBox>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkActivo1" runat="server" Checked='<%# Eval("Activo") %>' Width="140"></asp:CheckBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblPrecio" runat="server" Text='<%# Eval("OfertaPrecio") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPrecio" runat="server" Text='<%# Eval("OfertaPrecio") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Accion">
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" CommandName="Edit" Text="Editar" Width="45px"></asp:Button>
                            <asp:Button ID="btnBorrar" runat="server" CommandName="Delete" Text="Borrar" Width="50px"
                                OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                            <asp:Button ID="btnActualizar" runat="server" CommandName="Update" Text="Actualizar" Visible="false" Width="45px"></asp:Button>
                            <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar" Visible="false" Width="45px"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div class="container" runat="server" id="divNuevaOferta">
                <asp:TextBox ID="txtIdOferta" runat="server" Visible=" false"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="Titulo:"></asp:Label>
                <br />
                <asp:TextBox ID="txtTituloOferta" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Fecha desde:"></asp:Label>
                <br />
                <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="DateTime"></asp:TextBox>
                <br />
                <asp:Label ID="Label4" runat="server" Text="Fecha hasta:"></asp:Label>
                <br />
                <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="DateTime"></asp:TextBox>
                <br />
                <asp:Label ID="Label6" runat="server" Text="Descripción:"></asp:Label>
                <br />
                <asp:TextBox ID="txtDescripcionOferta" runat="server" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:Label ID="Label7" runat="server" Text="Precio:"></asp:Label>
                <br />
                <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Text="Imagenes:"></asp:Label>
                <br />
                <asp:FileUpload ID="fuImagenes" runat="server" AllowMultiple="false" />
                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Subir" />
                <asp:TextBox ID="txtURLs" Style="display: none;" runat="server"></asp:TextBox>
                <asp:GridView ID="GridView1" runat="server" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-CssClass="col-6 p-0" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:ImageField DataImageUrlField="Value" ControlStyle-Width="40%" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="lblResultado" runat="server"></asp:Label>
                <br />
                <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" />
                <asp:Button ID="btnEiminar" runat="server" OnClick="btnEiminar_Click" Style="margin-left: 20px" Text="Eliminar" />
                <asp:Button ID="btnModificar" runat="server" OnClick="btnModificar_Click" Style="margin-left: 15px" Text="Modificar" />
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
