<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="GestOn2.Inicio" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" EnableSessionState="True">
    <link href="../Styles/InicioCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid m-0">
                <br>
                <div id="myCarousel" class="carousel slide" data-ride="carousel">
                    <ol class="carousel-indicators">
                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                        <li data-target="#myCarousel" data-slide-to="1"></li>
                        <li data-target="#myCarousel" data-slide-to="2"></li>
                    </ol>

                    <div class="carousel-inner" role="listbox">
                        <asp:Repeater ID="repetidor" runat="server">
                            <ItemTemplate>
                                <div class="item <%#iterarFotos(Container.ItemIndex) %>">
                                    <asp:Image ID="Image" runat="server" ImageUrl='<%# Container.DataItem %>' Height="300px" Width="100%" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                        <span class="sr-only">Anterior</span>
                    </a>
                    <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
                        <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                        <span class="sr-only">Siguiente</span>
                    </a>
                </div>
            </div>
            
            <footer class="fixed-bottom bg-primary"role="contentinfo">
                            <div class="col-md-3 col-sm-6 offset-3 mb-2">
                                    <h4 class="text-center">Sobre nosotros</h4>
                                    <ul class="list-inline">
                                        <li><a href="#">Manual de uso</a></li>
                                        <li><a href="https://www.google.com.uy/maps/place/Calle+18+de+Julio+684,+60000+Paysand%C3%BA,+Departamento+de+Paysand%C3%BA/@-32.3161108,-58.0951473,17z/data=!3m1!4b1!4m5!3m4!1s0x95afcbfe98dfd89f:0xa1ff470573cf0d0a!8m2!3d-32.3161154!4d-58.0929586" target="_blank">Ubicación</a></li>
                                        <li><a href="#">Quienes Somos?</a></li>
                                    </ul>
                            </div>
                                <h4>Seguínos</h4>
                                <ul class="social-network social-circle">
                                    <li><a href="https://www.facebook.com/Bertinat-Papeler%C3%ADa-105499617646440/" target="_blank" class="icoFacebook" title="Facebook"><i class="fa fa-facebook"></i></a></li>
                                    <li><a href="https://www.instagram.com/bertinatpapeleria/?hl=es-la" target="_blank" class="icoInstagram" title="Instagram"><i class="fa fa-instagram"></i></a></li>
                                </ul>
                            <div class="col-md-12 colxl-12 col-lg-12 col-sm-12 col-xs-12 copy">
                                <p class="text-left">&copy; Copyright 2021 - Bertinat Papelería.</p>
                            </div>
            </footer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
