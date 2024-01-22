<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultadosPaciente.aspx.cs" Inherits="TamizajePortal.Reportes.ResultadosPaciente" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/EstilosGenerales.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #topImage{
            width: 960px;
            height: 150px;
        }
        .tituloPagina{
            /*text-align:center;*/
        }
        .divOculto
        {
            visibility:hidden;
        }
        .divResultado
        {
            visibility:visible;
        }
        .colEtiquetas{
             /*background-color:#ffffff;*/
             font-weight:bold;
             /*width:200px;*/
        }
        .colDatos{
            /*background-color:#ffffff;*/
             /*width:200px;*/
        }
        #divResultados{
            width: 600px;
            height: 520px;
            background-color:#ffffff;
            border: 1px solid #000000;
            margin: auto;
           

        }
        .centrado{
            margin:10px;
        }
        .divCentrar{
            margin:auto;
        }
        .botonAccion{
            border-bottom-style:none;
            border-top-style:none;
            border-right-style:none;
            border-left-style:none;
            background-color:#b43c7d;
            color:#ffffff;
        }
        .colGrilla{
            
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div <%--class="page"--%>>

        
        <div class="header">
            <%--<div class="clear hideSkiplink">
                <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal" Visible="true">
                </asp:Menu>
            </div>--%>
        </div>
            <br />
        <div id="divResultados">
                <div class="centrado">
        
                <table style="width:100%;">
                    <tr>
                        <td>
                            
                        </td>
                        <td colspan="2">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:HiddenField ID="hdfDNI" runat="server" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td ></td>
                        <td  colspan="2" class="SubTitulosTabla1">RESULTADOS DE TAMIZAJE NEONATAL</td>
                        <td ></td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td >&nbsp;</td>
                        <td>
                            <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>DNI:</td>
                        <td>
                            <asp:TextBox ID="txtDNI" runat="server" MaxLength="8" CssClass="textoMedio"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtDNI_FilteredTextBoxExtender" runat="server" BehaviorID="txtDNI_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtDNI">
                            </asp:FilteredTextBoxExtender>
                            
                            &nbsp;<asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" Text="Consultar" CssClass="BotonAccion" />
                            &nbsp;<asp:Button ID="btnImprimir" runat="server" OnClick="btnImprimir_Click" Text="Imprimir" CssClass="BotonAccion" />
                        &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    </table>
        
            
                <asp:Panel ID="panelResultados" runat="server" Visible="False">
                    <div class="divCentrar">
                        <table style="width: 100%;">
                        <tr>
                            <td>&nbsp;</td>
                            <td class="colEtiquetas">&nbsp;</td>
                            <td class="colDatos">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="colEtiquetas">Nombre de la Madre:</td>
                                <td class="colDatos">
                                    <asp:Label ID="lblNombreMadre" runat="server" Text="NombreMadre"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="colEtiquetas">DNI de la Madre:</td>
                            <td class="colDatos">
                                <asp:Label ID="lblDNI" runat="server" Text="DNI"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="colEtiquetas">Establecimiento:</td>
                            <td class="colDatos">
                                <asp:Label ID="lblEstablecimiento" runat="server" Text="Establecimiento"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="colEtiquetas">#Historia Clinica:</td>
                            <td class="colDatos">
                                <asp:Label ID="lblHClinica" runat="server" Text="HClinica"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="colEtiquetas">Fecha de Nacimiento</td>
                                <td class="colDatos">
                                    <asp:Label ID="lblFechaNacimiento" runat="server" Text="FechaNacimiento"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="colEtiquetas">&nbsp;</td>
                            <td class="colDatos">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="colGrilla" colspan="2">
                                <asp:GridView ID="dgvResultado" runat="server" AutoGenerateColumns="False" CssClass="Grid">
                                    <Columns>
                                        <asp:BoundField DataField="CodigoMuestra" HeaderText="Codigo Muestra" />
                                        <asp:BoundField DataField="Madre" HeaderText="Madre" Visible="False" />
                                        <asp:BoundField DataField="DNI" HeaderText="DNI" Visible="False" />
                                        <asp:BoundField DataField="Neonato" HeaderText="Neonato" Visible="False" />
                                        <asp:BoundField DataField="Test" HeaderText="Prueba" />
                                        <asp:BoundField DataField="HistoriaClinica" HeaderText="H. Clinica" Visible="False" />
                                        <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nac." Visible="False" />
                                        <asp:BoundField DataField="FechaToma" HeaderText="Fecha Toma" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="Conc" HeaderText="Concentracion" />
                                        <asp:BoundField DataField="Unidad" HeaderText="Unidad" />
                                        <asp:BoundField DataField="Rango" HeaderText="Rango" />
                                    </Columns>
                                    <RowStyle CssClass="itemGrilla" />
                                    <HeaderStyle CssClass="cabeceraGrilla" HorizontalAlign="Center" />
                                </asp:GridView>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="colEtiquetas">&nbsp;</td>
                                <td class="colDatos">&nbsp;</td>
                                <td >&nbsp;</td>
                            </tr>
                    </table>
                         </div>
                </asp:Panel>
                </div>
            </div>
            </div>
    </form>
</body>
</html>
