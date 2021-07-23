<%@ Page Title="Login" Language="C#" MasterPageFile="~/PaginasMaestras/Account.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="GestOn2.Login" EnableSessionState="True" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/LoginCSS.css" rel="stylesheet" />
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
                            <h3 class="col-md-12 col-lg-12 col-sm-12 col-12 text-center">Ingreso al sistema</h3>
                            <div class="form-row">
                                <div class="form-group col-md-12 col-lg-12 col-12 col-sm-12">
                                    <asp:TextBox ID="txtEmail" placeholder="Correo electronico" class=" form-control offset-1 col-md-10 col-lg-10 col-10 col-sm-10 mt-2" TextMode="Email" runat="server"></asp:TextBox>
                                    <div class="form-row col-md-12 col-lg-11 col-11 col-sm-11">
                                    <asp:Label ID="lblResultado" class="alert alert-danger offset-1 col-md-12 col-lg-12 col-12 col-sm-12 mt-0 mb-2 mt-2 ml-4" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                    <asp:TextBox ID="txtPassUser" placeholder="Contraseña" type="password" CssClass="form-control offset-1 col-md-10 col-lg-10 col-10 col-sm-10 mt-2" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnIngresar" runat="server" class="btn btn-primary offset-1 col-md-10 col-lg-10 col-10 col-sm-10 mt-3" Text="Ingresar" OnClick="btnIngresar_Click" />
                                    <asp:LinkButton ID="lnkRestablecerContraseña" class="btn btn-link offset-1 col-md-10 col-lg-10 col-10 col-sm-10 mt-2" runat="server"> ¿ Olvidaste tu contraseña ?</asp:LinkButton>
                                    <br />
                                    <asp:Label ID="Label3" class="offset-1 col-md-6 col-lg-6 col-xl-6 col-sm-6 mt-2" runat="server" Text="No posee una cuenta?"></asp:Label>
                                    &nbsp;<asp:LinkButton ID="lnkRegistrarse" class="btn btn-link" runat="server" OnClick="lnkRegistrarse_Click">Registrarse</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
