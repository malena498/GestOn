<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormUsuario.aspx.cs" Inherits="GestOn2.FormUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <div id="DivFiltros" runat="server" class="form-row border border-info col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-2">
                    <h1 class="col-md-12 col-lg-12 col-sm-12 col-xl-12">Selección de Filtros</h1>
                    <div class="col-xl-5 col-lg-5 col-md-5 col-sm-5 col-xs-5">
                    <asp:Label ID="Label8" runat="server" Text="Nombre Usuario" class="col-4 col-md-4 col-lg-4" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtNomUsuario"  class="col-5 col-md-5 col-lg-5 col-sm-5 mb-5" runat="server"></asp:TextBox>
                    <asp:Button ID="btnBuscar" CssClass="btn btn-outline-success col-xl-3 col-xs-3 col-sm-3 col-md-3 col-lg-3" runat="server" OnClick="btnBuscar_Click" Text ="Buscar"/>
                    </div>
                    <div class="col-xl-5 col-lg-5 col-md-5 col-sm-5 col-xs-5">
                    <asp:Label ID="Label9" runat="server" Text="Cedula" class="col-4 col-md-4 col-lg-4 ml-4" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtCedulaFiltro" TextMode="number" class="col-4 col-md-4 col-lg-4 col-sm-4 mb-4" runat="server"></asp:TextBox>
                    <asp:Button ID="btnBuscarCedula" CssClass="btn btn-outline-success col-xl-3 col-xs-3 col-sm-3 col-md-3 col-lg-3" runat="server" OnClick="btnBuscarCedula_Click" Text ="Buscar cedula"/>
                    <asp:LinkButton ID="lnkNuevoUsuario" runat="server" CssClass="btn btn-outline-danger col-xl-3 col-xs-3 col-sm-3 col-md-3 col-lg-3" OnClick="lnkNuevoUsuario_Click"> Agregar usuario </asp:LinkButton>
                    </div>
                </div>
            <hr />
            <div id="DivGridUsuario" class="col-md-12 col-lg-12 col-sm-12 col-12 col-xl-12" runat="server">
            <asp:GridView ID="GridViewUsuarios" AutoGenerateColumns="False" EmptyDataText="No hay registros."
                AllowPaging="True" runat="server" DataKeyNames="UserId" OnRowDataBound="GridViewUsuarios_RowDataBound"
                OnRowEditing="GridViewUsuarios_RowEditing" OnRowUpdating="GridViewUsuarios_RowUpdated" class="table table-light table-striped table-hover col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-3"
                OnRowCancelingEdit="GridViewUsuarios_RowCancelingEdit" OnRowDeleting="GridViewUsuarios_OnRowDeleting" OnPageIndexChanging="GridViewUsuarios_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="IdUsuario" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblIdUsuario" runat="server" Text='<%# Eval("UserId") %>' ReadOnly="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre">
                        <ItemTemplate>
                            <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("UserNombre") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNombre" runat="server" Text='<%# Eval("UserNombre") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="E-mail" >
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("UserEmail") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("UserEmail") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Documento">
                        <ItemTemplate>
                            <asp:Label ID="lblDocumento" runat="server" Text='<%# Eval("UserCedula") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDocumento" runat="server" Text='<%# Eval("UserCedula") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teléfono">
                        <ItemTemplate>
                            <asp:Label ID="lblTeléfono" runat="server" Text='<%# Eval("UserTelefono") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTeléfono" runat="server" Text='<%# Eval("UserTelefono") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Activo">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkActivo" runat="server" Checked='<%# Eval("Activo") %>'></asp:CheckBox>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkActivo1" runat="server" Checked='<%# Eval("Activo") %>'></asp:CheckBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rol">
                        <ItemTemplate>
                            <asp:Label ID="lblNivel" runat="server" Text='<%# Eval("nivel.NombreNivel") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlNivel" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Accion">
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" CommandName="Edit" Text="Editar" CssClass="btn btn-outline-success col-md-5 col-xl-5 col-lg-5 col-sm-5" ></asp:Button>
                            <asp:Button ID="btnBorrar" runat="server" CommandName="Delete" Text="Borrar" CssClass="btn btn-outline-danger col-md-5 col-xl-5 col-lg-5 col-sm-5"
                                OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btnActualizar" runat="server" CommandName="Update" Text="Actualizar" CssClass="btn btn-outline-success col-md-5 col-xl-5 col-lg-5 col-sm-5"></asp:Button>
                            <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar" CssClass="btn btn-outline-danger col-md-5 col-xl-5 col-lg-5 col-sm-5"></asp:Button>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                </div>
            <div id="divNuevoUsuario" visible="false" class="container-fluid col-md-4 col-lg-4 col-sm-4 col-4 col-xl-4" runat="server">
                <asp:Label ID="Label7" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Text="Id"></asp:Label>
                <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="Nombre" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                <asp:TextBox ID="txtNombreUser" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="E-Mail" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                <asp:TextBox ID="txtEmailUser" runat="server" TextMode="Email" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="Documento" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                <asp:TextBox ID="txtCedulaUser" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="Teléfono" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                <asp:TextBox ID="txtTelefonoUser" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" Text="Contraseña" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                <asp:TextBox ID="txtPassUser" runat="server" TextMode="Password" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:Label ID="Label6" runat="server" Text="Categoría" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                <asp:DropDownList ID="ddlCategoriaUsuario" runat="server" CssClass="active col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3">
                </asp:DropDownList>
                <asp:Label ID="lblResultado" CssClass="alert alert-danger col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Visible="false" runat="server"></asp:Label>
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-outline-success col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" OnClick="btnGuardar_Click" Text="Guardar" />

                <asp:Button ID="btnCerrar" runat="server" CssClass="btn btn-outline-danger offset-4 col-md-3 col-lg-4 col-sm-4 col-md-4 col-xl-4 mt-3" OnClick="btnCerrar_Click" Text="Ver listado" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
