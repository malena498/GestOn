<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarCuenta.aspx.cs" Inherits="GestOn2.CambiarContraseña" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="contenedor" class="container col-md-4 col-lg-4 col-sm-4 col-4 col-xl-4 offset-4 mt-5" runat="server">
                <form id="form1">
                    <div class="row bg-light border border-dark">
                        <div class="col-12 col-md-12 col-lg-12">
                            <h3 class="col-md-12 col-lg-12 col-sm-12 col-12 text-center">Administrar cuenta</h3>
                            <div class="form-row">
                                <div class="form-group col-md-12 col-lg-12 col-12 col-sm-12">
                                    <asp:Label ID="Label1" runat="server" Text="Nombre" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                                    <asp:TextBox ID="txtNombreUser" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                                    <asp:Label ID="Label2" runat="server" Text="E-Mail" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                                    <asp:TextBox ID="txtEmailUser" runat="server" TextMode="Email" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                                    <asp:Label ID="Label3" runat="server" Text="Documento" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                                    <asp:TextBox ID="txtCedulaUser" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                                    <asp:Label ID="Label4" runat="server" Text="Teléfono" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                                    <asp:TextBox ID="txtTelefonoUser" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>

                                    <asp:TextBox ID="txtContraseña" placeholder="Contraseña" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" TextMode="Password" runat="server"></asp:TextBox>
                                    <div class="form-row col-md-12 col-lg-11 col-11 col-sm-11">
                                        <asp:TextBox ID="txtConfirmarContraseña" placeholder="Confirmar ontraseña" type="password" TextMode="Password" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblResultado" class="alert alert-danger offset-1 col-md-12 col-lg-12 col-12 col-sm-12 mt-0 mb-2 mt-2 ml-4" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                    <asp:Button ID="btnGuardar" runat="server" class="btn btn-primary offset-1 col-md-10 col-lg-10 col-10 col-sm-10 mt-3" Text="Ingresar" OnClick="btnGuardar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
