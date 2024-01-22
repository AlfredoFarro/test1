<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdministrarPublicacion.aspx.cs" Inherits="TamiLifeSA.Publicacion.AdministrarPublicacion" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Administrar publicación</h1>
    <div id="DivPrincipal">
        <table style="width: 100%;">
            <tr>
                <td class="filaDelgada" colspan="3">
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="3">
                    Filtros
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Fecha de Resultado:
                </td>
                <td colspan="2">
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
                    Equipo:
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlEquipo" runat="server" CssClass="textoMedio">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Prueba:
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlPrueba" runat="server" CssClass="textoMedio">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Publicado :
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlEstadoPublicado" runat="server" CssClass="textoMedio">
                        <asp:ListItem Value="0">NO</asp:ListItem>
                        <asp:ListItem Value="1">SI</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" Text="Buscar" OnClick="btnBuscar_Click" />
                    &nbsp;<asp:Button ID="btnExportarINMP" runat="server" CssClass="BotonAccion" Text="Exportar"
                        OnClick="btnExportarINMP_Click" />
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="3">
                    Ensayos
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:UpdatePanel ID="upaGrilla" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Registros Consultados: 0 "
                                Visible="False"></asp:Label>
                            <asp:GridView ID="dgvEnsayos" runat="server" AutoGenerateColumns="False" OnRowCommand="dgvEnsayos_RowCommand"
                                AllowPaging="True" OnPageIndexChanging="dgvEnsayos_PageIndexChanging" PageSize="50">
                                <Columns>
                                    <asp:BoundField DataField="idEnsayo" HeaderText="Id" Visible="false" />
                                    <asp:BoundField DataField="AssayRunID" HeaderText="ID de Ensayo">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TestName" HeaderText="Prueba">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Kitlot" HeaderText="Kitlot">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaFinish" DataFormatString="{0:dd/MM/yy}" HeaderText="Fecha de Resultado">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Instrument" HeaderText="Equipo">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:CheckBoxField DataField="Publicado" ReadOnly="False" HeaderText="Publicado">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:CheckBoxField>
                                    <asp:BoundField DataField="Publicado" HeaderText="Publicado1" Visible="false">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaPublicacion" DataFormatString="{0:dd/MM/yy}" HeaderText="Fecha de Publicación">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Reporte">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer2" runat="server" ImageUrl="~/images/boxdownload32.png"
                                                Width="15px" Height="15px" CommandArgument='<%# Eval("idEnsayo") %>' CommandName="reporte" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Revisar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer" runat="server" ImageUrl="~/images/gtk-edit.png" Width="15px"
                                                Height="15px" CommandArgument='<%# Eval("idEnsayo") %>' CommandName="revisar" />
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
                            <asp:PostBackTrigger ControlID="dgvEnsayos" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
