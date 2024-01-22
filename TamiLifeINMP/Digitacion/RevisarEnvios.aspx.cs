using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BC;

namespace TamiLifeSA.Digitacion
{
    public partial class RevisarEnvios : System.Web.UI.Page
    {
        //Clases
        private readonly EstablecimientoBC _establecimientoBc = new EstablecimientoBC();
        private readonly TipoEstablecimientoBC _tipoEstablecimientoBc = new TipoEstablecimientoBC();
        private readonly EnvioBC _envioBc = new EnvioBC();
        //private readonly DigitacionBC digitacionBC = new DigitacionBC();
        private readonly UsuarioBC _usuarioBc = new UsuarioBC();
        private readonly Reportes _fun = new Reportes();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool usuarioLogeado = (HttpContext.Current.User != null) &&
                                      HttpContext.Current.User.Identity.IsAuthenticated;
                if (!usuarioLogeado)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    CargarTipoEstablecimiento();
                    CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));

                    if (HttpContext.Current.User.IsInRole("Administrador"))
                    {
                        Master.CambiarSiteMap("AdminSiteMap");
                        //CargarEstablecimiento(int.Parse(ddlTipoResultado.SelectedValue));
                        btnRecibido.Visible = true;
                    }
                    else
                    {
                        if (HttpContext.Current.User.IsInRole("Central"))
                        {
                            Master.CambiarSiteMap("CentralSiteMap");
                            btnRecibido.Visible = true;
                        }

                        else
                        {
                            Usuario usuarioAux = _usuarioBc.ObtenerUsuario(HttpContext.Current.User.Identity.Name);
                            Establecimiento establecimientoAux = _establecimientoBc.ObtenerEstablecimientoxIdEstablecimiento(usuarioAux.IdEstablecimiento);

                            CargarEstablecimiento(establecimientoAux.idTipoEstablecimiento);

                            int idHospital = usuarioAux.IdEstablecimiento;
                            ddlTipoEstablecimiento.Enabled = false;
                            ddlEstablecimiento.SelectedValue = idHospital.ToString();
                            ddlEstablecimiento.Enabled = false;
                            //litTipoResultado.Visible = false;
                            //ddlTipoResultado.SelectedValue = "1";
                            //ddlTipoResultado.Visible = false;
                        }
                    }
                    CargarGrilla();
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }


        //Metodos
        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = _tipoEstablecimientoBc.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();
            var item = new ListItem("--Seleccionar--", "0");
            ddlTipoEstablecimiento.Items.Insert(0, item);
            ddlTipoEstablecimiento.SelectedValue = "0";
        }
        public void CargarEstablecimiento(int tipoEstablecimiento)
        {
            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            var establecimientos = new List<Establecimiento>();
            if (tipoEstablecimiento != 0)
            {
                establecimientos = _establecimientoBc.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);
            }
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataBind();

            var item = new ListItem("--Seleccionar--", "0");
            ddlEstablecimiento.Items.Insert(0, item);
            ddlEstablecimiento.SelectedValue = "0";
        }

        private List<Vista_Envio> ObtenerListaEnvios()
        {
            var lista = new List<Vista_Envio>();

            int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            bool usarInicio = false;
            bool usarFin = false;
            DateTime dateInicio;
            DateTime dateFin;
            usarInicio = DateTime.TryParse(txtFechaInicio.Text, out dateInicio);
            usarFin = DateTime.TryParse(txtFechaFin.Text, out dateFin);
            int estadoEnvio = int.Parse(ddlEstadoEnvio.SelectedValue);

            lista = _envioBc.ObtenerEnvios(idEstablecimiento, usarInicio, usarFin, dateInicio, dateFin, estadoEnvio);
            return lista;

        }

        private void CargarGrilla()
        {
            List<Vista_Envio> listaEnvios = ObtenerListaEnvios();
            dgvEnvios.DataSource = listaEnvios;
            dgvEnvios.DataBind();

            lblNumRegistros.Text = "Registros Consultados: " + listaEnvios.Count();
            lblNumRegistros.Visible = true;
            //if (listaEnvios.Count() > 0)
            //    chkAll.Visible = true;
        }


        protected void dgvMuestras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idEnvio = 0;
            //if (e.CommandName.CompareTo("editar") == 0)
            //{
            //    Response.Redirect("~/Muestras/EditarMuestra.aspx?CodigoMuestra=" + e.CommandArgument);
            //}
            if (e.CommandName.CompareTo("eliminar") == 0)
            {

                if (int.TryParse(e.CommandArgument.ToString(), out idEnvio))
                {
                    if (_envioBc.EliminarEnvio(idEnvio, HttpContext.Current.User.Identity.Name))
                    {
                        CargarGrilla();
                    }
                    else
                    {
                        lblNumRegistros.Text = "Error al eliminar el envio";
                    }
                }
                /*
                if (digitacionBC.EliminarMuestra())
                {
                    CargarGrilla();
                }
                else
                {
                    lblNumRegistros.Text = "Error al eliminar la muestra";
                }
                */
                //Response.Redirect("~/Muestras/EditarMuestra.aspx?CodigoMuestra=" + e.CommandArgument);

            }
            if (e.CommandName.CompareTo("reporte") == 0)
            {

                if (int.TryParse(e.CommandArgument.ToString(), out idEnvio))
                {
                    Envio envio = _envioBc.ObtenerEnvio(idEnvio);
                    List<Vista_MuestrasxEnvio> listaMuestras = _envioBc.ObtenerListaMuestrasxEnvio(envio.idEnvio);
                    byte[] bytes = _fun.ReporteEnvio(envio, listaMuestras);
                    string nombreArchivo = string.Concat(envio.Establecimiento.Nombre, "_", DateTime.Parse(envio.FechaCreacion.ToString()).ToShortDateString());
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + "cesar" + "\\nCountry: " + "Home" + "');", true);
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".pdf");
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + txtDNI.Text + ".pdf");
                    Response.ContentType = "application/pdf";
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes);
                    Response.End();
                    Response.Close();
                }
            }

            if (e.CommandName.CompareTo("editar") == 0)
            {

                if (int.TryParse(e.CommandArgument.ToString(), out idEnvio))
                {

                    Response.Redirect("~/Digitacion/EditarEnvio.aspx?idEnvio=" + idEnvio);

                }
            }
        }

        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }

        protected void dgvMuestras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEnvios.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void btnRecibido_Click(object sender, EventArgs e)
        {
            //var listaIdEnvio = new List<string>();
            foreach (GridViewRow row in dgvEnvios.Rows)
            {
                // Access the CheckBox
                var cb = (CheckBox)row.FindControl("chkRecibido");
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        string idEnvio = row.Cells[1].Text;
                        _envioBc.MarcarEnvioRecibido(HttpContext.Current.User.Identity.Name, int.Parse(idEnvio));
                    }
                }

            }
            CargarGrilla();

        }
    }
}