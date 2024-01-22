<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarUsuario.aspx.cs" Inherits="TamiLifeSA.Account.RegistrarUsuario" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>
        Registrar Usuario</h1>

        <span class="failureNotification">
        <asp:Label ID="Msg" runat="server" ForeColor="maroon" />
        </span>
        <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="RegisterUserValidationGroup" />
    <div class="accountInfo">
        <fieldset class="login">
            <legend>Información de usuario</legend>
            <p>Red Asistencial
                <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" CssClass="textEntry" AutoPostBack="True" 
                    Width="320px" 
                    onselectedindexchanged="ddlTipoEstablecimiento_SelectedIndexChanged">
                </asp:DropDownList>
            </p>
            Establecimiento
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEstablecimiento" runat="server" CssClass="textEntry" 
                        Width="320px">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlTipoEstablecimiento" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            
           <p>
           Tipo de Usuario
                <asp:DropDownList ID="ddlRoles" runat="server" CssClass="textEntry" 
                    Width="320px">
                </asp:DropDownList>
            </p>
            <p>
                Usuario
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="textEntry" CausesValidation="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUsuario"
                    CssClass="failureNotification" ErrorMessage="Se requiere un nombre de usuario" ToolTip="Se requiere un nombre de usuario."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                Contraseña
                <asp:TextBox ID="txtPassword" runat="server" CssClass="passwordEntry" TextMode="Password"
                    CausesValidation="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                    CssClass="failureNotification" ErrorMessage="Se requiere una contraseña." ToolTip="Se requiere una contraseña."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                Repetir Contraseña
                <asp:TextBox ID="txtRepPassword" runat="server" CssClass="passwordEntry" TextMode="Password"
                    CausesValidation="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtRepPassword"
                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="Se requiere una confirmación de la contraseña."
                    ToolTip="Se requiere una confirmación de la contraseña." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtPassword"
                    ControlToValidate="txtRepPassword" CssClass="failureNotification" Display="Dynamic"
                    ErrorMessage="La contraseña y la repetición de la contraseña deben coincidir." ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
            </p>
            <p>
                Correo Electrónico
                <asp:TextBox ID="txtCorreo" runat="server" CssClass="textEntry" CausesValidation="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtCorreo"
                    CssClass="failureNotification" ErrorMessage="Se requiere el ingreso de una dirección de correo electronico." ToolTip="Se requiere un correo."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="btnRegistrar" runat="server" CssClass="BotonAccion" OnClick="btnRegistrar_Click"
                Text="Registrar" />
        </p>
    </div>
</asp:Content>
