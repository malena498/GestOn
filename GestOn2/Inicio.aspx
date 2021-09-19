<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/Inicio.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="GestOn2.Inicio" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" EnableSessionState="True">
    <link href="../Styles/InicioCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid m-0 p-0">
                <div id="myCarousel" class="carousel slide m-0 p-0" data-ride="carousel">
                    <ol class="carousel-indicators">
                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                        <li data-target="#myCarousel" data-slide-to="1"></li>
                        <li data-target="#myCarousel" data-slide-to="2"></li>
                    </ol>
                    <div class="carousel-inner m-0 p-0" role="listbox">
                        <asp:Repeater ID="repetidor" runat="server">
                            <ItemTemplate>
                                <div class="carousel-item <%#iterarFotos(Container.ItemIndex) %>">
                                    <asp:Image ID="Image" runat="server" ImageUrl='<%# Container.DataItem %>' Height="350px" Width="100%" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <a class="carousel-control-prev" href="#myCarousel" role="button" data-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="sr-only">Anterior</span>
                    </a>
                    <a class="carousel-control-next" href="#myCarousel" role="button" data-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="sr-only">Siguiente</span>
                    </a>
                </div>
            </div>

            <footer class="page-footer font-small blue" role="contentinfo">
                <div class="form-row col-xl-12 col-lg-12 col-sm-12 col-md-12">
                    <div class=" col-xl-6 col-lg-6 col-sm-6 col-md-6">
                        <h4 class="text-center">Sobre nosotros</h4>
                        <ul class="offset-3 list-inline">
                            <li class="list-inline-item"><a target="_blank" href="#">Manual de uso</a></li>
                            <li class="list-inline-item"><a href="https://www.google.com.uy/maps/place/Calle+18+de+Julio+684,+60000+Paysand%C3%BA,+Departamento+de+Paysand%C3%BA/@-32.3161108,-58.0951473,17z/data=!3m1!4b1!4m5!3m4!1s0x95afcbfe98dfd89f:0xa1ff470573cf0d0a!8m2!3d-32.3161154!4d-58.0929586" target="_blank">Ubicación</a></li>
                            <li class="list-inline-item"><a target="_blank" href="#">Quienes somos?</a></li>
                        </ul>
                    </div>
                    <div class=" col-xl-4 col-lg-4 col-sm-4 col-md-4">
                        <h4>Encontranos también</h4>
                        <ul class="social-network social-circle offset-3">
                            <li><a href="https://www.facebook.com/Bertinat-Papeler%C3%ADa-105499617646440/" target="_blank" class="icoFacebook" title="Facebook"><i class="fa fa-facebook"></i></a></li>
                            <li><a href="https://www.instagram.com/bertinatpapeleria/?hl=es-la" target="_blank" class="icoInstagram" title="Instagram"><i class="fa fa-instagram"></i></a></li>
                        </ul>
                    </div>
                        <p class="text-left">&copy; Copyright 2021 - Bertinat Papelería.</p>
                </div>
            </footer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
