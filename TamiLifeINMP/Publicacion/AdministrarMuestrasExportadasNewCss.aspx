<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarMuestrasExportadasNewCss.aspx.cs" Inherits="TamiLifeSA.Publicacion.AdministrarMuestrasExportadasNewCss" %>
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
        <div class="container">
            <div class="filaDelgada" ></div>
            <div class="SubTituloTabla1">Filtros</div>
            <div class="row">
                <asp:UpdatePanel ID="upaTipoEstablecimiento" runat="server">
                    <ContentTemplate>
                        <div class="columnaEtiquetas">
                            Establecimiento:</div>
                        <asp:DropDownList ID="ddlTipoEstablecimiento" runat="server" AutoPostBack="True" CssClass="textoMedio" OnSelectedIndexChanged="ddlTipoEstablecimiento_SelectedIndexChanged" Width="210px"></asp:DropDownList>
                        <asp:DropDownList ID="ddlEstablecimiento" runat="server" CssClass="textoLargo" Width="350px"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoEstablecimiento" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="row">
                <div class="columnaEtiquetas">Digitador:</div>
                <asp:DropDownList ID="ddlDigitador" runat="server" CssClass="textoLargo"></asp:DropDownList>
            </div>
            <div class="row">
                <div class="columnaEtiquetas">Estado:</div>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textoMedio">
                    <asp:ListItem Value="0">Pendiente</asp:ListItem>
                    <asp:ListItem Value="1">Enviada</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="row">
                <div class="columnaEtiquetas">Codigo de Tarjeta:</div>
                <asp:TextBox ID="txtCodigoMuestra" runat="server" CssClass="textoMedio" MaxLength="9"></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="txtCodigoMuestra_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtCodigoMuestra"></asp:FilteredTextBoxExtender>
            </div>
            <div class="rowbtn">
                <div></div>
                <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccion" OnClick="btnBuscar_Click" Text="Buscar" />
                <asp:Button ID="btnPublicar" runat="server" CssClass="BotonAccion" Text="Publicar" OnClick="btnPublicar_Click" />
            </div>
            <div class="SubTituloTabla1">Resultados</div>
            <div class="upaGrillac">
                <asp:UpdatePanel ID="upaGrilla" runat="server">
                    <ContentTemplate>
                        <div class="container">
                            <div class="container1">
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" Text="Marcar Todos" Visible="False" />
                                
                            </div>
                            <div class="container2">
                                <asp:Label ID="lblNumRegistros" runat="server" CssClass="numeroRegistro" 
                                    Text="Registros Consultados: 0 " Visible="False"></asp:Label>
                            </div>
                            <div>
                            </div>
                        </div>
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
                 </div>
            </div>
    </div>
</asp:Content>

