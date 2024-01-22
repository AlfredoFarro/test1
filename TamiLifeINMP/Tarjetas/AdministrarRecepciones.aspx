<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarRecepciones.aspx.cs" Inherits="TamizajePortal.Tarjetas.AdministrarRecepciones" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="DivPrincipalBusquedas">
    <table style="width: 100%;">
        <tr>
            <td class="filaDelgada" colspan="2">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td class="SubTitulosTabla1" colspan="2">
                Filtros</td>
        </tr>
        <tr>
            <td>
                Tipo Establecimiento:</td>
            <td>
                <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" 
                    AutoPostBack="True" CssClass="textoMedio" 
                    onselectedindexchanged="ddlTipoEstablecimiento_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>
                EstaEstablecimiento:</td>
            <td>
                <asp:UpdatePanel ID="upaTipoEstablecimiento" runat="server">
                     <ContentTemplate>
                         <asp:DropDownList ID="ddlEstablecimiento" runat="server" CssClass="textoLargo" 
                                Width="350px">
                         </asp:DropDownList>
                     </ContentTemplate>
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="ddlTipoEstablecimiento" EventName="SelectedIndexChanged" />
                     </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                Año:</td>
            <td>
                
                <asp:DropDownList ID="ddlAnho" runat="server" AutoPostBack="True" CssClass="textoMedio" OnSelectedIndexChanged="ddlAnho_SelectedIndexChanged">
                </asp:DropDownList>
                
            </td>
        </tr>
        <tr>
            <td>
                Mes:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlMes" runat="server" CssClass="textoMedio" Enabled="False">
                            </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlAnho" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>

                <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" Text="Buscar" onclick="btnBuscar_Click" />

            &nbsp;<asp:Button ID="btnExportar" runat="server" CssClass="BotonAccion" Text="Exportar" onclick="btnExportar_Click" />

            </td>
        </tr>
        <tr>
            <td class="filaDelgada" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" class="SubTitulosTabla1">
                            Recepciones</td>
        </tr>
        </table>
        <div id="DivGrilla">
        
        <asp:UpdatePanel ID="upaGrilla" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" Text="Registros Consultados: 0 " Visible="False"></asp:Label>
                <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="dgvResultados_PageIndexChanging" PageSize="20" OnRowCommand="dgvResultados_RowCommand">
                     <Columns>
                                    <asp:BoundField DataField="TipoEstablecimiento" HeaderText="Tipo Establecimiento" >
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="idRecepcion" HeaderText="idRecepcion" Visible="False">
                                    <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Establecimiento" HeaderText="Establecimiento" >
                                    <ItemStyle Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaRecepcion" HeaderText="Fecha Recepción" DataFormatString="{0:dd/MM/yy}" >
                                    <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="idEstablecimiento" HeaderText="idEstablecimiento" Visible="False">
                                    <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="CodigoInicial" HeaderText="Código Inicial" >
                                        <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoFinal" HeaderText="Código Final">
                                    <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="idTipoEstablecimiento" HeaderText="idTipoEstablecimiento" Visible="False">
                                    <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer" runat="server" CommandArgument='<%# Eval("idRecepcion") %>' CommandName="Editar" Height="15px" ImageUrl="~/images/gtk-edit.png" Width="15px" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEli" runat="server" CommandArgument='<%# Eval("idRecepcion") %>' CommandName="Eliminar" Height="15px" ImageUrl="~/images/delete.png" Width="15px" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="CodigoEstablecimiento" HeaderText="Establecimiento">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>--%>
                       </Columns>
                         <RowStyle CssClass="itemGrilla" />
                         <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                         <FooterStyle CssClass="itemGrilla"/>
                 </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click"/>
            </Triggers>
        </asp:UpdatePanel>
        </div>
</div> 
</asp:Content>
