<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FromProductos.aspx.cs" Inherits="GestOn2.ABMS.FromProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlNuevaCat" cssClass="" runat="server" Visible="False" style="position:absolute;align-content:flex-start; top: 318px; left: 67px; height: 60%; width: 40%;" BackColor="#CC99FF" BorderColor="Black" BorderStyle="Solid">
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
            <asp:Panel ID="Panel2" runat="server" style="position:absolute;align-content:flex-start; margin-left:10px; left: 50%; top: 0%; height: 492px; width:100%;" BackColor="#999999">
                <asp:Label ID="Label1" runat="server" Font-Size="13pt" ForeColor="Black" Text="Id:" Width="140px" Font-Bold="True"></asp:Label>
                <asp:TextBox ID="txtIdProducto" runat="server" Width="70px"></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" Width="130px" />
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
                <asp:Button ID="btnModificar" runat="server" Enabled="False" OnClick="btnModificar_Click" Text="Modificar" Width="100px" />
                <asp:Button ID="btnEliminar" runat="server" Enabled="False" OnClick="btnEliminar_Click" Text="Eliminar" Width="100px" />
                <br />
                <br />
            </asp:Panel>
            <asp:GridView  ID="gridProductos" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Height="100px" Width="700px">
                <Columns>
                    <asp:BoundField DataField="ProductoId" HeaderText="ID" HeaderStyle-Width ="10%" >
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductoNombre" HeaderText="NOMBRE" />
                    <asp:BoundField DataField="ProductoMarca" HeaderText="MARCA" />
                    <asp:BoundField DataField="IdCategoria" HeaderText="CATEGORÍA" />
                    <asp:BoundField DataField="Cantidad" HeaderText="CANTIDAD" />
                    <asp:BoundField DataField="ProductoPrecioCompra" HeaderText="PRECIO COMPRA" />
                    <asp:BoundField DataField="ProductoPrecioVenta" HeaderText="PRECIO VENTA" />
                </Columns>
                <HeaderStyle CssClass="bg-danger" Height="30px" BorderColor="Black" BorderStyle="Solid" BorderWidth="3px" />
                <PagerStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="3px" />
                <RowStyle Cssclass="alert-danger" BorderColor="Black" BorderStyle="Solid" BorderWidth="3px" />
            </asp:GridView>
<br />
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
