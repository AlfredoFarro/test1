<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditarEnvio.aspx.cs" Inherits="TamiLifeSA.Digitacion.EditarEnvio" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>
            Editar lista de envio
        </h1>
    </div>
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
                <td class="SubTituloTabla1" colspan="4">
                    Datos de Envio
                </td>
                <td class="SubTituloTabla1" align="center" style="border-left-color: #5AADDD">
                    Número de Carta :
                    <asp:Label ID="lblNumCorrelativo" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
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
                                CssClass="textBoxNombres" OnSelectedIndexChanged="ddlTipoEstablecimiento_SelectedIndexChanged"
                                Enabled="False">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlEstablecimiento" runat="server" CssClass="textBoxNombres"
                                AutoPostBack="True" Width="320px" OnSelectedIndexChanged="ddlEstablecimiento_SelectedIndexChanged"
                                Enabled="False">
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
                    <asp:FilteredTextBoxExtender ID="txtNotas_FilteredTextBoxExtender" runat="server"
                        BehaviorID="txtNotas_FilteredTextBoxExtender" FilterMode="ValidChars" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                        TargetControlID="txtNotas" ValidChars=" ñÑ.," />
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Courier:
                </td>
                <td>
                    <%--<asp:TextBox ID="txtCourier" runat="server" CssClass="textBoxNombresx2" 
                        Visible="False"></asp:TextBox>--%>
                    <%--<asp:FilteredTextBoxExtender ID="txtCourier_FilteredTextBoxExtender" runat="server"
                        BehaviorID="txtCourier_FilteredTextBoxExtender" FilterMode="ValidChars" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                        TargetControlID="txtCourier" ValidChars=" ñÑ." />--%>
                    <asp:DropDownList ID="ddlCourier" runat="server" CssClass="textBoxNombres">
                        <asp:ListItem>Aval S.A.C.</asp:ListItem>
                        <asp:ListItem>Expreso Marvisur</asp:ListItem>
                        <asp:ListItem>Flores Courier</asp:ListItem>
                        <asp:ListItem>Mi Courier S.A.</asp:ListItem>
                        <asp:ListItem>Olva Courier</asp:ListItem>
                        <asp:ListItem>Servis Piura</asp:ListItem>
                        <asp:ListItem>Otro</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td class="columnaEtiquetas">
                    Código Recibo:
                </td>
                <td>
                    <asp:TextBox ID="txtCodigoRecibo" runat="server" CssClass="textBoxNombres"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtCodigoRecibo_FilteredTextBoxExtender" runat="server"
                        BehaviorID="txtCodigoRecibo_FilteredTextBoxExtender" FilterMode="ValidChars"
                        FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" TargetControlID="txtCodigoRecibo"
                        ValidChars=".-" />
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Fecha Envio:
                </td>
                <td>
                    <asp:TextBox ID="txtFechaEnvio" runat="server" CssClass="textoMedio"></asp:TextBox>
                    <asp:CalendarExtender ID="txtFechaEnvio_CalendarExtender" runat="server" BehaviorID="txtFechaEnvio_CalendarExtender"
                        Format="dd/MM/yyyy" TargetControlID="txtFechaEnvio" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="columnaEtiquetas">
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="columnaEtiquetas">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="4">
                    <asp:Button ID="btnCancelar" runat="server" CssClass="BotonAccion" Text="Cancelar"
                        OnClick="btnCancelar_Click" />
                    &nbsp;<asp:Button ID="btnGuardar" runat="server" CssClass="BotonAccion" Text="Guardar"
                        OnClick="btnGuardar_Click" />
                    &nbsp;<asp:Button ID="btnRecibido" runat="server" CssClass="BotonAccion" Text="Recibido"
                        OnClick="btnRecibido_Click" Visible='False' Enabled="False" />
                </td>
                <%--<td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>--%>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="5">
                    Muestras Asociadas al Envio
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:UpdatePanel ID="upaGrilla" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Tarjetas Consultadas: 0 "
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="BotonAccion"
                                            OnClick="btnAgregar_Click" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="dgvMuestras" runat="server" AutoGenerateColumns="False" PageSize="25"
                                OnRowCommand="dgvMuestras_RowCommand">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAgregar" runat="server" DataField="MuestraEnviada" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>--%>
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
                                    <asp:TemplateField HeaderText="Eliminar" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer2" runat="server" ImageUrl="~/images/delete.png" Width="15px"
                                                Height="15px" CommandArgument='<%# Eval("idMuestra") %>' CommandName="eliminar" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
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
                            <asp:PostBackTrigger ControlID="dgvMuestras" />
                            <%--<asp:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel runat="server" ID="panelPopUp" CssClass="modalPopupGrilla">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <table style="width: 100%;">
                        <tr>
                            <td class="SubTituloTabla2" colspan="2">
                                Tarjetas Digitadas Pendientes de Envio
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
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
                                            <asp:Label ID="lblNumPendientes" runat="server" CssClass="numeroRegistro" Text="Tarjetas Consultadas: 0 "
                                                Visible="False"></asp:Label>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="dgvMuestrasPendientes" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="True" OnPageIndexChanging="dgvMuestrasPendientes_PageIndexChanging"
                                    PageSize="20">
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
                                        <asp:BoundField DataField="NombresNeonato" HeaderText="Nombres Neonato">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApellidosNeonato" HeaderText="Apellidos Neonato">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="Talla" HeaderText="Talla">
                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Peso" HeaderText="Peso">
                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                </asp:BoundField>--%>
                                        <asp:BoundField DataField="EdadGestacional" HeaderText="E.G.">
                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="NombresMadre" HeaderText="Nombres Madre">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>--%>
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
                                        <%--<asp:BoundField DataField="Telefono1" HeaderText="Telefono">
                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                </asp:BoundField>--%>
                                        <%--<asp:CheckBoxField DataField="MuestraEnviada" HeaderText="Recibida">
                                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                                                </asp:CheckBoxField>--%>
                                    </Columns>
                                    <RowStyle CssClass="itemGrilla" />
                                    <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                    <FooterStyle CssClass="itemGrilla" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:PostBackTrigger ControlID="dgvMuestras" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="ddlEstablecimiento" EventName="SelectedIndexChanged" />--%>
                <%--<asp:PostBackTrigger ControlID="dgvMuestrasPendientes" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />--%>
                <asp:AsyncPostBackTrigger ControlID="dgvMuestrasPendientes" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="DivBotonesPopUp">
            <asp:Button ID="imgBtnPopupClose" runat="server" Text="Cancelar" CssClass="BotonAccion" />
            &nbsp;<asp:Button ID="btnAceptarPopup" runat="server" CssClass="BotonAccion" Text="Aceptar"
                OnClick="btnAceptarPopup_Click" />
        </div>
    </asp:Panel>
</asp:Content>
