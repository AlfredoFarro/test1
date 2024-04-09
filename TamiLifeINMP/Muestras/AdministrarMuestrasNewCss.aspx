<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarMuestrasNewCss.aspx.cs" Inherits="TamiLifeSA.Muestras.AdministrarMuestrasNewCss" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl ("~/Scripts/ScriptFile.js") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblTitulo" runat="server" Text="Administrar Muestras" Font-Bold="True"></asp:Label>
    </h1>
    <div id="DivPrincipal">
        <div class="tableall">
            <div>
                <td class="filaDelgada" colspan="5">
                    <asp:HiddenField ID="hdfPopup" runat="server" />
                    <asp:HiddenField ID="hdnSede" runat="server" Value="0" />
                    <ajaxToolkit:ModalPopupExtender ID="mpeAttendanceReport" runat="server" TargetControlID="hdfPopup"
                        PopupControlID="panelPopUp" CancelControlID="imgBtnPopupClose" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                </td>
            </div>

                <div class="SubTituloTabla1">
                    Filtros
                </div>


                <div class="columnaEtiquetasnu">
                    Establecimiento:
                    <div class="ddlTipoEstAdm">
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
                    </div>
                </div>
                


                <div class="columnaEtiquetasnudob">
                    Apellidos(Neonato):
                    <div class="txtApeNeoAdm">
                    <asp:TextBox ID="txtApellidosNeonato" runat="server" CssClass="textoLargo" MaxLength="50"
                        Width="350px"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtApellidosNeonato_FilteredTextBoxExtender" runat="server"
                        Enabled="True" FilterType="Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ñÑ"
                        TargetControlID="txtApellidosNeonato"></asp:FilteredTextBoxExtender>
                    </div>
                    <div class="ddlEstAdm">
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textoMedio" 
                        Visible="False">
                        <asp:ListItem Value="1">Activa</asp:ListItem>
                        <asp:ListItem Value="0">Eliminada</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                </div>
                


                <div class="columnaEtiquetasnudob">
                    Apellidos(Madre):
                    <div class="txtApMadrAdm">
                    <asp:TextBox ID="txtApellidosMadre" runat="server" CssClass="textoLargo" MaxLength="50"
                        Width="350px"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtApellidosMadre_FilteredTextBoxExtender" runat="server"
                        Enabled="True" FilterType="Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ñÑ"
                        TargetControlID="txtApellidosMadre"></asp:FilteredTextBoxExtender>
                    </div>
                    DNI(Madre)
                    <div class="txtdniAdm">
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="textoMedio" MaxLength="10"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtDNI_FilteredTextBoxExtender" runat="server" BehaviorID="txtDNI_FilteredTextBoxExtender"
                        FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtDNI" />
                    </div>
                </div>
                

                
                


                <div class="columnaEtiquetasnudob">
                    Fecha Nacimiento:
                    <div class="txtFechAdm">
                    <asp:TextBox ID="txtFechaInicioNac" runat="server" CssClass="textoMedio" Width="95px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtFechaInicioNac_FilteredTextBoxExtender"
                        runat="server" BehaviorID="txtFechaInicio_FilteredTextBoxExtender" FilterType="Custom,Numbers"
                        ValidChars="/" TargetControlID="txtFechaInicioNac"></ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:CalendarExtender ID="txtFechaInicioNac_CalendarExtender" Format="dd/MM/yyyy"
                        runat="server" BehaviorID="txtFechaInicio_CalendarExtender" TargetControlID="txtFechaInicioNac">
                    </ajaxToolkit:CalendarExtender>
                    -
                    <asp:TextBox ID="txtFechaFinNac" runat="server" CssClass="textoMedio" Width="95px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtFechaFinNac_FilteredTextBoxExtender"
                        runat="server" BehaviorID="txtFechaFin_FilteredTextBoxExtender" FilterType="Custom,Numbers"
                        ValidChars="/" TargetControlID="txtFechaFinNac"></ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:CalendarExtender ID="txtFechaFinNac_CalendarExtender" runat="server"
                        Format="dd/MM/yyyy" BehaviorID="txtFechaFin_CalendarExtender" TargetControlID="txtFechaFinNac">
                    </ajaxToolkit:CalendarExtender>
                    </div>
                    Codigo de Tarjeta:
                    <div class="txtCodAdm">
                    <asp:TextBox ID="txtCodigoMuestra" runat="server" CssClass="textoMedio" MaxLength="9"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtCodigoMuestra_FilteredTextBoxExtender" runat="server"
                        Enabled="True" FilterType="Numbers" TargetControlID="txtCodigoMuestra"></asp:FilteredTextBoxExtender>
                    </div>
                </div>
                


                <div class="columnaEtiquetasnudob">
                    Fecha de Toma:
                    <div class="txtFechTomAdm">
                    <asp:TextBox ID="txtFechaInicioToma" runat="server" CssClass="textoMedio" Width="95px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        BehaviorID="txtFechaInicioToma_FilteredTextBoxExtender" FilterType="Custom,Numbers"
                        ValidChars="/" TargetControlID="txtFechaInicioToma"></ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server"
                        BehaviorID="txtFechaInicioToma_CalendarExtender" TargetControlID="txtFechaInicioToma">
                    </ajaxToolkit:CalendarExtender>
                    -
                    <asp:TextBox ID="txtFechaFinToma" runat="server" CssClass="textoMedio" Width="95px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                        BehaviorID="txtFechaFinToma_FilteredTextBoxExtender" FilterType="Custom,Numbers"
                        ValidChars="/" TargetControlID="txtFechaFinToma"></ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                        BehaviorID="txtFechaFinToma_CalendarExtender" TargetControlID="txtFechaFinToma">
                    </ajaxToolkit:CalendarExtender>
                    </div>
                    Codigo Correlativo:
                    <div class="txtCodCorrAdm">
                    <asp:TextBox ID="txtCodigoCorrelativo" runat="server" CssClass="textoMedio" MaxLength="9"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtCodigoCorrelativo_FilteredTextBoxExtender" runat="server"
                        Enabled="True" FilterType="Numbers" TargetControlID="txtCodigoCorrelativo"></asp:FilteredTextBoxExtender>
                    </div>
                </div>
                


                
                <div class="columnaEtiquetasnu">
                    <div class="btnBusAdm">
                    <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" OnClick="btnBuscar_Click"
                        Text="Buscar" />
                    &nbsp;<asp:Button ID="btnExportar" runat="server" CssClass="BotonAccion" OnClick="btnExportar_Click"
                        Text="Exportar" Visible="False" />
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hdfPopup"
                        PopupControlID="panelPopUp" CancelControlID="imgBtnPopupClose" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    &nbsp;<asp:Button ID="btnReporte" runat="server" CssClass="BotonAccion" OnClick="btnReporte_Click"
                        Text="Reporte" Visible="False" />
                    &nbsp;<asp:Button ID="btnConsolidado" runat="server" CssClass="BotonAccion" OnClick="btnConsolidado_Click"
                        Text="Consolidado" Visible="False" />
                    </div>
                </div>

                <div class="SubTituloTabla1" >
                    Resultados
                </div>

                <div class="tablaAdm">
                    <asp:UpdatePanel ID="upaGrilla" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Registros Consultados: 0 "
                                Visible="False"></asp:Label>
                            <asp:GridView ID="dgvMuestras" runat="server" AutoGenerateColumns="False" OnRowCommand="dgvMuestras_RowCommand"
                                AllowPaging="True" OnPageIndexChanging="dgvMuestras_PageIndexChanging" 
                                onrowdatabound="dgvMuestras_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="idMuestra" HeaderText="ID" Visible="false">
                                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoInternoLab" HeaderText="Correlativo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NombresNeonato" HeaderText="Nombres"></asp:BoundField>
                                    <asp:BoundField DataField="ApellidosNeonato" HeaderText="Apellidos"></asp:BoundField>
                                    <asp:BoundField DataField="Sexo" HeaderText="Sexo" Visible="false">
                                        <ItemStyle Width="30px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApellidosMadre" HeaderText="Apellidos Madre"></asp:BoundField>
                                    <%--<asp:BoundField DataField="NombresMadre" HeaderText="Nombre Madre" Visible="false"/>
                             <asp:BoundField DataField="ApellidosMadre" HeaderText="Apellido Madre" Visible="false"/>--%>
                                    <asp:BoundField DataField="DNI" HeaderText="DNI">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumMuestra" HeaderText="N° Mx">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
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
                                    <asp:BoundField DataField="FechaRecepcion" HeaderText="Fecha Recp" DataFormatString="{0:d}">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Telefono1" HeaderText="Telefono">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="Telefono2" HeaderText="Telf 2" Visible="false" />--%>
                                    <asp:TemplateField HeaderText="Detalle" Visible="true">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer3" runat="server" ImageUrl="~/images/buscar1.png" Width="15px"
                                                Height="15px" CommandArgument='<%# Eval("CodigoMuestra") %>' CommandName="detalle" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer" runat="server" ImageUrl="~/images/gtk-edit.png" Width="15px"
                                                Height="15px" CommandArgument='<%# Eval("CodigoMuestra") %>' CommandName="editar" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reporte" Visible=false>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer2" runat="server" ImageUrl="~/images/DownloadPDF.png"
                                                Width="15px" Height="15px" CommandArgument='<%# Eval("CodigoMuestra") %>' CommandName="reporte" />
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
                </div>
        </div>
    </div>

    <asp:Panel runat="server" ID="panelPopUp" CssClass="modalPopup">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<h1 class="SubTituloTabla2">
                    Detalles de la Muestra</h1>--%>
                <div class="tablePopUp">
                    

                            <div class="SubTituloTabla2">
                                Detalles de la Muestra
                            </div>

                            <div class="columnaEtiquetasnu">
                                Codigo de Muestra
                                <div class="lblCodMueAdm">
                                <asp:Label ID="lblCodigoMuestra" runat="server" Text="CodigoMuestra" CssClass="DatosMuestra"></asp:Label>
                                </div>
                            </div>
                            

                            <div class="columnaEtiquetasnu">
                                Establecimiento:
                                <div class="lblEstAdm">
                                <asp:Label ID="lblEstablecimiento" runat="server" Text="Establecimiento" CssClass="DatosMuestra"></asp:Label>
                                </div>
                            </div>
                            

               
                            <div class="columnaEtiquetasnudob">
                                Codigo Correlativo:
                                <div class="lblCodCorAdm">
                                <asp:Label ID="lblCodigoCorrelativo" runat="server" CssClass="DatosMuestra" Text="CodigoCorrelativo"></asp:Label>
                                </div>
                                Fecha de Recepcion:
                                <div class="lblFecRecAdm">
                                <asp:Label ID="lblFechaRecepcion" runat="server" CssClass="DatosMuestra" Text="FechaRecepcion"></asp:Label>
                                </div>
                            </div>
                            
                           
                            
                        
                            <div class="columnaEtiquetasnu">
                            <div class="SubTituloTabla3">
                                Madre
                                <div class="lblMadreAdm">
                                <asp:Label ID="lblMadre" runat="server" Text="NombreMadre" CssClass="DatosMuestra"></asp:Label>
                                </div>
                            </div>
                            </div>
                            <br />
                        
                            <div class="columnaEtiquetasnu">
                                Dirección:
                                <div class="lblDireAdm">
                                <asp:Label ID="lblDireccion" runat="server" Text="Direccion" CssClass="DatosMuestra"></asp:Label>
                                </div>
                            </div>
                            
                        
                        
                            <div class="columnaEtiquetasnudob">
                                DNI:
                                <div class="lblDniAdm">
                                <asp:Label ID="lblDNI" runat="server" CssClass="DatosMuestra" Text="DNI"></asp:Label>
                                </div>
                                Telefono:
                                <div class="lblTeleAdm">
                                <asp:Label ID="lblTelefono1" runat="server" Text="Telefono1" CssClass="DatosMuestra"></asp:Label>
                                </div>
                            </div>
                            
                         
                            
                        
                        
                            <div class="columnaEtiquetasnu">
                                Edad:
                                <div class="lblEdMadAdm">
                                <asp:Label ID="lblEdadMadre" runat="server" CssClass="DatosMuestra" Text="EdadMadre"></asp:Label>
                                </div>
                            </div>
                            
                            
                            <br />
                            <div class="columnaEtiquetasnu">
                            <div class="SubTituloTabla3">
                                Neonato
                                <div class="lblNeoAdm">
                                <asp:Label ID="lblNeonato" runat="server" Text="NombreNeonato" CssClass="DatosMuestra"></asp:Label>
                                </div>
                            </div>
                            </div>
                            <br />
                        
                        
                            <div class="columnaEtiquetasnudob">
                                Fecha de Nacimiento:
                                <div class="lblFecNacAdm">
                                <asp:Label ID="lblFechaNacimiento" runat="server" Text="FechaNacimiento" CssClass="DatosMuestra"></asp:Label>
                                </div>
                                Hora de Nacimiento:
                                <div class="lblHorNacAdm">
                                <asp:Label ID="lblHoraNacimiento" runat="server" CssClass="DatosMuestra" Text="HoraNacimiento"></asp:Label>
                                </div>
                            </div>
                            
                           
                            
                        
                        
                            <div class="columnaEtiquetasnudob">
                                Edad Gestacional:
                                <div class="lblEdGesAdm">
                                <asp:Label ID="lblEdadGestacional" runat="server" CssClass="DatosMuestra" Text="EdadGestacional"></asp:Label>
                                </div>
                                Sexo:
                                <div class="lblSexoAdm">
                                <asp:Label ID="lblSexo" runat="server" CssClass="DatosMuestra" Text="Sexo"></asp:Label>
                                </div>
                            </div>
                            
                         
                            
                        
                        
                            <div class="columnaEtiquetasnudob">
                                Peso(g.):
                                <div class="lblPesAdm">
                                <asp:Label ID="lblPeso" runat="server" CssClass="DatosMuestra" Text="Peso"></asp:Label>
                                </div>
                                Talla(cm)
                                <div class="lblTallaAdm">
                                <asp:Label ID="lblTalla" runat="server" CssClass="DatosMuestra" Text="Talla"></asp:Label>
                                </div>
                            </div>
                           

                           
                            
                        
                            <div class="columnaEtiquetasnu">
                            <div class="SubTituloTabla3">
                                Muestra
                            </div>
                            </div>
                        
                        
                            <div class="columnaEtiquetasnudob">
                                N° Muestra:
                                <div class="lblNmuesAdm">
                                <asp:Label ID="lblNMuestra" runat="server" CssClass="DatosMuestra" Text="NMuestra"></asp:Label>
                                </div>
                                Transfundido:
                                <div class="lblTransAdm">
                                <asp:Label ID="lblTransfundido" runat="server" CssClass="DatosMuestra" 
                                    Text="Transfundido"></asp:Label>
                                </div>
                            </div>
                            
                           
                            
                        

                            <div class="columnaEtiquetasnudob">
                                Fecha de Toma:
                                <div class="lblFecTomAdm">
                                <asp:Label ID="lblFechaToma" runat="server" CssClass="DatosMuestra" Text="FechaToma"></asp:Label>
                                </div>
                                Hora de Toma:
                                <div class="lblHorTomAdm">
                                <asp:Label ID="lblHoraToma" runat="server" CssClass="DatosMuestra" Text="HoraToma"></asp:Label>
                                </div>
                            </div>
                            
                            
                            
                        
                       
                            <div class="columnaEtiquetasnu">
                                Tomado Por:
                                <div class="lblTomPoAdm">
                                <asp:Label ID="lblTomadoPor" runat="server" CssClass="DatosMuestra" Text="TomadoPor"></asp:Label>
                                </div>
                            </div>

                            <br />
                            <div class="columnaEtiquetasnu">
                                Notas:
                                <div class="lblNotAdm">
                                <asp:Label ID="lblNotas" runat="server" CssClass="DatosMuestra" Text="Notas"></asp:Label>
                                </div>
                            </div>
                            
                        
                            <br />
                            <div class="columnaEtiquetasnu">
                            <div class="SubTituloTabla3">
                                Resultados
                            </div>
                            </div>
                        <div class="TablaPequ">
                            <td colspan="4">
                                <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False" 
                                    CssClass="grid">
                                    <Columns>
                                        <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TestName" HeaderText="Prueba">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="concTexto" HeaderText="Resultado">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unidad" HeaderText="Unidad">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="rdcDeterminationLevel" HeaderText="Det" Visible="False">
                                            <ItemStyle Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaMedicion" HeaderText="F. Resultado" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Rango" HeaderText="Referencia">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle CssClass="itemGrilla" />
                                    <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                    <FooterStyle CssClass="itemGrilla" />
                                </asp:GridView>
                            </td>
                        </div>
                        <%--<tr>
                            <td colspan="4">

                            </td>
                        </tr>--%>
                    
                </div>
                <%--Button to close modal popup--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="DivBotonesPopUp">
            <asp:Button ID="imgBtnPopupClose" runat="server" Text="Cancelar" CssClass="BotonAccion" />
            &nbsp;<asp:Button ID="btnReportePopup" runat="server" CssClass="BotonAccion" Text="Reporte"
                OnClick="btnReportePopup_Click" />
            &nbsp;<asp:Button ID="btnHistorico" runat="server" CssClass="BotonAccion" Text="Historico"
                OnClick="btnHistorico_Click" Visible="False" />
        </div>
    </asp:Panel>
</asp:Content>
