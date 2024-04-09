<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarPublicaciónNewCss.aspx.cs" Inherits="TamiLifeSA.Publicacion.AdministrarPublicaciónNewCss" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h1>Administrar publicación</h1>
     <div class="filaDelgada"></div>
     <div class="SubTituloTabla1">
         Filtros
     </div>
     <div class="filaAd">
         <div class="inputContainer">
         <div class="columnaEtiquetasAd">
             Fecha de Resultado:
         </div>
             <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="textoMedio" Width="95px"></asp:TextBox>
             <ajaxToolkit:FilteredTextBoxExtender ID="txtFechaInicio_FilteredTextBoxExtender" runat="server" BehaviorID="txtFechaInicio_FilteredTextBoxExtender" FilterType="Custom,Numbers" ValidChars="/" TargetControlID="txtFechaInicio"></ajaxToolkit:FilteredTextBoxExtender>
             <ajaxToolkit:CalendarExtender ID="txtFechaInicio_CalendarExtender" Format="dd/MM/yyyy" runat="server" BehaviorID="txtFechaInicio_CalendarExtender" TargetControlID="txtFechaInicio"></ajaxToolkit:CalendarExtender>
             -
             <asp:TextBox ID="txtFechaFin" runat="server" CssClass="textoMedio" Width="95px"></asp:TextBox>
             <ajaxToolkit:FilteredTextBoxExtender ID="txtFechaFin_FilteredTextBoxExtender" runat="server" BehaviorID="txtFechaFin_FilteredTextBoxExtender" FilterType="Custom,Numbers" ValidChars="/" TargetControlID="txtFechaFin"></ajaxToolkit:FilteredTextBoxExtender>
             <ajaxToolkit:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" Format="dd/MM/yyyy" BehaviorID="txtFechaFin_CalendarExtender" TargetControlID="txtFechaFin"></ajaxToolkit:CalendarExtender>
         </div>
     </div>
     <div class="filaAd">
         <div class="columnaEtiquetasAd">
             Equipo:
         </div>
         <div class="inputContainer">
             <asp:DropDownList ID="ddlEquipo" runat="server" CssClass="textoMedio">
             </asp:DropDownList>
         </div>
     </div>
     <div class="filaAd">
         <div class="columnaEtiquetasAd">
             Prueba:
         </div>
         <div class="inputContainer">
             <asp:DropDownList ID="ddlPrueba" runat="server" CssClass="textoMedio">
             </asp:DropDownList>
         </div>
     </div>
     <div class="filaAd">
         <div class="columnaEtiquetasAd">
             Publicado:
         </div>
         <div class="inputContainer">
             <asp:DropDownList ID="ddlEstadoPublicado" runat="server" CssClass="textoMedio">
                 <asp:ListItem Value="0">NO</asp:ListItem>
                 <asp:ListItem Value="1">SI</asp:ListItem>
             </asp:DropDownList>
         </div>
     </div>
     <div class="filaAd">
         <div class="inputContainerbtn">
             <asp:Button ID="btnBuscar" runat="server" CssClass="BotonAccionAd" Text="Buscar" OnClick="btnBuscar_Click" />
             &nbsp;<asp:Button ID="btnExportarINMP" runat="server" CssClass="BotonAccionAd" Text="Exportar" OnClick="btnExportarINMP_Click" />
         </div>
     </div>
     <div class="SubTituloTabla1">
         Ensayos
     </div>
     <div class="filaAd">
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
    </div>
</asp:Content>

