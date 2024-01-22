<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="LoginAux.aspx.cs" Inherits="TamizajePortal.Usuario.LoginAux" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%--<%@ Import Namespace="System.Activities.Statements" %>--%>
<%@ Import Namespace="System.Web.Security" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="accountInfo">
        <table style="width: 100%;">
            
            <tr>
                <td>
                    <span>Usuario:</span></td>
                <td>
                    
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="textoMedio" ValidationGroup="login"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Ingrese su Usuario" Font-Bold="True" ForeColor="Red" ValidationGroup="login">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><span>Contraseña:</span></td>
                <td>
                    <asp:TextBox ID="txtContrasena" runat="server" CssClass="textoMedio" TextMode="Password" ValidationGroup="login"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContrasena" ErrorMessage="Ingrese su Contraseña" Font-Bold="True" ForeColor="Red" ValidationGroup="login">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Button ID="btnIngresar" runat="server" CssClass="BotonAccion" Text="Ingresar" OnClick="btnIngresar_Click" ValidationGroup="login" /></td>
            </tr>
            
        </table>
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Reportes/ResultadosPaciente.aspx" >Consultar Resultados Paciente</asp:LinkButton>
    </div> 
    <br /> 
    <div class="sumaryList">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
            ValidationGroup="login" ShowMessageBox="True" ShowSummary="False" 
            ToolTip="Lista Errores" />
    </div>
</asp:Content>
