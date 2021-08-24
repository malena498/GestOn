<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="GestOn2.Registrarse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container col-md-4 col-lg-4 col-sm-4 col-4 col-xl-4 offset-4 mt-3" runat="server">
                <form id="form1">
                    <div class="row bg-light border border-dark">
                        <div class="col-12 col-md-12 col-lg-12">
                            <h3 class="col-md-12 col-lg-12 col-sm-12 col-12 text-center">Registro al sistema</h3>
                            <div class="form-row">
                                <div class="form-group col-md-12">

                                    <asp:TextBox ID="txtNombre" placeholder="Nombre y apellido" class=" form-control col-md-12 col-lg-12 col-12 col-sm-12 mt-3" runat="server"></asp:TextBox>
                                    
                                    <div class="form-row mb-0 mt-3">
                                    <asp:Label ID="lblResultado" class="alert alert-danger col-md-12 col-lg-12 col-12 col-sm-12" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>

                                    <asp:TextBox ID="txtEmail" placeholder="Email" type="Email"  class="form-control mt-3" runat="server"></asp:TextBox>

                                    <asp:TextBox ID="txtDocumento" placeholder="Cédula" type="number"  class="form-control mt-3" runat="server"></asp:TextBox>

                                    <asp:TextBox ID="txtTelefono" placeholder="Número de celular" type="number"  class="form-control mt-3" runat="server"></asp:TextBox>

                                    <asp:TextBox ID="txtContrasenia" placeholder="Contraseña" type="password" TextMode="Password" class="form-control mt-3" runat="server"></asp:TextBox>

                                    <asp:TextBox ID="txtConfirmarContrasenia" placeholder="Repetir contraseña" type="password" TextMode="Password" class="form-control mt-3" runat="server"></asp:TextBox>

                                    <div class="form-row col-md-12 col-lg-12 col-12 col-sm-12 mt-3">
                                    <label class="col-5 col-md-5 col-lg-5 col-sm-5 mr-3" for="inputCity">Categoría de usuario</label>
                                    <asp:DropDownList ID="ddlCategoriaUsuario" class="form-select col-6 col-md-6 col-lg-6 col-sm-6" runat="server"></asp:DropDownList>
                                    </div>

                                    <asp:Button ID="btnRegistrarse" runat="server" class="btn btn-primary col-md-12 col-lg-12 col-12 col-sm-12 mt-3" OnClick="btnRegistrarse_Click" Text="Registrarse" />
                                   
                                    <div class="form-row col-md-12 col-lg-12 col-12 col-sm-12 mt-3">
                                    <asp:Label ID="Label10" class="col-md-6 col-lg-6 col-xl-6 col-sm-6 ml-5" runat="server" Text="Ya tienes una cuenta?"></asp:Label>
                                    &nbsp;<asp:LinkButton ID="lnkLogin" runat="server" class="btn btn-link" OnClick="lnkLogin_Click">Iniciar Sesión</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
