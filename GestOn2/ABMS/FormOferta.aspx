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
            <asp:GridView ID="GridViewOfertas" AutoGenerateColumns="False" EmptyDataText="No data available."
                AllowPaging="True" runat="server" DataKeyNames="IdOferta">
                <Columns>
                    <asp:BoundField HeaderText="IdOferta" DataField="IdOferta" Visible="false" />
                    <asp:BoundField HeaderText="Titulo" DataField="OfertaTitulo" />
                    <asp:BoundField HeaderText="Descripcion" DataField="OfertaDescripcion" />
                    <asp:BoundField HeaderText="Fecha desde" DataField="OfertaFechaDesde" />
                    <asp:BoundField HeaderText="Fecha hasta" DataField="OfertaFechaHasta" />
                    <asp:BoundField HeaderText="Activo" DataField="Activo" />
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
