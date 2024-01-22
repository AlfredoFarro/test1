<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="TamizajePortal.Usuario.Login" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="accountInfo">
        <%--<asp:Login ID="LoginControl" runat="server">
        </asp:Login>--%>
        <asp:Login ID="LoginControl" runat="server" OnAuthenticate="LoginControl_Authenticate"
            LoginButtonText="INGRESAR" TitleText="" UserNameLabelText="Usuario:" RememberMeText="Mantener Sesión.">
            <LabelStyle Font-Bold="False" Font-Italic="False" HorizontalAlign="Left" Wrap="True" />
            <LayoutTemplate>
                <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                    <tr>
                        <td>
                            <table cellpadding="0">
                                <tr>
                                    <td align="left" style="font-weight: normal; font-style: normal;">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Usuario:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server" CssClass="textoMedio"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="El nombre de usuario es obligatorio." Font-Bold="True" ForeColor="Red"
                                            ToolTip="El nombre de usuario es obligatorio." ValidationGroup="LoginControl">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="font-weight: normal; font-style: normal;">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña: </asp:Label>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" CssClass="textoMedio" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="La contraseña es obligatoria." Font-Bold="True" ForeColor="Red"
                                            ToolTip="La contraseña es obligatoria." ValidationGroup="LoginControl">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="RememberMe" runat="server" Text="Mantener Sesión." />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color: Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="BotonAccion"
                                            Text="INGRESAR" ValidationGroup="LoginControl" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <LoginButtonStyle CssClass="BotonAccion" />
            <TextBoxStyle CssClass="textoMedio" />
            <ValidatorTextStyle Font-Bold="True" />
        </asp:Login>
        <%--(<asp:LoginStatus ID="LoginStatus" runat="server" LogoutAction="Redirect" LogoutPageUrl="~/default.aspx" />)--%>
    </div>
</asp:Content>
