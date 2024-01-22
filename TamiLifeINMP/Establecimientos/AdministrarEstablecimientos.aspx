<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdministrarEstablecimientos.aspx.cs" Inherits="TamiLifeSA.Establecimientos.AdministrarEstablecimientos" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblTitulo" runat="server" Text="Administrar Establecimientos" Font-Bold="True"></asp:Label></h1>
    <div id="DivPrincipal">
        <table style="width: 100%;">
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
                    <asp:TextBox ID="txtEstablecimiento" runat="server" CssClass="textoMedio" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Red Asistencial:
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" CssClass="textoLargo"
                        Width="200px">
                        <asp:ListItem Value="1">HOSPITAL</asp:ListItem>
                        <asp:ListItem Value="2">CLINICA</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Departamento:
                </td>
                <td>
                    <asp:UpdatePanel ID="upaDepartamento" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlDepartamento" runat="server" AutoPostBack="True" CssClass="textoLargo"
                                OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"/>--%>
                            <asp:AsyncPostBackTrigger ControlID="ddlDepartamento" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="columnaEtiquetas">
                    Provincia:
                </td>
                <td>
                    <asp:UpdatePanel ID="upaProvincia" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="textoLargo">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"/>--%>
                            <asp:AsyncPostBackTrigger ControlID="ddlDepartamento" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" Text="Buscar" OnClick="btnBuscar_Click" />&nbsp;<asp:Button
                        ID="btnExportar" runat="server" CssClass="BotonAccion" OnClick="btnExportar_Click"
                        Text="Exportar" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="SubTituloTabla1">
                    Establecimientos
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="upaGrilla" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Registros Consultados: 0 "
                                Visible="False"></asp:Label>
                            <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False" OnRowCommand="dgvEstablecimientos_RowCommand"
                                AllowPaging="True" OnPageIndexChanging="dgvResultados_PageIndexChanging" 
                                PageSize="25">
                                <Columns>
                                    <asp:BoundField DataField="idEstablecimiento" HeaderText="idEstablecimiento" Visible="false" />
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TipoEstablecimientoNombre" HeaderText="Red Asistencial" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Establecimiento"></asp:BoundField>
                                    <%--<asp:BoundField DataField="idTipoEstablecimiento" HeaderText="idTipoEstablecimiento" Visible="false" />--%>
                                    <%--<asp:BoundField DataField="TipoEstablecimiento" HeaderText="Tipo" Visible="false" />--%>
                                    <asp:BoundField DataField="Direccion" HeaderText="Direccion" Visible="False">
                                        <ItemStyle Width="160px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DepartamentoNombre" HeaderText="Departamento">
                                        <ItemStyle Width="70px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProvinciaNombre" HeaderText="Provincia">
                                        <ItemStyle Width="70px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DistritoNombre" HeaderText="Distrito">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Telefono1" HeaderText="Telef 1">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Telefono2" HeaderText="Telef 2">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer" runat="server" CommandArgument='<%# Eval("idEstablecimiento") %>'
                                                CommandName="editar" Height="15px" ImageUrl="~/images/gtk-edit.png" Width="15px" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="itemGrilla" />
                                <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                <PagerStyle CssClass="itemGrilla" />
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
</asp:Content>
