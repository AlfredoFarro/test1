<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RegistrarEstablecimiento.aspx.cs" Inherits="TamiLifeSA.Establecimientos.RegistrarEstablecimiento" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl ("~/Scripts/ScriptFile.js") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblTitulo" runat="server" Text="Registrar Establecimiento" Font-Bold="True"></asp:Label></h1>
    <div id="DivPrincipal">
        <div id="tabcontenido" runat="server">
            <asp:HiddenField ID="hdnIdEstablecimiento" runat="server" />
            <table style="width: 100%;">
                <tr>
                    <td class="SubTituloTabla1" colspan="2">
                        Datos del Establecimiento
                    </td>
                </tr>
                <tr>
                    <td class="columnaEtiquetas">
                        Codigo:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textoMedio"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCodigo" runat="server" ControlToValidate="txtCodigo"
                            ErrorMessage="Ingrese un Codigo " Font-Bold="True" ForeColor="Red" ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="columnaEtiquetas">
                        Establecimiento:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEstablecimiento" runat="server" CssClass="textoMedio" Width="400px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEstablecimiento" runat="server" ControlToValidate="txtEstablecimiento"
                            ErrorMessage="Ingrese el Nombre del Establecimiento" Font-Bold="True" ForeColor="Red"
                            ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="columnaEtiquetas">
                        Red Asistencial:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" CssClass="textoLargo"
                            Width="200px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvTipoEstablecimiento" runat="server" ControlToValidate="ddlTipoEstablecimiento"
                            ErrorMessage="Seleecione un Tipo de Establecimiento" Font-Bold="True" ForeColor="Red"
                            ValidationGroup="guardar" InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="columnaEtiquetas">
                        Direccion:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="textoLargo" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="columnaEtiquetas">
                        Departamento:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="upaDepartamento" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlDepartamento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged"
                                    CssClass="textoLargo">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"/>--%>
                                <asp:AsyncPostBackTrigger ControlID="ddlDepartamento" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="columnaEtiquetas">
                        Provincia:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="upaProvincia" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlProvincia" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged"
                                    CssClass="textoLargo">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"/>--%>
                                <asp:AsyncPostBackTrigger ControlID="ddlDepartamento" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="columnaEtiquetas">
                        Distrito:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="upaDistrito" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlDistrito" runat="server" CssClass="textoLargo" OnSelectedIndexChanged="ddlDistrito_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"/>--%>
                                <asp:AsyncPostBackTrigger ControlID="ddlProvincia" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="columnaEtiquetas">
                        Telefono 1:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTelefono1" runat="server" MaxLength="12" CssClass="textoMedio"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono1"
                            ErrorMessage="Ingrese un Telefono" Font-Bold="True" ForeColor="Red" ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="columnaEtiquetas">
                        Telefono 2:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTelefono2" runat="server" MaxLength="12" CssClass="textoMedio"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClientClick="return checkBeforeConfirm();"
                            CssClass="BotonAccion" OnClick="btnGuardar_Click" ValidationGroup="guardar" />
                        <%--<ajaxToolkit:ConfirmButtonExtender ID="btnGuardar_ConfirmButtonExtender" runat="server" BehaviorID="btnGuardar_ConfirmButtonExtender" ConfirmText="Guardar Establecimiento?" TargetControlID="btnGuardar" />--%>
                        &nbsp;<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="BotonAccion"
                            OnClick="btnCancelar_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="Lista de Errores"
                            CssClass="columnaDatos" ShowMessageBox="True" ShowSummary="False" ToolTip="Lista de Errores"
                            ValidationGroup="guardar" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
