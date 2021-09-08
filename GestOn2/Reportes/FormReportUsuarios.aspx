<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="FormReportUsuarios.aspx.cs" Inherits="GestOn2.Reportes.FormReportUsuarios" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivFiltros" runat="server" class="container col-md-12 col-lg-12 col-sm-12 col-xl-12 m-0 border bg-light">
                <div class="form-row border-top-0 col-md-12 col-lg-12 col-sm-12 col-xl-12 mt-1">
                    <div class="container-fluid border border-primary p-3 col-md-10 col-lg-10 col-sm-10 col-xl-10 mt-1">
                        <asp:Label ID="Label12" runat="server" Text="Filtrar por: " class="col-2 col-md-2 col-lg-2 m-2" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="ddlSeleccionaFiltro" CssClass="form-select p-1 col-4 col-md-4 col-lg-4 col-sm-4" runat="server" Autopostback="true" OnSelectedIndexChanged="ddlSeleccionaFiltro_SelectedIndexChanged">
                            <asp:ListItem Value="Documentos" Selected="True">Cliente que mas documentos envió</asp:ListItem>
                            <asp:ListItem Value="Productos">Usuario que realizó más pedidos</asp:ListItem>
                            <asp:ListItem Value="Ganancia">Usuarios que mas han gastado</asp:ListItem>
                            <asp:ListItem Value="ProductoVendido">Producto más vendido</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtFecha1" CssClass="col-xl-2 col-sm-2 col-lg-2 col-md-2 ml-3" runat="server" TextMode="Date"></asp:TextBox>
                        <asp:TextBox ID="txtFecha2" CssClass="col-xl-2 col-sm-2 col-lg-2 col-md-2" runat="server" TextMode="Date"></asp:TextBox>
                        <asp:LinkButton ID="btnGraficar" CssClass="btn btn-outline-success col-xl-1 col-sm-1 col-lg-1 col-md-1" runat="server" OnClick="btnGraficar_Click"><i class="fa fa-search"></i></asp:LinkButton><br />
                        <asp:Label ID="lblMensaje" CssClass="alert alert-danger col-xl-6 col-sm-6 col-lg-6 col-md-6  mb-2" runat="server" Visible="false" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="container-fluid col-xl-8 col-sm-8 col-lg-8 col-md-8 mt-3" contenteditable="inherit">
                    <asp:Chart ID="GraficaUsuariosPedidos" CssClass="col-xl-10 col-lg-10 col-sm-10 col-md-10" runat="server" Height="400px" PaletteCustomColors="Red" TextAntiAliasingQuality="Normal" Width="500px">
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
                    <hr size="2px" color="black" />
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
