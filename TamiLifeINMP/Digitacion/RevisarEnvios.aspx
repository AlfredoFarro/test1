<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RevisarEnvios.aspx.cs" Inherits="TamiLifeSA.Digitacion.RevisarEnvios" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Revisar envios</h1>
    <div id="DivPrincipal">
        <table style="width: 100%;">
            <tr>
                <td class="filaDelgada" colspan="2">
                    <asp:HiddenField ID="hdfPopup" runat="server" />
                    <ajaxToolkit:ModalPopupExtender ID="mpeAttendanceReport" runat="server" TargetControlID="hdfPopup"
                        PopupControlID="panelPopUp" CancelControlID="imgBtnPopupClose" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="2">
                    Datos de Envio
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Establecimiento:
                </td>
                <td>
                    <asp:UpdatePanel ID="upaTipoEstablecimiento" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" AutoPostBack="True"
                                CssClass="textoMedio" 
                                OnSelectedIndexChanged="ddlTipoEstablecimiento_SelectedIndexChanged" 
                                Width="250px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlEstablecimiento" runat="server" CssClass="textoLargo" Width="320px">
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="rfvEstablecimiento" runat="server" ControlToValidate="ddlEstablecimiento"
                                    ErrorMessage="Seleccione un Establecimiento" Font-Bold="True" ForeColor="Red"
                                    InitialValue="0" Enabled="False">*</asp:RequiredFieldValidator>--%>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTipoEstablecimiento" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Fecha de Creación:
                </td>
                <td>
                    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="textoMedio"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtFechaInicio_FilteredTextBoxExtender"
                        runat="server" BehaviorID="txtFechaInicio_FilteredTextBoxExtender" FilterType="Custom,Numbers"
                        ValidChars="/" TargetControlID="txtFechaInicio"></ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:CalendarExtender ID="txtFechaInicio_CalendarExtender" Format="dd/MM/yyyy"
                        runat="server" BehaviorID="txtFechaInicio_CalendarExtender" TargetControlID="txtFechaInicio">
                    </ajaxToolkit:CalendarExtender>
                    -
                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="textoMedio"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtFechaFin_FilteredTextBoxExtender" runat="server"
                        BehaviorID="txtFechaFin_FilteredTextBoxExtender" FilterType="Custom,Numbers"
                        ValidChars="/" TargetControlID="txtFechaFin"></ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                        BehaviorID="txtFechaFin_CalendarExtender" TargetControlID="txtFechaFin"></ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Estado Envio:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEstadoEnvio" runat="server" CssClass="textoMedio">
                        <asp:ListItem Value="1">Enviados</asp:ListItem>
                        <asp:ListItem Value="2">Recibidos</asp:ListItem>
                        <asp:ListItem Value="0">Todos</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" Text="Buscar" OnClick="btnBuscar_Click" />
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hdfPopup"
                        PopupControlID="panelPopUp" CancelControlID="imgBtnPopupClose" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    &nbsp;<asp:Button ID="btnRecibido" runat="server" CssClass="BotonAccion" Text="Recibido"
                        OnClick="btnRecibido_Click" Visible="False" />
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="2">
                    Tarjetas Digitadas
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="upaGrilla" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Registros Consultados: 0 "
                                Visible="False"></asp:Label>
                            <asp:GridView ID="dgvEnvios" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="25" OnRowCommand="dgvMuestras_RowCommand" OnPageIndexChanging="dgvMuestras_PageIndexChanging">
                                <Columns>
                                   
                                    <asp:BoundField DataField="CodigoEnvio" HeaderText="Codigo" Visible="true">
                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="idEnvio" HeaderText="ID" Visible="true">
                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoEstablecimiento" HeaderText="Codigo Hospital" Visible="false">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Establecimiento" HeaderText="Establecimiento">
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Notas" HeaderText="Notas">
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha Creacion" DataFormatString="{0:d}">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumTarjetas" HeaderText="#Tarjetas">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="Recibido">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRecibido" DataField="EnvioRecibido" runat="server" Checked='<%#bool.Parse(Eval("EnvioRecibido").ToString())%>'/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer3" runat="server" ImageUrl="~/images/gtk-edit.png" Width="15px"
                                                Visible='<%#!bool.Parse(Eval("EnvioRecibido").ToString())%>' Height="15px" CommandArgument='<%# Eval("idEnvio") %>'
                                                CommandName="editar" />
                                            <%--<asp:ImageButton ID="imgVer4" runat="server" ImageUrl="~/images/buscar1.png" Width="15px" Visible='<%#bool.Parse(Eval("EnvioRecibido").ToString())%>'
                                                Height="15px" CommandArgument='<%# Eval("idEnvio") %>' CommandName="detalle" />--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reporte">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer" runat="server" ImageUrl="~/images/DownloadPDF.png" Width="15px"
                                                Height="15px" CommandArgument='<%# Eval("idEnvio") %>' CommandName="reporte" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Eliminar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer2" runat="server" ImageUrl="~/images/delete.png" Width="15px"
                                                Height="15px" CommandArgument='<%# Eval("idEnvio") %>' CommandName="eliminar" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>--%>
                                </Columns>
                                <RowStyle CssClass="itemGrilla" />
                                <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                <FooterStyle CssClass="itemGrilla" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                            <%--<asp:AsyncPostBackTrigger ControlID="dgvMuestras" EventName="RowCommand" />--%>
                            <asp:PostBackTrigger ControlID="dgvEnvios" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel runat="server" ID="panelPopUp" CssClass="modalPopup">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<h1 class="SubTituloTabla2">
                    Detalles de la Muestra</h1>--%>
                <div>
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="4" class="SubTituloTabla2">
                                Detalles del Envio
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Codigo de Envio
                            </td>
                            <td>
                                <asp:Label ID="lblCodigoEnvio" runat="server" Text="CodigoEnvio" CssClass="DatosMuestra"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Establecimiento:
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblEstablecimiento" runat="server" Text="Establecimiento" CssClass="DatosMuestra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha de Creación:
                            </td>
                            <td>
                                <asp:Label ID="lblFechaCreacion" runat="server" Text="FechaCreacion" CssClass="DatosMuestra"></asp:Label>
                            </td>
                            <td>
                                H. Clinica:
                            </td>
                            <td>
                                <asp:Label ID="lblHClinica" runat="server" CssClass="DatosMuestra" Text="HClinica"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha de Toma:
                            </td>
                            <td>
                                <asp:Label ID="lblFechaToma" runat="server" Text="FechaToma" CssClass="DatosMuestra"></asp:Label>
                            </td>
                            <td>
                                Sexo:
                            </td>
                            <td>
                                <asp:Label ID="lblSexo" runat="server" CssClass="DatosMuestra" Text="Sexo"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha de Recepcion:
                            </td>
                            <td>
                                <asp:Label ID="lblFechaRecepcion" runat="server" CssClass="DatosMuestra" Text="FechaRecepcion"></asp:Label>
                            </td>
                            <td>
                                Peso(gr.):
                            </td>
                            <td>
                                <asp:Label ID="lblPeso" runat="server" CssClass="DatosMuestra" Text="Peso"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                E.G:
                            </td>
                            <td>
                                <asp:Label ID="lblEdadGestacional" runat="server" CssClass="DatosMuestra" Text="EdadGestacional"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubTituloTabla3">
                                Madre
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblMadre" runat="server" Text="NombreMadre" CssClass="DatosMuestra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Dirección:
                            </td>
                            <td>
                                <asp:Label ID="lblDireccion" runat="server" Text="Direccion" CssClass="DatosMuestra"></asp:Label>
                            </td>
                            <td>
                                DNI:
                            </td>
                            <td>
                                <asp:Label ID="lblDNI" runat="server" CssClass="DatosMuestra" Text="DNI"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Depatamento:
                            </td>
                            <td>
                                <asp:Label ID="lblDepartamento" runat="server" CssClass="DatosMuestra" Text="Departamento"></asp:Label>
                            </td>
                            <td>
                                Telefono 1:
                            </td>
                            <td>
                                <asp:Label ID="lblTelefono1" runat="server" Text="Telefono1" CssClass="DatosMuestra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Provincia:
                            </td>
                            <td>
                                <asp:Label ID="lblProvincia" runat="server" CssClass="DatosMuestra" Text="Provincia"></asp:Label>
                            </td>
                            <td>
                                Telefono 2:
                            </td>
                            <td>
                                <asp:Label ID="lblTelefono2" runat="server" Text="Telefono2" CssClass="DatosMuestra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="SubTituloTabla3">
                                Resultados
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="dgvMuestrasPendientes" runat="server" AutoGenerateColumns="False"
                                    PageSize="25">
                                    <Columns>
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
                                        <asp:BoundField DataField="NombresMadre" HeaderText="Nombres Madre">
                                            <ItemStyle Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApellidosMadre" HeaderText="Apellidos Madre">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DNI" HeaderText="DNI">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nac." DataFormatString="{0:d}">
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaToma" HeaderText="Fecha Toma" DataFormatString="{0:d}">
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle CssClass="itemGrilla" />
                                    <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                    <FooterStyle CssClass="itemGrilla" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--Button to close modal popup--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="DivBotonesPopUp">
            <asp:Button ID="imgBtnPopupClose" runat="server" Text="Cancelar" CssClass="BotonAccion" />
            &nbsp;<asp:Button ID="btnReportePopup" runat="server" CssClass="BotonAccion" Text="Reporte" />
        </div>
    </asp:Panel>
</asp:Content>
