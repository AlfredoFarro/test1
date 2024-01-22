<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarDigitacion.aspx.cs" Inherits="TamiLifeSA.Digitacion.AdministrarDigitacion" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <script type="text/javascript" src="<%= ResolveUrl ("~/Scripts/ScriptFile.js") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblTitulo" runat="server" Text="Administrar Muestras" Font-Bold="True"></asp:Label>&nbsp;Digitadas</h1>
    <div id="DivPrincipal">
        <table style="width: 100%;">
            <tr>
                <td class="filaDelgada" colspan="2">
                    <asp:HiddenField ID="hdnSede" runat="server" Value="0" />
                    <asp:HiddenField ID="hdfPopup" runat="server" />
                    <ajaxToolkit:ModalPopupExtender ID="mpeAttendanceReport" runat="server" TargetControlID="hdfPopup"
                        PopupControlID="panelPopUp" CancelControlID="imgBtnPopupClose" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="2" >
                    Filtros
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Establecimiento</td>
                <td>
                <asp:UpdatePanel ID="upaTipoEstablecimiento" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" AutoPostBack="True"
                                CssClass="textoMedio" OnSelectedIndexChanged="ddlTipoEstablecimiento_SelectedIndexChanged"
                                Width="210px">
                            </asp:DropDownList>
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
                <td class="columnaEtiquetas">
                    Apellidos(Neonato):
                </td>
                <td>
                    <asp:TextBox ID="txtApellidosNeonato" runat="server" CssClass="textoLargo" MaxLength="50"
                        Width="350px"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtApellidosNeonato_FilteredTextBoxExtender" runat="server"
                        Enabled="True" FilterType="Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ñÑ"
                        TargetControlID="txtApellidosNeonato"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Apellidos(Madre):
                </td>
                <td>
                    <asp:TextBox ID="txtApellidosMadre" runat="server" CssClass="textoLargo" MaxLength="50"
                        Width="350px"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtApellidosMadre_FilteredTextBoxExtender" runat="server"
                        Enabled="True" FilterType="Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ñÑ"
                        TargetControlID="txtApellidosMadre"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Fecha de Digitado:
                </td>
                <td>
                    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="textoMedio" Width="95px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtFechaInicio_FilteredTextBoxExtender"
                        runat="server" BehaviorID="txtFechaInicio_FilteredTextBoxExtender" FilterType="Custom,Numbers"
                        ValidChars="/" TargetControlID="txtFechaInicio"></ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:CalendarExtender ID="txtFechaInicio_CalendarExtender" Format="dd/MM/yyyy"
                        runat="server" BehaviorID="txtFechaInicio_CalendarExtender" TargetControlID="txtFechaInicio">
                    </ajaxToolkit:CalendarExtender>
                    -
                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="textoMedio" Width="95px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtFechaFin_FilteredTextBoxExtender" runat="server"
                        BehaviorID="txtFechaFin_FilteredTextBoxExtender" FilterType="Custom,Numbers"
                        ValidChars="/" TargetControlID="txtFechaFin"></ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                        BehaviorID="txtFechaFin_CalendarExtender" TargetControlID="txtFechaFin"></ajaxToolkit:CalendarExtender>    
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Codigo de Tarjeta:
                </td>
                <td>
                    <asp:TextBox ID="txtCodigoMuestra" runat="server" CssClass="textoMedio" MaxLength="9"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtCodigoMuestra_FilteredTextBoxExtender" runat="server"
                        Enabled="True" FilterType="Numbers" TargetControlID="txtCodigoMuestra"></asp:FilteredTextBoxExtender>
                    &nbsp;<asp:CustomValidator ID="cvaCodigoMuestra" runat="server" ClientValidationFunction="validateLength_7"
                        ControlToValidate="txtCodigoMuestra" ErrorMessage="Ingrese un Código de Muestra válido"
                        Font-Bold="True" ForeColor="Red" Font-Size="X-Small"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    DNI/CE(Madre)
                </td>
                <td>
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="textoMedio" MaxLength="10"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtDNI_FilteredTextBoxExtender" runat="server" BehaviorID="txtDNI_FilteredTextBoxExtender"
                        FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtDNI" />
                    &nbsp;<asp:CustomValidator ID="cvaDNI" runat="server" ClientValidationFunction="validateLength_8"
                        ControlToValidate="txtDNI" ErrorMessage="Ingrese un número de DNI válido" Font-Bold="True"
                        ForeColor="Red" Font-Size="X-Small"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" OnClick="btnBuscar_Click" Text="Buscar" />
                    &nbsp;<asp:Button ID="btnExportar" runat="server" CssClass="BotonAccion" OnClick="btnExportar_Click" Text="Exportar" />
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="2">
                    Resultados
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="upaGrilla" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Registros Consultados: 0 "
                                Visible="False"></asp:Label>
                            <asp:GridView ID="dgvMuestras" runat="server" AutoGenerateColumns="False" OnRowCommand="dgvMuestras_RowCommand"
                                AllowPaging="True" OnPageIndexChanging="dgvMuestras_PageIndexChanging" 
                                PageSize="30" onrowdatabound="dgvMuestras_RowDataBound">
                                <Columns>
                                    <%--<asp:BoundField DataField="idMuestra" HeaderText="ID">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NombresNeonato" HeaderText="Nombres Neonato"></asp:BoundField>
                                    <asp:BoundField DataField="ApellidosNeonato" HeaderText="Apellidos Neonato"></asp:BoundField>
                                    <asp:BoundField DataField="ApellidosMadre" HeaderText="Apellidos Madre"></asp:BoundField>
                                    <%--<asp:BoundField DataField="NombresMadre" HeaderText="Nombre Madre" Visible="false"/>
                             <asp:BoundField DataField="ApellidosMadre" HeaderText="Apellido Madre" Visible="false"/>--%>
                                    <asp:BoundField DataField="DNI" HeaderText="DNI">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nac." DataFormatString="{0:d}">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaToma" HeaderText="Fecha Toma" DataFormatString="{0:d}">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Telefono1" HeaderText="Telefono">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="idEnvio" HeaderText="ID Envio">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MuestraAceptada" HeaderText="Estado">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="Telefono2" HeaderText="Telf 2" Visible="false" />--%>
                                    <asp:TemplateField HeaderText="Editar" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer" runat="server" ImageUrl="~/images/gtk-edit.png" Width="15px"
                                                Height="15px" CommandArgument='<%# Eval("CodigoMuestra") %>' CommandName="editar" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer2" runat="server" ImageUrl="~/images/delete.png" Width="15px"
                                                Height="15px" CommandArgument='<%# Eval("idMuestra") %>' CommandName="eliminar" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Detalle">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer3" runat="server" ImageUrl="~/images/buscar1.png" Width="15px"
                                                Height="15px" CommandArgument='<%# Eval("CodigoMuestra") %>' CommandName="detalle" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    </Columns>
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle CssClass="itemGrilla" />
                                <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                <FooterStyle CssClass="itemGrilla" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                            <asp:PostBackTrigger ControlID="dgvMuestras" />
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
                                Detalles de la Muestra
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Codigo de Muestra
                            </td>
                            <td>
                                <asp:Label ID="lblCodigoMuestra" runat="server" Text="CodigoMuestra" CssClass="DatosMuestra"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Estado:</td>
                            <td>
                                <asp:Label ID="lblEstadoMuestra" runat="server" CssClass="DatosMuestra" 
                                    Text="EstadoMuestra"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEtiquetaMotivo" runat="server" Text="Motivo Rechazo:" 
                                    Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMotivoRechazo" runat="server" CssClass="DatosMuestra" 
                                    Text="MotivoRechazo" Visible="False"></asp:Label>
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
                                Codigo Correlativo:
                            </td>
                            <td>
                                <asp:Label ID="lblCodigoCorrelativo" runat="server" CssClass="DatosMuestra" Text="CodigoCorrelativo"></asp:Label>
                            </td>
                            <td>
                                Fecha de Recepcion:
                            </td>
                            <td>
                                <asp:Label ID="lblFechaRecepcion" runat="server" CssClass="DatosMuestra" Text="FechaRecepcion"></asp:Label>
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
                            <td colspan="3">
                                <asp:Label ID="lblDireccion" runat="server" Text="Direccion" CssClass="DatosMuestra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                DNI:
                            </td>
                            <td>
                                <asp:Label ID="lblDNI" runat="server" CssClass="DatosMuestra" Text="DNI"></asp:Label>
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
                                Edad:
                            </td>
                            <td>
                                <asp:Label ID="lblEdadMadre" runat="server" CssClass="DatosMuestra" Text="EdadMadre"></asp:Label>
                            </td>
                            <td>
                                Telefono 2:
                            </td>
                            <td>
                                <asp:Label ID="lblTelefono2" runat="server" Text="Telefono2" CssClass="DatosMuestra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubTituloTabla3">
                                Neonato
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblNeonato" runat="server" Text="NombreNeonato" CssClass="DatosMuestra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha de Nacimiento:
                            </td>
                            <td>
                                <asp:Label ID="lblFechaNacimiento" runat="server" Text="FechaNacimiento" CssClass="DatosMuestra"></asp:Label>
                            </td>
                            <td>
                                Hora de Nacimiento:
                            </td>
                            <td>
                                <asp:Label ID="lblHoraNacimiento" runat="server" CssClass="DatosMuestra" Text="HoraNacimiento"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Edad Gestacional:
                            </td>
                            <td>
                                <asp:Label ID="lblEdadGestacional" runat="server" CssClass="DatosMuestra" Text="EdadGestacional"></asp:Label>
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
                                Prematuro:
                            </td>
                            <td>
                                <asp:Label ID="lblPrematuro" runat="server" CssClass="DatosMuestra" Text="Prematuro"></asp:Label>
                            </td>
                            <td>
                                Transfundido:
                            </td>
                            <td>
                                <asp:Label ID="lblTransfundido" runat="server" CssClass="DatosMuestra" Text="Transfundido"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Peso(Kg.):
                            </td>
                            <td>
                                <asp:Label ID="lblPeso" runat="server" CssClass="DatosMuestra" Text="Peso"></asp:Label>
                            </td>
                            <td>
                                Talla(cm)
                            </td>
                            <td>
                                <asp:Label ID="lblTalla" runat="server" CssClass="DatosMuestra" Text="Talla"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubTituloTabla3">
                                Muestra
                            </td>
                            <td>
                                &nbsp;
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
                                N° Muestra:
                            </td>
                            <td>
                                <asp:Label ID="lblNMuestra" runat="server" CssClass="DatosMuestra" Text="NMuestra"></asp:Label>
                            </td>
                            <td>
                                Muestra de Talon:
                            </td>
                            <td>
                                <asp:Label ID="lblTalon" runat="server" CssClass="DatosMuestra" Text="Talon"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha de Toma:
                            </td>
                            <td>
                                <asp:Label ID="lblFechaToma" runat="server" CssClass="DatosMuestra" Text="FechaToma"></asp:Label>
                            </td>
                            <td>
                                Hora de Toma:
                            </td>
                            <td>
                                <asp:Label ID="lblHoraToma" runat="server" CssClass="DatosMuestra" Text="HoraToma"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tomado Por:
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblTomadoPor" runat="server" CssClass="DatosMuestra" Text="TomadoPor"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Notas:
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblNotas" runat="server" CssClass="DatosMuestra" Text="Notas"></asp:Label>
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
                                &nbsp;
                                <asp:Label ID="lblAfiliacion" runat="server" CssClass="DatosMuestra" 
                                    Text="Afiliacion" Visible="False"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        
                    </table>
                </div>
                <%--Button to close modal popup--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="DivBotonesPopUp">
            <asp:Button ID="imgBtnPopupClose" runat="server" Text="Cancelar" CssClass="BotonAccion" />
        </div>
    </asp:Panel>
</asp:Content>
