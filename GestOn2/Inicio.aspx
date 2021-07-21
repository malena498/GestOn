<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="GestOn2.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" EnableSessionState="True">
    <link href="Styles/LoginCSS.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div runat="server" id="myCarousel" class="carousel slide vertical m-0 p-0" data-ride="carousel" style="overflow: hidden">
                <ol class="carousel-indicators">
                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                    <li data-target="#myCarousel" data-slide-to="1"></li>
                    <li data-target="#myCarousel" data-slide-to="2"></li>
                </ol>
                <div class="carousel-inner col-md-8 offset-2">
                    <div class="item active">
                        <asp:Image ID="img1" alt="Promocion 1" runat="server" ImageUrl="~/Imagenes/fondo1.jpg" AlternateText="Imagen no disponible" Height="300px" Width="100%" />
                    </div>

                    <div class="item">
                        <asp:Image ID="img2" alt="Promocion 2" runat="server" ImageUrl="~/Imagenes/fondo2.jfif" Style="width: 100%;" AlternateText="Imagen no disponible" Height="300px" Width="100%" />
                    </div>

                    <div class="item">
                        <asp:Image ID="img3" alt="Promocion 3" runat="server" ImageUrl="~/Imagenes/fondo3.png" AlternateText="Imagen no disponible" Height="300px" Width="100%" />
                    </div>
                </div>
                <a class="left carousel-control" data-target="#myCarousel" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left"></span>
                    <span class="sr-only">Anterior</span>
                </a>
                <a class="right carousel-control" data-target="#myCarousel" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right"></span>
                    <span class="sr-only">Siguiente</span>
                </a>
            </div>
            <footer class="mainfooter fixed-bottom bg-light" role="contentinfo">
                <div class="footer-middle">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-3 col-sm-6 offset-4">
                                <div class="footer-pad">
                                    <h4>Sobre nosotros</h4>
                                    <ul class="list-unstyled">
                                        <li><a href="#">Pagina web</a></li>
                                        <li><a href="https://www.google.com.uy/maps/place/Calle+18+de+Julio+684,+60000+Paysand%C3%BA,+Departamento+de+Paysand%C3%BA/@-32.3161108,-58.0951473,17z/data=!3m1!4b1!4m5!3m4!1s0x95afcbfe98dfd89f:0xa1ff470573cf0d0a!8m2!3d-32.3161154!4d-58.0929586" target="_blank">Ubicación</a></li>
                                        <li><a href="#">¿ Quienes Somos?</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <h4>Seguínos</h4>
                                <ul class="social-network social-circle">
                                    <li><a href="https://www.facebook.com/Bertinat-Papeler%C3%ADa-105499617646440/" target="_blank" class="icoFacebook" title="Facebook"><i class="fa fa-facebook"></i></a></li>
                                    <li><a href="https://www.instagram.com/bertinatpapeleria/?hl=es-la" target="_blank" class="icoInstagram" title="Instagram"><i class="fa fa-instagram"></i></a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="row bg-primary col-md-12 col-12">
                            <div class="col-md-12 copy">
                                <p class="text-center">&copy; Copyright 2021 - Bertinat Papelería.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
