using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BC;
using BE;

namespace TamiLifeSA.Publicacion
{
    public partial class AdministrarPublicaciónNewCss : System.Web.UI.Page
    {
        //Instancias de Negocio --------------------------------------------------
        private readonly PruebaBC pruebaBC = new PruebaBC();
        private readonly EnsayoBC ensayoBC = new EnsayoBC();
        private readonly ResultadoBC resultadoBC = new ResultadoBC();
        private readonly Reportes rep = new Reportes();
        private readonly MuestraBC muestraBC = new MuestraBC();
        private readonly ParametroGeneralBC parametroGeneralBC = new ParametroGeneralBC();
        private readonly UsuarioBC usuarioBC = new UsuarioBC();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool usuarioLogeado = (HttpContext.Current.User != null) &&
                      HttpContext.Current.User.Identity.IsAuthenticated;
                if (usuarioLogeado)
                {
                    if (HttpContext.Current.User.IsInRole("Administrador"))
                    {
                        Master.CambiarSiteMap("AdminSiteMap");
                    }
                    else
                    {
                        if (HttpContext.Current.User.IsInRole("Central"))
                            Master.CambiarSiteMap("CentralSiteMap");
                        else
                        {
                            Response.Redirect("~/Default.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                dgvEnsayos.PageSize = int.Parse(ConfigurationManager.AppSettings["numpaginacion"]);
                //CargarDigitadores();
                CargarPruebas();
                CargarEquipos();
                CargarGrilla();

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            string prueba = ddlPrueba.SelectedItem.Value;
            string inicio = txtFechaInicio.Text;
            string fin = txtFechaFin.Text;
            string publicado = ddlEstadoPublicado.SelectedItem.Value;
            string equipo = ddlEquipo.SelectedItem.Value;
            //string digitador = ddlDigitador.SelectedValue;

            List<Ensayo> listaEnsayos = ensayoBC.ObtenerEnsayos(prueba, inicio, fin, publicado, equipo);
            dgvEnsayos.DataSource = listaEnsayos;
            dgvEnsayos.DataBind();
            lblNumRegistros.Text = "Registros Consultados: " + listaEnsayos.Count();
            lblNumRegistros.Visible = true;
        }

        //private void CargarDigitadores()
        //{
        //    ddlDigitador.DataSource = usuarioBC.ObtenerUsuariosDigitadores();

        //    ddlDigitador.DataTextField = "NombreUsuario";
        //    ddlDigitador.DataValueField = "NombreUsuario";
        //    ddlDigitador.DataBind();
        //    var item = new ListItem("--TODOS--", "0");
        //    ddlDigitador.Items.Insert(0, item);
        //    ddlDigitador.SelectedValue = "0";
        //}

        private void CargarPruebas()
        {
            ddlPrueba.DataSource = pruebaBC.ObtenerPruebas(1);
            ddlPrueba.DataTextField = "NombreCorto";
            ddlPrueba.DataValueField = "idPrueba";
            ddlPrueba.DataBind();
            var item = new ListItem("--TODOS--", "0");
            ddlPrueba.Items.Insert(0, item);
            ddlPrueba.SelectedValue = "0";
            //ddlPrueba.SelectedValue = DateTime.Today.Month.ToString();
        }

        private void CargarEquipos()
        {
            ddlEquipo.DataSource = parametroGeneralBC.ListaInstrumentos();
            ddlEquipo.DataTextField = "ValorTexto";
            ddlEquipo.DataValueField = "ValorTexto";
            ddlEquipo.DataBind();
            var item = new ListItem("--TODOS--", "0");
            ddlEquipo.Items.Insert(0, item);
            ddlEquipo.SelectedValue = "0";

            //ddlPrueba.SelectedValue = DateTime.Today.Month.ToString();
        }

        protected void dgvEnsayos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("revisar") == 0)
            {
                Response.Redirect("~/Publicacion/PublicarResultados.aspx?ID=" + e.CommandArgument);
            }
            if (e.CommandName.CompareTo("reporte") == 0)
            {
                DescargarReporteResultados(e.CommandArgument.ToString());
            }
        }

        protected void dgvEnsayos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEnsayos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        private void DescargarReporteResultados(string Id)
        {
            int EnsayoId = int.Parse(Id);
            var ensayo = ensayoBC.ObtenerEnsayoId(EnsayoId);
            var listaResultadosGSP = resultadoBC.ObtenerListaResultadosGSP(EnsayoId);

            byte[] bytes = rep.ReporteResultadosGSP(ensayo, listaResultadosGSP);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ensayo.AssayRunID + ".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();

        }

        protected void btnExportarINMP_Click(object sender, EventArgs e)
        {
            muestraBC.ExportarMuestrasINMP();
        }
    }
}