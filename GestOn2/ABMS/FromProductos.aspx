<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FromProductos.aspx.cs" Inherits="GestOn2.ABMS.FromProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlNuevaCat" cssClass="" runat="server" Visible="False" style="position:absolute;align-content:flex-start; top: 266px; left: 160px; height: 30%; width: 30%; margin-top: 2px;" BackColor="#CC99FF" BorderColor="Black" BorderStyle="Solid">
                <asp:Button ID="btnClosePanel" runat="server" CssClass="btn-danger"  Height="25px" Text="X" Width="25px" style="float:right" Font-Size="12pt" ForeColor="Black" OnClick="btnClosePanel_Click"  />
                <br />
                <asp:Label ID="lbllll" runat="server" Text="Id Categoría:" Width="140px"></asp:Label>
                <asp:TextBox ID="txtIdCat" runat="server" Width="70px"></asp:TextBox>
                <asp:Button ID="btnBuscarCat" runat="server" OnClick="btnBuscarCat_Click" Text="Buscar" Width="130px" />
                <br />
                <asp:Label ID="lblCategoriasMsj" runat="server" Font-Size="14pt" CssClass="alert alert-danger" style="margin-left: 0px" Visible="False" Width="100%"></asp:Label>
                <br />
                <asp:Label ID="Label9" runat="server" Text="Nombre Categoría" Width="140px"></asp:Label>
                <asp:TextBox ID="txtNomCat" runat="server" Width="200px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnGuardarCategoria" runat="server" OnClick="btnGuardarCategoria_Click" style="margin-left: 140px" Text="Guardar" Width="100px" Height="33px" />
                <asp:Button ID="btnEliminarCategoria" runat="server" OnClick="btnEliminarCategoria_Click" Text="Eliminar" Width="100px" Height="33px" />
                <br />
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" style="position:absolute;align-content:flex-start; margin-left:10px; left: 55%; top: 24%; height: 492px; width:100%;" BackColor="#999999">
                <br />
                <asp:Label ID="lblInformativo" runat="server" Font-Size="14pt" ForeColor="#FF3300" style="margin-left: 0px" Visible="False" CssClass="alert-danger" Width="40%"></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" Font-Size="13pt" ForeColor="Black" Text="Nombre:" Width="140px" Font-Bold="True"></asp:Label>
                <asp:TextBox ID="txtNombreProducto" runat="server" Width="200px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Size="13pt" ForeColor="Black" Text="Precio compra:" Width="140px" Font-Bold="True"></asp:Label>
                <asp:TextBox ID="txtPrecioCompra" runat="server" Width="200px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label4" runat="server" Font-Size="13pt" ForeColor="Black" Text="Precio venta:" Width="140px" Font-Bold="True"></asp:Label>
                <asp:TextBox ID="txtPrecioVenta" runat="server" Width="200px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label6" runat="server" Font-Size="13pt" ForeColor="Black" Text="Marca:" Width="140px" Font-Bold="True"></asp:Label>
                <asp:TextBox ID="txtMarca" runat="server" Width="200px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label7" runat="server" Font-Size="13pt" Text="Cantidad:" Width="140px" Font-Bold="True"></asp:Label>
                <asp:TextBox ID="txtCantidad" runat="server" Width="200px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Font-Size="13pt" ForeColor="Black" Text="Categoría:" Width="140px" Font-Bold="True"></asp:Label>
                <asp:DropDownList ID="lstCategorias" runat="server" Width="200px">
                </asp:DropDownList>
                <br />
                <asp:LinkButton ID="linkNewCategoria" runat="server" OnClick="linkNewCategoria_Click" style="margin-left: 140px" Width="200px">Nueva Categoría</asp:LinkButton>
                <br />
                <br />
                <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" style="margin-left: 120px" Text="Guardar" Width="100px" />
                <br />
                <br />
            </asp:Panel>
            </hr>
            <asp:GridView ID="GridViewProductos" AutoGenerateColumns="False" EmptyDataText="No hay registros."
                AllowPaging="True" runat="server" DataKeyNames="ProductoId" OnRowDataBound="GridViewProductos_RowDataBound"
                OnRowEditing="GridViewProductos_RowEditing" OnRowUpdating="GridViewProductos_RowUpdated"
                OnRowCancelingEdit="GridViewProductos_RowCancelingEdit" OnRowDeleting="GridViewProductos_OnRowDeleting" OnPageIndexChanging="GridViewProductos_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="IdProducto" ItemStyle-Width="150" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblIdProducto" runat="server" Text='<%# Eval("ProductoId") %>' ReadOnly="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("ProductoNombre") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNombre" runat="server" Text='<%# Eval("ProductoNombre") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Marca" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblMarca" runat="server" Text='<%# Eval("ProductoMarca") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMarca" runat="server" Text='<%# Eval("ProductoMarca") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Categoria" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblCategoria" runat="server" Text='<%# Eval("Categoria.NombreCategoria") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropdownList ID="ddlCategoria" runat="server"  Width="140"></asp:DropdownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio Compra" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblPrecioCompra" runat="server" Text='<%# Eval("ProductoPrecioCompra") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPrecioCompra" runat="server" Text='<%# Eval("ProductoPrecioCompra") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio Venta" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lbloPrecioVenta" runat="server" Text='<%# Eval("ProductoPrecioVenta") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtoPrecioVenta" runat="server" Text='<%# Eval("ProductoPrecioVenta") %>' Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Accion" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" CommandName="Edit" Text="Editar" ></asp:Button>
                            <asp:Button ID="btnBorrar" runat="server" CommandName="Delete" Text="Borrar" Width="50px"
                                OnClientClick="return confirm('Esta seguro que deseea eliminar el registro?');"></asp:Button>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btnActualizar" runat="server" CommandName="Update" Text="Actualizar" Width="45px"></asp:Button>
                            <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" Text="Cancelar"  Width="45px"></asp:Button>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Timer ID="TimerMensajes" runat="server" Enabled="False" Interval="3000" OnTick="TimerMensajes_Tick">
            </asp:Timer>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
