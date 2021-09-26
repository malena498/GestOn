<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="Configuraciones.aspx.cs" Inherits="GestOn2.Configuraciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <script type="text/javascript">
    
        function alerta(mensaje) {
            alert('Error: ' + mensaje);
        }
    
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="contenedor" class="container col-md-4 col-lg-4 col-sm-4 col-4 col-xl-4 offset-4 mt-5" runat="server">
                <form id="form1">
                    <div class="row bg-light border border-dark">
                        <div class="col-12 col-md-12 col-lg-12">
                            <h3 class="col-md-12 col-lg-12 col-sm-12 col-12 text-center">Configuraciones</h3>
                            <div class="form-row">
                                <div class="form-group col-md-12 col-lg-12 col-12 col-sm-12">
                                    <asp:Label ID="Label1" runat="server" Text="E-mail empresa" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                                    <asp:Label ID="Label7" runat="server" Text="Contraseña" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                                    <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                                    <asp:Label ID="Label6" runat="server" Text="E-mail Administrador" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                                    <asp:TextBox ID="txtEmailAdmin" runat="server" TextMode="Email" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                                    <asp:Label ID="Label5" runat="server" Text="Telefono empresa" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>                                    
                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                                    <asp:Label ID="Label2" runat="server" Text="Link IG" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                                    <asp:TextBox ID="txtLinkIG" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                                    <asp:Label ID="Label3" runat="server" Text="Link Facebook" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                                    <asp:TextBox ID="txtLinkFB" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                                    <asp:Label ID="Label4" runat="server" Text="Costo envio a domicilio" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                                    <asp:TextBox ID="txtCostoEnvio" runat="server" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:TextBox>
                                    
                                </div>
                                    <asp:Button ID="btnGuardar" runat="server" class="btn btn-primary offset-1 col-md-10 col-lg-10 col-10 col-sm-10 mt-3" Text="Ingresar" OnClick="btnGuardar_Click" />
                                    <asp:Label ID="lblInformativo" runat="server" Text="" CssClass="col-md-12 col-lg-12 col-sm-12 col-md-12 col-xl-12 mt-3"></asp:Label>
                                    <asp:LinkButton ID="lnkRespaldo" runat="server" OnClick="button1_Click" >Realizar respaldo</asp:LinkButton>

                            </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
            
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
