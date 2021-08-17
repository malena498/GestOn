<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormUsuario.aspx.cs" Inherits="GestOn2.FormUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/UsuarioCss.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <div id="DivFiltros" runat="server" class="form-row border border-info col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-2">
                    <h1 class="col-md-12 col-lg-12 col-sm-12 col-xl-12">Selección de Filtros</h1>
                    <div class="col-md-3 col-lg-3 col-sm-3 col-xl-3 mt-4">
                        <asp:Label ID="Label12" runat="server" Text="Filtrar por: " CssClass="col-2 col-md-2 col-lg-2" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="ddlSeleccionaFiltro" CssClass="form-select col-7 col-md-7 col-lg-7 col-sm-7" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSeleccionaFiltro_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Nombre</asp:ListItem>
                            <asp:ListItem Value="Cedula">Cédula</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-xl-5 col-lg-5 col-md-5 col-sm-5 col-xs-5 mt-3"  id="DivFiltroXNombre" runat="server">
                        <asp:Label ID="Label8" runat="server" Text="Nombre Usuario" class="col-4 col-md-4 col-lg-4" Font-Bold="True"></asp:Label>
                        <asp:TextBox ID="txtNomUsuario"  class="col-5 col-md-5 col-lg-5 col-sm-5" runat="server"></asp:TextBox>
                        <asp:Button ID="btnBuscar" CssClass="btn btn-outline-success col-xl-3 col-xs-3 col-sm-3 col-md-3 col-lg-3 mb-2" runat="server" OnClick="btnBuscar_Click" Text ="Buscar"/>
                    </div>
                    <div class="col-xl-5 col-lg-5 col-md-5 col-sm-5 col-xs-5 mt-3" id="DivFiltroXCedula" runat="server" visible="false">
                        <asp:Label ID="Label9" runat="server" Text="Cedula" class="col-4 col-md-4 col-lg-4 ml-4" Font-Bold="True"></asp:Label>
                        <asp:TextBox ID="txtCedulaFiltro" TextMode="number" class="col-4 col-md-4 col-lg-4 col-sm-4" runat="server"></asp:TextBox>
                        <asp:Button ID="btnBuscarCedula" CssClass="btn btn-outline-success col-xl-3 col-xs-3 col-sm-3 col-md-3 col-lg-3 mb-2" runat="server" OnClick="btnBuscarCedula_Click" Text ="Buscar cedula"/>
                    </div>
                    <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 col-xs-2 mt-3" id="Div1" runat="server">
                        <asp:LinkButton ID="lnkNuevoUsuario" runat="server" CssClass="btn btn-outline-danger col-xl-12 col-xs-12 col-sm-12 col-md-12 col-lg-12" OnClick="lnkNuevoUsuario_Click"> Agregar usuario </asp:LinkButton>
                    </div>
                </div>
            <div id="DivGridUsuario" class="col-md-12 col-lg-12 col-sm-12 col-12 col-xl-12" runat="server">
            <hr />
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
             <asp:Label ID="lblResultadoGrilla" CssClass="alert alert-danger col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" Visible="false" runat="server"></asp:Label>
            </div>
            <div id="divNuevoUsuario" visible="false" class="form-control border border-dark bg-light container-fluid col-md-4 col-lg-4 col-sm-4 col-4 col-xl-4 mt-1" runat="server">
                <asp:TextBox ID="txtIdUsuario" placeholder="Id" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-1"></asp:TextBox>
                <div id="divMensaje" visible="false" class="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-2" runat="server">
                <asp:Label ID="lblResultado" CssClass="alert alert-danger col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12" runat="server"></asp:Label>
                </div>
                <asp:TextBox ID="txtNroCarpeta" placeholder="Número de carpeta" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" TextMode="number"></asp:TextBox>
                <asp:TextBox ID="txtNombreUser" placeholder="Nombre y Apellido" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:TextBox ID="txtEmailUser" placeholder="E-mail" runat="server" TextMode="Email" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:TextBox ID="txtCedulaUser" placeholder="Cédula de identidad" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:TextBox ID="txtTelefonoUser" placeholder="Télefono" runat="server" TextMode="SingleLine" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:TextBox ID="txtPassUser"  placeholder="Contraseña" runat="server" TextMode="Password" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:TextBox ID="txtPassUser2" placeholder="Repetir Contraseña" runat="server" TextMode="Password" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                <asp:DropDownList ID="ddlCategoriaUsuario" runat="server" CssClass="active col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3">
                </asp:DropDownList>
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-outline-success col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" OnClick="btnGuardar_Click" Text="Guardar" />
                <asp:Button ID="btnCerrar" runat="server" CssClass="btn btn-outline-danger col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" OnClick="btnCerrar_Click" Text="Ver listado" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
