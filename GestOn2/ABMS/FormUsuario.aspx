<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormUsuario.aspx.cs" Inherits="GestOn2.FormUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container" runat="server">
                <form class="form-control">
                    <asp:Label runat="server">Id</asp:Label>
                    <asp:TextBox ID="txtIdUsuario" runat="server"></asp:TextBox>
                    <asp:Label runat="server">Nombre</asp:Label>
                    <asp:TextBox ID="txtNomUsuario" runat="server"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" class="btn btn-info" Style="margin-left: 14px" Text="Buscar" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnNuevo" runat="server" class="btn btn-primary" Style="margin-left: 14px" Text="Nuevo" OnClick="btnNuevo_Click" />
                </form>
            </div>
            <hr />
            <asp:GridView ID="GridViewUsuarios" AutoGenerateColumns="False" EmptyDataText="No hay registros."
                AllowPaging="True" runat="server" DataKeyNames="UserId" OnRowDataBound="GridViewUsuarios_RowDataBound"
                OnRowEditing="GridViewUsuarios_RowEditing" OnRowUpdating="GridViewUsuarios_RowUpdated"
                OnRowCancelingEdit="GridViewUsuarios_RowCancelingEdit" OnRowDeleting="GridViewUsuarios_OnRowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="IdUsuario" ItemStyle-Width="150" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblIdUsuario" runat="server" Text='<%# Eval("UserId") %>' ReadOnly="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("UserNombre") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNombre" runat="server" Text='<%# Eval("UserNombre") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="E-mail" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("UserEmail") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("UserEmail") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Documento" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblDocumento" runat="server" Text='<%# Eval("UserCedula") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDocumento" runat="server" Text='<%# Eval("UserCedula") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teléfono" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblTeléfono" runat="server" Text='<%# Eval("UserTelefono") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTeléfono" runat="server" Text='<%# Eval("UserTelefono") %>' Width="140"></asp:TextBox>
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
                    <asp:TemplateField HeaderText="Rol" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblNivel" runat="server" Text='<%# Eval("nivel.NombreNivel") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlNivel" runat="server" Width="140"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Accion" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" CommandName="Edit" Text="Editar" ></asp:Button>
                            <asp:Button ID="btnBorrar" runat="server" CommandName="Delete" Text="Borrar" Width="50px"
                                OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                            <asp:Button ID="btnActualizar" runat="server" CommandName="Update" Text="Actualizar" Visible="false" Width="45px"></asp:Button>
                            <asp:Button ID="btnCancelar" runat="server" CommandName="Cancelar" Text="Cancelar" Visible="false"  Width="45px"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div class="container" runat="server" id="divNuevoUsuario">
                <asp:Label ID="Label7" runat="server" Text="Id"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label>
                <br />
                <asp:TextBox ID="txtNombreUser" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label2" runat="server" Text="E-Mail"></asp:Label>
                <br />
                <asp:TextBox ID="txtEmailUser" runat="server" TextMode="Email"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label3" runat="server" Text="Documento"></asp:Label>
                <br />
                <asp:TextBox ID="txtCedulaUser" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label4" runat="server" Text="Teléfono"></asp:Label>
                <br />
                <asp:TextBox ID="txtTelefonoUser" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Text="Contraseña"></asp:Label>
                <br />
                <asp:TextBox ID="txtPassUser" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label6" runat="server" Text="Categoría"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlCategoriaUsuario" runat="server">
                </asp:DropDownList>
                <br />
                <br />
                <asp:Label ID="lblResultado" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" />
                <asp:Button ID="btnModificar" runat="server" OnClick="btnModificar_Click" Text="Modificar" />
                <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Text="Eliminar" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
