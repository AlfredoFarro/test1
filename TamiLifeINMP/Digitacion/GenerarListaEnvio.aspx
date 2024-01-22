<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenerarListaEnvio.aspx.cs" Inherits="TamiLifeSA.Digitacion.RevisarDigitacion" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Generar lista de envio
    </h1>
    <div id="DivPrincipal">
        <table style="width: 100%;">
            <tr>
                <td class="filaDelgada" colspan="5">
                    <asp:HiddenField ID="hdnIdEnvio" runat="server" />
                    <asp:HiddenField ID="hdfPopup" runat="server" />
                    <ajaxToolkit:ModalPopupExtender ID="mpeAttendanceReport" runat="server" TargetControlID="hdfPopup"
                        PopupControlID="panelPopUp" CancelControlID="imgBtnPopupClose" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <%--<asp:HiddenField ID="hdfPopup" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="mpeAttendanceReport" runat="server" TargetControlID="hdfPopup"
                            PopupControlID="panelPopUp" CancelControlID="imgBtnPopupClose" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>--%>
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="5">
                    Datos de Envio
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Establecimiento:
                </td>
                <td colspan="4">
                    <asp:UpdatePanel ID="upaTipoEstablecimiento" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" AutoPostBack="True"
                                CssClass="textBoxNombres" 
                                onselectedindexchanged="ddlTipoEstablecimiento_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlEstablecimiento" runat="server" 
                                CssClass="textBoxNombres" AutoPostBack="True"
                                Width="320px" 
                                OnSelectedIndexChanged="ddlEstablecimiento_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="rfvEstablecimiento" runat="server" ControlToValidate="ddlEstablecimiento"
                                        ErrorMessage="Seleccione un Establecimiento" Font-Bold="True" ForeColor="Red"
                                        InitialValue="0" Enabled="False">*</asp:RequiredFieldValidator>--%>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"/>--%>
                            <asp:AsyncPostBackTrigger ControlID="ddlTipoEstablecimiento" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Notas:
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtNotas" runat="server" CssClass="textBoxNombresx2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtCourier" runat="server" CssClass="textBoxNombres" 
                        Visible="False"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtCourier_FilteredTextBoxExtender" runat="server"
                        BehaviorID="txtCourier_FilteredTextBoxExtender" FilterMode="ValidChars" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                        TargetControlID="txtCourier" ValidChars=" ñÑ." />
                    <asp:TextBox ID="txtCodigoRecibo" runat="server" CssClass="textBoxNombres" 
                        Visible="False"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtCodigoRecibo_FilteredTextBoxExtender" runat="server"
                        BehaviorID="txtCodigoRecibo_FilteredTextBoxExtender" FilterMode="ValidChars"
                        FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" TargetControlID="txtCodigoRecibo"
                        ValidChars=".-" />
                    <asp:TextBox ID="txtFechaEnvio" runat="server" CssClass="textoMedio" 
                        Visible="False"></asp:TextBox>
                    <asp:CalendarExtender ID="txtFechaEnvio_CalendarExtender" runat="server" BehaviorID="txtFechaEnvio_CalendarExtender"
                        Format="dd/MM/yyyy" TargetControlID="txtFechaEnvio" />
                </td>
            </tr>
           
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="4">
                    <asp:Button ID="btnRegistrar" runat="server" CssClass="BotonAccion" Text="Registrar"
                        OnClick="btnRegistrar_Click" />
                    <asp:Label ID="lblErrorEnvio" runat="server" Font-Bold="True" ForeColor="Red" 
                        Text="Selecciona al menos 1 muestra." Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="5">
                    Tarjetas Digitadas Pendientes de Envio
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:UpdatePanel ID="upaGrilla" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        &nbsp;
                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged"
                                            Text="Marcar Todos" Visible="False" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Tarjetas Consultadas: 0 "
                                            Visible="False"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="dgvMuestras" runat="server" AutoGenerateColumns="False" PageSize="25">
                                <Columns>
                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAgregar" runat="server" DataField="MuestraEnviada" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="NombresMadre" HeaderText="Nombre Madre" Visible="false"/>
                                 <asp:BoundField DataField="ApellidosMadre" HeaderText="Apellido Madre" Visible="false"/>--%>
                                    <asp:BoundField DataField="idMuestra" HeaderText="idMuestra" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApellidosNeonato" HeaderText="Apellidos Neonato">
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Talla" HeaderText="Talla">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Peso" HeaderText="Peso">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EdadGestacional" HeaderText="E.G.">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NombresMadre" HeaderText="Nombres Madre">
                                        <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApellidosMadre" HeaderText="Apellidos Madre">
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DNI" HeaderText="DNI">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="HistoriaClinica" HeaderText="H. Clinica" Visible="False">
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:BoundField>--%>
                                    <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nac." DataFormatString="{0:d}">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaToma" HeaderText="Fecha Toma" DataFormatString="{0:d}">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Telefono1" HeaderText="Telefono">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <%--<asp:CheckBoxField DataField="MuestraEnviada" HeaderText="Recibida">
                                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                                                </asp:CheckBoxField>--%>
                                </Columns>
                                <RowStyle CssClass="itemGrilla" />
                                <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                <FooterStyle CssClass="itemGrilla" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEstablecimiento" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="dgvMuestras" />
                            <%--<asp:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel runat="server" ID="panelPopUp" CssClass="modalPopupConfirmacion">
        <h2 align="center">Descargar formato de Envío</h2>
        <div class="DivBotonesPopUp">
            <asp:Button ID="imgBtnPopupClose" runat="server" Text="Cerrar" 
                CssClass="BotonAccion" />
            &nbsp;<asp:Button ID="btnAceptarPopup" runat="server" CssClass="BotonAccion" Text="Descargar"
                OnClick="btnAceptarPopup_Click" />
        </div>
    </asp:Panel>
</asp:Content>
