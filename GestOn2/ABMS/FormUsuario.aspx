<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormUsuario.aspx.cs" Inherits="GestOn2.FormUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container" runat="server">
                <asp:Label runat="server">Id</asp:Label>                
                <asp:TextBox ID="txtIdUsuario" runat="server"></asp:TextBox>
                <asp:Label runat="server">Nombre</asp:Label>
                <asp:TextBox ID="txtNomUsuario" runat="server"></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" Style="margin-left: 14px" Text="Buscar" OnClick="btnBuscar_Click" />
                <asp:Button ID="btnNuevo" runat="server" Style="margin-left: 14px" Text="Nuevo" OnClick="btnNuevo_Click" />
            </div>
            <hr />
            <asp:GridView id="GridViewUsuarios"  autogeneratecolumns="False" emptydatatext="No data available." 
                allowpaging="True" runat="server" DataKeyNames="UserId">
                <Columns>
                    <asp:BoundField HeaderText="IdUsuario" DataField="UserId" Visible="false"/>
                    <asp:BoundField HeaderText="Nombre" DataField="UserNombre"/>
                     <asp:BoundField HeaderText="E-mail" DataField="UserEmail"/>
                     <asp:BoundField HeaderText="Documento" DataField="UserCedula"/>
                     <asp:BoundField HeaderText="Teléfono" DataField="UserTelefono"/>
                     <asp:BoundField HeaderText="Activo" DataField="Activo"/>
                     <asp:BoundField HeaderText="Rol" DataField="nivel.NombreNivel"/>
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
