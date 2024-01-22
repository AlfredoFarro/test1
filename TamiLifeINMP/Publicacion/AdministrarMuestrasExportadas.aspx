<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdministrarMuestrasExportadas.aspx.cs" Inherits="TamiLifeSA.Publicacion.AdministrarMuestrasExportadas" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
    Administrar muestras exportadas
        <%--<asp:Label ID="lblTitulo" runat="server" Text="Administrar muestras exportadas" Font-Bold="True"></asp:Label>--%>
    </h1>
    <div id="DivPrincipal">
        <table style="width: 100%;">
            <tr>
                <td class="filaDelgada" colspan="2">
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
                    Digitador:</td>
                <td>
                    <asp:DropDownList ID="ddlDigitador" runat="server" CssClass="textoLargo">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Estado:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textoMedio">
                        <asp:ListItem Value="0">Pendiente</asp:ListItem>
                        <asp:ListItem Value="1">Enviada</asp:ListItem>
                    </asp:DropDownList>
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
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" OnClick="btnBuscar_Click"
                        Text="Buscar" />
                    &nbsp;<asp:Button ID="btnPublicar" runat="server" CssClass="BotonAccion" Text="Publicar"
                        OnClick="btnPublicar_Click" />
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
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged"
                                            Text="Marcar Todos" Visible="False" />
                                    </td>
                                    <td align="right" colspan="1">
                                        <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Registros Consultados: 0 "
                                            Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="dgvMuestras" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                OnPageIndexChanging="dgvMuestras_PageIndexChanging" PageSize="100">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAgregar" runat="server" Checked='<%# Eval("Importado") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
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
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
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
                                    <asp:BoundField DataField="CreadoPor" HeaderText="Creado Por">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle CssClass="itemGrilla" />
                                <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                <FooterStyle CssClass="itemGrilla" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnPublicar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
