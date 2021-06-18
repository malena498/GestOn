﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PaginasMaestras/PageMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GestOn2.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="Label1" runat="server" Text="Usuario"></asp:Label>
<br />
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
            <br />
<br />
            <asp:Label ID="Label2" runat="server" Text="Contraseña"></asp:Label>
<br />
            <asp:TextBox ID="txtPassUser" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnIngresar" runat="server" OnClick="btnIngresar_Click" Text="Ingresar" />
            <br />
            <br />
            <asp:Label ID="lblResultado" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="No posee una cuenta?"></asp:Label>
            &nbsp;<asp:LinkButton ID="lnkRegistrarse" runat="server" OnClick="lnkRegistrarse_Click">Registrarse</asp:LinkButton>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>