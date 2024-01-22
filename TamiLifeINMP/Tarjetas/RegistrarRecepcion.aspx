<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarRecepcion.aspx.cs" Inherits="TamizajePortal.Tarjetas.RegistrarRecepcion" %>
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
    function checkBeforeConfirm()//put this javascript function
    {
        if (Page_ClientValidate("Guardar") == true)//method to check validations
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
    <div id="tabcontenido" runat="server">
           <table style="width: 100%;" >
            <tr>
                <td class="filaDelgada" colspan="4">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:HiddenField ID="hdnIdRecepcion" runat="server" />
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
                                onselectedindexchanged="ddlTipoEstablecimiento_SelectedIndexChanged" CausesValidation="False">
                            </asp:DropDownList>
                    
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipoEstablecimiento" ErrorMessage="Seleccione un Tipo de Establecimiento" InitialValue="0" Font-Bold="True" ForeColor="Red" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                    
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
                                Width="350px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEstablecimiento" ErrorMessage="Seleccione un Establecimiento" Font-Bold="True" ForeColor="Red" InitialValue="0" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTipoEstablecimiento" EventName="SelectedIndexChanged" />
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
                
                    <asp:TextBox ID="txtFechaRecepcion" runat="server" CssClass="textoMedio"></asp:TextBox>
                    <asp:CalendarExtender ID="txtFechaRecepcion_CalendarExtender" runat="server" BehaviorID="txtFechaRecepcion_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txtFechaRecepcion" />
                    <asp:RequiredFieldValidator ID="rfvFechaRecepcion" runat="server" ControlToValidate="txtFechaRecepcion" ErrorMessage="Ingrese una Fecha de Recepcion" Font-Bold="True" ForeColor="Red" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                
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
                                CssClass="BotonAccion" OnClick="btnAgregar_Click" ValidationGroup="Agregar" />
                            <asp:RequiredFieldValidator ID="rfvCodigoAgregar" runat="server" Font-Bold="True" ForeColor="Red"
                                ControlToValidate="txtCodigoAgregar" ErrorMessage="Ingrese un Codigo" ValidationGroup="Agregar">*</asp:RequiredFieldValidator>

                            <asp:CustomValidator ID="cusCodigoAgregar" runat="server" ControlToValidate="txtCodigoAgregar" ValidationGroup="Agregar"
                                ErrorMessage="Ingrese un Codigo Correcto" Font-Bold="True" ForeColor="Red" ClientValidationFunction="validateLength">*</asp:CustomValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAgregar" EventName="Click"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" onclientclick="return checkBeforeConfirm();"
                        CssClass="BotonAccion" onclick="btnGuardar_Click" ValidationGroup="Guardar" />
                    <%--<ajaxToolkit:ConfirmButtonExtender ID="btnGuardar_ConfirmButtonExtender" runat="server" BehaviorID="btnGuardar_ConfirmButtonExtender" ConfirmText="Registrar Recepcion?" TargetControlID="btnGuardar" />--%>
                     &nbsp;<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                        CssClass="BotonAccion" OnClick="btnCancelar_Click" CausesValidation="False" />
                &nbsp;</td>
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
                            <asp:CustomValidator ID="cusGridValidator" runat="server" ErrorMessage="Debe Agregar Tarjetas." Font-Bold="True" Font-Size="Smaller" ForeColor="Red" OnServerValidate="cusGridValidator_ServerValidate" ValidationGroup="Guardar"></asp:CustomValidator>
                            <table id="tablaAux" border="1" style="border-collapse:collapse;" runat="server" >
                                <tr>
                                    <td class="cabeceraGrilla2" style="width: 30px">#Fila</td>
                                    <td class="cabeceraGrilla2" style="width: 100px">Codigo</td>
                                    <td class="cabeceraGrilla2" style="width: 50px">Rechazar</td>
                                    <td class="cabeceraGrilla2" style="width: 50px">Eliminar</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False" PageSize="5" OnRowCommand="dgvResultados_RowCommand" Visible="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#Fila">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                 <ItemStyle Width="30px" HorizontalAlign="Center"/>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo" >
                                            <ItemStyle HorizontalAlign="Center" Width="100px"/>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Rechazar">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRechazada" runat="server" DataField="Rechazada" />
                                                </ItemTemplate>
                                                 <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgVer3" runat="server" ImageUrl="~/images/delete.png"  Width="15px"  Height="15px" CausesValidation="False" CommandArgument='<%# Eval("CodigoMuestra") %>'  CommandName="Eliminar" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
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
                    <asp:ValidationSummary ID="vasGuardar" runat="server" ForeColor="Red" HeaderText="Lista de Errores" CssClass="columnaDatos" ValidationGroup="Guardar" ShowMessageBox="True" ShowSummary="False" Visible="False"/>
                    <asp:ValidationSummary ID="vasAgregar" runat="server" ForeColor="Red" HeaderText="Lista de Errores" CssClass="columnaDatos" ValidationGroup="Agregar" ShowMessageBox="True" ShowSummary="False" Visible="False"/>
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
