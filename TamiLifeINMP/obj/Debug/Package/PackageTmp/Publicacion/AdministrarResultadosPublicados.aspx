﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarResultadosPublicados.aspx.cs" Inherits="TamiLifeSA.Publicacion.AdministrarResultadosPublicados" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Administrar Resultados Publicados</h1>
    <div id="DivPrincipalBusquedas">
        <table style="width: 100%;">
            <tr>
                <td class="filaDelgada" colspan="4">
                    <asp:HiddenField ID="hdnCodigoMuestra" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="4">
                    Filtros
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
                 <td class="columnaEtiquetas">
                    DNI(Madre):</td>
                <td>
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="textoMedio" MaxLength="10"></asp:TextBox>
                   <asp:FilteredTextBoxExtender ID="txtDNI_FilteredTextBoxExtender" runat="server" BehaviorID="txtDNI_FilteredTextBoxExtender"
                        FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtDNI" />
                </td>
                
            </tr>
            <tr>
            <td class="columnaEtiquetas">Codigo de Correlativo:</td>
                <td>
                    <asp:TextBox ID="txtCodigoMuestra" runat="server" CssClass="textoMedio" MaxLength="9"></asp:TextBox>
                </td>
               
                <td class="columnaEtiquetas">&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtRunID" runat="server" CssClass="textoMini" Visible="False"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" Text="Buscar" OnClick="btnBuscar_Click" />
                    &nbsp;<asp:Button ID="btnPublicar" runat="server" CssClass="BotonAccion" Text="Publicar" OnClick="btnPublicar_Click" />
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="filaDelgada" colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4" class="SubTituloTabla1">
                    Resultados
                </td>
            </tr>
        </table>
        <div>
            <asp:UpdatePanel ID="upaGrilla" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Registros Consultados: 0 "
                            Visible="False"></asp:Label>
                    </div>
                    <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False">
                        <Columns>
                        <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAgregar" runat="server" Checked= '<%# Eval("Publicado") %>' />
                                            <asp:HiddenField ID="hdnIdDetalleEnsayo" runat="server" Value='<%# Bind("idDetalleEnsayo") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                            <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo">
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Neonato" HeaderText="Neonato">
                                <ItemStyle Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ApellidosMadre" HeaderText="Apellidos de Mamá">
                                <ItemStyle Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DNI" HeaderText="DNI Madre">
                                <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Establecimiento" HeaderText="Establecimiento Origen">
                                <ItemStyle Width="220px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ConcTexto" HeaderText="Conc">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Prueba" HeaderText="Prueba">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumMuestra" HeaderText="N° Mx">
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaNacimiento" DataFormatString="{0:dd/MM/yy}" HeaderText="Fecha de Nac.">
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaToma" DataFormatString="{0:dd/MM/yy}" HeaderText="Fecha Toma">
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaResultado" DataFormatString="{0:dd/MM/yy}" HeaderText="Fecha Result.">
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EdadGestacional" HeaderText="E.G.">
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="Publicado" HeaderText="Publicado">
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:Button ID="btnPublicar" runat="server" CssClass="BotonAccion" Text="Publicar" Visible='<%# Eval("Publicado").ToString() == "False" ? true : false%>'/>
                                            <asp:HiddenField ID="hdnIdDetalleEnsayo" runat="server" Value='<%# Bind("idDetalleEnsayo") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                             </asp:TemplateField>
                            <%--<asp:BoundField DataField="FechaMedicion" DataFormatString="{0:dd/MM/yy}" HeaderText="F. Resultado" />--%>
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
        </div>
    </div>
</asp:Content>
