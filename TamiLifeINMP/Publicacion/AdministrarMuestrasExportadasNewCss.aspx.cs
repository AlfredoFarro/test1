using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BC;

namespace TamiLifeSA.Publicacion
{
    public partial class AdministrarMuestrasExportadasNewCss : System.Web.UI.Page
    {
        private readonly EstablecimientoBC establecimientoBC = new EstablecimientoBC();
        private readonly TipoEstablecimientoBC tipoEstablecimientoBC = new TipoEstablecimientoBC();
        private readonly MuestraBC muestraBC = new MuestraBC();
        private readonly ResultadoBC _resultadoBc = new ResultadoBC();
        private readonly MuestraCompletaBC _muestraCompletaBc = new MuestraCompletaBC();
        private readonly Reportes _rep = new Reportes();
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

                    //dgvMuestras.PageSize = int.Parse(ConfigurationManager.AppSettings["numpaginacion"]);
                    CargarTipoEstablecimiento();
                    CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
                    CargarDigitadores();
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
        }
        private void CargarDigitadores()
        {
            ddlDigitador.DataSource = usuarioBC.ObtenerUsuariosDigitadores();

            ddlDigitador.DataTextField = "NombreUsuario";
            ddlDigitador.DataValueField = "NombreUsuario";
            ddlDigitador.DataBind();
            var item = new ListItem("--TODOS--", "0");
            ddlDigitador.Items.Insert(0, item);
            ddlDigitador.SelectedValue = "0";
        }

        private void CargarTipoEstablecimiento()
        {
            ddlTipoEstablecimiento.DataSource = tipoEstablecimientoBC.ObtenerTipoEstablecimiento();
            ddlTipoEstablecimiento.DataTextField = "Nombre";
            ddlTipoEstablecimiento.DataValueField = "idTipoEstablecimiento";
            ddlTipoEstablecimiento.DataBind();

            var item = new ListItem("--Seleccionar--", "0");
            ddlTipoEstablecimiento.Items.Insert(0, item);
            ddlTipoEstablecimiento.SelectedValue = "0";


        }
        private void CargarEstablecimiento(int tipoEstablecimiento)
        {
            ddlEstablecimiento.DataTextField = "Nombre";
            ddlEstablecimiento.DataValueField = "idEstablecimiento";
            var establecimientos = new List<Establecimiento>();
            if (tipoEstablecimiento != 0)
            {
                establecimientos = establecimientoBC.ObtenerEstablecimientos(string.Empty, tipoEstablecimiento);

            }
            ddlEstablecimiento.DataSource = establecimientos;
            ddlEstablecimiento.DataBind();

            var item = new ListItem("--Seleccionar--", "0");
            ddlEstablecimiento.Items.Insert(0, item);
            ddlEstablecimiento.SelectedValue = "0";

        }



        protected void btnPublicar_Click(object sender, EventArgs e)
        {
            ExportarSeleccion();
            CargarGrilla();
            chkAll.Checked = false;
        }

        protected void dgvMuestras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMuestras.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void ExportarSeleccion()
        {
            string listaCodigosCorrelativos = string.Empty;
            int i = 0;
            foreach (GridViewRow row in dgvMuestras.Rows)
            {
                // Access the CheckBox
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        if (i == 0)
                        {
                            listaCodigosCorrelativos = string.Concat("'", row.Cells[3].Text, "'");
                        }
                        else
                        {
                            listaCodigosCorrelativos = string.Concat(listaCodigosCorrelativos, ",'", row.Cells[3].Text, "'");
                        }
                        i++;
                    }
                }

            }
            try
            {
                muestraBC.ExportarMuestrasINMP(listaCodigosCorrelativos);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void CargarGrilla()
        {
            bool estadoExportacion = false;
            if (ddlEstado.SelectedItem.Value.CompareTo("0") != 0) estadoExportacion = true;
            string digitador = ddlDigitador.SelectedValue;
            int idEstablecimiento = int.Parse(ddlEstablecimiento.SelectedValue);
            string codigoMuestra = txtCodigoMuestra.Text;

            var listaMuestras = muestraBC.ObtenerMuestras(idEstablecimiento, codigoMuestra, estadoExportacion, digitador);
            dgvMuestras.DataSource = listaMuestras;
            dgvMuestras.DataBind();
            chkAll.Visible = true;
            lblNumRegistros.Text = "Registros Consultados: " + listaMuestras.Count();
            lblNumRegistros.Visible = true;
        }

        protected void ddlTipoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstablecimiento(int.Parse(ddlTipoEstablecimiento.SelectedValue));
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            CambiarEstadoCheckBox(chkAll.Checked);
        }

        private void CambiarEstadoCheckBox(bool estadoCheckBox)
        {
            foreach (GridViewRow row in dgvMuestras.Rows)
            {
                var cb = (CheckBox)row.FindControl("chkAgregar");
                if (cb != null) cb.Checked = estadoCheckBox;
            }
        }
    }
}