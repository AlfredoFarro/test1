<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BuscarResultado.aspx.cs" Inherits="TamiLifeSA.Resultados.BuscarResultado" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblTitulo" runat="server" Text="Buscar Resultado" Font-Bold="True"></asp:Label></h1>
    <div id="DivPrincipalBusquedas">
        <table style="width: 100%;">
            <tr>
                <td class="filaDelgada" colspan="2">
                    <asp:HiddenField ID="hdnCodigoMuestra" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td class="SubTituloTabla1" colspan="2">
                    Filtros
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Codigo Correlativo:
                </td>
                <td>
                    <asp:TextBox ID="txtCodigoMuestra" runat="server" CssClass="textoMedio" MaxLength="9"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" Text="Buscar" OnClick="btnBuscar_Click" />
                    &nbsp;&nbsp;<asp:Button ID="btnActualizar" runat="server" CssClass="BotonAccion" Text="Actualizar"
                        OnClick="btnActualizar_Click" />
                </td>
            </tr>
            <tr>
                <td class="filaDelgada" colspan="2">
                            <asp:TextBox ID="txtRunID" runat="server" CssClass="textoMini" 
                        Visible="False"></asp:TextBox>
                            <asp:Button ID="btnExport" runat="server" CssClass="BotonAccion" Text="Exportar"
                        OnClick="btnExport_Click" Visible="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="SubTituloTabla1">
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
                    <div>
                        <asp:Label ID="lblNumEnsayo" runat="server" CssClass="numeroRegistro" Text="N° Ensayo: "
                            Visible="False"></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblFechaResultado" runat="server" CssClass="numeroRegistro" Text="Fecha de Proceso: "
                            Visible="False"></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblPrueba" runat="server" CssClass="numeroRegistro" Text="Prueba: "
                            Visible="False"></asp:Label>
                    </div>
                    <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False">
                        <Columns>
                        
                            <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo Correlativo">
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="Apellidos" HeaderText="Apellidos del RN">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="ApellidosMadre" HeaderText="Apellidos de Mamá">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DNI" HeaderText="DNI Madre">
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Establecimiento" HeaderText="Establecimiento Origen">
                                <ItemStyle Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ConcTexto" HeaderText="Conc"> <%--4--%>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Prueba" HeaderText="Prueba">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="AssayRunID" HeaderText="ID Ensayo">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumMuestra" HeaderText="N° Muestra">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaNacimiento" DataFormatString="{0:dd/MM/yy}" HeaderText="Fecha de Nacimiento">
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaToma" DataFormatString="{0:dd/MM/yy}" HeaderText="Fecha de Toma">
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaResultado" DataFormatString="{0:dd/MM/yy}" HeaderText="Fecha de Resultado">
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EdadGestacional" HeaderText="E.G.">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Publicado">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAgregar" runat="server" Checked= '<%# Eval("Publicado") %>' />
                                            <asp:HiddenField ID="hdnIdDetalleEnsayo" runat="server" Value='<%# Bind("idDetalleEnsayo") %>' />
                                            <asp:HiddenField ID="hdnIdResultado" runat="server" Value='<%# Bind("idResultado") %>' />
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
