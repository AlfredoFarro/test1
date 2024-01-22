<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdministrarUsuarios.aspx.cs" Inherits="TamiLifeSA.Account.AdministrarUsuarios" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl ("~/Scripts/ScriptFile.js") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblTitulo" runat="server" Text="Administrar Usuarios" Font-Bold="True"></asp:Label></h1>
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
                    Filtros
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
                    Usuario:
                </td>
                <td>
                    <asp:TextBox ID="txtUser" runat="server" CssClass="textoLargo" MaxLength="50"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="txtUser_FilteredTextBoxExtender" runat="server"
                        Enabled="True" FilterType="Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ñÑ"
                        TargetControlID="txtUser"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Estado:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textoLargo">
                        <asp:ListItem Value="false">Todos</asp:ListItem>
                        <asp:ListItem Value="true">Bloqueados</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" OnClick="btnBuscar_Click"
                        Text="Buscar" />
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hdfPopup"
                        PopupControlID="panelPopUp" CancelControlID="imgBtnPopupClose" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
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
                            <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False" OnRowCommand="dgvMuestras_RowCommand"
                                AllowPaging="True" OnPageIndexChanging="dgvResultados_PageIndexChanging" 
                                onrowdatabound="dgvResultados_RowDataBound">
                                <Columns>
                                    <%--<asp:BoundField DataField="NombresMadre" HeaderText="Nombre Madre" Visible="false"/>
                             <asp:BoundField DataField="ApellidosMadre" HeaderText="Apellido Madre" Visible="false"/>--%>
                                    <%--<asp:BoundField DataField="Telefono2" HeaderText="Telf 2" Visible="false" />--%>
                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario">
                                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TipoEstablecimiento" HeaderText="Red">
                                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Establecimiento" HeaderText="Establecimiento">
                                        <ItemStyle Width="300px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="Estado" HeaderText="Estado">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="FechaCreacion" HeaderText="F. Creación" DataFormatString="{0:d}" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="IsLockedOut" HeaderText="Bloqueado">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:BoundField>--%>
                                    <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Bloqueado" >
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:CheckBoxField>
                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer" runat="server" ImageUrl="~/images/gtk-edit.png" Width="20px"
                                                Height="20px" CommandArgument='<%# Eval("NombreUsuario") %>' CommandName="editar" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unlock">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer1" runat="server" ImageUrl="~/images/cambioPass3.png" Width="20px"
                                                Height="20px" CommandArgument='<%# Eval("NombreUsuario") %>' CommandName="desbloquear" />
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
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
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
                            <td colspan="2" class="SubTituloTabla2">
                                Cambiar Contrasña</td>
                        </tr>
                        <tr>
                            <td>
                                Usuario:</td>
                            <td>
                                <asp:Label ID="lblUsuario" runat="server" CssClass="DatosMuestra" 
                                    Text="CodigoMuestra"></asp:Label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                Nueva Contraseña:</td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="textoLargo" 
                                    TextMode="Password"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                Repetir Contraseña:
                            </td>
                            <td >
                                <asp:TextBox ID="txtRepPassword" runat="server" CssClass="textoLargo" 
                                    TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                
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
            &nbsp;<asp:Button ID="btnGuardarPopup" runat="server" CssClass="BotonAccion" 
                Text="Guardar" onclick="btnGuardarPopup_Click" />
        </div>
    </asp:Panel>
</asp:Content>
