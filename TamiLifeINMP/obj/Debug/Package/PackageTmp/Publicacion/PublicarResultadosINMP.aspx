<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PublicarResultadosINMP.aspx.cs" Inherits="TamiLifeSA.Publicacion.PublicarResultadosINMP" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl ("~/Scripts/ScriptFile.js") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Publicar Resultados
    </h1>
    <div id="DivPrincipal">
        <table style="width: 100%;">
            <tr>
                <td class="filaDelgada" colspan="4">
                    <asp:HiddenField ID="hdnIdEnsayo" runat="server" />
                    <asp:HiddenField ID="hdnCorrelativoAnterior" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="4">
                    Datos del Ensayo
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    ID del Ensayo:
                </td>
                <td>
                    <asp:Label ID="lblEnsayo" runat="server" Text="idEnsayo" Font-Bold="True"></asp:Label>
                </td>
                <td class="columnaEtiquetas">
                    Equipo:
                </td>
                <td>
                    <asp:Label ID="lblEquipo" runat="server" Text="Equipo" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Prueba:
                </td>
                <td>
                    <asp:Label ID="lblPrueba" runat="server" Text="Prueba" Font-Bold="True"></asp:Label>
                </td>
                <td class="columnaEtiquetas">
                    Fecha Resultado:
                </td>
                <td>
                    <asp:Label ID="lblFechaResultado" runat="server" Text="FechaResultado" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Unidad:
                </td>
                <td>
                    <asp:Label ID="lblUnidad" runat="server" Text="Unidad" Font-Bold="True"></asp:Label>
                </td>
                <td class="columnaEtiquetas">
                    KitLot:
                </td>
                <td>
                    <asp:Label ID="lblKitLot" runat="server" Text="KitLot" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    <asp:Label ID="lblTipoResultado" runat="server" Text="Tipo Resultado:" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipoResultados" runat="server" Visible="False">
                    </asp:DropDownList>
                </td>
                <td class="columnaEtiquetas">
                    <asp:Label ID="lblCodigoCorrelativo" runat="server" Text="Código Correlativo:" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodigoCorrelativo" runat="server" CssClass="textoMedio" MaxLength="9"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td colspan="2">
                    <asp:Button ID="btnPublicar" runat="server" CssClass="BotonAccion" Text="Publicar"
                        OnClick="btnPublicar_Click" />
                    <asp:Button ID="btnFiltrar" runat="server" CssClass="BotonAccion" Text="Filtrar"
                        OnClick="btnFiltrar_Click" Visible="False" />
                    &nbsp;<asp:Button ID="btnActualizar" runat="server" CssClass="BotonAccion" Text="Actualizar"
                        OnClick="btnActualizar_Click" Visible="False" />
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="4">
                    Resultados
                    <asp:DropDownList ID="ddlOperador" runat="server" CssClass="textoMini" Visible="False">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>&gt;</asp:ListItem>
                        <asp:ListItem>&lt;</asp:ListItem>
                        <asp:ListItem>&gt;=</asp:ListItem>
                        <asp:ListItem>&lt;=</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtConcentracion" runat="server" CssClass="textoMini" onkeypress="return isFloatNumber(this,event);"
                        Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="upaGrilla" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged"
                                            Text="Marcar Todos" />
                                    </td>
                                    <td align="right" colspan="1">
                                        <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Registros Consultados: 0 "
                                            Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False" OnRowDataBound="dgvResultados_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAgregar" runat="server" Checked='<%# Eval("Publicado") %>' />
                                            <asp:HiddenField ID="hdnIdDetalleEnsayo" runat="server" Value='<%# Bind("idDetalleEnsayo") %>' />
                                            <asp:HiddenField ID="hdnRepeticion" runat="server" Value='<%# Bind("SampleUsedForAnswer") %>' />
                                            <asp:HiddenField ID="hdnIdResultado" runat="server" Value='<%# Bind("idResultado") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="idDetalleEnsayo" HeaderText="idDetalleEnsayo" Visible="False">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pocillo" HeaderText="Pocillo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo">
                                        <%--3--%>
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Prueba" HeaderText="Prueba" Visible="false">
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ConcTexto" HeaderText="Resultado">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Unidad" HeaderText="Unidad" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AssayLineNumber" HeaderText="Linea" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ResultCode" HeaderText="Estado" Visible="true">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="rdcDeterminationLevel" HeaderText="DeterminationLevel">
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <%-- <asp:BoundField DataField="CodigoInternoLab" HeaderText="Correlativo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="NombresMadre" HeaderText="Nombre de la Madre">
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="Apellidos" HeaderText="Apellidos RN">
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nac." DataFormatString="{0:d}">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaToma" HeaderText="Fecha Toma" DataFormatString="{0:d}">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EdadGestacional" HeaderText="E.G.">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Peso" HeaderText="Peso">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Talla" HeaderText="Talla">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NumMuestra" HeaderText="#Muestra">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Establecimiento" HeaderText="Establecimiento">
                                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle CssClass="itemGrilla" />
                                <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                <FooterStyle CssClass="itemGrilla" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="Click" />
                            <%--<asp:AsyncPostBackTrigger ControlID="ddlTipoResultados" 
                                EventName="SelectedIndexChanged" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
