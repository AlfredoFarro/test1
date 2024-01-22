<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ingresar.aspx.cs" Inherits="TamizajePortal.Usuario.Ingresar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="DivPrincipal">
    <asp:Login ID="login" runat="server" 
        FailureText="Fallo el Ingreso, Intente Nuevamente" LoginButtonText="Ingresar" 
        PasswordLabelText="Contraseña:" 
        PasswordRequiredErrorMessage="Ingrese su Contraseña" 
        RememberMeText="Recuerdame" TitleText="" UserNameLabelText="Usuario:" 
        UserNameRequiredErrorMessage="Ingrese su Usuario">
    </asp:Login>
</div>
</asp:Content>
