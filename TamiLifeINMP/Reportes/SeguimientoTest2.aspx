<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SeguimientoTest2.aspx.cs" Inherits="TamizajePortal.Reportes.SeguimientoTest2" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <table style="width:100%;">
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
            </td>
            <td rowspan="2" valign="top">
                <table style="width: 100%; background-color: #CCCCCC;">
                    <tr>
                        <td colspan="2">Detalle del Evento</td>
                    </tr>
                    <tr>
                        <td>Asistio:</td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Si" />
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="No" />
                        </td>
                    </tr>
                    <tr>
                        <td>Paciente:</td>
                        <td>Guadalupe Fernanda Gutierrez de las Casas Palma</td>
                    </tr>
                    <tr>
                        <td>Telefono:</td>
                        <td>390-9884</td>
                    </tr>
                    <tr>
                        <td>Titulo:</td>
                        <td>Cita Endocrinologo</td>
                    </tr>
                    <tr>
                        <td>Detalle:</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:BulletedList ID="BulletedList1" runat="server" Width="500px">
                    <asp:ListItem Value="1">Cita Endocrinologia</asp:ListItem>
                    <asp:ListItem Value="2">Cita </asp:ListItem>
                </asp:BulletedList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server">
    </asp:PasswordRecovery>
    <asp:ChangePassword ID="ChangePassword1" runat="server">
    </asp:ChangePassword>
    <br />
    <br />
</asp:Content>
