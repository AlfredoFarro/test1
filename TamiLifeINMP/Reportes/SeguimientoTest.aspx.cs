using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;  
using BC;
using BE;

namespace TamizajePortal.Reportes
{
    public partial class SeguimientoTest : System.Web.UI.Page
    {
        readonly UsuarioBC usuarioBC = new UsuarioBC();
        

        int codPagina = 11;
        string pagina = string.Empty;
        Hashtable HolidayList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!usuarioBC.VerificarPermiso(out pagina, Session["idUsuario"], codPagina))
            {
                Response.Redirect(pagina);
            }
            else
            {
                Master.ActivarMenu();
            }

            HolidayList = Getholiday();
            Calendar1.Caption = "Calender Seguimiento";
            Calendar1.FirstDayOfWeek = FirstDayOfWeek.Sunday;
            Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth;
            Calendar1.TitleFormat = TitleFormat.Month;
            Calendar1.ShowGridLines = true;
            Calendar1.DayStyle.Height = new Unit(60);
            Calendar1.DayStyle.Width = new Unit(150);
            Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Center;
            Calendar1.DayStyle.VerticalAlign = VerticalAlign.Middle;
            Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.AliceBlue;  
        }

        private Hashtable Getholiday()
        {
            Hashtable holiday = new Hashtable();
            holiday["2/11/2018"] = "Guru Nanak Jayanti";
            holiday["14/11/2018"] = "Children's Day";
            holiday["28/11/2018"] = "Bakrid";
            holiday["25/12/2018"] = "Christmas";
            holiday["28/12/2018"] = "Muharram";
            return holiday;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            LabelAction.Text = "Date changed to :" + Calendar1.SelectedDate.ToShortDateString();
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (HolidayList[e.Day.Date.ToShortDateString()] != null)
            {
                Literal literal1 = new Literal();
                literal1.Text = "<br/>";
                e.Cell.Controls.Add(literal1);
                Label label1 = new Label();
                label1.Text = (string)HolidayList[e.Day.Date.ToShortDateString()];
                label1.Font.Size = new FontUnit(FontSize.Small);
                e.Cell.Controls.Add(label1);
            }  
        }

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            LabelAction.Text = "Month changed to :" + e.NewDate.ToShortDateString();  
        }  
    }
}