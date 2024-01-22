<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="TamiLifeSA.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Inicio de sesión
    </h2>
    <%--<table style="width: 100%;">
        <tr>
            <td>--%>
                <p>
                    Por favor ingrese su usuario y contraseña.
                    <%--<asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink> if you don't have an account.--%>
                </p>
                <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
                    <LayoutTemplate>
                        <span class="failureNotification">
                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                        </span>
                        <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                            ValidationGroup="LoginUserValidationGroup" />
                        <div class="accountInfo">
                            <fieldset class="login">
                                <legend>Información de usuario</legend>
                                <p>
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Usuario:</asp:Label>
                                    <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        CssClass="failureNotification" ErrorMessage="El nombre de usuario es requerido"
                                        ToolTip="User Name is required." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña:</asp:Label>
                                    <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        CssClass="failureNotification" ErrorMessage="La contraseña es requerida" ToolTip="Password is required."
                                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <asp:CheckBox ID="RememberMe" runat="server" />
                                    <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Mantenerme conectado</asp:Label>
                                </p>
                            </fieldset>
                            <p class="submitButton">
                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Ingresar" ValidationGroup="LoginUserValidationGroup"
                                    CssClass="BotonAccion" />
                            </p>
                        </div>
                    </LayoutTemplate>
                </asp:Login>
      <%--      </td>
            <td>
                <img src="../Images/LOGO%20TAMIZAJE%20NEONATAL%20CON%20ESSALUD.png" width="300px" />
            </td>
        </tr>
    </table>--%>
</asp:Content>
