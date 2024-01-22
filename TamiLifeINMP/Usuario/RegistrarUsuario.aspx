<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarUsuario.aspx.cs" Inherits="TamizajePortal.Usuario.RegistrarUsuario" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function validateLength_8(oSrc, args) {
            args.IsValid = (args.Value.length >= 8);
        }
        function validarVacio(oSrc, args) {
            args.IsValid = (args.Value.length >= 2);
        }
        //function validarVacio(oSrc, args) {
        //    args.IsValid = (args.Value.length >= 2);
        //}
        function validateLength_7_9(oSrc, args) {
            var len = args.Value.length;
            if (len >= 7 && len <= 9)
                args.IsValid = true;
            else
                args.IsValid = false;
        }
        function checkBeforeConfirm()//put this javascript function
        {
            if (Page_ClientValidate() == true)//method to check validations
            {
                if (confirm('¿Guardar Registro?') == true)//confirm after validations check
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="DivPrincipal">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdnIdUsuario" runat="server" />
    <div id="tabcontenido" runat="server">
           <table style="width: 100%;" >
            <tr>
                <td class="SubTitulosTabla1" colspan="2">
                    Datos del Usuario</td>
            </tr>
            <tr>
                <td>
                    Nombres:</td>
                <td>
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="textoLargo" MaxLength="50"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtNombres_FilteredTextBoxExtender" 
                        runat="server" Enabled="True" FilterType="Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ñÑ"
                        TargetControlID="txtNombres">
                    </asp:FilteredTextBoxExtender>

                    <asp:RequiredFieldValidator ID="rfvNombres" runat="server" ControlToValidate="txtNombres" ErrorMessage="Ingrese los Nombres" Font-Bold="True" ForeColor="Red" ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                    
                </td>
            </tr>
            <tr>
                <td>
                    Apellidos:</td>
                <td>
                    <asp:TextBox ID="txtApellidos" runat="server" AutoCompleteType="Disabled" CssClass="textoLargo" MaxLength="50"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtApellidos_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Custom,UppercaseLetters,LowercaseLetters" TargetControlID="txtApellidos" ValidChars=" ñÑ">
                    </asp:FilteredTextBoxExtender>
                    
                    <asp:RequiredFieldValidator ID="rfvApellidos" runat="server" ControlToValidate="txtApellidos" ErrorMessage="Ingrese los Apellidos" Font-Bold="True" ForeColor="Red" ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                    
                </td>
            </tr>
            <tr>
                <td>
                    Cargo:</td>
                <td>
                    <asp:TextBox ID="txtCargo" runat="server" AutoCompleteType="Disabled" CssClass="textoLargo" MaxLength="50"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtCargo_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Custom,UppercaseLetters,LowercaseLetters" TargetControlID="txtCargo" ValidChars=" ñÑ">
                    </asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Tipo Establecimiento:</td>
                <td>
                <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" CssClass="textoLargo" AutoPostBack="true"
                        Width="200px" OnSelectedIndexChanged="ddlTipoEstablecimiento_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
            </tr>
            <tr>
                <td>
                    Establecimiento:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlEstablecimiento" runat="server" CssClass="textoLargo" Width="350px">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddlTipoEstablecimiento" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                            
                    
                </td>
            </tr>
            <tr>
                <td>
                    E-Mail:</td>
                <td>
                     <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" 
                         CssClass="textoLargo"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Ingrese un Email" Font-Bold="True" ForeColor="Red" ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                    
                </td>
            </tr>
            <tr>
                <td>
                    #Celular:</td>
                <td>
                    <asp:TextBox ID="txtCelular" runat="server" MaxLength="20" 
                        CssClass="textoMedio"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td>
                    Perfil:</td>
                <td>
                <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="textoLargo" 
                        Width="200px">
                            </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Usuario:</td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="textoMedio" MaxLength="50"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Ingrese un Usuario" Font-Bold="True" ForeColor="Red" ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblContraseña" runat="server" Text="Contraseña:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContrasena" runat="server" CssClass="textoMedio" MaxLength="50" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvContrasena" runat="server" ErrorMessage="Ingrese su Contraseña" Font-Bold="True" ForeColor="Red" ControlToValidate="txtContrasena" ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblConfirmarContraseña" runat="server" 
                        Text="Confirmar Contraseña:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtConfirmarContrasena" runat="server" CssClass="textoMedio" MaxLength="50" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvConfirmarContrasena" runat="server" ErrorMessage="Confirme su Contraseña" Font-Bold="True" ForeColor="Red" ControlToValidate="txtConfirmarContrasena" ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cpvContrasena" runat="server" Font-Bold="True" ForeColor="Red"
                        ErrorMessage="La Repeticion de la Contraseña no Coincide" ControlToCompare="txtContrasena" 
                        ControlToValidate="txtConfirmarContrasena">*</asp:CompareValidator>
                    
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" onclientclick="return checkBeforeConfirm();"
                        CssClass="BotonAccion" onclick="btnGuardar_Click" ValidationGroup="guardar" />
                    <%--<ajaxToolkit:ConfirmButtonExtender ID="btnGuardar_ConfirmButtonExtender" runat="server" BehaviorID="btnGuardar_ConfirmButtonExtender" ConfirmText="Guardar Usuario?" TargetControlID="btnGuardar" />--%>
                &nbsp;<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                        CssClass="BotonAccion" OnClick="btnCancelar_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
                        HeaderText="Lista de Errores" CssClass="columnaDatos" ShowMessageBox="True" 
                        ShowSummary="False" ToolTip="Lista de Errores" />
                </td>
            </tr>
            </table>
    </div>
            
</div>
</asp:Content>
