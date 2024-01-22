<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarEnvio.aspx.cs" Inherits="TamizajePortal.Tarjetas.RegistrarEnvio" %>
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
    <div id="tabcontenido" runat="server">
           <table style="width: 100%;" >
            <tr>
                <td class="filaDelgada" colspan="4">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdnRangoValido" runat="server" />
                    <asp:HiddenField ID="hdnIdEnvio" runat="server" />
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
                                onselectedindexchanged="ddlTipoEstablecimiento_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipoEstablecimiento" ErrorMessage="Seleccionar un Tipo de Establecimiento" InitialValue="0" Font-Bold="True" ForeColor="Red" ValidationGroup="Guardar" Font-Size="X-Small">*</asp:RequiredFieldValidator>
                    
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEstablecimiento" ErrorMessage="Seleccionar un Establecimiento" Font-Bold="True" ForeColor="Red" InitialValue="0" ValidationGroup="Guardar" Font-Size="X-Small">*</asp:RequiredFieldValidator>
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
                    Fecha Envio:</td>
                <td>
                
                    <asp:TextBox ID="txtFechaEnvio" runat="server" CssClass="textoMedio"></asp:TextBox>
                    <asp:CalendarExtender ID="txtFechaEnvio_CalendarExtender" runat="server" BehaviorID="txtFechaEnvio_CalendarExtender" Format="dd/MM/yyyy" TargetControlID="txtFechaEnvio" />
                    <asp:RequiredFieldValidator ID="rfvFechaEnvio" runat="server" ControlToValidate="txtFechaEnvio" ErrorMessage="Ingrese una Fecha de Envio" Font-Bold="True" ForeColor="Red" ValidationGroup="Guardar" Font-Size="X-Small">*</asp:RequiredFieldValidator>
                
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    Codigo Inicial:</td>
                <td>
                    
                    <asp:TextBox ID="txtCodigoInicial" runat="server" MaxLength="9"
                        CssClass="textoMedio"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtCodigoInicial_FilteredTextBoxExtender" 
                        runat="server" Enabled="True" FilterType="Numbers" 
                        TargetControlID="txtCodigoInicial">
                    </asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvCodigoInicial" runat="server" ControlToValidate="txtCodigoInicial" ErrorMessage="Ingrese un Codigo" Font-Bold="True" ForeColor="Red" ValidationGroup="Generar" Font-Size="X-Small">*</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusCodigoInicial" runat="server" ControlToValidate="txtCodigoInicial" ErrorMessage="Ingrese un Codigo Válido (9 digitos)" Font-Bold="True" ForeColor="Red" ClientValidationFunction="validateLength" ValidationGroup="Generar" Font-Size="Smaller"></asp:CustomValidator>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    Codigo Final:</td>
                <td>
                    <asp:TextBox ID="txtCodigoFinal" runat="server" MaxLength="9"
                        CssClass="textoMedio"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtCodigoFinal_FilteredTextBoxExtender" 
                        runat="server" Enabled="True" FilterType="Numbers" 
                        TargetControlID="txtCodigoFinal">
                    </asp:FilteredTextBoxExtender>
                    <asp:RequiredFieldValidator ID="rfvCodigoFinal" runat="server" ControlToValidate="txtCodigoFinal" ErrorMessage="Ingrese un Codigo" Font-Bold="True" ForeColor="Red" ValidationGroup="Generar" Font-Size="X-Small">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtCodigoFinal" ControlToValidate="txtCodigoInicial" ErrorMessage="El Rango de Tarjetas es Invalido" Font-Bold="True" Font-Size="X-Small" ForeColor="Red" Operator="LessThan" Type="Integer" ValidationGroup="Generar">*</asp:CompareValidator>
                    <asp:CustomValidator ID="cusCodigoFinal" runat="server" ControlToValidate="txtCodigoFinal" ErrorMessage="Ingrese un Codigo Válido (9 digitos)" Font-Bold="True" ForeColor="Red" ClientValidationFunction="validateLength" ValidationGroup="Generar" Font-Size="Smaller"></asp:CustomValidator>
                    
                
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
                    <asp:Button ID="btnGenerar" runat="server" Text="Generar" 
                     CssClass="BotonAccion" OnClick="btnGenerar_Click" ValidationGroup="Generar" />
                &nbsp;<asp:Button ID="btnGuardar" runat="server" CssClass="BotonAccion" onclick="btnGuardar_Click" onclientclick="return checkBeforeConfirm();" Text="Guardar" ValidationGroup="Guardar" Visible="False" />
                    <%-- <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGenerar" EventName="Click"/>
                        </Triggers>--%>&nbsp;<asp:Button ID="btnCancelar" runat="server" CssClass="BotonAccion" OnClick="btnCancelar_Click" Text="Cancelar" Visible="False" />
                    
                
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="filaDelgada">
                    &nbsp;</td>
                <td class="filaDelgada" colspan="2">
                            <asp:CustomValidator ID="cusValidarRango" runat="server" ControlToValidate="txtCodigoFinal" ErrorMessage="El Rango de Tarjetas ya existe" Font-Bold="True" Font-Size="Smaller" ForeColor="Red" onservervalidate="cusValidarRango_ServerValidate" ValidationGroup="Generar"></asp:CustomValidator>
                            </td>
                <td class="filaDelgada">
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table id="tablaAux" border="1" style="border-collapse:collapse;" runat="server" >
                                <tr>
                                    <td class="cabeceraGrilla2" style="width: 30px">#Fila</td>
                                    <td class="cabeceraGrilla2" style="width: 70px">Codigo</td>
                                    <td class="cabeceraGrilla2" style="width: 100px">Fecha Envio</td>
                                    <td class="cabeceraGrilla2" style="width: 100px">Fecha Recepcion</td>
                                    <td class="cabeceraGrilla2" style="width: 70px">Recibida</td>
                                    <td class="cabeceraGrilla2" style="width: 70px">Rechazada</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False" PageSize="5" Visible="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#Fila">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                 <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
<%--                                            <asp:BoundField DataField="NumFila" HeaderText="#Fila">
                                            <ItemStyle Width="30px" CssClass="NumFila" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo" >
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                            </asp:BoundField>
       
                                            <asp:BoundField DataField="FechaEnvio" HeaderText="Fecha Envio" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FechaRecepcion" HeaderText="Fecha Recepcion" DataFormatString="{0:d}">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
       
                                            <asp:CheckBoxField DataField="Recibido" HeaderText="Recibida">
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                            </asp:CheckBoxField>
                                            <asp:CheckBoxField DataField="Rechazado" HeaderText="Rechazada">
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                            </asp:CheckBoxField>

       
                                            <asp:BoundField DataField="Estado" HeaderText="Estado" Visible="False">
                                            <ItemStyle Width="70px" />
                                            </asp:BoundField>

       
                                        </Columns>
                                        <RowStyle CssClass="itemGrilla" />
                                        <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                        <FooterStyle CssClass="itemGrilla"/>
                            </asp:GridView>
                        </ContentTemplate>
                       <%-- <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGenerar" EventName="Click"/>
                        </Triggers>--%>
                    </asp:UpdatePanel>

                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                    <asp:ValidationSummary ID="vasGuardar" runat="server" ValidationGroup="Guardar" Visible="False" />
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Generar" Visible="False" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="filaDelgada" colspan="4">
                    &nbsp;</td>
            </tr>
            </table>
    </div>
            
</div>
</asp:Content>
