<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TamiLifeSA._Default" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:HiddenField ID="hdnIdEstablecimiento" runat="server" />
    <asp:HiddenField ID="hdfPopup" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeAttendanceReport" runat="server" TargetControlID="hdfPopup"
        PopupControlID="panelPopUp" CancelControlID="imgBtnPopupClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <%--<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hdfPopup"
        PopupControlID="panelPopUp" CancelControlID="imgBtnPopupClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>--%>
    <div id="DivInformativo" runat="server">
        <h2>
            Tamizaje Neonatal
        </h2>
        <p>
            ¿Qué es el Tamizaje Neonatal? El Tamizaje Neonatal es una prueba obligatoria que
            se realiza poco después del nacimiento de tu hijo/a para identificar si está en
            riesgo de padecer desórdenes metabólicos serios que son tratables pero que sin embargo
            no son visibles. En la mayoría de casos, este examen es hecho en el mismo centro
            de salud donde nace tu hijo. Este examen consiste en una prueba de la audición y
            la vista de tu bebé, además de sacarle una gota de sangre del talón a tu recién
            nacido a través de una simple e inofensiva punción. Hacer este examen ayuda que
            sus doctores puedan identificar si tu hijo sufre enfermedades como; fibrosis quística,
            catarata congénita, hipoacusia, hiperplasia suprarrenal, hipotiroidismo congénito
            y fenilcetonuria. En caso de recibir un diagnóstico positivo tu bebé podrá empezar
            un tratamiento antes que comiencen síntomas que puedan afectar su vida.
        </p>
    </div>
    <div id="DivPrincipal" runat="server" visible="false">
        <h2>
            <asp:Label ID="lblTitulo" runat="server" Text="Nombre del Establecimiento"></asp:Label></h2>
        <h3>
            RESULTADOS ALTERADOS
        </h3>
        <asp:UpdatePanel ID="upaGrilla" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Registros Consultados: 0 "
                    Visible="False"></asp:Label>
                <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False"
                    OnPageIndexChanging="dgvResultados_PageIndexChanging" 
                    OnRowCommand="dgvResultados_RowCommand" PageSize="8">
                    <Columns>
                        <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo">
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NombresNeonato" HeaderText="Nombres RN">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ApellidosNeonato" HeaderText="Apellidos RN">
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ApellidosMadre" HeaderText="ApellidosMadre">
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DNI" HeaderText="DNI(Madre)" >
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Telefono1" HeaderText="Telefono" />
                        <asp:BoundField DataField="TestName" HeaderText="Prueba">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ConcTexto" HeaderText="Resultado">
                            <ItemStyle Width="65px" BackColor="Red" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Unidad" HeaderText="Unidad">
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ResultCode" HeaderText="Resultado" Visible="False" />
                        <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nac." DataFormatString="{0:dd/MM/yy}">
                            <ItemStyle Width="50px" HorizontalAlign="Center"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaToma" HeaderText="Fecha Toma" DataFormatString="{0:dd/MM/yy}">
                            <ItemStyle Width="50px" HorizontalAlign="Center"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaRecepcion" HeaderText="Fecha Recep." DataFormatString="{0:dd/MM/yy}">
                            <ItemStyle Width="50px" HorizontalAlign="Center"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaResultado" HeaderText="Fecha Result." DataFormatString="{0:dd/MM/yy}">
                            <ItemStyle Width="50px" HorizontalAlign="Center"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="CodigoEstablecimiento" HeaderText="Codigo Est." Visible="False">
                            <ItemStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Establecimiento" HeaderText="Establecimiento" Visible="False" />
                        <asp:TemplateField HeaderText="Detalle" Visible="true">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgVer2" runat="server" ImageUrl="~/images/buscar1.png"
                                    Width="15px" Height="15px" CommandArgument='<%# Eval("CodigoMuestra") %>' CommandName="detalle" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reporte" Visible="true">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgVer3" runat="server" ImageUrl="~/images/DownloadPDF.png" Width="15px"
                                    Height="15px" CommandArgument='<%# Eval("CodigoMuestra") %>' CommandName="reporte" />
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
                <asp:PostBackTrigger ControlID="dgvResultados" />
            </Triggers>
        </asp:UpdatePanel>
        <div>
            <asp:Button ID="btnDescargarAlterados" runat="server" CssClass="BotonAccion" 
                Text="Descargar" onclick="btnDescargarAlterados_Click" />
        </div>
        <h3>
            MUESTRAS RECHAZADAS</h3>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblNumRechazados" runat="server" CssClass="numeroRegistro" Text="Registros Consultados: 0 "
                    Visible="False"></asp:Label>
                <asp:GridView ID="dgvRechazadas" runat="server" AutoGenerateColumns="False"
                    OnPageIndexChanging="dgvRechazadas_PageIndexChanging" 
                    OnRowCommand="dgvRechazadas_RowCommand" PageSize="8">
                    <Columns>
                        <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ApellidosNeonato" HeaderText="Apellidos RN">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ApellidosMadre" HeaderText="ApellidosMadre">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DNI" HeaderText="DNI(Madre)" >
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Telefono1" HeaderText="Telefono" />
                        <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nac." DataFormatString="{0:dd/MM/yy}">
                            <ItemStyle Width="50px"  HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FechaToma" HeaderText="Fecha Toma" DataFormatString="{0:dd/MM/yy}">
                            <ItemStyle Width="50px"  HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TomadoPor" HeaderText="Tomado Por" />
                        <asp:BoundField DataField="MotivoRechazo" HeaderText="Rechazado Por">
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Establecimiento" HeaderText="Establecimiento" Visible="False" />
                        <asp:TemplateField HeaderText="Detalle" Visible="true">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgVer2" runat="server" ImageUrl="~/images/buscar1.png"
                                    Width="15px" Height="15px" CommandArgument='<%# Eval("CodigoMuestra") %>' CommandName="detalle" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agregar" Visible="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgVer3" runat="server" ImageUrl="~/images/agregar1.png" Width="15px"
                                    Height="15px" CommandArgument='<%# Eval("CodigoMuestra") %>' CommandName="agregar" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        
                    </Columns>
                    <RowStyle CssClass="itemGrilla" />
                    <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                    <FooterStyle CssClass="itemGrilla" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
        <div>
            <asp:Button ID="btnDescargarRechazadas" runat="server" CssClass="BotonAccion" 
                Text="Descargar" onclick="btnDescargarRechazadas_Click" />
        </div>
    </div>
    <asp:Panel runat="server" ID="panelPopUp" CssClass="modalPopup">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                &nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblAfiliacion" runat="server" CssClass="DatosMuestra" Text="Afiliacion"
                                    Visible="False"></asp:Label>
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
                                &nbsp; &nbsp;
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
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="SubTituloTabla3">
                                &nbsp;<asp:Label ID="lblTextoResultado" runat="server" Text="Resultados"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="dgvResultado" runat="server" AutoGenerateColumns="False" 
                                    CssClass="grid" onrowdatabound="dgvResultado_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TestName" HeaderText="Prueba">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="concTexto" HeaderText="Conc">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unidad" HeaderText="Unidad">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="rdcDeterminationLevel" HeaderText="Det" 
                                            Visible="False" >
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
                        </tr>
                        <%--<tr>
                            <td colspan="4">

                            </td>
                        </tr>--%>
                    </table>
                </div>
                <%--Button to close modal popup--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="DivBotonesPopUp">
            <asp:Button ID="imgBtnPopupClose" runat="server" Text="Cancelar" CssClass="BotonAccion" />
            <%--&nbsp;<asp:Button ID="btnReportePopup" runat="server" Visible="false" CssClass="BotonAccion" Text="Reporte"
                OnClick="btnReportePopup_Click" />--%>
        </div>
    </asp:Panel>
</asp:Content>
