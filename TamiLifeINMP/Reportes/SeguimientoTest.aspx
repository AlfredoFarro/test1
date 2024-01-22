<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SeguimientoTest.aspx.cs" Inherits="TamizajePortal.Reportes.SeguimientoTest" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Calendar ID="Calendar4" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px">
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
        <DayStyle Width="14%" />
        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
        <TodayDayStyle BackColor="#CCCC99" />
    </asp:Calendar>
    <p style="text-align: center">  
        <b> 
        <asp:Label ID="LabelAction" runat="server" Font-Bold="True" Font-Names="Arial Black" Font-Size="Medium"  
            ForeColor="#0066FF">Indian List of Holidays 2009</asp:Label><br /></b>  
        </p> 
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White"  
            BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt"  
            ForeColor="Black" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged"  
            OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" Height="500px" NextPrevFormat="FullMonth" Width="700px">  
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />  
            <TodayDayStyle BackColor="#CCCCCC" />  
            <OtherMonthDayStyle ForeColor="#999999" />  
            <NextPrevStyle Font-Size="8pt" ForeColor="#333333" Font-Bold="True" VerticalAlign="Bottom" />  
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />  
            <TitleStyle BackColor="White" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" BorderColor="Black" BorderWidth="4px" />  
        </asp:Calendar>  
</asp:Content>
