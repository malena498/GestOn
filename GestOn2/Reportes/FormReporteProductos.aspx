<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormReporteProductos.aspx.cs" Inherits="GestOn2.Reportes.FormReporteProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <div id="DivFiltros" runat="server" class="form-row border border-info col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-1 ">
                    <h2 class="col-md-12 col-lg-12 col-sm-12 col-xl-12">Selección de Filtros</h2>
                    <div class="col-md-3 col-lg-3 col-sm-3 col-xl-3 mt-1">
                        <asp:Label ID="Label12" runat="server" Text="Filtrar por: " class="col-2 col-md-2 col-lg-2" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="ddlSeleccionaFiltro" CssClass="form-select col-7 col-md-7 col-lg-7 col-sm-7" runat="server">
                            <asp:ListItem Selected="True">Más compras</asp:ListItem>
                            <asp:ListItem>Más gastos</asp:ListItem>
                            <asp:ListItem>Producto más vendido</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            <div class="container-fluid bg-light col-xl-6 col-sm-6 col-lg-6 col-md-6 mt-3" font-family: Arial; font-size: medium; color: #FF0000" title="Usuarios que mas han comprado">
                <div  class="col-xl-12 col-sm-12 col-lg-12 col-md-12 mt-3 ml-3">
                <asp:TextBox ID="txtFecha1" CssClass="col-xl-5 col-sm-5 col-lg-5 col-md-5" runat="server" TextMode="Date"></asp:TextBox>
                <asp:TextBox ID="txtFecha2" CssClass="col-xl-5 col-sm-5 col-lg-5 col-md-5" runat="server" TextMode="Date"></asp:TextBox>
                <asp:LinkButton ID="btnGraficar" CssClass="btn btn-outline-success col-xl-1 col-sm-1 col-lg-1 col-md-1"  runat="server" OnClick="btnGraficar_Click"><i class="fa fa-search"></i></asp:LinkButton>
                <asp:Label ID="lblMensaje" CssClass="alert alert-danger col-xl-6 col-sm-6 col-lg-6 col-md-6" runat="server" Visible="false" Text="Label"></asp:Label>
                </div>
            <div  class="container-fluid col-xl-8 col-sm-8 col-lg-8 col-md-8 mt-3" contenteditable="inherit">
            <asp:Chart ID="GraficaUsuariosPedidos" CssClass="col-xl-12 col-lg-12 col-sm-12 col-md-12" runat="server" Height="400px" PaletteCustomColors="Red" TextAntiAliasingQuality="Normal" Width="500px">
                <Series>
                    <asp:Series Name="Series" Font="Microsoft YaHei, 9.75pt, style=Bold" LabelForeColor="Maroon">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderDashStyle="Dash" ShadowColor="255, 224, 192">
                    </asp:ChartArea>
                </ChartAreas>
                <BorderSkin PageColor="MistyRose" />
            </asp:Chart>
            </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
