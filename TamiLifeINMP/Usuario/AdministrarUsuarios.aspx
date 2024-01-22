<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuarios.aspx.cs" Inherits="TamizajePortal.Usuario.AdministrarUsuarios" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="DivPrincipalBusquedas">
    <table style="width: 100%;">
        <tr>
            <td colspan="2" class="filaDelgada">
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
                <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" CssClass="textoLargo" AutoPostBack="true"
                        Width="200px" OnSelectedIndexChanged="ddlTipoEstablecimiento_SelectedIndexChanged">
                            </asp:DropDownList>
                    
            </td>
        </tr>
        <tr>
            <td>
                Establecimiento:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEstablecimiento" runat="server" CssClass="textoLargo" 
                                Width="350px" Enabled="False">
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
                Usuario:</td>
            <td>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="textoMedio" 
                    Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Perfil:</td>
            <td>
                <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="textoLargo" 
                        Width="200px">
                            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" Text="Buscar" onclick="btnBuscar_Click" /></td>
        </tr>
        <tr>
            <td class="filaDelgada" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" class="SubTitulosTabla1">
                            Usuarios</td>
        </tr>
        </table>
    <div id="DivGrilla">
        <asp:UpdatePanel ID="upaGrilla" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" 
                                        Text="Registros Consultados: 0 " Visible="False"></asp:Label>
                <asp:GridView ID="dgvResultados" runat="server" AutoGenerateColumns="False" onrowcommand="dgvResultados_RowCommand" AllowPaging="True" OnPageIndexChanging="dgvResultados_PageIndexChanging" PageSize="20">
                                <Columns>
                                   <%-- <asp:TemplateField HeaderText="Inf">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgRep" runat="server" ImageUrl="~/images/boxdownload32.png"  Width="15px"  Height="15px"  CommandArgument='<%# Eval("idEstablecimiento") %>'  CommandName="reporte" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>--%>
                                    <%--<asp:HyperLinkField HeaderText="link" DataNavigateUrlFields="idEstablecimiento" DataTextField="idEstablecimiento"
                                        NavigateUrl="~/CarpetaEstablecimiento/wfrRegistrarEstablecimiento.aspx?idEstablecimiento={0}" 
                                        Text="Editar" />--%>
                                    <asp:BoundField DataField="idUsuario" HeaderText="idUsuario" Visible="false"/>
                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" >
                                    <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Perfil" HeaderText="Perfil" >
                                    <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nombres" HeaderText="Nombres" >
                                    <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" >
                                    <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Celular" HeaderText="Celular" />
                                    <asp:BoundField DataField="Email" HeaderText="E-Mail" />
                                    <asp:BoundField DataField="Codigo" HeaderText="Establecimiento">
                                   
                                    <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                   
                                    <asp:BoundField DataField="TipoEstablecimiento" HeaderText="Tipo Est." />
                                   
                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer" runat="server" ImageUrl="~/images/gtk-edit.png"  Width="15px"  Height="15px"  CommandArgument='<%# Eval("idUsuario") %>'  CommandName="Editar" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"  Width="30px" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Agregar Contraseña">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgVer2" runat="server" ImageUrl="~/images/gtk-edit.png"  Width="15px"  Height="15px"  CommandArgument='<%# Eval("idUsuario") %>'  CommandName="Agregar" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"  Width="30px" />
                                    </asp:TemplateField>--%>
                                </Columns>
                                <RowStyle CssClass="itemGrilla" />
                                <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                            </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
</asp:Content>
