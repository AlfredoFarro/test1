﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarMuestraNewCss.aspx.cs" Inherits="TamiLifeSA.Muestras.RegistrarMuestraNewCss" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl ("~/Scripts/ScriptFile.js") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblTitulo" runat="server" Text="Registrar Muestra" Font-Bold="True"></asp:Label></h1>
    <div id="DivPrincipal">
        <asp:HiddenField ID="hdnCodigoMuestra" runat="server" />
        <div id="tabcontenido">
            <asp:UpdatePanel ID="upaEstablecimiento" runat="server">
                <ContentTemplate>
                    <div class="parte1">
                        <tr>
                            <td class="filaDelgada" colspan="2">
                                <asp:HiddenField ID="hdnIdNeonato" runat="server" />
                                <asp:HiddenField ID="hdnIdMadre" runat="server" />
                                <asp:HiddenField ID="hdnSede" runat="server" />
                                <asp:HiddenField ID="hdnExisteMuestra" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnExisteCorrelativo" runat="server" Value="0" />
                            </td>
                        </tr>

                            <div class="columnaEtiquetasnu">
                                <%--<asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="textoMedio">
                                    <asp:ListItem Value="1">DNI</asp:ListItem>
                                    <asp:ListItem Value="2">C.E.</asp:ListItem>
                                </asp:DropDownList>--%>
                                        DNI/C.E.:
                                <div class="txtdnin">
                                <asp:TextBox ID="txtDNI" runat="server" CssClass="textoLargo" MaxLength="10"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtDNI_FilteredTextBoxExtender" runat="server" BehaviorID="txtDNI_FilteredTextBoxExtender"
                                    FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtDNI" />

                                    &nbsp;<asp:Button ID="btnIngresar" runat="server" CssClass="BotonAccion" 
                                        OnClick="btnIngresar_Click" Text="Ingresar" UseSubmitBehavior="False" 
                                        ValidationGroup="dni" />
                                    <%--<asp:RequiredFieldValidator ID="rfvCodigoMuestra" runat="server" ErrorMessage="Ingrese un Codigo de Muestra" ControlToValidate="txtCodigoMuestra" Font-Bold="True" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                    &nbsp;<asp:Button ID="btnGuardar" runat="server" CssClass="BotonAccion" 
                                        OnClick="btnGuardar_Click" OnClientClick="return checkBeforeConfirm();" 
                                        Text="Guardar" Visible="False" />
                                    <asp:RequiredFieldValidator ID="rfvDNI" runat="server" 
                                        ControlToValidate="txtDNI" ErrorMessage="Ingrese un Número de DNI" 
                                        Font-Bold="True" ForeColor="#FF3300" ValidationGroup="dni">*</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="cvaDNI" runat="server" 
                                        ClientValidationFunction="validateLength_8" ControlToValidate="txtDNI" 
                                        ErrorMessage="Ingrese un Número de DNI Válido" Font-Bold="True" ForeColor="Red" 
                                        ValidationGroup="dni">*</asp:CustomValidator>
                                    <%--<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />--%>

                                </div>
                            </div>
                            


                            <div class="columnaEtiquetasnu">
                                Establecimiento:
                                <div class="tipoEstn">
                                <asp:UpdatePanel ID="upaTipoEstablecimiento" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" AutoPostBack="True"
                                            CssClass="textoMedio" OnSelectedIndexChanged="ddlTipoEstablecimiento_SelectedIndexChanged"
                                            Width="230px">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlEstablecimiento" runat="server" CssClass="textoLargo" Width="380px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTipoEstablecimiento" runat="server" ControlToValidate="ddlTipoEstablecimiento"
                                            Enabled="False" ErrorMessage="Seleccione un Tipo de Establecimiento" Font-Bold="True"
                                            ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvEstablecimiento" runat="server" ControlToValidate="ddlEstablecimiento"
                                            Enabled="False" ErrorMessage="Seleccione un Establecimiento" Font-Bold="True"
                                            ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlTipoEstablecimiento" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                </div>
                            </div>
                            

                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnIngresar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upaMadre" runat="server">
                <ContentTemplate>
                    <div class="parte2">

                            <div class="SubTituloTabla1">
                                Datos de la Madre
                            </div>


                            <div class="columnaEtiquetasnudob">
                                Apellidos:
                                <div class="txtApen">
                                <asp:TextBox ID="txtApellidoMadre" runat="server" CssClass="textBoxNombres"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvApellidoMadre" runat="server" ControlToValidate="txtApellidoMadre"
                                    Enabled="False" ErrorMessage="Ingrese los Apellidos de la Madre" Font-Bold="True"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:FilteredTextBoxExtender ID="txtApellidoMadre_FilteredTextBoxExtender" runat="server"
                                    BehaviorID="txtApellidoMadre_FilteredTextBoxExtender" FilterMode="ValidChars"
                                    FilterType="Custom,UppercaseLetters,LowercaseLetters" TargetControlID="txtApellidoMadre"
                                    ValidChars=" ñÑ" />
                                </div>
                                Nombres:
                                <div class="txtNomMadn">
                                <asp:TextBox ID="txtNombreMadre" runat="server" CssClass="textBoxNombres" Style="margin-bottom: 0px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombreMadre" runat="server" ControlToValidate="txtNombreMadre"
                                    Enabled="False" ErrorMessage="Ingrese el Nombre de la Madre" Font-Bold="True"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:FilteredTextBoxExtender ID="txtNombreMadre_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Custom,UppercaseLetters,LowercaseLetters" TargetControlID="txtNombreMadre"
                                    ValidChars=" ñÑ" />
                                </div>
                            </div>
                            
                        
                            
                        

                            <div class="columnaEtiquetasnu">
                                Dirección:
                                 <div class="txtdirecMadrn">
                                <asp:TextBox ID="txtDireccionMadre" runat="server" CssClass="textBoxNombresx2"></asp:TextBox>
                                </div>
                            </div>
                           


                            <div class="columnaEtiquetasnudob">
                                Teléfono:
                                <div class="txtTelefn">
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textoCorto" MaxLength="12"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtTelefono_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtTelefono" />
                                <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono"
                                    Enabled="False" ErrorMessage="Ingrese un Número Telefonico" Font-Bold="True"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                                </div>
                                Edad:
                                <div class="txtEdadMadn">
                                <asp:TextBox ID="txtEdadMadre" runat="server" CssClass="textoMini" MaxLength="2"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtEdadMadre_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtEdadMadre" />
                                </div>
                            </div>

                            


                            

                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnIngresar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upaNeonato" runat="server">
                <ContentTemplate>
                    <div class="parte3">

                            <div class="TituloCombon">
                                <asp:Label ID="lblNeonato" runat="server" Font-Bold="True" Text="Neonatos:" Visible="False"></asp:Label>

                                <asp:DropDownList ID="ddlNeonato" runat="server" CssClass="textoLargo" Width="450px" 
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlNeonato_SelectedIndexChanged"
                                    Visible="False">
                                </asp:DropDownList>
                                <asp:Label ID="lblAviso" runat="server" Font-Bold="True" ForeColor="#FF3300" Text="Se esta registrando un nuevo neonato"
                                    Visible="False"></asp:Label>

                            </div>
                            


                            <div class="SubTituloTabla1">
                                Datos del Neonato
                            </div>


                            <div class="columnaEtiquetasnu">
                                T. Gestación(sem):
                                <div class="txtEdadGestn">
                                <asp:TextBox ID="txtEdadGestacional" runat="server" CssClass="textoMini" MaxLength="2"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtEdadGestacional_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtEdadGestacional" />
                                <asp:RequiredFieldValidator ID="rfvEdadGestacional" runat="server" ControlToValidate="txtEdadGestacional"
                                    Enabled="False" ErrorMessage="Ingrese la Edad Gestacional" Font-Bold="True" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            



                            <div class="columnaEtiquetasnudob">
                                Apellidos:
                                <div class="txtApeNeon">
                                <asp:TextBox ID="txtApellidoNeonato" runat="server" AutoCompleteType="Disabled" CssClass="textBoxNombres"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvApellidosNeonato" runat="server" ControlToValidate="txtApellidoNeonato"
                                    Enabled="False" ErrorMessage="Ingrese los Apellidos del Neonato" Font-Bold="True"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:FilteredTextBoxExtender ID="txtApellidoNeonato_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Custom,UppercaseLetters,LowercaseLetters" TargetControlID="txtApellidoNeonato"
                                    ValidChars=" ñÑ" />
                                </div>
                                Nombres:
                                <div class="txtNomNeon">
                                <asp:TextBox ID="txtNombreNeonato" runat="server" CssClass="textBoxNombres"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    Enabled="True" FilterType="Custom,UppercaseLetters,LowercaseLetters" TargetControlID="txtNombreNeonato"
                                    ValidChars=" ñÑ" />
                                </div>
                            </div>
                            
                          
                            

                    
                            <div class="columnaEtiquetasnudob">
                                Fecha de Nac.:
                                <div class="txtFechaNacn">
                                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="textoMedio"></asp:TextBox>
                                <asp:CalendarExtender ID="txtFechaNacimiento_CalendarExtender" runat="server" BehaviorID="txtFechaNacimiento_CalendarExtender"
                                    Format="dd/MM/yyyy" TargetControlID="txtFechaNacimiento" />
                                <asp:RequiredFieldValidator ID="rfvFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento"
                                    Enabled="False" ErrorMessage="Ingrese la Fecha de Nacimiento" Font-Bold="True"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" Font-Bold="True" ForeColor="Red"
                                    Type="Date" ControlToValidate="txtFechaNacimiento" ErrorMessage="Ingrese una Fecha de Nacimiento Válida"
                                    MaximumValue="01/01/2100" MinimumValue="01/01/2000" >*</asp:RangeValidator>
                                    <ajaxToolkit:MaskedEditExtender
                                    ID="MaskedEditExtender2" runat="server" TargetControlID="txtFechaNacimiento" MaskType="Date" Mask="99/99/9999" MessageValidatorTip="true" UserDateFormat="None" UserTimeFormat="None" InputDirection="RightToLeft" ErrorTooltipEnabled="true"/>
                                </div>
                                Hora de Nacimiento:
                                <div class="txtHoraNacn">
                                <asp:TextBox ID="txtHoraNacimiento" runat="server" CssClass="textoMini"></asp:TextBox>
                                <asp:MaskedEditExtender ID="txtHoraNacimiento_MaskedEditExtender" runat="server"
                                    AcceptAMPM="False" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" Mask="99:99"
                                    MaskType="Time" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtHoraNacimiento" />
                                <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="txtHoraNacimiento_MaskedEditExtender"
                                    ControlToValidate="txtHoraNacimiento" Display="Dynamic" EmptyValueBlurredText="*"
                                    EmptyValueMessage="Ingrese la Hora de Nacimiento" Font-Bold="True" ForeColor="Red"
                                    InvalidValueBlurredMessage="*" InvalidValueMessage="Ingrese una Hora de Nacimiento valida"
                                    IsValidEmpty="False" TooltipMessage="" >*</asp:MaskedEditValidator>
                                </div>
                            </div>

                        
                            <div class="columnaEtiquetasnudob">
                                Sexo:
                                <div class="txtRadioBtFen">
                                <asp:RadioButton ID="radFemenino" runat="server" Checked="True" GroupName="grupoSexo"
                                    Text="Femenino" />
                                &nbsp;<asp:RadioButton ID="radMasculino" runat="server" GroupName="grupoSexo" Text="Masculino" />
                                </div>
                                Transfundido:
                                <div class="txtRadioBtTrann">
                                <asp:RadioButton ID="radTransfundidoNo" runat="server" Checked="True" GroupName="grupoTransfundido"
                                    Text="NO" />
                                &nbsp;<asp:RadioButton ID="radTransfundidoSi" runat="server" GroupName="grupoTransfundido"
                                    Text="SI" />
                                </div>
                            </div>
                            
                            

                            
                        
                        
                            <div class="columnaEtiquetasnudob">
                                Peso(g):
                                <div class="txtPeson">
                                <asp:TextBox ID="txtPeso" runat="server" CssClass="textoMini" MaxLength="6" onkeypress="return isFloatNumber(this,event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revPeso" runat="server" ControlToValidate="txtPeso"
                                    ErrorMessage="Ingrese un Peso correcto" Font-Bold="True" ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,3})?$"
                                    >*</asp:RegularExpressionValidator>
                                </div>
                                Talla(cm):
                                <div class="txtTallan">
                                <asp:TextBox ID="txtTalla" runat="server" CssClass="textoMini" MaxLength="6" onkeypress="return isFloatNumber(this,event);"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revTalla" runat="server" ControlToValidate="txtTalla"
                                    ErrorMessage="Ingrese una Talla correcta" Font-Bold="True" ForeColor="Red" ValidationExpression="^\d+([,\.]\d{1,2})?$">*</asp:RegularExpressionValidator>
                                </div>
                            </div>
                            
                          
                            
                        
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnIngresar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlNeonato" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upaMuestra" runat="server">
                <ContentTemplate>
                    <div class="parte4">

                            <div class="filaDelgada">
                                <asp:RadioButton ID="radTalonNo" runat="server" GroupName="grupoTalon" Text="NO"
                                    Visible="False" />
                                <asp:RadioButton ID="radTalonSi" runat="server" Checked="True" GroupName="grupoTalon"
                                    Text="SI" Visible="False" />
                                <asp:RadioButton ID="radPrematuroNo" runat="server" Checked="True" GroupName="grupoPrematuro"
                                    Text="NO" Visible="False" />
                                <asp:RadioButton ID="radPrematuroSi" runat="server" GroupName="grupoPrematuro" Text="SI"
                                    Visible="False" />
                            </div>


                            <div class="SubTituloTabla1" >
                                Datos de la Muestra
                            </div>

                        
                            <div class="columnaEtiquetasnudob">
                                Código de Tarjeta:
                                <div class="txtCodMuen">
                                <asp:TextBox ID="txtCodigoMuestra" runat="server" CssClass="textoMedio" 
                                    MaxLength="12" onkeydown = "return (event.keyCode!=13);"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtCodigoMuestra_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtCodigoMuestra" />
                                <asp:RequiredFieldValidator ID="rfvCodigoMuestra" runat="server" ControlToValidate="txtCodigoMuestra"
                                    Enabled="False" ErrorMessage="Ingrese un Codigo de Muestra" Font-Bold="True"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                                <%--<asp:CustomValidator ID="cfvCodigoMuestra" runat="server" ClientValidationFunction="validateLength_7_9"
                                    ControlToValidate="txtCodigoMuestra" ErrorMessage="Ingrese un Codigo de Muestra Válido (7 a 9 digitos)"
                                    Font-Bold="True" ForeColor="Red">*</asp:CustomValidator>--%>
                                <asp:CustomValidator ID="cfvExisteCodigoMuestra" runat="server" ControlToValidate="txtCodigoMuestra"
                                    ErrorMessage="El codigo de muestra ya existe en el sistema" Font-Bold="True"
                                    ForeColor="Red" OnServerValidate="cfvExisteCodigoMuestra_ServerValidate">*</asp:CustomValidator>
                                </div>
                                N° Muestra:
                                <div class="txtMuestn">
                                <asp:TextBox ID="txtNMuestra" runat="server" CssClass="textoMini">1</asp:TextBox>
                                </div>
                            </div>
                            
                            
                        
                        
                            <div class="columnaEtiquetasnudob">
                                Fecha de Toma:
                                <div class="txtFecTomn">
                                <asp:TextBox ID="txtFechaToma" runat="server" CssClass="textoMedio"></asp:TextBox>
                                <asp:CalendarExtender ID="txtFechaToma_CalendarExtender" runat="server" BehaviorID="txtFechaToma_CalendarExtender"
                                    Format="dd/MM/yyyy" TargetControlID="txtFechaToma" />
                                <asp:RequiredFieldValidator ID="rfvFechaToma" runat="server" ControlToValidate="txtFechaToma"
                                    Enabled="False" ErrorMessage="Ingrese la Fecha de Toma" Font-Bold="True" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="La Fecha de Toma debe ser mayor a la Fecha de Nacimiento."
                                    Font-Bold="True" ForeColor="Red" ControlToCompare="txtFechaNacimiento" ControlToValidate="txtFechaToma"
                                    Operator="GreaterThan" Type="Date">*</asp:CompareValidator>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" Font-Bold="True" ForeColor="Red"
                                    Type="Date" ControlToValidate="txtFechaToma" ErrorMessage="Ingrese una Fecha de Toma Válida"
                                    MaximumValue="01/01/2100" MinimumValue="01/01/2018" >*</asp:RangeValidator>
                                <%--<asp:TextBox ID="TextBox1" runat="server" TextMode="Date"></asp:TextBox>--%>
                                <ajaxToolkit:MaskedEditExtender
                                    ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaToma" MaskType="Date" Mask="99/99/9999" MessageValidatorTip="true" UserDateFormat="None" UserTimeFormat="None" InputDirection="RightToLeft" ErrorTooltipEnabled="true"/>
                                </div>
                                Hora de Toma:
                                <div class="txtHoraTon">
                                <asp:TextBox ID="txtHoraToma" runat="server" CssClass="textoMini"></asp:TextBox>
                                <asp:MaskedEditExtender ID="txtHoraToma_MaskedEditExtender" runat="server" AcceptAMPM="False"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" Mask="99:99"
                                    MaskType="Time" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtHoraToma" />
                                <asp:MaskedEditValidator ID="txtHoraToma_MaskedEV" runat="server" ControlExtender="txtHoraToma_MaskedEditExtender"
                                    ControlToValidate="txtHoraToma" Display="Dynamic" 
                                    EmptyValueBlurredText="*" EmptyValueMessage="Ingrese una Hora de Toma"
                                    Font-Bold="True" ForeColor="Red" InvalidValueBlurredMessage="*" InvalidValueMessage="Ingrese la Hora de Toma valida"
                                    IsValidEmpty="False" TooltipMessage="" >*</asp:MaskedEditValidator>
                                </div>
                            </div>
                            

                            
                        

                            <div class="columnaEtiquetasnudob">
                                Tomado por:
                                <div class="txtTomPon">
                                <asp:TextBox ID="txtTomadoPor" runat="server" CssClass="textBoxNombres"></asp:TextBox>
                                </div>
                                Notas:
                                <div class="txtNotn">
                                <asp:TextBox ID="txtNotas" runat="server" CssClass="textBoxNombres"></asp:TextBox>
                                </div>
                            </div>
                            


                    </div>
                    <asp:Panel ID="pnlAdministrador" runat="server" Visible="False">
                        <div class="parte5">

                                <div class="SubTituloTabla1">
                                    Datos del Laboratorio
                                </div>


                                <div class="columnaEtiquetasnudob">
                                    Código Correlativo:
                                    <div class="txtCodCorrn">
                                    <asp:TextBox ID="txtCodigoCorrelativo" MaxLength="9" runat="server" CssClass="textoMedio"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        FilterType="Numbers" TargetControlID="txtCodigoCorrelativo"></asp:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rfvCodigoCorrelativo" runat="server" ErrorMessage="Ingrese un Codigo Correlativo"
                                        Font-Bold="True" ForeColor="Red" ControlToValidate="txtCodigoCorrelativo" Enabled="False">*</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="cfvExisteCodigoCorrelativo" runat="server" ControlToValidate="txtCodigoCorrelativo"
                                        ErrorMessage="El codigo correlativo ya existe en el sistema." Font-Bold="True"
                                        ForeColor="Red" OnServerValidate="cfvExisteCodigoCorrelativo_ServerValidate">*</asp:CustomValidator>
                                    </div>
                                    Fecha de Recepción:
                                    <div class="txtFechaRecn">
                                    <asp:TextBox ID="txtFechaRecepcion" runat="server" CssClass="textoCorto"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtFechaRecepcion_CalendarExtender" runat="server" BehaviorID="txtFechaRecepcion_CalendarExtender"
                                        Format="dd/MM/yyyy" TargetControlID="txtFechaRecepcion" />
                                    <asp:RequiredFieldValidator ID="rfvFechaRecepcion" runat="server" ControlToValidate="txtFechaRecepcion"
                                        Enabled="False" ErrorMessage="Ingrese la Fecha de Recepción" Font-Bold="True"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cmpFechaRecepcion" runat="server" ErrorMessage="La Fecha de Recepción debe ser mayor a la Fecha de Toma."
                                        Font-Bold="True" ForeColor="Red" ControlToCompare="txtFechaToma" ControlToValidate="txtFechaRecepcion"
                                        Operator="GreaterThanEqual" Type="Date" Enabled="False">*</asp:CompareValidator>
                                    <asp:RangeValidator ID="rnvFechaRecepcion" runat="server" Font-Bold="True" ForeColor="Red"
                                        Type="Date" ControlToValidate="txtFechaRecepcion" ErrorMessage="Ingrese una Fecha de Recepción Válida"
                                        MaximumValue="01/01/2100" MinimumValue="01/01/2018" >*</asp:RangeValidator>
                                        <ajaxToolkit:MaskedEditExtender
                                    ID="MaskedEditExtender3" runat="server" TargetControlID="txtFechaRecepcion" MaskType="Date" Mask="99/99/9999" MessageValidatorTip="true" UserDateFormat="None" UserTimeFormat="None" InputDirection="RightToLeft" ErrorTooltipEnabled="true"/>
                                    </div>
                                </div>
                                

                                


                               
                                <div class="txtceln">
                                    <asp:TextBox ID="txtCelular" runat="server" CssClass="textBoxNombres" 
                                        MaxLength="12" Visible="False"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txtCelular_FilteredTextBoxExtender" 
                                        runat="server" FilterType="Numbers" TargetControlID="txtCelular" />
                                </div>


                                <div class="txtHClin">
                                    <asp:TextBox ID="txtHClinica" runat="server" CssClass="textBoxNombres" 
                                        Visible="False"></asp:TextBox>
                                </div>

                        </div>
                    </asp:Panel>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="Lista de Errores"
                        CssClass="columnaDatos" ShowMessageBox="True" ShowSummary="False" ToolTip="Lista de Errores" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnIngresar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

