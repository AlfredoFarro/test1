<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarRecepcionAux.aspx.cs" Inherits="TamizajePortal.Tarjetas.RegistrarRecepcionAux" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <script type="text/javascript">
     function validateLength(oSrc, args) {
         var len = args.Value.length;
         if (len >= 7 && len <= 9)
             args.IsValid = true;
         else
             args.IsValid = false;
     }
</script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="DivPrincipal">
    <div id="tabcontenido" runat="server">
           <table style="width: 100%;" >
            <tr>
                <td class="filaDelgada" colspan="4">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:HiddenField ID="hdnIdEstablecimiento" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="columnaEspacio">
                    &nbsp;</td>
                <td class="SubTitulosTabla1" colspan="2">
                    Datos del Envio</td>
                <td class="columnaEspacio">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    Tipo de Establecimiento:</td>
                <td>
                    
                            <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" 
                                AutoPostBack="True" CssClass="textoMedio" 
                                onselectedindexchanged="ddlTipoEstablecimiento_SelectedIndexChanged" ControlToValidate="ddlTipoEstablecimiento" CausesValidation="False">
                            </asp:DropDownList>
                    
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipoEstablecimiento" ErrorMessage="Seleccione un Tipo de Establecimiento" InitialValue="0" Font-Bold="True" ForeColor="Red">*</asp:RequiredFieldValidator>
                    
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    Establecimiento:</td>
                <td>
                    <asp:UpdatePanel ID="upaTipoEstablecimiento" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlEstablecimiento" runat="server" CssClass="textoLargo" 
                                Width="350px" OnSelectedIndexChanged="ddlEstablecimiento_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEstablecimiento" ErrorMessage="Seleccione un Establecimiento" Font-Bold="True" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"/>--%>
                            <asp:AsyncPostBackTrigger ControlID="ddlTipoEstablecimiento" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>                           
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    Fecha Recepcion:</td>
                <td>
                
                    <asp:TextBox ID="txtFechaEnvio" runat="server" CssClass="textoMedio"></asp:TextBox>
                    <asp:CalendarExtender ID="txtFechaEnvio_CalendarExtender" runat="server" BehaviorID="txtFechaEnvio_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txtFechaEnvio" />
                    <asp:RequiredFieldValidator ID="rfvFechaEnvio" runat="server" ControlToValidate="txtFechaEnvio" ErrorMessage="Ingrese una Fecha de Envio" Font-Bold="True" ForeColor="Red">*</asp:RequiredFieldValidator>
                
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    Codigo Agregar:</td>
                <td>
                    <asp:UpdatePanel ID="upaAgregar" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtCodigoAgregar" runat="server" MaxLength="9"
                                CssClass="textoMedio"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtCodigoAgregar_FilteredTextBoxExtender" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtCodigoAgregar">
                            </asp:FilteredTextBoxExtender>
                            <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                runat="server" Enabled="True" FilterType="Numbers" 
                                TargetControlID="txtCodigoAgregar">
                            </asp:FilteredTextBoxExtender>--%>
                            &nbsp;<asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                                CssClass="BotonAccion" OnClick="btnAgregar_Click" />
                        </ContentTemplate>
                        
                    </asp:UpdatePanel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                        CssClass="BotonAccion" onclick="btnGuardar_Click" ValidationGroup="guardar" />
                    <ajaxToolkit:ConfirmButtonExtender ID="btnGuardar_ConfirmButtonExtender" runat="server" BehaviorID="btnGuardar_ConfirmButtonExtender" ConfirmText="Registrar Envio?" TargetControlID="btnGuardar" />
                     &nbsp;<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                        CssClass="BotonAccion" OnClick="btnCancelar_Click" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="SubTitulosTabla1" colspan="2">
                    Lista de Tarjetas</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    <asp:UpdatePanel ID="upaResultados" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False" PageSize="5">
                                        <Columns>
                                            <asp:BoundField DataField="NumFila" HeaderText="#Fila" >
                                            <ItemStyle Width="30px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Codigo Muestra">
                                                <ItemTemplate>
                                                    <asp:TextBox id="txtCodigo" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="120px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rechazada">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRechazada" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="BotonAccion" OnClick="AgregarFila"/>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:CheckBoxField DataField="Rechazada" HeaderText="Rechazada">
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                            </asp:CheckBoxField>--%>
                                        </Columns>
                                        <RowStyle CssClass="itemGrilla" />
                                        <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                        <FooterStyle CssClass="itemGrilla"/>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click"/>
                        </Triggers>
                    </asp:UpdatePanel>
                        
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    </td>
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="Lista de Errores" CssClass="columnaDatos" ShowMessageBox="True" ShowSummary="False" />
                </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td class="filaDelgada" colspan="4">
                    &nbsp;</td>
            </tr>
            </table>
    </div>
            
</div>
</asp:Content>
